using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float floatStrength;
    [SerializeField] private float textFadeDelay;
    [SerializeField] private float textFadeDuration;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private Image bg;
    private float delayTimer = 0;
    private float fadeTimer = 0;

    private bool delayOver = false;
    private bool fadeOver = false;

    private float bgStartAlpha;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.up * floatStrength;
        bgStartAlpha = bg.color.a;
    }

    private void FixedUpdate()
    {
        delayTimer += Time.deltaTime;
        if (!delayOver && delayTimer > textFadeDelay) delayOver = true;

        if (delayOver && !fadeOver)
        {
            fadeTimer += Time.deltaTime;

            if (bg != null)
                bg.color = SetAlpha(bg.color, bgStartAlpha);
            
            textObject.color = SetAlpha(textObject.color, 1);
        }

        if (!fadeOver && fadeTimer > textFadeDuration)
        {
            fadeOver = true;
            Destroy(gameObject, 3f);
        }
    }

    private Color SetAlpha(Color c, float originalAlpha)
    {
        float t = Mathf.InverseLerp(0, textFadeDuration, fadeTimer);
        float alphaValue = originalAlpha * (1 - t);

        return new Color(c.r, c.g, c.b, alphaValue);
    }

    public void SetText(string text) => textObject.text = text;

    public void SetColor(Color color) => textObject.color = color;
}
