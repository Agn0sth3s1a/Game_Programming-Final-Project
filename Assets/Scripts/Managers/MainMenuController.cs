using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject checkMark;

    [SerializeField] private GameObject SettingsPanel;

    [SerializeField] private AudioClip buttonPressSound;

    [SerializeField] private AudioClip MenuMusic;

    //[SerializeField] private BackgroundMusicManager musicManager;

    public static bool TrajectoryLine;

    private bool settingsOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackgroundMusicManager.BGMinstance.playMenuMusic();
        TrajectoryLine = false;
        SettingsPanel.SetActive(false);
        Cursor.visible = true;
    }

    public void StartGame()
    {
        AudioManager.instance.playSoundFXClip(buttonPressSound, transform, 1f);
        LevelManager.Lives = 3;
        SceneSwapper.Swapper.ChangeTo(1);
    }

    public void CheckUncheck()
    {
        if(!TrajectoryLine)
        {
            checkMark.SetActive(true);
            TrajectoryLine = true;
        }
        else if(TrajectoryLine)
        {
            checkMark.SetActive(false);
            TrajectoryLine = false;
        }
        AudioManager.instance.playSoundFXClip(buttonPressSound, transform, 1f);
    }

    public void OpenCloseSettings()
    {
        AudioManager.instance.playSoundFXClip(buttonPressSound, transform, 1f);
        settingsOpen = !settingsOpen;
        SettingsPanel.SetActive(settingsOpen);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
