using UnityEngine;

public class EnemySpriteBob : MonoBehaviour
{
    private Transform spriteObject;
    private float bobSpeed = 0.1f;
    private float bobAmmount = 0.01f;
    private float restingPos;
    private float bobTimer;

    public void Initialize(EnemyDefinitionSO data, SpriteRenderer sr)
    {
        bobSpeed = data.bobSpeed;
        bobAmmount = data.bobAmount / 100f;
        spriteObject = sr.transform;
        restingPos = sr.transform.position.y;
    }

    private void Update()
    {
        TryBobbing();   
    }

    private void TryBobbing()
    {
        if (spriteObject == null)
        {
            Debug.LogError("Enemy sprite bobber missing transform on: " + name);
            return;
        }

        bobTimer += Time.deltaTime;
        if (bobTimer >= bobSpeed)
        {
            bobTimer = 0;
            BobSprite();
        }
    }

    private void BobSprite()
    {
        bobAmmount *= -1;

        spriteObject.localPosition = new Vector3(0, bobAmmount + restingPos, 0);
    }
}
