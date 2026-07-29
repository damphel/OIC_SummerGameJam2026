using System;
using UnityEngine;
using DG.Tweening;
public class SceneController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] float moveTime = 2.5f;
    [SerializeField] float timeRequieredToComplete = 10f;
    [SerializeField] BoxCollider2D sizeBox;
    [SerializeField] TargetController targetController;

    public int ID => _id;

    float sceneProgress = 0f;
    bool isComplete = false;

    public Action onCompleteScene;

    public bool IsComplete => isComplete;
    public float SceneProgress { get; set; }
    public float TimeRequieredToComplete => timeRequieredToComplete;

    public Vector2 SceneSize => sizeBox.size;
    public TargetController ThisTargetController => targetController;

    private void Awake()
    {
        if(sizeBox==null) sizeBox = GetComponent<BoxCollider2D>();
    }

    public void DoOnSceneInstantiate(Action OnCompleteInstance = null)
    {
        Debug.Log("�f�j�Y�FDo On Scene UInstantiate", this);
        DoMoveSceneToTargetPos(0f, OnCompleteInstance);
    }

    public void DoOnActionButtonPressed(float time)
    {
        if(!isComplete)
        {
            sceneProgress = time;

            if (sceneProgress >= timeRequieredToComplete)
                CompleteScene();
        }
    }

    public void DoOnActionButtonReleased()
    {
        sceneProgress = 0;
    }

    public void CompleteScene()
    {
        if (isComplete)
            return;

        isComplete = true;

        onCompleteScene?.Invoke();

        DoMoveSceneToTargetPos(this.transform.position.x - sizeBox.size.x, 
        () => {
            Destroy(this.gameObject);
        });
    }

    public void DoMoveSceneToTargetPos(float targetXPos, Action OnComplete = null)
    {
        this.transform.DOMoveX(targetXPos, moveTime).OnComplete(
            () =>
            {
                OnComplete?.Invoke();
            });
            
    }
}
