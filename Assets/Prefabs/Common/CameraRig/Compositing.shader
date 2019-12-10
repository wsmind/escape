Shader "Escape/Compositing"
{
    Properties
    {
        _LightWorldTexture ("Texture", 2D) = "white" {}
        _DarkWorldTexture ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "SimplexNoise3D.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _LightWorldTexture;
            float4 _LightWorldTexture_ST;

            sampler2D _DarkWorldTexture;
            float4 _DarkWorldTexture_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = 1.0 - v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed3 light = tex2D(_LightWorldTexture, i.uv).rgb;
                fixed3 dark = tex2D(_DarkWorldTexture, i.uv).rgb;
                //float mask = smoothstep(0.4, 0.6, i.uv.y + sin(i.uv.x * 10.0 + _Time.y) * 0.2 - 0.1);
                float mask = 1.0 - smoothstep(0.4, 0.9, length(i.uv * 2.0 - 1.0));

                float maskGate = snoise(float3(i.uv * float2(1.6, 0.9) * 40.0, _Time.x * 4.0)) * 0.5 + 0.5;
                maskGate *= maskGate;

                float smooth = 0.1;
                float2 gateRange = saturate(float2(maskGate - smooth, maskGate + smooth));

                float interpolator = smoothstep(gateRange.x, gateRange.y, mask);

                fixed3 color = lerp(light, dark, interpolator);
                //fixed3 color = mask.xxx;

                return fixed4(color, 1.0);
            }
            ENDCG
        }
    }
}
