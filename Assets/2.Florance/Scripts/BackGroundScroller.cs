using UnityEngine;
using UnityEngine.UI;

public class BackGroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage backgroundImage;
    [SerializeField] private float x;
    [SerializeField] private float y;

    private void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, backgroundImage.uvRect.size);
    }
}


