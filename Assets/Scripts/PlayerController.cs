using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float playerMovSpeed = 30f;
    public bool canMove = false;

    private int limiteRange = 80;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    

    void Update()
    {
        if (canMove)
        {
            
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0, moveZ);
            transform.Translate(move * playerMovSpeed * Time.deltaTime, Space.World);

            //Limits player inside the borders
            Vector3 pos = transform.position;
            if (pos.x < -limiteRange)
            {
                pos.x = -limiteRange;
            }
            else if (pos.x > limiteRange)
            {
                pos.x = limiteRange;
            }
            // Check Z limits
            if (pos.z < -limiteRange)
            {
                pos.z = -limiteRange;
            }
            else if (pos.z > limiteRange)
            {
                pos.z = limiteRange;
            }
            transform.position = pos; // Apply corrected position
        }
    }
}