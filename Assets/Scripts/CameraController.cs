using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraTarget;
    public Vector3 cameraOffset = new Vector3(0, 5, -5);

    void LateUpdate()
    {
        Vector3 camPos = cameraTarget.position + cameraOffset;
        camPos.y = 5;
        transform.position = camPos;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.LookAt(cameraTarget); //Setted this only at the start up, so it dont follow the Ghost up/down flaoting animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
