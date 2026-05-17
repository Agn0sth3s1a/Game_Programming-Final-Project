using UnityEngine;

public class ExpandedPlatformerPlayer : MonoBehaviour
{
    [SerializeField] public float speed = 4.5f;
    [SerializeField] GameObject rotator;

    [SerializeField] Throwing ThrowingScript;

    private Rigidbody2D bodyRigid;
    private Animator anim;
    private CapsuleCollider2D box;

    private Rigidbody2D headRigid;
    private Animator headAnim;
    private CapsuleCollider2D headCollider;

    private Camera mainCam;

    public enum State { Walking, Throwing, Rolling }
    public State currentState = State.Walking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bodyRigid = GetComponent<Rigidbody2D>();
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
                Vector2 movement = new Vector2(deltaX, bodyRigid.linearVelocityY);
                bodyRigid.linearVelocity = movement;

                /*Vector3 max = box.bounds.max;
                Vector3 min = box.bounds.min;
                Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
                Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
                Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

                bool grounded = false;

                if( hit != null)
                {
                    grounded = true;
                }*/

                anim.SetFloat("speed", Mathf.Abs(deltaX) * speed);

                if (!Mathf.Approximately(deltaX, 0))
                {
                    transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
                    //Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.y);
                    //transform.rotation = Quaternion.Euler(rotator);
                }

                /*if (Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }*/
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    anim.SetFloat("speed", 0);
                    currentState = State.Throwing;
                    rotator.SetActive(true);
                }
                break;
            case State.Throwing:
                if (Input.GetMouseButtonDown(0))
                {
                    ThrowingScript.Throw();
                    currentState = State.Walking;
                    rotator.SetActive(false);
                    anim.SetBool("Crumbled", true);
                }
                break;
            case State.Rolling:
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = State.Walking;
                }
                break;
        }

    }
}
