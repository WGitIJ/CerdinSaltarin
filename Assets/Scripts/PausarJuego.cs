using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;

    private void Start()
    {
        if (menuPausa == null)
        {
            Debug.LogError("PausarJuego: falta asignar 'menuPausa' en el Inspector.", this);
            return;
        }

        menuPausa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        if (menuPausa == null) return;

        menuPausa.SetActive(true);
        Time.timeScale = 0f; // Detiene el tiempo del juego
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        if (menuPausa == null) return;

        menuPausa.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        juegoPausado = false;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
