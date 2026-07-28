using System;
using UnityEngine;
using DG.Tweening;
public class SceneController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] float moveTime = 2.5f;

    public int ID { get; private set; }

    BoxCollider2D sizeBox;
    float sceneProgress;
    bool isComplete = false;

    public Action<float> onProgressChange; // Send the sceneProgress by Param
    public Action onCompleteScene;

    private void Awake()
    {
        if(sizeBox==null) sizeBox = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

    }

    public void CompleteScene()
    {
        isComplete = true;
        onCompleteScene?.Invoke();
    }

    public void DoMoveSceneToTargetPos(Vector3 targetPos)
    {
        this.transform.DOMove(targetPos, moveTime);
    }
}
