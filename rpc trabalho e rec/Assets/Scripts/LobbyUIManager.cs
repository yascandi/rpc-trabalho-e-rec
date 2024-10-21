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
}

