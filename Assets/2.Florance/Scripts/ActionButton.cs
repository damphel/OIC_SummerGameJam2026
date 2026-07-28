using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button thisButton;
    private float pointerDownTime;
    private bool isPressed = false;

    public Button ThisButton => thisButton;

    public Action<float> onHoldButton;
    public Action onReleaseButton;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentScene.IsComplete)
            return;

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
        if (isPressed && GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            pointerDownTime += Time.deltaTime;

            onHoldButton?.Invoke(pointerDownTime);

            Debug.Log($"Button is being held for: {pointerDownTime} seconds");
        }
    }

    public void ResetPointerDownTimer()
    {
        pointerDownTime = 0f;
    }
}
