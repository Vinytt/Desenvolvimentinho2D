using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //namespace necessário para mexer no áudio

//Nesse script criaremos uma classe chamada Som, que servirá para tocarmos os efeitos sonoros mais facilmente

[System.Serializable] //Faz com que essa classe possa aparecer na interface da Unity
public class Som
{
    public AudioClip Clipe; //Cada objeto da classe Som tem um AudioClip

    public string Nome; //Cada objeto da classe Som terá um Nome

    [Range(0, 1f)]
    public float Volume; //Volume do Clipe de Áudio

    [Range(-3f, 3f)]
    public float Altura; //Altura do Clipe de Áudio

    [Range(0, 1f)]
    public float SpatialBlend; //O quanto o som é afetado pela sua posição no espaço

    public GameObject ObjetoFonte; //Objeto que será a fonte do Som

    [HideInInspector] //Esconde a variável "Fonte" no inspetor da Unity
    public AudioSource FonteAudio; //Essa variável ainda deve ser public para que possa ser acessada por outros scripts
                                   //Serve para criar uma Audio Source, que necessária para tocar o Áudio

    public float DistanciaMinima; //Distância a partir do qual o som para de aumentar conforme chega-se perto da fonte
    public float DistanciaMaxima; //Distância a partir da qual para-se de ouvir o som
}
