using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJogo : MonoBehaviour
{
    bool Pausado; //Retorna true quando o jogo estiver pausado

    public GameObject MenuPausa;

    public GameObject LoveFoxxx;

    // Start is called before the first frame update
    void Start()
    {
        Pausado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pausado)
            {
                Despausar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        Pausado = true;
        MenuPausa.SetActive(true); //Ativa o menu de pausa
        Time.timeScale = 0f; //Congela o tempo

        LoveFoxxx.GetComponent<MovimentoLoveFoxxx>().enabled = false; //Impede Love Foxxx de se mover
    }

    public void Despausar()
    {
        Pausado = false;
        MenuPausa.SetActive(false); //Desativa o menu de pausa
        Time.timeScale = 1f; //Faz o tempo voltar a correr

        LoveFoxxx.GetComponent<MovimentoLoveFoxxx>().enabled = true; //Love Foxxx volta a poder se mover
    }

    //Função para sair do jogo
    public void Sair()
    {
        Application.Quit();
    }
}
