using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoLoveFoxxx : MonoBehaviour
{
    public GameObject LoveFoxxx; //Usado para se referir à Love Foxxx

    public float Velocidade; //Velocidade de movimento da Love Foxxx

    public Vector2 Pulo; //Força do Pulo da Love Foxxx

    bool NoChao; //Retorna true quando LoveFoxxx estiver no chão

    int MascaraChao = 1 << 8; //Essa Layer Mask seleciona apenas a layer 8 (Chao), usada para identificar quando Love Foxxx está no chão

    bool rcastChaoE, rcastChaoM, rcastChaoD; //Booleanos que representam os raycasts que verificarão se Love Foxxx está no chão

    int MascaraObstaculos = 1 << 9 | 1 << 10 | 1 << 11; //Essa Layer Mask seleciona as layers 9 (LoveFoxxx), 10 (itens) e 11 (inimigos)

    bool ObstaculoE, rcastOE1, rcastOE2; //Retorna true quando há um obstáculo à esquerda de Love Foxxx

    bool ObstaculoD, rcastOD1, rcastOD2; //Retorna true quando há um obstáculo à direita de Love Foxxx

    public bool OlhandoE; //Retorna true quando Love Foxxx está olhando para a Esquerda

    public Animator AnimadorLF; //Animador da Love Foxxx

    Vector3 PosLoveFoxxx; //Guarda a posição da Love Foxxx, importante para a animação!

    bool Agachada; //Retorna true quando Love Foxxx está agachada

    Vector2 CaixaColisao; //Guarda o tamanho da caixa de colisão da Love Foxxx antes de agachar

    Vector2 PosCaixaColisao; //Guarda a posição da caixa de colisão em relação à Love Foxxx antes de ela agachar

    public GameObject GerenciadorAudio; //Gerenciador de Audio

    public bool Recuando; //Retorna true quando Love Foxxx estiver sob ataque

    // Start é chamada antes do primeiro update de frame
    void Start()
    {
        //Detecção de Obstáculos
        MascaraObstaculos = ~MascaraObstaculos; //Inverte a Layer Mask para selecionar todas as layer exceto a 9 e 10

        //Agachar
        Agachada = false;
        CaixaColisao = LoveFoxxx.GetComponent<BoxCollider2D>().size;
        PosCaixaColisao = LoveFoxxx.GetComponent<BoxCollider2D>().offset;

        Recuando = false;
    }

    // Update é chamada uma vez por frame
    void Update()
    {
        //Detecção de Obstáculos
        rcastOD1 = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x, LoveFoxxx.transform.position.y), Vector2.right, 0.7f, MascaraObstaculos);
        rcastOD2 = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x, LoveFoxxx.transform.position.y) - new Vector2(0, 1.3f), Vector2.right, 0.7f, MascaraObstaculos);

        if (rcastOD1 | rcastOD2)
        {
            ObstaculoD = true;
        }
        else
        {
            ObstaculoD = false;
        }
        //Debug.DrawRay(LoveFoxxx.transform.position, Vector3.right * 0.7f, Color.blue);
        //Debug.DrawRay(LoveFoxxx.transform.position - new Vector3(0, 1.3f, 0), Vector3.right * 0.7f, Color.yellow);

        rcastOE1 = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x, LoveFoxxx.transform.position.y), -Vector2.right, 0.7f, MascaraObstaculos);
        rcastOE2 = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x, LoveFoxxx.transform.position.y) - new Vector2(0, 1.3f), -Vector2.right, 0.7f, MascaraObstaculos);

        if (rcastOE1 | rcastOE2)
        {
            ObstaculoE = true;
        }
        else
        {
            ObstaculoE = false;
        }
        //Debug.DrawRay(LoveFoxxx.transform.position, -Vector2.right * 0.7f, Color.red);
        //Debug.DrawRay(LoveFoxxx.transform.position - new Vector3(0, 1.3f,0), -Vector2.right * 0.7f, Color.red);

        //Movimentação Horizontal

        //Guarda a Posição da Love Foxxx nesse frame (animação!)
        PosLoveFoxxx = LoveFoxxx.transform.position;

        if (Input.GetKey("d") && !ObstaculoD && !Recuando) //A função GetKey retorna true enquanto a tecla estiver pressionada
        {
            LoveFoxxx.transform.position += new Vector3(Velocidade * Time.deltaTime, 0, 0); //A posição da LoveFoxx é alterada a cada frame

            //Animaçao de Corrida
            if (NoChao)
            {
                AnimadorLF.SetBool("Correndo", true); //Ativa a animação de correr
            }

            //OBS: A mudança de direção não pode ser feita diretamente, por isso usamos o vetor Direcao
            if (OlhandoE)
            {
                Virar();
                OlhandoE = false;
            }
        }

        if (Input.GetKey("a") && !ObstaculoE  && !Recuando) //OBS: A letra que representa a tecla (parâmetro de GetKey) tem que ser minúscula
        {
            LoveFoxxx.transform.position += new Vector3(-Velocidade * Time.deltaTime, 0, 0);

            //Animação de Corrida
            if (NoChao)
            {
                AnimadorLF.SetBool("Correndo", true);
            }

            if (!OlhandoE)
            {
                Virar();
                OlhandoE = true;
            }
        }

        //NoChao (define quando Love Foxxx está no chão ou não)

        //Criamos Raycasts que estão limitado pela Layer Mask "MascaraChao", e portanto só retornarão true quando
        //colidirem com um Collider de um GameObject cuja a camada seja "Chao". Caso isso ocorra, saberemos que
        //Love Foxxx estará no chão

        //Raycast do Meio
        rcastChaoM = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x, LoveFoxxx.transform.position.y), Vector2.down, 1.5f, MascaraChao);
        //Raycast da Esquerda
        rcastChaoE = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x - 0.6f, LoveFoxxx.transform.position.y), Vector2.down, 1.5f, MascaraChao);
        //Raycast da Direita
        rcastChaoD = Physics2D.Raycast(new Vector2(LoveFoxxx.transform.position.x + 0.5f, LoveFoxxx.transform.position.y), Vector2.down, 1.5f, MascaraChao);
        //Os parâmetros do Raycast são: origem (Vector2), direção (Vector2), tamanho, Layer Mask

        //Se um dos Raycasts estiver tocando o chão, então Love Foxxx está no chão
        if (rcastChaoD | rcastChaoM | rcastChaoE)
        {
            NoChao = true;
            AnimadorLF.SetBool("Caindo", false);
            AnimadorLF.SetBool("NoChao", true);
        }
        else
        {
            NoChao = false;
        }

        //Pulo (só deve ser chamada quando Love Foxxx não estiver no ar!)

        if (Input.GetKeyDown("w") && NoChao && !Agachada && !Recuando) //A função GetKeyDown retorna true apenas no frame que a tecla foi pressionada
        {
            //Diferentemente da Movimentação Horizontal, o Pulo não pode ser implementado mudando a posição da Love Foxxx
            //pois isso apenas a transportaria para cima
            //Assim, devemos dar uma velocidade a ela, usando seu componente de física (Rigidbody)
            //Isso pode ser feito de duas maneiras:

            //Adicionando uma velocidade ao Rigidbody:
            //Lovefoxxx.GetComponent<Rigidbody2D>().velocity = new Vector3(0, Pulo * Time.deltaTime, 0);
            //Porém esse método torna o pulo menos realista

            //Adicionando uma FORÇA ao Rigidbody:
            LoveFoxxx.GetComponent<Rigidbody2D>().AddForce(Pulo); //Adiciona uma força do valor de "Pulo"

            //Desfaz o Boost da cereja
            if (Pulo.y > 400f)
            {
                Pulo = new Vector2(2, 400f);
                LoveFoxxx.GetComponent<SpriteRenderer>().color = Color.white;
                GerenciadorAudio.GetComponent<Audio>().TocarSom("Pulo Alto");
            }
            else
            {
                GerenciadorAudio.GetComponent<Audio>().TocarSom("Pulo"); //Toca o som de pulo normal
            }
        }

        //Agachar
        if (Input.GetKey("s") && NoChao && !Agachada)
        {
            Agachada = true;

            AnimadorLF.SetBool("Agachando", true); //Animação de Agachar

            LoveFoxxx.GetComponent<BoxCollider2D>().size = new Vector2(CaixaColisao.x, CaixaColisao.y * 0.5f);
            LoveFoxxx.GetComponent<BoxCollider2D>().offset = new Vector2(PosCaixaColisao.x, -0.11f);
        }

        if (Input.GetKeyUp("s") && Agachada)
        {
            Agachada = false;

            AnimadorLF.SetBool("Agachando", false);

            LoveFoxxx.GetComponent<BoxCollider2D>().size = CaixaColisao;
            LoveFoxxx.GetComponent<BoxCollider2D>().offset = PosCaixaColisao;
        }
    }

    //Aqui verificaremos se a Love Foxxx está em movimento ou parada.
    //Caso ela mantenha a mesma posição do início do frame, será considerada parada, do contrário estará em movimento
    private void LateUpdate()
    {
        //Verifica se há movimento horizontal da Love Foxxx
        if (Mathf.Abs(LoveFoxxx.GetComponent<Rigidbody2D>().velocity.x) < 0.5f)
        {
            AnimadorLF.SetBool("Correndo", false); //Para animação de corrida
        }

        //Verifica se Love Foxxx está caindo ou pulando analisando se existe uma velocidade na vertical
        if (LoveFoxxx.GetComponent<Rigidbody2D>().velocity.y < 0.5f)
        {
            AnimadorLF.SetBool("Caindo", true);
            AnimadorLF.SetBool("Pulando", false);
            AnimadorLF.SetBool("NoChao", false);
        }
        else if (LoveFoxxx.GetComponent<Rigidbody2D>().velocity.y > 0.5f)
        {
            AnimadorLF.SetBool("Pulando", true);
            AnimadorLF.SetBool("Caindo", false);
            AnimadorLF.SetBool("NoChao", false);
        }
    }

    //Função para rodar Love Foxxx
    private void Virar()
    {
        LoveFoxxx.transform.Rotate(0f, 180f, 0f);
    }
}
