using System;
using UnityEngine;
using DG.Tweening;

public class FadeAnimation : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private Tween fadeTween;

  
    public void FadeOut(Action onComplete = null)
    {
        fadeTween?.Kill();
        fadeTween = fadeCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() => DoOnFadeOutComplete(onComplete));
    }

    private void DoOnFadeOutComplete(Action onComplete)
    {
        fadeCanvasGroup.blocksRaycasts = false;
        fadeCanvasGroup.interactable = false;
        gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    
    
    private void OnDestroy()
    {
        fadeTween?.Kill();
    }
}