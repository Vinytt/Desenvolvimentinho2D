using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofrerDano : MonoBehaviour
{
    public float VidaTotal;

    public float VidaAtual;

    public GameObject Personagem;

    public Animator AnimadorPersonagem;

    public AnimationClip ClipeDeMorte;

    public GameObject GerenciadorJogo;

    public GameObject GerenciadorAudio;

    bool ValorSprite; //Importante para piscar o sprite de Love Foxxx

    float TempoAtual;

    public float DuracaoRecuo;

    public float ForcaRecuoX;

    public float ForcaRecuoY;

    public bool Morto;

    private void Start()
    {
        ValorSprite = false;
        Morto = false;
    }

    private void FixedUpdate()
    {
        TempoAtual += Time.deltaTime; //Incrementa TempoAtual conforme o tempo passa

        if (TempoAtual > DuracaoRecuo) //Quando o recuo terminar:
        {
            //Se o personagem for a Love Foxxx:
            if (Personagem.GetComponent<MovimentoLoveFoxxx>() != null)
            {
                Personagem.GetComponent<MovimentoLoveFoxxx>().Recuando = false; //Volta a poder se mover
                Physics2D.IgnoreLayerCollision(9, 11, false); //Love Foxxx volta a poder colidir com inimigos
                CancelInvoke(); //Cancela Piscada
            }

            //Se o personagem for um inimmigo terrestre:
            else if (Personagem.GetComponent<MovimentoInimigoTerrestre>() != null)
            {
                Personagem.GetComponent<MovimentoInimigoTerrestre>().Recuando = false; //Volta a poder se mover
            }
        }

        if (Personagem.transform.position.y < -40f && Morto == false)
        {
            Morto = true;
            Morte();
        }
    }

    //Dá pra usar essa função para curar também!
    public void DanoTomado(int Dano, GameObject Causador)
    {
        //Atualiza a vida
        VidaAtual += Dano;

        //Não deixa a vida passar de seu valor máximo
        if (VidaAtual > VidaTotal)
        {
            VidaAtual = VidaTotal;
        }

        //Morre com menos de 0 de vida
        if (VidaAtual < 0.999 && Morto == false)
        {
            VidaAtual = 0f;
            Morto = true;
            Morte();
        }

        //Se for dano e não cura:
        else if (Dano < 0.0001f)
        {
            TempoAtual = 0; //Começa a contar o tempo de recuo

            //Recuo:
            if (Personagem.transform.position.x < Causador.transform.position.x)
            {
                Personagem.GetComponent<Rigidbody2D>().AddForce(new Vector2(-ForcaRecuoX, ForcaRecuoY));
            }
            else
            {
                Personagem.GetComponent<Rigidbody2D>().AddForce(new Vector2(ForcaRecuoX, ForcaRecuoY));
            }

            //Se o personagem for a Love Foxxx:
            if (Personagem.GetComponent<MovimentoLoveFoxxx>() != null)
            {
                Personagem.GetComponent<MovimentoLoveFoxxx>().Recuando = true; //Impede a Love Foxxx de se mover
                Physics2D.IgnoreLayerCollision(11, 9, true); //Impede Love Foxxx de colidir com inimigos

                GerenciadorAudio.GetComponent<Audio>().TocarSom("Dano");

                Debug.Log("Entru");
                InvokeRepeating("PiscarSprite", 0f, 0.06f); //Pisca a cada 0.03s
            }
            //Se o personagem for um inmigo terrestre:
            else if (Personagem.GetComponent<MovimentoInimigoTerrestre>() != null)
            {
                Personagem.GetComponent<MovimentoInimigoTerrestre>().Recuando = true;
            }
        }
    }

    private void Morte()
    {
        //Se o personagem não for a Love Foxxx:
        if (!Personagem.CompareTag("LoveFoxxx"))
        {
            AnimadorPersonagem.SetBool("Morto", true);
            Destroy(Personagem, ClipeDeMorte.length);
        }
        //Se o personagem for a Love Foxxx:
        else
        {
            GerenciadorJogo.GetComponent<VidaLoveFoxxx>().Morte();
        }
    }

    //Função para piscar o sprite (Apenas para a Love Foxxx!)
    public void PiscarSprite()
    {
        Debug.Log("Piscou");
        Personagem.GetComponent<SpriteRenderer>().enabled = ValorSprite;
        ValorSprite = !ValorSprite;
    }
}
