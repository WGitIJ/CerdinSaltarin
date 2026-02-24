using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public float speed = 5.0f;
  private Rigidbody2D rb;

  private float moveInput;

  public float jumpForce = 4.0f;
  private bool isGrounded; // Esta variable se usa para verificar si el jugador está en el suelo
  public Transform groundCheck; // Punto desde donde se verifica el suelo
  public float groundRadius = 0.1f; // Radio del círculo para verificar el suelo
  public LayerMask groundLayer;

  private Animator animator;

  private int coins;
  public TMP_Text textCoins;

  public AudioClip coinSound;
  public AudioClip barrelSound;
  private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        UpdateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if(moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded); 
    }

    void FixedUpdate()
    {
        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        isGrounded = touchingGround && rb.linearVelocity.y <= 0.01f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinSound);
            Destroy(collision.gameObject);
            coins++;
            textCoins.text = coins.ToString();
            UpdateCoinText();
        }

        if(collision.transform.CompareTag("Trophie"))
        {
            LoadNextScene();
        }


        if(collision.transform.CompareTag("Spike"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(collision.transform.CompareTag("Barrel"))
        {
            audioSource.PlayOneShot(barrelSound);
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = Vector2.zero; // Detener el movimiento actual del jugador
            rb.AddForce(knockbackDirection * 5.0f, ForceMode2D.Impulse);

            BoxCollider2D[] colliders = collision.gameObject.GetComponents<BoxCollider2D>();

            foreach(BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }

            collision.GetComponent<Animator>().enabled = true;
            Destroy(collision.gameObject, 0.5f);
        }
    }

    private void UpdateCoinText()
    {
        if(textCoins != null)
        {
            textCoins.text = coins.ToString();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            return;
        }

        SceneManager.LoadScene(0);
    }
}
