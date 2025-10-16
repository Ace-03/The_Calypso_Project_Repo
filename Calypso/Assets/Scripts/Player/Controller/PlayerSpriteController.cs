using UnityEngine;

public class PlayerSpriteController
{
    private playerRenderers sr;
    private playerSprites ps;

    public void Initialize(playerRenderers renderer, playerSprites sprites)
    {
        sr = renderer;
        ps = sprites;
    }

    public void SetSprite(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x < 0)
            {
                //sr.sprite = ps.leftSprite;
            }
            else
            {
                //sr.sprite = ps.rightSprite;
            }
        }
        else
        {
            if (dir.z <= 0)
            {
                //sr.sprite = ps.frontSprite;
            }
            else
            {
                //sr.sprite = ps.backSprite;
            }
        }
    }

    private void UpdatePlayer()
    {

    }

    private void UpdateWeapon()
    {

    }

    private void UpdateShadow()
    {

    }
}
