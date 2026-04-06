using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float floatStrength;
    [SerializeField] private float textFadeDelay;
    [SerializeField] private float textFadeDuration;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI textObject;

    private float delayTimer = 0;
    private float fadeTimer = 0;

    private bool delayOver = false;
    private bool fadeOver = false;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.up * floatStrength;
    }

    private void FixedUpdate()
    {
        delayTimer += Time.deltaTime;
        if (!delayOver && delayTimer > textFadeDelay) delayOver = true;

        if (delayOver && !fadeOver)
        {
            fadeTimer += Time.deltaTime;

            Color c = textObject.color;
            float t = Mathf.InverseLerp(0, textFadeDuration, fadeTimer);
            float alphaValue = 1 - t;

            textObject.color = new Color(c.r, c.g, c.b, alphaValue);
        }

        if (!fadeOver && fadeTimer > textFadeDuration)
        {
            fadeOver = true;
            Destroy(gameObject, 3f);
        }

    }

    public void SetText(string text) => textObject.text = text;

    public void SetColor(Color color) => textObject.color = color;
}
