using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //Necessário para mexer nos áudios
using System; //Necessário para usar a função Array.find()

public class Audio : MonoBehaviour
{

    //Para trabalharmos com os áudios não compensa fazermos uma função para cada som. É mais fácil fazer uma função
    //universal que procure cada som e o toque

    public Som[] Soms; //Cria um vetor de objetos da classe "Som" chamado "Soms"

    private void Start() //A função Awake funciona tal como a Start, porém é chamada antes
    {


        foreach (Som s in Soms) //para cada objeto da classe Som no vetor Soms:
        {
            s.FonteAudio = s.ObjetoFonte.AddComponent<AudioSource>(); //Damos a ele uma AudioSource...
            
            s.FonteAudio.clip = s.Clipe; //que tocará o clipe imbutido no objeto

            s.FonteAudio.spatialBlend = s.SpatialBlend; //e tenha a spatial blend também imbutida no objeto

            s.FonteAudio.rolloffMode = AudioRolloffMode.Linear; //Faz com que os Soms diminuam caso estejam distantes da
                                                                //fonte

            //Aqui preenchemos mais informaçoes da AudioSource com as já imbutidas no objeto da classe Som
            //(Pois não podemos fazer isso diretamente)
            s.FonteAudio.volume = s.Volume;

            s.FonteAudio.pitch = s.Altura;

            s.FonteAudio.maxDistance = s.DistanciaMaxima;

            s.FonteAudio.minDistance = s.DistanciaMinima;
        }
        
    }

    //Função universal para tocar Soms
    public void TocarSom(string Nome)
    {
        Som s = Array.Find(Soms, som => som.Nome == Nome); //o Som s recebe o Som cujo nome é igual ao nome pedido
        s.FonteAudio.Play();
    }
}
