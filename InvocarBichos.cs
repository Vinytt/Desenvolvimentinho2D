using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocarBichos : MonoBehaviour
{
    public GameObject GambaPrefab;
    public Vector3 PosGamba;

    public GameObject LoveFoxxxPrefab;
    public Vector3 PosLove;

    public GameObject LoveFoxxx;

    public GameObject GerenciadorJogo;
    public GameObject GerenciadorAudio;
    public GameObject CameraPrincipal;
    public GameObject[] Cerejas;
    public GameObject[] Joias;

    public int NumeroSEFXLoveFoxxx;

    // Start is called before the first frame update
    public void Awake()
    {
        GameObject Gambazito = Instantiate(GambaPrefab) as GameObject;
        Gambazito.transform.position = PosGamba;
        Gambazito.GetComponent<SofrerDano>().GerenciadorAudio = GerenciadorAudio;
        Gambazito.GetComponent<SofrerDano>().GerenciadorJogo = GerenciadorJogo;

        //Ao invocar LoveFoxxx
        LoveFoxxx = Instantiate(LoveFoxxxPrefab) as GameObject;
        LoveFoxxx.transform.position = PosLove;

        //Coloca ela nas variáveis necessárias e coloca nela as variáveis necessárias
        GerenciadorJogo.GetComponent<VidaLoveFoxxx>().LoveFoxxx = LoveFoxxx;
        GerenciadorJogo.GetComponent<VidaLoveFoxxx>().AnimadorLoveFoxxx = LoveFoxxx.GetComponent<Animator>();
        GerenciadorJogo.GetComponent<PausarJogo>().LoveFoxxx = LoveFoxxx;

        CameraPrincipal.GetComponent<CameraSeguir>().LoveFoxxx = LoveFoxxx;

        for (int i = 0; i < Cerejas.Length; i++)
        {
            Cerejas[i].GetComponent<PegarCereja>().LoveFoxxx = LoveFoxxx;
        }

        LoveFoxxx.GetComponent<SofrerDano>().GerenciadorJogo = GerenciadorJogo;
        LoveFoxxx.GetComponent<SofrerDano>().GerenciadorAudio = GerenciadorAudio;
        LoveFoxxx.GetComponent<MovimentoLoveFoxxx>().GerenciadorAudio = GerenciadorAudio;
        
        //Os primeiros Efeitos Sonoros devem ser SEMPRE da LoveFoxxx
        for (int i = 0; i < NumeroSEFXLoveFoxxx; i++)
        {
            GerenciadorAudio.GetComponent<Audio>().Soms[i].ObjetoFonte = LoveFoxxx;
        }
    }
}
