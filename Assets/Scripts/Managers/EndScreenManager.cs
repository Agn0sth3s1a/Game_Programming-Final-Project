using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private Transform walkTo;

    [SerializeField] private GameObject theCharacter;

    [SerializeField] public float speed = 0.2f;

    [SerializeField] private AudioClip EndScreenMusic;
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;

    private Animator characterAnimator;

    private float step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackgroundMusicManager.BGMinstance.playSongMusic(EndScreenMusic);
        step = speed * Time.deltaTime;
        characterAnimator = theCharacter.GetComponent<Animator>();
        Cursor.visible = true;
        //targetPosition = new
        //targetPosition = new Vector3(walkTo.position.x, theCharacter.transform.position.y, walkTo.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(theCharacter.transform.position.x < 3.3)
        {
            theCharacter.transform.position = Vector3.MoveTowards(theCharacter.transform.position, walkTo.position, step);
        }
        else
        {
            characterAnimator.SetBool("StopWalking", true);
        }
    }

    public void ExitButton()
    {
        SceneSwapper.Swapper.BackToMenu();
    }
}
