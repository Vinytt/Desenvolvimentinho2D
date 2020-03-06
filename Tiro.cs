using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public GameObject Tirinho;
    public float VelocidadeTiro;

    public int DanoTiro;

    // Start is called before the first frame update
    void Start()
    {
        Tirinho.GetComponent<Rigidbody2D>().velocity = transform.right * VelocidadeTiro;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Se o tiro tocar em alguém que não seja a Love Foxxx mas for capaz de tomar dano:
        if (!other.gameObject.CompareTag("LoveFoxxx") && other.gameObject.GetComponent<SofrerDano>() != null)
        {
            other.GetComponent<SofrerDano>().DanoTomado(-DanoTiro, Tirinho);
        }
    }
}
