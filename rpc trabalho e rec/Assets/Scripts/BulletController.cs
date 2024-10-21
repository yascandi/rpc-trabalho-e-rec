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
}
