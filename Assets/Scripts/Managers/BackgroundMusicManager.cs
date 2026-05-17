using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{

    [SerializeField] private AudioSource theAudioSource;

    [SerializeField] private AudioClip MenuMusic;

    [SerializeField] private AudioClip levelMusic;

    public static BackgroundMusicManager BGMinstance;

    private void Awake()
    {
        if(BGMinstance == null)
        {
            BGMinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(BGMinstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        theAudioSource.clip = MenuMusic;
        theAudioSource.Play();
    }

    public void playLevelMusic()
    {
        theAudioSource.Stop();
        theAudioSource.generator = levelMusic;
        theAudioSource.Play();
    }

    public void playMenuMusic()
    {
        theAudioSource.Stop();
        theAudioSource.clip = MenuMusic;
        theAudioSource.Play();
    }

    public void playSongMusic(AudioClip TheSong)
    {
        Debug.Log("Playing level music");
        theAudioSource.Stop();
        theAudioSource.generator = TheSong;
        theAudioSource.Play();
    }
}
