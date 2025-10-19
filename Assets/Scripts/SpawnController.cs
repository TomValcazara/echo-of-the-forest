using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    private int randomXpos;
    private int randomZpos;
    private int spawnRangeLore = 80;
    private int spawnRangeBush = 120;
    private int spawnRangeDoor = 80;
    public GameObject prefabLorePiece;
    public List<GameObject> prefabEnvironmentBush;
    public GameObject finalDoor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Create Pieces of Lore
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log("Current index"+i);

            randomXpos = Random.Range(-spawnRangeLore, spawnRangeLore);
            randomZpos = Random.Range(-spawnRangeLore, spawnRangeLore);
            //var _currentInstance = Instantiate(prefabLorePiece, new Vector3(randomXpos, 1, randomZpos), Quaternion.identity);
            Instantiate(prefabLorePiece, new Vector3(randomXpos, 0, randomZpos), Quaternion.identity);
            // if (i == 0){
            //     //Debug.Log("Gotin 1");
            //     _currentInstance.GetComponent<LorePieceController>().LorePieceState(true);
            //     //Debug.Log("Gotin 2");
            // }

        } 
        
        //Create Random Bushes
        for (int i = 0; i < 300; i++)
        {
            randomXpos = Random.Range(-spawnRangeBush, spawnRangeBush);
            randomZpos = Random.Range(-spawnRangeBush, spawnRangeBush);
            Instantiate(prefabEnvironmentBush[Random.Range(0, prefabEnvironmentBush.Count)], new Vector3(randomXpos, 0, randomZpos), Quaternion.identity);
            
        }
    }

    public void SpawnFinalDoor()
    {
        //finalDoor
        
        randomXpos = Random.Range(-spawnRangeDoor, spawnRangeDoor);
        randomZpos = Random.Range(-spawnRangeDoor, spawnRangeDoor);
        Instantiate(finalDoor, new Vector3(randomXpos, 0, randomZpos), Quaternion.identity);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
