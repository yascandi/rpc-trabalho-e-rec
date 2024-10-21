using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class TankController : MonoBehaviourPun, IDamageable
{
    public float velocidadeRotacao = 100f;  // velocidade de rotação do tanque

    public float velocidadeMovimento = 5f; // velocidade de movimento do tanque

    private Rigidbody2D rb;
    private GameManager gm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (gm.ehGameOver)
        {
            return;
        }

        if (photonView.IsMine) // verifica se "sou eu" controlando o tanque
        {
            float moverHorizonalmente = Input.GetAxis("Horizontal"); // pega o comando de rotação(A ou D)
            float moverVerticalmente = Input.GetAxis("Vertical");  // pega o comando de movimento (W ou S)

            MoverTanque(moverHorizonalmente, moverVerticalmente);
        }
    }

    void MoverTanque(float moverHorizonalmente, float moverVerticalmente)
    {
        // movimento do tanque na direção que ele está apontando
        Vector2 movimento = transform.right * moverVerticalmente * velocidadeMovimento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);

        // rotaciona o tanque (A ou D) - move no eixo Z para 2D
        float rotacao = -moverHorizonalmente * velocidadeRotacao * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotacao);
    }

    // método que trata o recebimento de dano
    public void TakeDamage()
    {
        // quando o tanque leva dano, ele é teleportado para a área de respawn; por isso, envia uma mensagem ao dono do tanque para resetar a posição
        photonView.RPC("ResetarPosicaoNoSpawn", photonView.Owner);
    }

    // método para resetar a posição do tanque
    [PunRPC]
    public void ResetarPosicaoNoSpawn()
    {
        // pega a posição do respawn com base no player
        var localizacaoSpawn1 = FindFirstObjectByType<GameManager>().ObterLocalizacaoSpawn(photonView.Owner);

        transform.position = localizacaoSpawn1.transform.position; // define a posição do tanque para o respawn

        transform.rotation = localizacaoSpawn1.transform.rotation; // define a rotação do tanque para o respawn
    }
}

