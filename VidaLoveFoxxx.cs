using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaLoveFoxxx : MonoBehaviour
{

    public GameObject BarraDeVida;

    public GameObject LoveFoxxx;

    public GameObject TelaDeMorte;

    public GameObject GerenciadorJogo;

    public GameObject GerenciadorAudio;

    public float VidaAtual;

    public float VidaTotal;

    public Animator AnimadorLoveFoxxx;

    public GameObject CameraPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        AnimadorLoveFoxxx.SetBool("Morta", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LoveFoxxx = GerenciadorJogo.GetComponent<InvocarBichos>().LoveFoxxx;

        VidaAtual = LoveFoxxx.GetComponent<SofrerDano>().VidaAtual;

        //Modifica o tamanho da Barra de Vida atual com base no quanto de vida resta
        BarraDeVida.GetComponent<RectTransform>().localScale = new Vector3 (VidaAtual / VidaTotal, 1f, 1f);
    }

    //Função para quando Love Foxxx falecer
    public void Morte()
    {
        VidaAtual = 0;
        BarraDeVida.GetComponent<RectTransform>().localScale = new Vector3(0, 1f, 1f); //Zera a barra de vida
        TelaDeMorte.SetActive(true); //Ativa tela de morte
        GerenciadorJogo.GetComponent<PausarJogo>().enabled = false; //Impede de pausar o jogo depois da morte
        LoveFoxxx.GetComponent<MovimentoLoveFoxxx>().enabled = false; //Impede de mover Love Foxxx depois da morte
        LoveFoxxx.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200f)); //Impulsiona Love Foxxx pra cima
        LoveFoxxx.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        LoveFoxxx.GetComponent<Rigidbody2D>().gravityScale = 2f; //Cai mais rápido
        CameraPrincipal.GetComponent<CameraSeguir>().enabled = false; //Faz a Câmera parar de Seguir
        LoveFoxxx.GetComponent<BoxCollider2D>().enabled = false; //Desativa a caixa de colisão
        AnimadorLoveFoxxx.SetBool("Morta", true); //Ativa animação de morte
    }
}
