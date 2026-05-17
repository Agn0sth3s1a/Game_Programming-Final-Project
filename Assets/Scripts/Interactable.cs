using UnityEngine;

public enum Interactables { Door, RespawnPoint, Destination, Button }

public class Interactable : MonoBehaviour
{
    [SerializeField] public Interactables interactableType;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] PlayerManager theManager;

    [SerializeField] LevelManager levelManager;

    [SerializeField] private bool oneTime = false;

    [SerializeField] private string NextLevel;

    [SerializeField] private WhatButtonDoes buttonPurpose;

    [SerializeField] private ChestOpener whatItDoes;

    [SerializeField] private bool isLocked;

    [SerializeField] private AudioClip[] interactSound;

    private bool withinRange;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        withinRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(withinRange && Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Trying to interact");
            switch (interactableType)
            {
                case Interactables.Door:
                    animator = GetComponent<Animator>();
                    if (theManager.GetCurrentFocus() == "body")
                    {
                        if(isLocked)
                        {
                            AudioManager.instance.playSoundFXClip(interactSound[0], transform, 1f);
                        }
                        else
                        {
                            animator.SetTrigger("OpenDoor");
                            AudioManager.instance.playSoundFXClip(interactSound[1], transform, 1f);
                        }
                    }
                    break;
                case Interactables.RespawnPoint:
                    if (theManager.GetCurrentFocus() == "head")
                    {
                        theManager.BodyOnly(new Vector3(transform.position.x, transform.position.y, 0));
                        if(oneTime)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            LevelManager.updateSpawn(transform);
                        }
                    }
                    break;
                case Interactables.Destination:
                    if (theManager.GetCurrentFocus() == "body")
                    {
                        AudioManager.instance.playSoundFXClip(interactSound[0], transform, 1f);
                        levelManager.NextLevel();
                    }
                    break;
                case Interactables.Button:
                    if(theManager.GetCurrentFocus() == "body")
                    {
                        if(buttonPurpose != null)
                        {
                            buttonPurpose.OpenCloseDoors();
                        }
                        else if(whatItDoes != null)
                        {
                            whatItDoes.ChestOpens();
                        }
                    }
                    break;
            }
        }
        /*else if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressing E but not in range");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Entered Zone");
        if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            withinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Left Zone");
        if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            withinRange = false;
        }
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
