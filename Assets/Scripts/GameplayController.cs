using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameplayController : MonoBehaviour
{
    public int currentRound = 0;
    public TextMeshProUGUI textRounds;

    public TextMeshProUGUI textWarning;
    public List<GameObject> lorePieces = new List<GameObject>();
    private float elapsedTime = 0f;
    private bool isTimeRunning = false;
    public GameObject canvasGameOver;
    public GameObject canvasTitleScreen;
    public GameObject canvasHowToPlay;

    public GameObject canvasInfoLore;
    public GameObject pieceOfLoreDescription;

    public GameObject mainCamera;

    public GameObject player;
    public string[] story;
    public GameObject spawnManager;

    private Color originalColor;

    public GameObject hellPortal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //DragToHell();

        //Creates teh fragments of the Lore
        story = new string[6];
        story[0] = "The forest whispered tonight - a new arrival had drifted in, soft as doubt, pale as confession.\nThe sleeping ones stirred, their sighs weaving beneath the roots.";
        story[1] = "He took two slices, one murmured, their voice damp with envy.\nTwo! When one was enough for salvation.";
        story[2] = "Another sighed. It’s always the gentle ones who wander too far - loving too much, laughing too loud, wanting too freely.\nTheir pity dripped like prayer wax.";
        story[3] = "The fresh soul said nothing.\nHe remembered warmth, and hands that once fit his - forbidden, fleeting, holy.";
        story[4] = "Sin, whispered the wind. Sin for loving, sin for being, sin for not hiding.\nAnd the in-between bloomed with forgiveness no one believed in.";
        story[5] = "When dawn came, the sleeping ones fell quiet — ashamed, perhaps, or simply empty.\nThe fresh soul drifted on, too bright for limbo, too tender for The Red Garden Below.";

    }

    IEnumerator FadeLoopWarning()
    {
        float speed = 1f; 

        while (true)
        {
            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // oscillates 0→1→0
            Color c = originalColor;
            c.a = t; // apply alpha
            textWarning.color = c;
            yield return null;
        }
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
        StartCoroutine(FadeInCanvasGroup(canvasInfoLore.GetComponent<CanvasGroup>(), 1f));

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



    IEnumerator FadeInCanvasGroup(CanvasGroup cg, float duration)
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
            //FinishTheGame();
            spawnManager.GetComponent<SpawnController>().SpawnFinalDoor();

            //Flash A Warning for the final Door
            textWarning.gameObject.SetActive(true);
            originalColor = textWarning.color;
            StartCoroutine(FadeLoopWarning());

        }
    }


    public void DragToHell()
    {

        textWarning.gameObject.SetActive(false);

        //Stops calming main music
        mainCamera.GetComponent<AudioSource>().Stop();

        //Locks player from moving
        player.GetComponent<PlayerController>().canMove = false; //Stops the player from moving

        isTimeRunning = false; //Stops and loads the timer
        //canvasGameOver.Find("Text Time").text = FormatTime(elapsedTime);
        canvasGameOver.transform.Find("Text Time").GetComponent<TextMeshProUGUI>().text = "Total time: " + FormatTime(elapsedTime);

        //Adds Hell Portal
        //Instantiate(hellPortal, new Vector3(player.transform.position.x, -0.9f, player.transform.position.z), Quaternion.identity);
        // Adds Hell Portal and store the instance
        GameObject portalInstance = Instantiate(hellPortal, new Vector3(player.transform.position.x, -1.9f, player.transform.position.z), Quaternion.identity);

        //Animates to Hell
        //StartCoroutine(HellAnimation());

        //Stops Ghost floating
        player.GetComponent<PlayerController>().canFloat = false;        

        // Animates to Hell (pass the instance)
        StartCoroutine(HellAnimation(portalInstance));

    }
    
    IEnumerator HellAnimation(GameObject portalInstance)
    {
        //Debug.Log("#1#");
        float speed = 0.25f;

        Transform cylinder = portalInstance.transform.Find("Cylinder");
        if (cylinder == null)
        {
            Debug.LogError("Cylinder not found inside hell portal!");
            yield break;
        }

        // Cylinder: 0,1,0 → 3,1,3
        yield return ScaleTo(cylinder, new Vector3(3, 1, 3), speed);

        // Player: current → Y -3
        Vector3 targetPos = player.transform.position;
        targetPos.y = -3;
        yield return MoveTo(player.transform, targetPos, speed);

        // Cylinder: back to 0,1,0
        yield return ScaleTo(cylinder, new Vector3(0, 1, 0), speed);

        // End
        //Debug.Log("#3#");
        FinishTheGame();
    }

    IEnumerator MoveTo(Transform obj, Vector3 target, float duration)
    {
        Vector3 start = obj.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * duration;
            obj.position = Vector3.Lerp(start, target, t);
            yield return null;
        }
    }

    IEnumerator ScaleTo(Transform obj, Vector3 target, float duration)
    {
        //Debug.Log("#4#");
        Vector3 start = obj.localScale;
        float t = 0f;
        while (t < 1f)
        {
            //Debug.Log("#5#");
            t += Time.deltaTime * duration;
            obj.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        //Debug.Log("#6#");
    }


    public void FinishTheGame()
    {

        
        //Debug.Log("M3");
        

        

        canvasGameOver.GetComponent<CanvasGroup>().alpha = 0f;
        canvasGameOver.SetActive(true);
        StartCoroutine(FadeInCanvasGroup(canvasGameOver.GetComponent<CanvasGroup>(), 1f));
        //canvasGameOver.SetActive(true); //Reveals tha Game over Screen

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
