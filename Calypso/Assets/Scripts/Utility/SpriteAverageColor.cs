using UnityEngine;

public static class SpriteAverageColor
{
    public static Color GetAverageColor(Sprite sprite)
    {
        if (sprite == null || sprite.texture == null)
        {
            Debug.LogError("Sprite or its texture is null.");
            return Color.black;
        }

        if (!sprite.texture.isReadable)
        {
            Debug.LogError("Sprite texture is not readable. Please enable 'Read/Write Enabled' in the texture import settings.");
            return Color.black;
        }

        Texture2D texture = sprite.texture;
        Color32[] pixels = texture.GetPixels32();

        long r = 0;
        long g = 0;
        long b = 0;
        int totalPixels = pixels.Length;

        for (int i = 0; i < totalPixels; i++)
        {
            r += pixels[i].r;
            g += pixels[i].g;
            b += pixels[i].b;
        }

        // Calculate the average for each component
        byte avgR = (byte)(r / totalPixels);
        byte avgG = (byte)(g / totalPixels);
        byte avgB = (byte)(b / totalPixels);

        return new Color32(avgR, avgG, avgB, 255); // Alpha is set to full opacity
    }
}