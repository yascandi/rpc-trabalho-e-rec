using Photon.Pun;
using UnityEngine;

public class TankCannon : MonoBehaviourPun, IShootable
{
    public Transform LocalizacaoSaidaBala;  // onde a bala vai sair do tanque

    // tempo que o tanque leva para carregar e atirar
    public float frequenciaBala = 1f;
    public float frequenciaBalaAtual = 0f;
}

