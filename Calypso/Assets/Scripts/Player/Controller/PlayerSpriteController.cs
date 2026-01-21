using UnityEngine;

public class PlayerSpriteController
{
    public float bobAmmount = 0.01f;

    private playerRenderers sr;
    private playerSprites ps;
    private weaponSprites ws;
    private bool facingRight = true;
    private bool facingUp = true;


    public void Initialize(spriteControllerData scData)
    {
        sr = scData.renderers;
        ps = scData.player;
        ws = scData.weapon;
    }

    public void SetSprite(Vector3 dir)
    {
        int playerLayer = sr.player.sortingOrder;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {

            if (dir.x < 0)
            {
                UpdatePlayer(ps.leftSprite);
                UpdateWeapon(ws.sideSprite, playerLayer - 1, -1, -0.06f);
                UpdateShadow(1.2f);
                facingRight = false;
            }
            else
            {
                UpdatePlayer(ps.rightSprite);
                UpdateWeapon(ws.sideSprite, playerLayer + 1, 1, 0.06f);
                UpdateShadow(1.2f);
                facingRight = true;
            }
        }
        else
        {
            if (dir.z <= 0)
            {
                UpdatePlayer(ps.frontSprite);
                UpdateWeapon(ws.forwardSprite, playerLayer + 1, 1);
                UpdateShadow(1.5f);
                facingUp = false;
            }
            else
            {
                UpdatePlayer(ps.backSprite);
                UpdateWeapon(ws.forwardSprite, playerLayer - 1, -1);
                UpdateShadow(1.5f);
                facingUp = true;
            }

        }
    }

    public void BobSprite()
    {
        bobAmmount *= -1;

        sr.player.transform.localPosition = new Vector3(0, bobAmmount, 0);
        sr.weapon.transform.localPosition = new Vector3(sr.weapon.transform.localPosition.x, bobAmmount * 0.6f, 0);
        sr.shadow.transform.localPosition = new Vector3(0, -bobAmmount, 0);
    }

    public void StopBob()
    {
        sr.player.transform.localPosition = Vector3.zero;
        sr.weapon.transform.localPosition = new Vector3(sr.weapon.transform.localPosition.x, 0, 0);
    }

    private void UpdatePlayer(Sprite sprite)
    {
        sr.player.sprite = sprite;
    }

    private void UpdateWeapon(Sprite sprite, int layerOrder, float scale)
    {
        sr.weapon.sprite = sprite;
        sr.weapon.sortingOrder = layerOrder;
        sr.weapon.transform.localScale = new Vector3(scale, 1, 1);
        sr.weapon.transform.localPosition = new Vector3(0, sr.weapon.transform.localPosition.y, 0);
    }
    private void UpdateWeapon(Sprite sprite, int layerOrder, float scale, float offSet)
    {
        sr.weapon.sprite = sprite;
        sr.weapon.sortingOrder = layerOrder;
        sr.weapon.transform.localScale = new Vector3(scale, 1, 1);
        sr.weapon.transform.localPosition = new Vector3(offSet, sr.weapon.transform.localPosition.y, 0);
    }

    private void UpdateShadow(float scale)
    {
        sr.shadow.transform.localScale = new Vector3(scale, 1.5f, 1.5f);
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }

    public bool GetFacingUp()
    {
        return facingUp;
    }
}
