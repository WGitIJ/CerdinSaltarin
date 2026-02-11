using UnityEngine;

public class QUitGame : MonoBehaviour
{
   public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
