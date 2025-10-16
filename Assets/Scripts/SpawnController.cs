using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private int randomXpos;
    private int randomZpos;
    private int spawnRange = 80;

    public GameObject prefabLorePiece;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            //Debug.Log("Current index"+i);

            randomXpos = Random.Range(-spawnRange, spawnRange);
            randomZpos = Random.Range(-spawnRange, spawnRange);
            //var _currentInstance = Instantiate(prefabLorePiece, new Vector3(randomXpos, 1, randomZpos), Quaternion.identity);
            Instantiate(prefabLorePiece, new Vector3(randomXpos, 1, randomZpos), Quaternion.identity);
            // if (i == 0){
            //     //Debug.Log("Gotin 1");
            //     _currentInstance.GetComponent<LorePieceController>().LorePieceState(true);
            //     //Debug.Log("Gotin 2");
            // }
            
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
