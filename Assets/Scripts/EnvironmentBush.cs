using UnityEngine;
using System.Collections.Generic;

public class EnvironmentBush : MonoBehaviour
{

    public List<Sprite> environmentBushes;
    public float[] posY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // posY = new float[3];
        // //posY = [1.5, 0.9, 1.9];
        // int imageIndex = Random.Range(0, environmentBushes.Count);
        // GameObject.FindGameObjectsWithTag("EnvironmentBushPos").GetComponent<SpriteRenderer>().sprite = environmentBushes[imageIndex];
        // GameObject.FindGameObjectsWithTag("EnvironmentBushPos").transform = Vector3(0, posY[imageIndex], 0);
        //GetComponent<SpriteRenderer>().sprite = environmentBushes[imageIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
