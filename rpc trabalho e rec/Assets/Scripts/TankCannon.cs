using Photon.Pun;
using UnityEngine;

public class TankCannon : MonoBehaviourPun, IShootable
{
    public Transform LocalizacaoSaidaBala;  // onde a bala vai sair do tanque

    // tempo que o tanque leva para carregar e atirar
    public float frequenciaBala = 1f;
    public float frequenciaBalaAtual = 0f;

    public void Shoot()
    {
        frequenciaBalaAtual = 0f;  // reseta o tempo de carregamento, pois o tanque precisa recarregar para o próximo tiro

        // dispara a bala e cria uma instância na rede
        var bala = PhotonNetwork.Instantiate("BalaPrefab", LocalizacaoSaidaBala.transform.position, LocalizacaoSaidaBala.transform.rotation);
        bala.GetComponent<BulletController>().Inicializar(GetComponentInParent<TankController>().gameObject);
    }
}

