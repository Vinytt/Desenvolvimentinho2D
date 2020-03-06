using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadilha : MonoBehaviour
{
    public GameObject GerenciadorJogo;

    public GameObject Espinhos;

    public int Dano; //Dano dado a quem tocar

    //Sempre que alguémm tocar na armadilha:
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<SofrerDano>() != null)
        {
            other.gameObject.GetComponent<SofrerDano>().DanoTomado(Dano, Espinhos);
        }
    }
}
