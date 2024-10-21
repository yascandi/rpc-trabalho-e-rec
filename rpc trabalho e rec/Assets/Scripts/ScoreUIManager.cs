using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviourPunCallbacks
{
    public Text textPontuacao;  // referência do texto que mostra a pontuação na UI
    public int actorNumber; // número do "Actor" do Photon PUN para saber de quem é essa UI

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)  // chamado automaticamente pelo PhotonPun quando alguém muda alguma propriedade
    {
        if (changedProps.ContainsKey("Pontuacao") && targetPlayer.ActorNumber == actorNumber)  // verifica se a propriedade "Pontuacao" mudou e se precisamos atualizar a UI
        {
            int newScore = (int)changedProps["Pontuacao"]; // pega a nova pontuação

            textPontuacao.text = newScore.ToString();  // atualiza o texto da UI com a nova pontuação
        }
    }
}
