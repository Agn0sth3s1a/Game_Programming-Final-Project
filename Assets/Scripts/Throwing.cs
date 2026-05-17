using System.Runtime.CompilerServices;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 rotation;

    public bool thrown;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform projectileOrigin;

    [SerializeField] private GameObject theProjectile;

    [SerializeField] PlayerManager theManager;

    private GameObject ProjectileInst;

    private Rigidbody2D skullRigid;

    [SerializeField] float LaunchForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        thrown = false;
        skullRigid = theProjectile.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!thrown)
            Aim();
    }

    private void Aim()
    {
        float facingDirection = playerTransform.localScale.x;

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
    }

    public void Throw()
    {
        //skullRigid.AddForce(transform.rotation, ForceMode2D.Impulse);
        //skullRigid.linearVelocity = Vector3.right * 100f;
        ProjectileInst = Instantiate(theProjectile, projectileOrigin.position, transform.rotation);
        theManager.ProjectileOnly(ProjectileInst);
        ProjectileInst.SetActive(true);
        ProjectileInst.GetComponent<Rigidbody2D>().linearVelocity = transform.right * LaunchForce;
            //AddForce(transform.right * 10f, ForceMode2D.Impulse);
        //theProjectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 1) * 5f;
    }
}
