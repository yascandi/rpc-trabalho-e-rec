using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviourPunCallbacks
{
    // Refer�ncia para o bot�o que inicia a partida
    public Button buttonIniciarPartida;

    // Refer�ncia para o bot�o que reinicia a partida
    public Button buttonRecomecarPartida;

    // Refer�ncia para o texto de status na UI
    public Text textStatus;

    // Start � chamado antes do primeiro frame
    void Start()
    {
        // Esconde os bot�es porque o jogo acabou de come�ar
        buttonIniciarPartida.gameObject.SetActive(false);
        buttonRecomecarPartida.gameObject.SetActive(false);

        // Mostra "Carregando..." enquanto o jogo est� iniciando
        textStatus.text = "Carregando...";
    }

    // Atualiza a interface
    public void AtualizarUI()
    {
        // Checa se sou o host da partida (normalmente quem cria a sala)
        if (PhotonNetwork.IsMasterClient)
        {
            // Mostra o bot�o de iniciar a partida, porque s� o host pode fazer isso
            buttonIniciarPartida.gameObject.SetActive(true);

            // Esconde o texto de status da partida
            textStatus.gameObject.SetActive(false);
        }
        else
        {
            // Esconde o bot�o de iniciar a partida, porque s� o host pode
            buttonIniciarPartida.gameObject.SetActive(false);

            // Mostra o texto de status da partida
            textStatus.gameObject.SetActive(true);
            textStatus.text = "Aguardando o dono da sala iniciar a partida";
        }
    }

    // Chamado ao clicar no bot�o de iniciar a partida
    public void OnClickButtonIniciarPartida()
    {
        // Verifica se sou o host e inicia a partida para todos na sala
        if (PhotonNetwork.IsMasterClient)
        {
            // Envia uma mensagem para todos que a partida vai come�ar
            photonView.RPC("IniciarPartidaParaTodos", RpcTarget.All);
        }
    }

    // Chamado ao clicar no bot�o de reiniciar a partida
    public void OnClickButtonRecomecarPartida()
    {
        // Verifica se sou o host e reinicia a partida para todos na sala
        if (PhotonNetwork.IsMasterClient)
        {
            // Envia uma mensagem para todos que a partida vai ser reiniciada
            photonView.RPC("RecomecarPartidaParaTodos", RpcTarget.All);
        }
    }
    [PunRPC]
    public void IniciarPartidaParaTodos()
    {
        // Esconde o texto e o bot�o porque a partida est� prestes a come�ar
        textStatus.gameObject.SetActive(false);
        buttonIniciarPartida.gameObject.SetActive(false);

        // Busca o GameManager e inicia a partida
        var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.IniciarPartida();
    }

    public void MostrarResultados()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Mostra o bot�o de recome�ar a partida
            buttonRecomecarPartida.gameObject.SetActive(true);
        }
    }

    [PunRPC]
    public void RecomecarPartidaParaTodos()
    {
        // Esconde tudo porque a partida vai reiniciar
        textStatus.gameObject.SetActive(false);
        buttonIniciarPartida.gameObject.SetActive(false);
        buttonRecomecarPartida.gameObject.SetActive(false);

        // Busca o GameManager e reinicia a partida
        var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.IniciarPartida();
    }

    // Chamado automaticamente pelo PhotonPun quando o host muda (ex.: se o criador sair da sala)
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // Atualiza a interface
        AtualizarUI();
    }
}

