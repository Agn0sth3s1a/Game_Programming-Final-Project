using System.Runtime.CompilerServices;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public bool thrown;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform projectileOrigin;

    [SerializeField] private GameObject TempProjectile;

    private GameObject ProjectileInst;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        thrown = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thrown)
            Aim();
    }

    private void Aim()
    {
        float facingDirection = playerTransform.localScale.x;

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
    }
}
