using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void IniciarPartida()  // m�todo que inicia a partida
    {
        ehGameOver = false;
        FindObjectOfType<ScoreManager>().ResetarPontuacao(PhotonNetwork.LocalPlayer);

        tempoDePartidaAtual = tempoDePartida;  // faz o cron�metro aparecer

        textTimer.gameObject.SetActive(true);
        AtualizarTimerUI();

        StartCoroutine(TimerCoroutine()); // inicia uma co-rotina que atualiza o tempo do cron�metro a cada 1 segundo

        var go = ObterLocalizacaoSpawn(PhotonNetwork.LocalPlayer); // pega a localiza��o do jogador para saber onde o tanque deve nascer

        var tanque = PhotonNetwork.Instantiate("TanquePrefab", go.transform.position, go.transform.rotation); // cria o tanque no local certo
    }

    public GameObject ObterLocalizacaoSpawn(Player player)
    {
        var indice = (player.ActorNumber - 1) % localizacoesSpawn.Count;
        return localizacoesSpawn[indice];
    }
}

