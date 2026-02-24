using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButtonUI;

    public void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo del juego
        pauseMenuUI.SetActive(true); // Muestra el menú de pausa
        pauseButtonUI.SetActive(false); // Oculta el botón de pausa
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        pauseMenuUI.SetActive(false); // Oculta el menú de pausa
        pauseButtonUI.SetActive(true); // Muestra el botón de pausa
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Asegura que el tiempo del juego esté en su estado normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
