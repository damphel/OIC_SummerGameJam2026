using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform initialPosition;
    [SerializeField] Transform finalPosition;
    [SerializeField] float moveDuration = 2.5f;

    public void MovePlayerToTarget(Vector3 targetPosition)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = targetPosition;

        Vector3 controlPoint = (startPos + endPos) / 2f + Vector3.up * 5f;

        Vector3[] pathWaypoints = new Vector3[] { controlPoint, endPos };

        transform.DOPath(pathWaypoints, moveDuration, PathType.CatmullRom)
                 .SetEase(Ease.Linear);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;    
        Gizmos.DrawWireSphere(initialPosition.position, 0.5f);
        Gizmos.DrawWireSphere(finalPosition.position, 0.5f);
    }
}
