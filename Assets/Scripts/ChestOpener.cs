using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] Interactable theDoor;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChestOpens()
    {
        animator.SetBool("OpenChest", true);
        theDoor.Unlock();
    }
}
