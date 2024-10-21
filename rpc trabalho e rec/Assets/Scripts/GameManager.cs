using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> localizacoesSpawn;   // refer�ncias das localiza��es onde cada jogador come�a
    
    public Text textTimer; // refer�ncia ao texto de UI que mostra o cron�metro
    
    public float tempoDePartida = 120f; // tempo de partida em segundos
    private float tempoDePartidaAtual = 0f;

    public bool ehGameOver = false;  // boolean que informa se o jogo acabou

    void Start()
    {
        textTimer.gameObject.SetActive(false);  // inicia como falso, pois o contador n�o deve aparecer antes de come�ar a partida
    }

   
   
}

