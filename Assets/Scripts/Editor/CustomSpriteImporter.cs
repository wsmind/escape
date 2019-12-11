using UnityEngine;
using UnityEditor;

// implementation taken from here:
// https://answers.unity.com/questions/1013874/setting-sprite-pixel-per-unit-on-importation.html
public class CustomSpriteImporter : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.spritePixelsPerUnit = 128;
    }
}