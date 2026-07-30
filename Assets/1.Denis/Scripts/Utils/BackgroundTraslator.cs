using UnityEngine;

public class BackgroundTraslator : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D[] sizeBox;

    private float totalWidth;

    private void Start()
    {
        // Calculate total combined width of the backgrounds using sizeBox
        totalWidth = 0f;
        for (int i = 0; i < sizeBox.Length; i++)
        {
            totalWidth += sizeBox[i].size.x;
        }
    }

    private void Update()
    {
        // Move every background frame by frame
        for (int i = 0; i < sizeBox.Length; i++)
        {
            Transform bgTransform = sizeBox[i].transform;

            // Move left
            bgTransform.Translate(Vector3.left * (speed * Time.deltaTime));

            // Check if this specific background went too far left
            // (When its current X position drops below its starting X relative to its size)
            if (bgTransform.localPosition.x <= -sizeBox[i].size.x)
            {
                // Recycle it to the right end by adding the total width
                Vector3 newPos = bgTransform.localPosition;
                newPos.x += totalWidth;
                bgTransform.localPosition = newPos;
            }
        }
    }
}