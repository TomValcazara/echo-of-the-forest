using UnityEngine;
using UnityEngine.EventSystems;
public class FinalDoor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Texture2D pointerCursor; // only need to set this one
    public Vector2 pointerHotspot = Vector2.zero;
    private Texture2D defaultCursor;
    private Vector2 defaultHotspot;
    private bool isClickable;
    public GameObject gameplayController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayController = GameObject.Find("Gameplay Controller");
        // Debug.Log("IN");
        // GetComponent<AudioSource>().Play();
        // Debug.Log("OUT");
        isClickable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        // Debug.Log("I was clicked pt1");
        // Debug.Log(isClickable);

        if (!isClickable) return; //Checks if it can click

        isClickable = false; //Makes sure there is no Double Click
        //AudioSource.PlayClipAtPoint(clickClip, Vector3.zero); //Leafs sound
        //gameplayController.GetComponent<GameplayController>().UpdateRound(); //Updates Round
        transform.Find("Door On").gameObject.SetActive(false);
        transform.Find("Door Off").gameObject.SetActive(true);
        
        //Default Cursor
        Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);

        //GameObject.FindGameObjectsWithTag("DoorOn").SetActive(false);
        //GameObject.FindGameObjectsWithTag("DoorOff").SetActive(true);

        GetComponent<AudioSource>().Stop();
        //Debug.Log("M1");
        gameplayController.GetComponent<GameplayController>().DragToHell();
        //Debug.Log("M2");
        //Debug.Log("I was clicked pt2");

        //Destroy(gameObject); //removes the bush
        //Debug.Log("I was clicked pt3");

        //StartCoroutine(DelayedUpdateRound()); //Forces to wait one frame, so the instanced is properly destroyied
        //Debug.Log("I was clicked pt4");

        //Calculates Score

        //Animate answer

        //Debug.Log("Clicked on " + gameObject.name);
        // Your logic here (e.g., play sound, collect item)
    }
    // IEnumerator DelayedUpdateRound()
    // {
    //     Debug.Log("I was clicked pt5");
    //     yield return null; // wait one frame
    //     gameplayController.GetComponent<GameplayController>().UpdateRound(); //Update Round
    //     Debug.Log("I was clicked pt6");
    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isClickable) {
            Cursor.SetCursor(pointerCursor, pointerHotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reverts back to whatever is defined in Player Settings
        Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
    }
}
