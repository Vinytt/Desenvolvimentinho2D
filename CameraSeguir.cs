using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public GameObject Camera;

    public GameObject LoveFoxxx;

    public Vector3 Ajuste; //Representa a distância que a câmera deve ficar do jogador

    Vector3 VelocidadeFuncao; //Velocidade de referência a ser usada pela função SmoothDamp

    public float TempoCamera; //Tempo que a camera demorará para chegar até a posição desejada

    // As funções FixedUpdate e LateUpdate funcional tal como a Uptade, mas são executadas imediatamente DEPOIS
    //Usaremos ela aqui pois o movimento do jogador é executado em um Update, e queremos que a câmera o siga após seu
    //movimento. Do Contrário, câmera e jogador "competirão" para se mover primeiro dentro do Update! 
    private void FixedUpdate()
    {
        Vector3 PosicaoDesejada = LoveFoxxx.transform.position + Ajuste; //Posição para a qual a câmera deve ir

        //A função SmoothDamp faz a progressão de um Vetor A até um Vetor B, ideal para o movimento suave da câmera
        //Parâmetros: Vetor de Origem (A), Vetor Destino (B), referenciação a um Vector3 que terá seu valor alterado pela própria função e o tempo que a progressão deve demorar
        Vector3 PosicaoSuavizada = Vector3.SmoothDamp(Camera.transform.position, PosicaoDesejada, ref VelocidadeFuncao, TempoCamera);

        Camera.transform.position = PosicaoSuavizada;
    }
}
