using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject playerHead;
    [SerializeField] public GameObject playerBody;

    [SerializeField] public GameObject crumbledBody;

    [SerializeField] FollowCam theCamera;

    [SerializeField] AudioClip[] sounds;

    private Rigidbody2D headRigid;
    private Rigidbody2D bodyRigid;

    private GameObject currentObject;
    private GameObject oldObject;

    private string CurrentFocus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        headRigid = playerHead.GetComponent<Rigidbody2D>();
        bodyRigid = playerBody.GetComponent<Rigidbody2D>();
        CurrentFocus = "body";
    }

    public void SetCurrent(GameObject thePlayer)
    {
        //Debug.Log("Setting current Focus");
        currentObject = thePlayer;
        CurrentFocus = "body";
        FollowCam.ChangeTarget(currentObject);
    }

    public void BodyOnly()
    {
        CurrentFocus = "body";
        oldObject = currentObject;
        currentObject = Instantiate(playerBody, currentObject.transform);
        AudioManager.instance.playSoundFXClip(sounds[0], currentObject.transform, 1f);
        Destroy(oldObject);
        currentObject.SetActive(true);
        FollowCam.ChangeTarget(currentObject);
    }

    public void BodyOnly(Vector2 dropPosition)
    {
        CurrentFocus = "body";
        oldObject = currentObject;
        currentObject = Instantiate(playerBody, dropPosition, Quaternion.Euler(0f, 180f, 0f));
        AudioManager.instance.playSoundFXClip(sounds[0], currentObject.transform, 1f);
        Destroy(oldObject);
        currentObject.SetActive(true);
        FollowCam.ChangeTarget(currentObject);
    }

    public void ProjectileOnly(GameObject flyingHead)
    {
        CurrentFocus = "projectile";
        //Instantiate(crumbledBody, currentObject.transform.position, currentObject.transform.rotation);
        currentObject = flyingHead;
        //Destroy(oldObject);
        //playerHead.SetActive(false);
        //playerBody.transform.position = playerHead.transform.position;
        //bodyRigid.MovePosition(playerHead.transform.position);
        //.transform.position = playerHead.transform.position;
        FollowCam.ChangeTarget(currentObject);
        //playerBody.SetActive(false);
    }

    public void ProjectileToRolling(float xPos, float yPos)
    {
        CurrentFocus = "head";
        oldObject = currentObject;
        currentObject = Instantiate(playerHead, new Vector2(xPos, yPos), Quaternion.Euler(0f, 180f, 0f));
        AudioManager.instance.playSoundFXClip(sounds[1], currentObject.transform, 1f);
        currentObject.SetActive(true);
        Destroy(oldObject);
        /*playerHead.transform.position = new Vector2(xPos,yPos);
        playerHead.SetActive(true);*/
        FollowCam.ChangeTarget(currentObject);
    }

    public void DropBody(float xPos, float yPos)
    {
        GameObject crumbled = Instantiate(crumbledBody, new Vector2(xPos, yPos), Quaternion.Euler(0f, 180f, 0f));
        //AudioManager.instance.playSoundFXClip(sounds[2], crumbled.transform, 1f);
        //Destroy(currentObject);
    }

    public string GetCurrentFocus()
    {
        return CurrentFocus;
    }
}
