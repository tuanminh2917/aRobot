using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Unity.VisualScripting;



public class PlayerController : MonoBehaviour
{
    private int score = 0; // Player's score
    public TextMeshProUGUI scoreText; // UI Text to display the score

    public Button restartButton;
    public Button resumeButton;
    public Button quitButton;

    public GameObject gameOverBanner; // Reference to the Game Over banner
    public GameObject pauseBanner; // Reference to the Pause banner

    public GameObject obstacleController; // Reference to the obstacle controller

    private Rigidbody rb; // Reference to the Rigidbody component

    private float moveSpeed = 3.5f ; 
    private float jumpForce = 6.0f; // Force applied when jumping

    private bool isGrounded = true; // Check if the player is on the ground

    private bool jumpable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to this GameObject

        restartButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene when the button is clicked
        });
        resumeButton.onClick.AddListener(() => 
        {
            pauseBanner.SetActive(false); // Hide the Pause banner when the button is clicked
            Time.timeScale = 1f; // Resume the game
        });
        quitButton.onClick.AddListener(() => 
        {
            Application.Quit(); // Quit the application when the button is clicked
        });

        gameOverBanner.SetActive(false); // Initially hide the Game Over banner
        pauseBanner.SetActive(false); // Initially hide the Pause banner
        obstacleController.SetActive(true); // Ensure the obstacle controller is active
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hướng di chuyển
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Vị trí mới
        Vector3 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Dịch quả bóng tới vị trí mới
        rb.MovePosition(newPosition);

    }

    void Update()
    {
        // Nhảy (dùng AddForce cho trục Y để mượt)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpable)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpable = false;
            StartCoroutine(EnableJumpAfterDelay(5f));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            pauseBanner.SetActive(true);
            // pause the game
            Time.timeScale = 0f;

        }
        isEndGame(); // Check if the game should end
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        { 
            score += 1; // Increment score when the player collides with an object tagged "Door"
            scoreText.text = "Your Score: " + score; // Update the score text in the UI
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra tiếp đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void isEndGame()
    {
        if (transform.position.y < -10.0f)
        {
            gameObject.SetActive(false); // Deactivate the player GameObject if it falls below a certain height
            obstacleController.SetActive(false); // Deactivate the obstacle controller
            gameOverBanner.SetActive(true); // Show the Game Over banner
        }
    }

    IEnumerator EnableJumpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        jumpable = true;
    }
}
