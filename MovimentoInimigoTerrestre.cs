using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigoTerrestre : MonoBehaviour
{
    public GameObject Inimigo;

    public int Dano;

    public float DuracaoMovimento;

    public float TempoAtual;

    public float Velocidade;

    bool OlhandoE;

    public Animator AnimadorGamba;

    public bool Recuando;

    // Start is called before the first frame update
    void Start()
    {
        TempoAtual = 0;

        OlhandoE = true;

        Recuando = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Recuando)
        {
            //Movimento
            Inimigo.transform.position += new Vector3(Velocidade * Time.deltaTime, 0, 0);

            TempoAtual += Time.deltaTime;

            //Muda direção do movimento
            if (TempoAtual > DuracaoMovimento)
            {
                //Troca Direção do Movimento
                TempoAtual = 0;
                Velocidade = -Velocidade;

                if (OlhandoE)
                {
                    OlhandoE = false;

                    Virar();
                }
                else
                {
                    OlhandoE = true;

                    Virar();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LoveFoxxx"))
        {
            foreach (ContactPoint2D PontoContato in other.contacts) //Para cada ponto de Contato do Gambá com a Love Foxxx:
            {
                if (PontoContato.normal.y < -0.9f) //Se o vetor normal ao ponto de colisão em relação à caixa de colisão
                                                  //tiver coordenada Y igual a -1, ele estará apontando para baixo. Isso
                                                  //indica que o Love Foxxx caiu em cima do Gambá
                {
                    //Causa dano no inimigo
                    Inimigo.GetComponent<SofrerDano>().DanoTomado(-20, other.gameObject);

                    //Impulsiona Love Foxxx para cima
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, 100f));
                }
                //Causar dano em Love Foxxx:
                else if (!other.gameObject.GetComponent<MovimentoLoveFoxxx>().Recuando)
                {
                    other.gameObject.GetComponent<SofrerDano>().DanoTomado(-Dano, Inimigo);
                }
            }  
        }
    }

    private void Virar()
    {
        Inimigo.transform.Rotate(0f, 180f, 0f);
    }
}
