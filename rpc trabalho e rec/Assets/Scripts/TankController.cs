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
}

