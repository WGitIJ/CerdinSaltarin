using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

     public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
