using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float playerMovSpeed = 30f;
    public bool canMove = false;
    public bool canFloat = true;
    private int limiteRange = 80;
    
    private SpriteRenderer spriteRenderer;
    public Sprite moveUp;
    public Sprite moveDown;
    public Sprite moveLeft;
    public Sprite moveRight;
    public Sprite moveUpLeft;
    public Sprite moveUpRight;
    public Sprite moveDownLeft;
    public Sprite moveDownRight;
    public Sprite idle;
    public float floatAmplitude = 0.1f; // how far up/down
    public float floatSpeed = 2f; // how fast
    private float baseY; // stores original Y height

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseY = transform.position.y;
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

            UpdateSpriteDirection(moveX, moveZ);

        }

        FloatEffect(); // Floating effect (additive, not overriding)

    }

    void UpdateSpriteDirection(float moveX, float moveZ)
    {
        // Only change sprite when the player is moving
        if (moveX == 0 && moveZ == 0)
            //return;
            spriteRenderer.sprite = idle;
        else if (moveZ > 0 && moveX == 0)
            spriteRenderer.sprite = moveUp;
        else if (moveZ < 0 && moveX == 0)
            spriteRenderer.sprite = moveDown;
        else if (moveX > 0 && moveZ == 0)
            spriteRenderer.sprite = moveRight;
        else if (moveX < 0 && moveZ == 0)
            spriteRenderer.sprite = moveLeft;
        else if (moveX > 0 && moveZ > 0)
            spriteRenderer.sprite = moveUpRight;
        else if (moveX < 0 && moveZ > 0)
            spriteRenderer.sprite = moveUpLeft;
        else if (moveX > 0 && moveZ < 0)
            spriteRenderer.sprite = moveDownRight;
        else if (moveX < 0 && moveZ < 0)
            spriteRenderer.sprite = moveDownLeft;
    }

    void FloatEffect()
    {
        if (canFloat)
        {
            // Calculate offset without overwriting X/Z movement
            float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            Vector3 pos = transform.position;
            pos.y = baseY + offsetY;
            transform.position = pos;            
        }
    }

}