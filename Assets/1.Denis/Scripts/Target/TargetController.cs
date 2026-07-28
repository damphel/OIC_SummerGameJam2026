using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private Transform targetPivot;

    public Transform TargetPivot => targetPivot;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(targetPivot.position, 0.5f);
    }
}
