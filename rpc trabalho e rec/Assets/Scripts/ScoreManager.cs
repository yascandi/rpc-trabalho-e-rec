using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class PontuacaoManager : MonoBehaviourPunCallbacks
{
    public void AdicionarPontuacao(Player player) // adiciona 1 ponto ao jogador
    {
        int pontuacaoAtual = 0; // pega a pontuação atual do jogador, se existir
        if (player.CustomProperties.ContainsKey("Pontuacao"))
        {
            pontuacaoAtual = (int)player.CustomProperties["Pontuacao"];
        }
        pontuacaoAtual += 1; // aumenta a pontuação em 1

        // atualiza a pontuação no PhotonPun e avisa todo mundo, isso vai chamar o método OnPlayerPropertiesUpdate na classe PontuacaoUIController
        Hashtable propriedadePontuacao = new Hashtable();
        propriedadePontuacao["Pontuacao"] = pontuacaoAtual;
        player.SetCustomProperties(propriedadePontuacao);
    }
}

