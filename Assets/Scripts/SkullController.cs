using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    [SerializeField] public float speed = 4.5f;

    [SerializeField] PlayerManager theManager;

    private Rigidbody2D body;
    private Animator anim;
    private CircleCollider2D box;
    private Camera mainCam;

    private float deltaX;
    private Vector2 movement;

    //public enum State { Moving, Flying }
    //public State currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<CircleCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //currentState = State.Flying;
    }

    private void Update()
    {
        deltaX = Input.GetAxis("Horizontal") * speed;

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX) * speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //switch (currentState)
        //{
            //case State.Moving:
                movement = new Vector2(deltaX, body.linearVelocityY);
                body.linearVelocity = movement;

    }
}
