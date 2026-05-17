using System.Collections;
using UnityEngine;

public enum objectType { block, enemy }
public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject crumbleToBones;

    [SerializeField] private objectType objectType;

    [SerializeField] AudioClip breakNoise;

    private LayerMask playerLayer;
    private bool Broken = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer.value = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Broken && (playerLayer == collision.gameObject.layer))
        {
            switch (objectType)
            {
                case objectType.block:

                    break;
                case objectType.enemy:
                    Instantiate(crumbleToBones, transform.position, transform.rotation);
                    break;
            }
            Broken = true;
            AudioManager.instance.playSoundFXClip(breakNoise, transform, 1f);
            StartCoroutine(waitToDestroy());
        }
    }

    private IEnumerator waitToDestroy()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
