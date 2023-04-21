using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlappyBirdController : MonoBehaviour
{
    //Bird Properties
    [Header("Bird Properties")]
    [SerializeField] private Rigidbody bird;
    [SerializeField] private float jumpForce = 300f;

    //Audio Properties
    [Header("Audio Properties")]
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource gameOverAudio;
    [SerializeField] private AudioSource scoreAudio;

    //UI Panels
    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject mainMenuPanel;

    //Score
    [Header("Score")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private int score = 0;
    [SerializeField] private int highScore = 0;

    //Pause
    [Header("Pause")]
    public bool isPaused;

    public bool gameOver;



    //Start
    void Awake()
    {
        Time.timeScale = 0f;
        isPaused = true;
        gameOver = false;
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        scoreText.text = "Score: 0";

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

    }

    //Controls
    void Update()
    {
        Controls();
    }

    private void Controls()
    {
        if (isPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
            {
                bird.AddForce(Vector3.up * jumpForce);
                jumpAudio.Play();
            }

            if (Input.GetMouseButtonDown(0) && !gameOver)
            {
                bird.AddForce(Vector3.up * jumpForce);
                jumpAudio.Play();
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !gameOver)
            {
                bird.AddForce(Vector3.up * jumpForce);
                jumpAudio.Play();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && (isPaused == false) && (gameOver == false))
            {
                PauseGame();
            }
        }
    }

    //Death
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameOver = true;
            gameOverAudio.Play();
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    //Scoring
    public void IncreaseScore()
    {
        scoreAudio.Play();
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    //Pause
    private void PauseResume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //UI Controls
    private void StartGame()
    {
        scoreText.text = "Score: 0";
        pauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
               
    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}