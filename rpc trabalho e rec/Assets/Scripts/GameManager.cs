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
    public List<GameObject> localizacoesSpawn;   // referências das localizações onde cada jogador começa
    
    public Text textTimer; // referência ao texto de UI que mostra o cronômetro
    
    public float tempoDePartida = 120f; // tempo de partida em segundos
    private float tempoDePartidaAtual = 0f;

    public bool ehGameOver = false;  // boolean que informa se o jogo acabou

    void Start()
    {
        textTimer.gameObject.SetActive(false);  // inicia como falso, pois o contador não deve aparecer antes de começar a partida
    }

    public void IniciarPartida()  // método que inicia a partida
    {
        ehGameOver = false;
        FindObjectOfType<ScoreManager>().ResetarPontuacao(PhotonNetwork.LocalPlayer);

        tempoDePartidaAtual = tempoDePartida;  // faz o cronômetro aparecer

        textTimer.gameObject.SetActive(true);
        AtualizarTimerUI();

        StartCoroutine(TimerCoroutine()); // inicia uma co-rotina que atualiza o tempo do cronômetro a cada 1 segundo

        var go = ObterLocalizacaoSpawn(PhotonNetwork.LocalPlayer); // pega a localização do jogador para saber onde o tanque deve nascer

        var tanque = PhotonNetwork.Instantiate("TanquePrefab", go.transform.position, go.transform.rotation); // cria o tanque no local certo
    }

    public GameObject ObterLocalizacaoSpawn(Player player)
    {
        var indice = (player.ActorNumber - 1) % localizacoesSpawn.Count;
        return localizacoesSpawn[indice];
    }
}

