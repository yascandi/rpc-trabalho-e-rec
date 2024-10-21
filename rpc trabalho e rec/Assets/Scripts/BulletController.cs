using Photon.Pun;
using System.Data.Common;
using UnityEngine;

public class BulletController : MonoBehaviourPun
{
    // Velocidade que a bala vai se mover
    public float velocidade = 5f;

    // Quanto tempo até a bala se destruir sozinha
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    // Referência para o tanque que atirou
    private GameObject atirador;

    // Inicializa a bala e diz quem foi que atirou ela
    public void Inicializar(GameObject atirador)
    {
        this.atirador = atirador;
    }

    void Update()
    {
        // Faz a bala se mover para frente
        MoveBala();

        // Conta o tempo que a bala está "viva"
        ContabilizarTempoDeVida();
    }


    void MoveBala()
    {
        // Move a bala
        transform.Translate(Vector3.right * velocidade * Time.deltaTime);
    }

    void ContabilizarTempoDeVida()
    {
        // Atualiza o tempo de vida da bala
        tempoDeVidaAtual += Time.deltaTime;
        if (tempoDeVidaAtual > tempoDeVida)
        {
            // Quando o tempo acabar, destrói a bala
            AutoDestruir();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Se a bala colidir com quem atirou, não faz nada
        if (atirador == collision.gameObject)
        {
            Debug.Log("A bala passou pelo próprio atirador");
            return;
        }

        // Vê se o objeto com quem a bala bateu pode levar dano
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Aplica o dano ao objeto que pode receber dano
            damageable.ReceberDano();

            // Se a bala for "minha" (do jogador atual), adiciona um ponto ao acertar
            if (photonView.IsMine)
            {
                // Adiciona ponto para o jogador
                FindObjectOfType<PontuacaoManager>().AdicionarPontuacao(PhotonNetwork.LocalPlayer);
            }
        }
        AutoDestruir();
    }

    void AutoDestruir()
    {
        // Apenas quem é dono da bala ou o host da partida pode destruir ela
        if (photonView.IsMine)
        {
            // Destrói a bala
            PhotonNetwork.Destroy(gameObject);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            // O host também pode destruir a bala
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
