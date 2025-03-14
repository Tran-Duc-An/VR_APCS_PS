using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TmpScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    [SerializeField] private Button stopScroll;



    private RectTransform textRectTransform;
    private RectTransform panelRectTransform;
    private float textHeight; // Total height of the text
    private float panelHeight; // Height of the visible panel area
    private TextMeshProUGUI tmpText; // Reference to TextMeshPro text component
    private bool isScrolling = true;

    void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
        panelRectTransform = transform.parent.GetComponent<RectTransform>();
        tmpText = GetComponent<TextMeshProUGUI>();

        panelHeight = panelRectTransform.rect.height;

        // Force layout update to get the correct text height
        UpdateTextHeight();
        ResetTextPosition();
        stopScroll.onClick.AddListener(ToggleScroll);
        //Debug.Log("Panel position: " + panelRectTransform.position);
        //Debug.Log("Text position: " + textRectTransform.position);
        //Debug.Log("Text height: " + textHeight);
        //Debug.Log("Panel height: " + panelHeight);
    }

    void Update()
    {
        
        if (!isScrolling) return;
  

        UpdateTextHeight();
        textRectTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        if (textRectTransform.localPosition.y > GetResetYPosition())
        {
            ResetTextPosition();
        }

    }

    private void UpdateTextHeight()
    {
        tmpText.ForceMeshUpdate();
        textHeight = tmpText.preferredHeight; // float real para height
    }

    private void ResetTextPosition()
    {
        Vector3 panelBot = Vector3.one * 0; // (0, 0, 0)
        panelBot.y -= panelHeight;
        textRectTransform.localPosition = panelBot;
    }

    private float GetResetYPosition()
    {
        return 0 + textHeight;
    }

    [ContextMenu("Toggle Scroll")]
    public void ToggleScroll()
    {
        isScrolling = !isScrolling;
    }



}

