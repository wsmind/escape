﻿Shader "Escape/Compositing"
{
    Properties
    {
        _LightWorldTexture ("Light World", 2D) = "white" {}
        _DarkWorldTexture ("Dark World", 2D) = "white" {}
        _MaskTexture ("Mask", 2D) = "white" {}
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

            uniform float4 CameraScaleBias;
            uniform float GlobalDarkness;
            uniform float Fade;

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

            sampler2D _MaskTexture;
            float4 _MaskTexture_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = 1.0 - v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 worldUv = i.uv * CameraScaleBias.xy + CameraScaleBias.zw;

                // sample the textures
                fixed3 light = tex2D(_LightWorldTexture, i.uv).rgb;
                fixed3 dark = tex2D(_DarkWorldTexture, i.uv).rgb;
                fixed3 mask = tex2D(_MaskTexture, i.uv).rgb;
                //float mask = sin(_Time.y) * 0.5 + 0.5;
                //float mask = smoothstep(0.4, 0.6, worldUv.y * 0.1 + sin(worldUv.x * 0.4 + _Time.y) * 0.2 + 0.4);
                //float mask = 1.0 - smoothstep(0.4, 0.9, length(i.uv * 2.0 - 1.0));

                mask = lerp(mask, 1.0 - mask, GlobalDarkness);

                float maskGate = snoise(float3(worldUv * 1.4, _Time.x * 4.0)) * 0.5 + 0.5;
                //maskGate *= maskGate;

                float smooth = 0.1;
                float2 gateRange = saturate(float2(maskGate - smooth, maskGate + smooth));

                float interpolator = smoothstep(gateRange.x, gateRange.y, mask);

                fixed3 color = lerp(light, dark, interpolator);
                //fixed3 borderColor = lerp(fixed3(1.0, 0.4, 0.0), fixed3(0.1, 0.4, 0.6), GlobalDarkness);
                fixed3 borderColor = fixed3(1.0, 0.4, 0.0);

                float borderMask = smoothstep(0.0, 0.2, interpolator) * smoothstep(1.0, 0.8, interpolator);
                color = saturate(color + borderColor * borderMask) * Fade;

                return fixed4(color, 1.0);
            }
            ENDCG
        }
    }
}
