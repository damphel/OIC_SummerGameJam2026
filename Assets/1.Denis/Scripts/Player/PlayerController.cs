using System;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform initialPosition;
    [SerializeField] Transform finalPosition;
    [SerializeField] float moveDuration = 2.5f;
    [SerializeField] Animator playerAnim;

    private Vector3 startPos;
    private Vector3 controlPoint;
    private Vector3 cachedTargetPos;
    private bool isSetup = false;

    private Tween moveTween;

    public Animator PlayerAnim => playerAnim;
    private void Start()
    {
        transform.position = initialPosition.position;
    }

    public void MovePlayerToInitialPosition()
    {
        MovePlayerToTargetDOTween(initialPosition.position, false);
    }
    
    public void MovePlayerToFinalPosition()
    {
        MovePlayerToTargetDOTween(finalPosition.position);
    }
    
    public void MovePlayerToTargetDOTween(Vector3 targetPosition, bool useCurve = true)
    {
        moveTween?.Kill();

        Vector3 start = transform.position;
        Vector3 control = (start + targetPosition) / 2f + Vector3.down * 3f;

        Vector3[] pathWaypoints = new Vector3[] { control, targetPosition };

        if (useCurve)
            moveTween = transform.DOPath(pathWaypoints, moveDuration, PathType.CatmullRom)
                .SetEase(Ease.Linear);
        else
            moveTween = transform.DOMove(targetPosition, moveDuration);
    }

    public void MovePlayerToTarget(Vector3 targetPosition, float progress)
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;
        
        if (!isSetup || cachedTargetPos != targetPosition || progress <= 0.001f)
        {
            startPos = initialPosition.position;
            cachedTargetPos = targetPosition;
            controlPoint = (startPos + targetPosition) / 2f + Vector3.down * 5f;
            isSetup = true;
        }

        float t = Mathf.Clamp01(progress);

        transform.position = CalculateQuadraticBezierPoint(t, startPos, controlPoint, targetPosition);

        if (t >= 1f)
        {
            isSetup = false;
        }
    }
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2f * u * t * p1;
        p += tt * p2;

        return p;
    }

    public void ChangePlayerAnimationToFlee()
    {
        playerAnim.ResetTrigger("Posting");
        playerAnim.ResetTrigger("Walking");
        playerAnim.SetTrigger("Escaping");

        // running loop audio
    }

    public void ChangePlayerAnimationToWalking()
    {
        playerAnim.ResetTrigger("Waiting");
        playerAnim.ResetTrigger("Posting");
        playerAnim.ResetTrigger("Escaping");
        playerAnim.SetTrigger("Walking");

        // walking loop audio
    }

    public void ChangePlayerAnimationToIdle()
    {
        playerAnim.ResetTrigger("Walking");
        playerAnim.ResetTrigger("Posting");
        playerAnim.ResetTrigger("Escaping");
        playerAnim.SetTrigger("Waiting");
    }

    public void ChangePlayerAnimationToPosting()
    {
        playerAnim.ResetTrigger("Waiting");
        playerAnim.ResetTrigger("Walking");
        playerAnim.SetTrigger("Posting");

        // posting sound play once
    }

    public void ChangePlayerAnimationToBeingCaught()
    {
        playerAnim.ResetTrigger("Waiting");
        playerAnim.ResetTrigger("Posting");
        playerAnim.ResetTrigger("Escaping");
        playerAnim.ResetTrigger("Walking");
        playerAnim.SetTrigger("BeingCaught");

        // surprise sound play once
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (initialPosition)
            Gizmos.DrawWireSphere(initialPosition.position, 0.5f);

        if (finalPosition)
            Gizmos.DrawWireSphere(finalPosition.position, 0.5f);
    }
}
