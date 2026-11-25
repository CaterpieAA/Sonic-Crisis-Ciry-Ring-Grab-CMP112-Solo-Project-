using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    public float movementSpeed = 3f;

    public float jumpForce = 1f;

    public float springForce = 2f;

    private float horizontalInput;
    private float verticalInput;

    private bool isAirborne;

    private AudioSource source;

    public AudioClip jumpSound;

    public AudioClip springSound;

    private SpriteRenderer spriteRenderer;

    private float positionX = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        source = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void flipSprite(float x)
    {
        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {

            spriteRenderer.flipX = false;
        }

    }
    
    
    void movement()
    {

        isAirborne = false;

        if (Input.GetKey(KeyCode.D))
        {

                positionX = 0;

                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Force);

                positionX = positionX + 1;

                flipSprite(positionX);
                        
        }
        else if (Input.GetKey(KeyCode.A))
        {
                positionX = 0;
            
                rb.AddForce(Vector2.left * movementSpeed, ForceMode2D.Force);

                positionX = positionX - 1;

                flipSprite(positionX);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            isAirborne = true;

            if (isAirborne == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    source.PlayOneShot(jumpSound, 1.0f);

                }
            }
            
        }
    }

    void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");



            movement();

        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);

        source.PlayOneShot(springSound, 1.0f);
    }

    
}


