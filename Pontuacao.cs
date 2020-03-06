using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    public Text TextoPontuacao;

    public int Pontos;

    // Update is called once per frame
    void Update()
    {
        TextoPontuacao.text = Pontos.ToString();
    }

    public void PegarJoia()
    {
        Pontos += 1;
    }
}
