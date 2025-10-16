using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform cameraTarget;
    public Vector3 cameraOffset = new Vector3(0, 5, -5);

    void LateUpdate()
    {
        transform.position = cameraTarget.position + cameraOffset;
        transform.LookAt(cameraTarget);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
