using System.Collections;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{

    [SerializeField] public Animator transition;

    public static SceneSwapper Swapper;

    public void Awake()
    {
        if (Swapper == null)
        {
            Swapper = this;
            DontDestroyOnLoad(gameObject);
        }
        else if( this != Swapper )
        {
            Destroy(gameObject);
        }
    }

    public void ChangeTo(int sceneNumber)
    {
        StartCoroutine(waitToSwap(sceneNumber));
    }

    IEnumerator waitToSwap(int swapTo)
    {
        transition.SetTrigger("Start Transition");
        yield return new WaitForSeconds(0.55f);
        SceneManager.LoadSceneAsync(swapTo);
        yield return new WaitForSeconds(0.55f);
        transition.SetTrigger("Start Transition");
    }

    public void BackToMenu()
    {
        //BackgroundMusicManager.BGMinstance.playMenuMusic();
        ChangeTo(0);
    }

    public int GetSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void FadeInAndOut()
    {
        StartCoroutine(fadeControl());
    }

    IEnumerator fadeControl()
    {
        transition.SetTrigger("Start Transition");
        yield return new WaitForSeconds(1f);
        transition.SetTrigger("Start Transition");
    }
}
