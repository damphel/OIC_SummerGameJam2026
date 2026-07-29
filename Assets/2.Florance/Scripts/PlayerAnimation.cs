using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public ActionButton actionButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsPressed", actionButton.isPressed);

    }
}
