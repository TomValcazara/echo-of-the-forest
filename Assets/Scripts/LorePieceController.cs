using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class LorePieceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isClickable;
    public GameObject gameplayController;
    public List<GameObject> bushesGroups;

    public Texture2D pointerCursor; // only need to set this one
    public Vector2 pointerHotspot = Vector2.zero;
    private Texture2D defaultCursor;
    private Vector2 defaultHotspot;
    public AudioClip clickClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayController = GameObject.Find("Gameplay Controller");
        
        //Creates a random bush
        int randPos = Random.Range(0, bushesGroups.Count);
        Instantiate(bushesGroups[randPos], transform);

    }

    void Awake()
    {
        LorePieceState(false);
    }

    public void LorePieceState(bool _isActive)
    {
        // Debug.Log("Messing with:");
        // Debug.Log(gameObject);
        // Debug.Log(_isActive);

        //Audio Source is starting disabled after first instance is destroied, for some reason.
        AudioSource audio = GetComponent<AudioSource>();
        if (!audio.enabled)
        {
            audio.enabled = true;
        }
        audio.clip = audio.clip; // force refresh    
            
        isClickable = _isActive;
        if (_isActive)
        {
            //AudioSource audio = GetComponent<AudioSource>();
            //Debug.Log($"AudioSource enabled: {audio.enabled}, gameObject active: {audio.gameObject.activeInHierarchy}");
            

                
            audio.Play();

            //GetComponent<AudioSource>().Play();
            //GetComponent<AudioSource>().mute = false;
            //Debug.Log("Gotin 3");
            //Play Sound and make clickable
        }
        else
        {
            audio.Stop();
            //GetComponent<AudioSource>().mute = true;
            //Debug.Log("Gotin NOPE");
            //Stop Sound and make unclickable
        }
    }

    void OnMouseDown()
    {
        // Debug.Log("I was clicked pt1");
        // Debug.Log(isClickable);

        if (!isClickable) return; //Checks if it can click

        isClickable = false; //Makes sure there is no Double Click
        AudioSource.PlayClipAtPoint(clickClip, Vector3.zero); //Leafs sound
        gameplayController.GetComponent<GameplayController>().UpdateRound(); //Updates Round
         

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

    // public void ResetCursor()
    // {
    //     // When clicked, restore the default cursor too
    //     Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
    // }
    // public void ClickSound()
    // {
    //     //Debug.Log("Sound");
    //     AudioSource.PlayClipAtPoint(clickClip, Vector3.zero); //Used this instead of having the AudioSource on the button, because when the canvas become inactive, all children elements get deactivate too, making the sound never play.

    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
