using UnityEngine;

public class PlatformerPlayerBackup : MonoBehaviour
{

    [SerializeField] public float speed = 4.5f;

    public float jumpForce = 12.0f;

    private Rigidbody2D body;

    private Animator anim;

    private CapsuleCollider2D box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.linearVelocityY);
        body.linearVelocity = movement;

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

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
