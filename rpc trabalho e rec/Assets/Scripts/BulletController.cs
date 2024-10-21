using Photon.Pun;
using System.Data.Common;
using UnityEngine;

public class BulletController : MonoBehaviourPun
{
    // velocidade que a bala vai se mover
    public float velocidade = 5f;

    // quanto tempo at� a bala se destruir sozinha
    public float tempoDeVida = 3f;
    float tempoDeVidaAtual = 0f;

    // refer�ncia para o tanque que atirou
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
            AutoDestruir(); // quando o tempo acabar, destr�i a bala
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // se a bala colidir com quem atirou, n�o faz nada
        if (atirador == collision.gameObject)
        {
            Debug.Log("A bala passou pelo pr�prio atirador");
            return;
        }

        // v� se o objeto com quem a bala bateu pode levar dano
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
        // apenas quem � dono da bala ou o host da partida pode destruir ela
        if (photonView.IsMine)
        {
            // destr�i a bala
            PhotonNetwork.Destroy(gameObject);
        }
        else if (PhotonNetwork.IsMasterClient)
        {
            // o host tamb�m pode destruir a bala
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
