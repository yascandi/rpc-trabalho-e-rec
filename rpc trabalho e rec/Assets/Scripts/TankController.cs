using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class TankController : MonoBehaviourPun, IDamageable
{
    public float velocidadeRotacao = 100f;  // velocidade de rota��o do tanque

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
            float moverHorizonalmente = Input.GetAxis("Horizontal"); // pega o comando de rota��o(A ou D)
            float moverVerticalmente = Input.GetAxis("Vertical");  // pega o comando de movimento (W ou S)

            MoverTanque(moverHorizonalmente, moverVerticalmente);
        }
    }

    void MoverTanque(float moverHorizonalmente, float moverVerticalmente)
    {
        // movimento do tanque na dire��o que ele est� apontando
        Vector2 movimento = transform.right * moverVerticalmente * velocidadeMovimento * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movimento);

        // rotaciona o tanque (A ou D) - move no eixo Z para 2D
        float rotacao = -moverHorizonalmente * velocidadeRotacao * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotacao);
    }

    // m�todo que trata o recebimento de dano
    public void TakeDamage()
    {
        // quando o tanque leva dano, ele � teleportado para a �rea de respawn; por isso, envia uma mensagem ao dono do tanque para resetar a posi��o
        photonView.RPC("ResetarPosicaoNoSpawn", photonView.Owner);
    }

    // m�todo para resetar a posi��o do tanque
    [PunRPC]
    public void ResetarPosicaoNoSpawn()
    {
        // pega a posi��o do respawn com base no player
        var localizacaoSpawn1 = FindFirstObjectByType<GameManager>().ObterLocalizacaoSpawn(photonView.Owner);

        transform.position = localizacaoSpawn1.transform.position; // define a posi��o do tanque para o respawn

        transform.rotation = localizacaoSpawn1.transform.rotation; // define a rota��o do tanque para o respawn
    }
}

