using System;
using UnityEngine;
using DG.Tweening;
public class SceneController : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] float moveTime = 2.5f;
    [SerializeField] float timeRequieredToComplete = 10f;
    [SerializeField] BoxCollider2D sizeBox;

    public int ID => _id;

    float sceneProgress = 0f;
    bool isComplete = false;

    public Action onCompleteScene;

    public float SceneProgress { get; set; }
    public float TimeRequieredToComplete => TimeRequieredToComplete;
    public Vector2 SceneSize => sizeBox.size;

    private void Awake()
    {
        if(sizeBox==null) sizeBox = GetComponent<BoxCollider2D>();
    }

    public void DoOnSceneInstantiate()
    {
        Debug.Log("デニズ：Do On Scene UInstantiate", this);
        DoMoveSceneToTargetPos(0f);
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
        this.transform.DOMoveX(targetXPos, moveTime).OnComplete(() => OnComplete?.Invoke());
            
    }
}
