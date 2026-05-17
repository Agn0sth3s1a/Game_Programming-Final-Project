using System.Collections;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{

    [SerializeField] public float speed = 4.5f;
    [SerializeField] GameObject rotator;

    [SerializeField] Throwing ThrowingScript;
    [SerializeField] PlayerManager theManager;

    [SerializeField] private AudioClip[] throwingSounds;

    //public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private Animator anim;
    private CapsuleCollider2D box;
    private Camera mainCam;

    public enum State { Walking, Throwing, Rolling }
    public State currentState = State.Walking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<CapsuleCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rotator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                float deltaX = Input.GetAxis("Horizontal") * speed;
                Vector2 movement = new Vector2(deltaX, body.linearVelocityY);
                body.linearVelocity = movement;

                if(deltaX == 0)
                {
                    body.linearVelocity = new Vector2(0, body.linearVelocityY);
                }

                Vector3 max = box.bounds.max;
                Vector3 min = box.bounds.min;
                Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
                Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
                Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

                bool grounded = false;

                if( hit != null)
                {
                    grounded = true;
                }

                anim.SetFloat("speed", Mathf.Abs(deltaX) * speed);

                if (!Mathf.Approximately(deltaX, 0))
                {
                    transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
                    //Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.y);
                    //transform.rotation = Quaternion.Euler(rotator);
                }

                if (Input.GetKeyDown(KeyCode.Q) && grounded)
                {
                    anim.SetFloat("speed", 0);
                    currentState = State.Throwing;
                    rotator.SetActive(true);
                }
                break;
            case State.Throwing:
                body.linearVelocity = new Vector2(0, 0);
                if (Input.GetMouseButtonDown(0))
                {
                    ThrowingScript.Throw();
                    AudioManager.instance.playRandomSFXClip(throwingSounds, transform, 0.75f);
                    currentState = State.Walking;
                    rotator.SetActive(false);
                    //anim.SetBool("Crumbled", true);
                    StartCoroutine(WaitToDestroy());
                    theManager.DropBody(transform.position.x, transform.position.y);
                }
                else if(Input.GetKeyDown(KeyCode.Q))
                {
                    rotator.SetActive(false);
                    currentState = State.Walking;
                }
                    break;
            case State.Rolling:
                if (Input.GetMouseButtonDown(1))
                {
                    currentState = State.Walking;
                }
                break;
        }
        
    }
    IEnumerator WaitToDestroy()
    {
        //Debug.Log("waiting");
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }

}
