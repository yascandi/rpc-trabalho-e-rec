using Photon.Pun;
using System.Data.Common;
using UnityEngine;

public class BulletController : MonoBehaviourPun
{
    // velocidade que a bala vai se mover
    public float velocidade = 5f;

    // quanto tempo até a bala se destruir sozinha
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    // referência para o tanque que atirou
    private GameObject atirador;

    // inicializa a bala e diz quem foi que atirou ela
    public void Inicializar(GameObject atirador)
    {
        this.atirador = atirador;
    }

    void Update()
    {
        transform.Translate(Vector3.right * velocidade * Time.deltaTime); // movimenta a bala
       
        tempoDeVidaAtual += Time.deltaTime;  // atualiza o tempo de vida da bala
        if (tempoDeVidaAtual > tempoDeVida)
        {
            AutoDestruir(); // quando o tempo acabar, destrói a bala
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // se a bala colidir com quem atirou, não faz nada
        if (atirador == collision.gameObject)
        {
            Debug.Log("A bala passou pelo próprio atirador");
            return;
        }

        // vê se o objeto com quem a bala bateu pode levar dano
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // aplica o dano ao objeto que pode receber dano
            damageable.TakeDamage();

            // se a bala for "minha" (do jogador atual), adiciona um ponto ao acertar
            if (photonView.IsMine)
            {
                // adiciona ponto para o jogador
                FindObjectOfType<ScoreManager>().AdicionarPontuacao(PhotonNetwork.LocalPlayer);
            }
        }
        AutoDestruir();
    }

    void AutoDestruir()
    {
        // apenas quem é dono da bala ou o host da partida pode destruir ela
        if (photonView.IsMine)
        {
            // destrói a bala
            PhotonNetwork.Destroy(gameObject);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            // o host também pode destruir a bala
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
