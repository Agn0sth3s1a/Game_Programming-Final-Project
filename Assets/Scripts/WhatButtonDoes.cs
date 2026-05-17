using System.Collections;
using UnityEngine;

public class WhatButtonDoes : MonoBehaviour
{
    /*[Header("Door Objects")]
    [SerializeField] private GameObject wallDoor;
    [SerializeField] private GameObject pitDoorLeft;
    [SerializeField] private GameObject pitDoorRight;*/

    [Header("Misc")]
    [SerializeField] private Animator WallMover;
    [SerializeField] private AudioClip GearsNoise;

    public void OpenCloseDoors()
    {
        WallMover.SetTrigger("ButtonPress");
        AudioManager.instance.playSoundFXClip(GearsNoise, transform, 1f);
    }
}
