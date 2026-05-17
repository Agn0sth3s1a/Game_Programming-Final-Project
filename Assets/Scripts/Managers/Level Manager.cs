using System.Collections;
using System.Diagnostics;
using TMPro;
using Unity.Multiplayer.PlayMode;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Other Managers")]
    [SerializeField] private PauseMenuManager PauseMenu;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private FollowCam theCamera;

    [Header("Prefabs to control")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform startingSpawn;

    [Header("Level's Music")]
    [SerializeField] private AudioClip LevelMusic;

    [Header("Lives Counter")]
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject lifeCounterObject;

    private static Transform spawnPoint;

    private GameObject playerCharacter;

    public static int Lives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //lifeCounter.text = Lives + "x";
        BackgroundMusicManager.BGMinstance.playSongMusic(LevelMusic);
        spawnPoint = startingSpawn.transform;
        playerCharacter = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        playerCharacter.SetActive(true);
        playerManager.SetCurrent(playerCharacter);
        Cursor.visible = false;
        lifeCounterObject.SetActive(true);

        if (Lives == 3)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(true);
        }
        else if (Lives == 2)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
        }
        else if (Lives == 1)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
    }

    public static void updateSpawn(Transform newSpawnPoint)
    {
        spawnPoint.position = new Vector3(newSpawnPoint.position.x, newSpawnPoint.position.y, 0);
    }

    public void respawnPlayer(GameObject currentPlayer)
    {
        if (Lives > 1)
        {
            --Lives;
            currentPlayer.SetActive(false);
            SceneSwapper.Swapper.FadeInAndOut();
            StartCoroutine(waitToRespawn(currentPlayer));
            //lifeCounter.text = Lives + "x";
        }
        else
        {
            --Lives;
            //lifeCounter.text = Lives + "x";
            lifeCounterObject.SetActive(false);
            SceneSwapper.Swapper.BackToMenu();
        }

        if (Lives == 3)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(true);
        }
        else if (Lives == 2)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
        }
        else if (Lives == 1)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
    }

    IEnumerator waitToRespawn(GameObject currentPlayer)
    {
        yield return new WaitForSeconds(1f);
        playerCharacter = Instantiate(playerPrefab, spawnPoint.position, Quaternion.Euler(0f, 180f, 0f));
        playerManager.SetCurrent(playerCharacter);
        FollowCam.ChangeTarget(playerCharacter);
        Destroy(currentPlayer);
    }

    public void NextLevel()
    {
        int nextLevel = SceneSwapper.Swapper.GetSceneID() + 1;
        SceneSwapper.Swapper.ChangeTo(nextLevel);
    }

}
