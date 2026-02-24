using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado;

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
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        if (menuPausa == null) return;

        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
