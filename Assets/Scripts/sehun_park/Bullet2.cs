using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Bullet2 : MonoBehaviourPun
{
    public float moveSpeed = 10;

    // RigidBody
    Rigidbody rb;

    // �浹 �Ǿ��� �� ȿ�� Prefab
    public GameObject exploFactory;

    public AudioClip explosionSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            // �÷��̾�� �浹�ߴ��� Ȯ��
            if (other.CompareTag("Player"))
            {
                // HPSystem ������Ʈ�� ������
                HPSystem hpSystem = other.GetComponent<HPSystem>();
                if (hpSystem != null)
                {
                    // HP 1 ����
                    hpSystem.UpdateHP(-1f);

                    // ���� 1 ���� (ScoreManager���� ����)
                    ScoreManager.instance.AddScore(1);  // ���� �߰�
                }
            }

            // ���� ȿ�� �� ���� ó��
            Ray ray = new Ray(Camera.main.transform.position, transform.position - Camera.main.transform.position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            photonView.RPC(nameof(CreatExplo), RpcTarget.All, transform.position, hit.normal);
            photonView.RPC(nameof(PlayExplosionSound), RpcTarget.All);

            // ���� �ı�
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void CreatExplo(Vector3 position, Vector3 normal)
    {
        GameObject explo = Instantiate(exploFactory);
        explo.transform.position = position;
        explo.transform.forward = normal;
    }

    [PunRPC]
    void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
    }
}