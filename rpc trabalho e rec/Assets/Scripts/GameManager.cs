using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    private IEnumerator TimerCoroutine() // co-rotina que conta e atualiza o cronômetro na tela
    {
        // enquanto o tempo não acabar e o jogo não finalizar, espera 1 segundo e atualiza a interface
        while (tempoDePartidaAtual > 0 && !ehGameOver)
        {
            yield return new WaitForSeconds(1f); // espera 1 segundo

            tempoDePartidaAtual -= 1f; // diminui o tempo em 1 segundo

            AtualizarTimerUI(); // atualiza o tempo na UI
        }

        if (tempoDePartidaAtual <= 0 && !ehGameOver) // se o tempo acabou e o jogo não finalizou, termina a partida
        {
            if (PhotonNetwork.IsMasterClient) // se eu sou o host, termina a partida
            {
                // finaliza o jogo e avisa todos com RPC que a partida acabou
                photonView.RPC("TerminarJogo", RpcTarget.All);
            }

            StopCoroutine(TimerCoroutine()); // para o contador de tempo
        }
    }

    [PunRPC]
    public void TerminarJogo() // método que finaliza o jogo
    {
        ehGameOver = true; // marca o jogo como finalizado

        FindObjectsByType<TankController>(FindObjectsSortMode.None).ToList().ForEach(tanque =>
        {
            if (tanque.photonView.IsMine)
            {
                PhotonNetwork.Destroy(tanque.gameObject); // destrói o tanque
            }
        });

        if (PhotonNetwork.IsMasterClient)
        {
            FindFirstObjectByType<LobbyUIManager>().MostrarResultados();
        }
    }

    void AtualizarTimerUI() // método que atualiza o cronômetro na UI
    {
        int minutos = Mathf.FloorToInt(tempoDePartidaAtual / 60);
        int segundos = Mathf.FloorToInt(tempoDePartidaAtual % 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}

