using UnityEngine;
using UnityEngine.UI;

public class AutoScrollText : MonoBehaviour
{
    public float scrollSpeed = 20f; // Speed of scrolling
    private RectTransform textRectTransform;
    private RectTransform panelRectTransform;
    private float textHeight; // Total height of the text
    private float panelHeight; // Height of the visible panel area

    void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
        panelRectTransform = transform.parent.GetComponent<RectTransform>();

        panelHeight = panelRectTransform.rect.height;

        // Force layout update to get the correct text height
        UpdateTextHeight();

        ResetTextPosition();
    }

    void Update()
    {
        // Update text height in case it changes dynamically
        UpdateTextHeight();

        // Move the text upward
        textRectTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Check if the entire text has scrolled off the screen
        if (textRectTransform.localPosition.y > GetResetYPosition())
        {
            ResetTextPosition();
        }
    }

    private void UpdateTextHeight()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(textRectTransform);
        textHeight = textRectTransform.rect.height;
    }

    private void ResetTextPosition()
    {
        Vector3 panelBottomWorld = panelRectTransform.TransformPoint(new Vector3(0, -panelHeight, 0));
        Vector3 resetPosition = textRectTransform.parent.InverseTransformPoint(panelBottomWorld);
        textRectTransform.localPosition = resetPosition;
    }

    private float GetResetYPosition()
    {
        Vector3 panelBottomWorld = panelRectTransform.TransformPoint(new Vector3(0, -panelHeight, 0));
        Vector3 resetPosition = textRectTransform.parent.InverseTransformPoint(panelBottomWorld);
        return resetPosition.y + textHeight;
    }
}
