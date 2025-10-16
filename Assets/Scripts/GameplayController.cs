using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void UpdateRound()
    {

        lorePieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("TagLorePiece")); //Captures All Lore Pieces isntances
        //Debug.Log(lorePieces[0].name);

        Destroy(lorePieces[0]);// Destroies current Lore Piece instance that was clicked

        //Animates stuff
        canvasInfoLore.SetActive(true);
        player.GetComponent<PlayerController>().canMove = false; //Stops Player from moving

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
    public void StartNextRound()
    {
        
        canvasInfoLore.SetActive(false);
        player.GetComponent<PlayerController>().canMove = true; //Allows Player to Move
        
        if (currentRound < 6) //Activates next Lore Piece, if it ins't the last round
        {
            lorePieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("TagLorePiece")); //Captures All Lore Pieces instances
            lorePieces[0].GetComponent<LorePieceController>().LorePieceState(true);  //Should be 1 ?            
            
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
