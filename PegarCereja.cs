using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarCereja : MonoBehaviour
{
    public GameObject LoveFoxxx;

    public GameObject Cereja;

    public Animator AnimadorCereja;

    public AnimationClip ColetandoCereja;

    public GameObject GerenciadorAudio;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LoveFoxxx"))
        {
            AnimadorCereja.Play("JoiaColetada");

            LoveFoxxx.GetComponent<SpriteRenderer>().color = Color.magenta;

            LoveFoxxx.GetComponent<MovimentoLoveFoxxx>().Pulo *= 1.5f;

            Destroy(Cereja, ColetandoCereja.length);

            GerenciadorAudio.GetComponent<Audio>().TocarSom("Comer");
        }
    }
}
