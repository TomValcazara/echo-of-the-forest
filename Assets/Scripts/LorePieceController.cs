using UnityEngine;
using System.Collections;
public class LorePieceController : MonoBehaviour
{
    private bool isClickable;
    public GameObject gameplayController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayController = GameObject.Find("Gameplay Controller");        
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

        isClickable = _isActive;
        if (_isActive)
        {
            //AudioSource audio = GetComponent<AudioSource>();
            //Debug.Log($"AudioSource enabled: {audio.enabled}, gameObject active: {audio.gameObject.activeInHierarchy}");
            

            //Audio Source is starting disabled after first instance is destroied, for some reason.
            AudioSource audio = GetComponent<AudioSource>();
            if (!audio.enabled)
            {
                audio.enabled = true;
            }
            audio.clip = audio.clip; // force refresh        
            audio.Play();

            //GetComponent<AudioSource>().Play();
            //GetComponent<AudioSource>().mute = false;
            //Debug.Log("Gotin 3");
            //Play Sound and make clickable
        }
        else
        {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
