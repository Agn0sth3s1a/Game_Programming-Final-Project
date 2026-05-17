using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    [SerializeField] private LayerMask whereCanLand;

    //[SerializeField] private GameObject controllableHead;

    [SerializeField] private PlayerManager theManager;

    private Rigidbody2D rb;

    GameObject instantiatedHead;

    private float xPos, yPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Landed on Ground");
        if ((whereCanLand.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Debug.Log("Landed!");
            xPos = gameObject.transform.position.x;
            yPos = gameObject.transform.position.y + 1f;
            theManager.ProjectileToRolling(xPos, yPos);
            StartCoroutine(WaitToDestroy());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    IEnumerator WaitToDestroy()
    {
        //Debug.Log("waiting");
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
