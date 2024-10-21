using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // conecta no servidor Photon com o AppId configurado
    }

    public override void OnConnectedToMaster()  // callback chamado quando a conexão com o servidor é bem-sucedida
    {
        Debug.Log("Conectado no servidor photon.");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()  // chamado quando entra no lobby, após o OnConnectedToMaster
    {
        Debug.Log("Executou OnJoinedLobby");

        PhotonNetwork.JoinOrCreateRoom("PanzerWars", new Photon.Realtime.RoomOptions { MaxPlayers = 2 }, TypedLobby.Default); // cria ou entra na sala chamada "PanzerWars"
    }

    public override void OnJoinedRoom() // chamado quando entra na sala, após o OnJoinedLobby
    {
        Debug.Log("Executou OnJoinedRoom");

        var lobbyUIManager = FindFirstObjectByType<LobbyUIManager>();  // atualiza a interface informando que o jogador entrou na sala
        lobbyUIManager.AtualizarUI();
    }
}

