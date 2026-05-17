using System.Collections;
using UnityEngine;

public class DangerSource : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private AudioClip killSound;

    private bool canRespawn = true;

    private int respawnNumber = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((playerLayer.value & (1 << collision.gameObject.layer)) > 0) && canRespawn)
        {
            AudioManager.instance.playSoundFXClip(killSound, transform, 1f);
            Debug.Log("Starting Respawn" + respawnNumber);
            levelManager.respawnPlayer(collision.gameObject);
            canRespawn = false;
            StartCoroutine(RespawnTimer());
        }
    }

    IEnumerator RespawnTimer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1f);
        canRespawn = true;
    }
}
