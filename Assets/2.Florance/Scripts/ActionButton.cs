using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float pointerDownTime;
    private bool isPressed = false;

    public Action<float> onHoldButton;
    public Action onReleaseButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTime = 0f;
        isPressed = true;
    }

  
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPressed) return;

        pointerDownTime = Time.deltaTime - pointerDownTime;
        onReleaseButton?.Invoke();
        isPressed = false;
    }

    private void Update()
    {
        if (isPressed)
        {
            pointerDownTime += Time.deltaTime;

            onHoldButton?.Invoke(pointerDownTime);

            Debug.Log($"Button is being held for: {pointerDownTime} seconds");
        }
    }
}
