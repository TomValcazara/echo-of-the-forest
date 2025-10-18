using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    public int currentRound = 0;
    public TextMeshProUGUI textRounds;
    public List<GameObject> lorePieces = new List<GameObject>();
    private float elapsedTime = 0f;
    private bool isTimeRunning = false;
    public GameObject canvasGameOver;
    public GameObject canvasTitleScreen;
    public GameObject canvasHowToPlay;

    public GameObject canvasInfoLore;
    public GameObject pieceOfLoreDescription;

    public GameObject player;
    public string[] story;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        story = new string[6]; 

        story[0] = "The forest whispered tonight - a new arrival had drifted in, soft as doubt, pale as confession.\nThe sleeping ones stirred, their sighs weaving beneath the roots.";
        story[1] = "He took two slices, one murmured, their voice damp with envy.\nTwo! When one was enough for salvation.";
        story[2] = "Another sighed. It’s always the gentle ones who wander too far - loving too much, laughing too loud, wanting too freely.\nTheir pity dripped like prayer wax.";
        story[3] = "The fresh soul said nothing.\nHe remembered warmth, and hands that once fit his - forbidden, fleeting, holy.";
        story[4] = "Sin, whispered the wind. Sin for loving, sin for being, sin for not hiding.\nAnd the in-between bloomed with forgiveness no one believed in.";
        story[5] = "When dawn came, the sleeping ones fell quiet — ashamed, perhaps, or simply empty.\nThe fresh soul drifted on, too bright for limbo, too tender for The Red Garden Below.";
    }

    public void UpdateRound()
    {

        lorePieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("TagLorePiece")); //Captures All Lore Pieces isntances
                                                                                              //Debug.Log(lorePieces[0].name);

        //lorePieces[0].transform.FindGameObjectsWithTag("BushGroupTag").gameObject.SetActive(false);

        //GameObject.FindGameObjectsWithTag("BushGroupTag");
        // or only children

        //Hides Bush
        foreach (Transform child in lorePieces[currentRound].transform)
        {
            if (child.CompareTag("BushGroupTag"))
            {
                child.gameObject.SetActive(false);
            }
        }
            
                

        //Reveals Fantastic Creature
        lorePieces[currentRound].transform.Find("FantasticCreature").gameObject.SetActive(true);
        lorePieces[currentRound].GetComponent<LorePieceController>().LorePieceState(false);
        SpriteRenderer fantasticCreatureSprite = lorePieces[currentRound].transform.Find("FantasticCreature").GetComponent<SpriteRenderer>();

        //Fades Creature away
        StartCoroutine(FadeRoutine(2f, fantasticCreatureSprite));
        //StartCoroutine(WaitingTime(2f));//Waits 2 secs b4 resuming
        //Destroy(lorePieces[0]);// Destroies current Lore Piece instance that was clicked

        //Animates stuff
        pieceOfLoreDescription.GetComponent<TextMeshProUGUI>().text = story[currentRound];
        player.GetComponent<PlayerController>().canMove = false; //Stops Player from moving

        canvasInfoLore.GetComponent<CanvasGroup>().alpha = 0f;
        canvasInfoLore.SetActive(true);
        StartCoroutine(FadeInPieceOfLore(canvasInfoLore.GetComponent<CanvasGroup>(), 1f));

        // if (currentRound < 5) //Only creats next Round if it isn't the last round
        // {
        //     lorePieces[1].GetComponent<LorePieceController>().LorePieceState(true); //Activates next Lore Piece
        // }


        currentRound += 1; //Updates HUD and counter of Rounds
        textRounds.text = "PARTS FOUND: " + currentRound + "/6";

        // else
        // {
        //     Debug.Log("Finish Game");
        // }
        // if (currentRound == 6)
        // {
        //     //Debug.Log("Finish Game");
        //     FinishTheGame();
        // }

        //Debug.Log("ONE 3");
    }



    IEnumerator FadeInPieceOfLore(CanvasGroup cg, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }
        cg.alpha = 1f;
    }
        
    IEnumerator WaitingTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }
    IEnumerator FadeRoutine(float duration, SpriteRenderer fantasticCreatureSprite)
    {
        float startAlpha = fantasticCreatureSprite.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            Color c = fantasticCreatureSprite.color;
            c.a = Mathf.Lerp(startAlpha, 0f, t);
            fantasticCreatureSprite.color = c;
            yield return null; // wait 1 frame
        }

        // Ensure fully transparent
        Color final = fantasticCreatureSprite.color;
        final.a = 0f;
        fantasticCreatureSprite.color = final;

        //Code here runs after fade is done
        //OnFadeComplete();
        //Debug.Log("Done Animation");
    }

    public void StartNextRound()
    {

        canvasInfoLore.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true; //Allows Player to Move

        if (currentRound < 6) //Activates next Lore Piece, if it ins't the last round
        {
            lorePieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("TagLorePiece")); //Captures All Lore Pieces instances
            lorePieces[currentRound].GetComponent<LorePieceController>().LorePieceState(true);  //Should be 1 ?            

        }
        else
        {
            //Debug.Log("Finish Game");
            FinishTheGame();
        }
    }
    public void StartGame()
    {
        player.GetComponent<PlayerController>().canMove = true; //Allows Player to Move

        textRounds.text = "PARTS FOUND: 0/ 6"; //Initial text
         
        //Starts Timer
        isTimeRunning = true;

        //Hides Title and How to Play Screen
        canvasTitleScreen.SetActive(false);
        canvasHowToPlay.SetActive(false);

        //StartSounds
        lorePieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("TagLorePiece")); //Captures All Lore Pieces isntances
        lorePieces[0].GetComponent<LorePieceController>().LorePieceState(true); //Activates First Lore Piece


    }
    public void ShowHowToPlay()
    {
        //Hides Title and How to Play Screen
        canvasTitleScreen.SetActive(false);
        canvasHowToPlay.SetActive(true);

    }

    private void FinishTheGame()
    {
        player.GetComponent<PlayerController>().canMove = false; //Stops the player from moving

        isTimeRunning = false; //Stops and loads the timer
        //canvasGameOver.Find("Text Time").text = FormatTime(elapsedTime);
        canvasGameOver.transform.Find("Text Time").GetComponent<TextMeshProUGUI>().text = FormatTime(elapsedTime);

        canvasGameOver.SetActive(true); //Reveals tha Game over Screen

    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
    // Update is called once per frame
    void Update()
    {
        if (isTimeRunning) //Timer Clock for the game
        {
            elapsedTime += Time.deltaTime;

            // if (timerText != null)
            //     timerText.text = FormatTime(elapsedTime);
        }

    }

    public void NextLevel()
    {
        //It is a fake call, since there is no next level.
        SceneManager.LoadScene("MainScene");
    }

}
