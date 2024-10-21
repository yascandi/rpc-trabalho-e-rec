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
        frequenciaBalaAtual = 0f;  // reseta o tempo de carregamento, pois o tanque precisa recarregar para o pr�ximo tiro

        // dispara a bala e cria uma inst�ncia na rede
        var bala = PhotonNetwork.Instantiate("BalaPrefab", LocalizacaoSaidaBala.transform.position, LocalizacaoSaidaBala.transform.rotation);
        bala.GetComponent<BulletController>().Inicializar(GetComponentInParent<TankController>().gameObject);
    }

    void Update()
    {
        frequenciaBalaAtual += Time.deltaTime;  // conta o tempo de carregamento da bala

        if (photonView.IsMine) // verifica se "sou eu" quem est� atirando
        {
            // checa se a tecla de espa�o foi pressionada e se o tanque j� est� pronto para atirar
            if (Input.GetKeyDown(KeyCode.Space) && frequenciaBalaAtual > frequenciaBala)
            {
                Shoot(); // chama o m�todo para atirar a bala
            }
        }
    }
}

