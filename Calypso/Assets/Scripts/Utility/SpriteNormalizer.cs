using UnityEngine;

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
}
