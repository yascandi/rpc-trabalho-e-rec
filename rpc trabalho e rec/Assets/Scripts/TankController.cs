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
}

