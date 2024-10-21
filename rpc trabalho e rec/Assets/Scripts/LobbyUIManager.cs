using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviourPunCallbacks
{
    // Referência para o botão que inicia a partida
    public Button buttonIniciarPartida;

    // Referência para o botão que reinicia a partida
    public Button buttonRecomecarPartida;

    // Referência para o texto de status na UI
    public Text textStatus;

    // Start é chamado antes do primeiro frame
    void Start()
    {
        // Esconde os botões porque o jogo acabou de começar
        buttonIniciarPartida.gameObject.SetActive(false);
        buttonRecomecarPartida.gameObject.SetActive(false);

        // Mostra "Carregando..." enquanto o jogo está iniciando
        textStatus.text = "Carregando...";
    }

    // Atualiza a interface
}

