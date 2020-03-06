using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarJoia : MonoBehaviour
{
    public Animator AnimadorJoia;

    public AnimationClip ColetandoJoia;

    public GameObject GerenciadorJogo;

    public GameObject Joia;

    //A função OnTriggerEnter é chamada quando o Box Collider do objeto entra na área de outro
    //Não seria possível detectar a colisão (OnCollisionEnter) pois Love Foxxx e a Joia não colidem
    //(Box Collider da Joia is trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LoveFoxxx"))
        {
            AnimadorJoia.Play("JoiaColetada"); //Animação da Joia sendo coletada

            GerenciadorJogo.GetComponent<Pontuacao>().PegarJoia(); //Chama a função para atualizar a pontuação

            Destroy(Joia,ColetandoJoia.length); //Destrói a Joia depois de passado o clipe dela sendo coletada
        }
    }
}
