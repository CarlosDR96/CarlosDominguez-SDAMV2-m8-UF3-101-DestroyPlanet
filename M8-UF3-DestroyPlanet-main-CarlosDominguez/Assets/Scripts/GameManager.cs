using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;

    public TMP_Text lbl_Score;
    public TMP_Text lbl_Lives;
    public TMP_Text maxScore;
    public TMP_Text maxScoreMenu;
    public TMP_Text yourScore;
    public TMP_Text gameOverText;
    public AudioSource desactivated;
    public AudioSource gameOver;
    public AudioSource youWin;
    public Canvas mainMenuCanvas;
    public Canvas livesCanvas;
    public Canvas gameOverCanvas;
    public int bestScore;
    // Start is called before the first frame update
    void Start()
    {
        // Cargar el mejor puntaje almacenado en PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        maxScoreMenu.text = "Max score: " + bestScore;
        livesCanvas.gameObject.SetActive(false);
        // Al inicio del juego, el menú principal está activo
        mainMenuCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f; // Pausa el juego mientras el menú está activo
        
    }
    // Función llamada cuando se presiona el botón de empezar en el menú principal
    public void StartGame()
    {
        // Desactiva el menú principal
        mainMenuCanvas.gameObject.SetActive(false);
        livesCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f; // Reanuda el tiempo para iniciar el juego
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore()
    {
        desactivated.Play();
        score++;
        lbl_Score.text = "Score: " + score;
        
    }
    
    public void TakeDamage()
    {
        lives--;
        lbl_Lives.text = "Lives: " + lives;
        if (lives < 0)
        {
            if (score > bestScore)
            {
                bestScore = score;
                // Guardar el mejor puntaje en PlayerPrefs
                PlayerPrefs.SetInt("BestScore", bestScore);
                PlayerPrefs.Save(); // Guardar los cambios en PlayerPrefs
                maxScore.text = "Record Score: " + bestScore;
                yourScore.text = "Your Score: " + score;
                gameOverText.text = "NEW RECORD!!";
                youWin.Play();
            }
            else
            {
                maxScore.text = "Record Score: " + bestScore;
                yourScore.text = "Your Score: " + score;
                gameOverText.text = "GAME OVER";
                gameOver.Play();
            }
            gameOverCanvas.gameObject.SetActive(true);
            livesCanvas.gameObject.SetActive(false);
            Time.timeScale = 0f; // Pausa el juego mientras el menú está activo
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
