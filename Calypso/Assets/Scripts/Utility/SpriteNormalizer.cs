using UnityEngine;
using UnityEngine.UI;

public class SpriteNormalizer
{
    public static void NormalizeSprite(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null)
        {
            Debug.LogWarning("SpriteRenderer or Sprite is missing on the provided GameObject.");
            return;
        }

        Vector2 spriteSize = sr.sprite.bounds.size;
        float scaleFactor = 1f / spriteSize.y;
        
        obj.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }

    public static void NormalizeImage(GameObject obj)
    {
        Image img = obj.GetComponent<Image>();
        RectTransform rect = obj.GetComponent<RectTransform>();
        if (img == null || img.sprite == null || rect == null) return;

        // 1. Capture the current visual area (approximate size)
        Vector2 currentSize = rect.rect.size;
        Vector3 currentScale = rect.localScale;

        // 2. Reset to the Sprite's actual pixel dimensions
        img.SetNativeSize();

        // 3. To keep it from "drastically changing size," we calculate the 
        // ratio between the old size and the new native pixel size.
        float widthRatio = (currentSize.x * currentScale.x) / rect.rect.width;
        float heightRatio = (currentSize.y * currentScale.y) / rect.rect.height;

        // Apply the ratio back to the scale so it looks the same but is "Normalized" 
        // to the sprite's native pixel data.
        rect.localScale = new Vector3(widthRatio, heightRatio, 1f);
    }
}
