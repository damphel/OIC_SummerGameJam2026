using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float pointerDownTime;
    private bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownTime = Time.time;
        isPressed = true;
    }

  
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPressed) return;
        
        float pressDuration = Time.time - pointerDownTime;
        //Debug.Log($"Button pressed for: {pressDuration} seconds");

        isPressed = false;
    }

    private void Update()
    {
        if (isPressed)
        {
            float currentHoldTime = Time.time - pointerDownTime;
            Debug.Log($"Button is being held for: {currentHoldTime} seconds");
        }
    }
}
