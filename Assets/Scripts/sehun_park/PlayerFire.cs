using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    // ť�� Prefab
    public GameObject cubeFactory;

    // Impact Prefab
    public GameObject impactFactory;

    // RigidBody �� �����̴� �Ѿ� Prefab
    public GameObject bulletFactory;
    // �Ѿ� Prefab
    public GameObject rpcBulletFactory;
    // �ѱ� Transform
    public Transform firePos;
    // Animator
    Animator anim;

    // ��ų �߽���
    public Transform skillCenter;

    // ���� �� �̴�?
    public bool isMyTurn;

    // ��Ÿ�� ����
    public bool canShoot = true;  // ��Ÿ�� ���� üũ
    public float cooldownTime = 10f;  // ��Ÿ�� �ð� (10��)
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        // HPSystem ��������.
        HPSystem hpSystem = GetComponentInChildren<HPSystem>();
        // onDie ������ OnDie �Լ� ����
        hpSystem.onDie = OnDie;
    }

    void Update()
    {
        // ���࿡ �� ���� �ƴ϶��
        if (photonView.IsMine == false) return;

        // ���콺�� lockMode �� None �̸� (���콺 �����Ͱ� Ȱ��ȭ �Ǿ� �ִٸ�) �Լ��� ������.
        if (Cursor.lockState == CursorLockMode.None)
            return;

        // HP 0 �� �Ǿ����� �� ���� ���ϰ�
        if (isDie) return;

        // �� ���� �ƴ϶�� �Լ��� ������
        // if (!isMyTurn) return;

        // ���콺 ���� ��ư ������
        if (Input.GetMouseButtonDown(0))
        {
            // �ѽ�� �ִϸ��̼� ���� (Fire Ʈ���� �߻�)
            photonView.RPC(nameof(SetTrigger), RpcTarget.All, "Fire");
            // �Ѿ˰��忡�� �Ѿ��� ����, �ѱ���ġ ����, �ѱ�ȸ�� ����
            PhotonNetwork.Instantiate("Bullet2", firePos.position, firePos.rotation);

            Debug.Log("�Ѿ� �߻��");
        }


        // R Ű�� ������
        if (Input.GetKeyDown(KeyCode.R) && canShoot)
        {
            StartCoroutine(ShootWithCooldown());  // ��Ÿ���� ������ �߻� ó��
        }
    }

    // �Ѿ� �߻� �� ��Ÿ�� �ڷ�ƾ
    IEnumerator ShootWithCooldown()
    {
        int maxBulletCnt = 10;  // ������ �Ѿ��� ����
        float angle = 360.0f / maxBulletCnt;  // �Ѿ��� ȸ���ϸ鼭 ������ ����

        // �Ѿ� �߻�
        for (int i = 0; i < maxBulletCnt; i++)
        {
            // skillCenter�� �߽����� ���� ����
            skillCenter.localEulerAngles = new Vector3(0, angle * i, 0);
            Vector3 pos = skillCenter.position + skillCenter.forward * 2;  // �Ѿ� ���� ��ġ
            Quaternion rot = Quaternion.LookRotation(Vector3.down, skillCenter.forward);  // �Ѿ� ȸ���� ����
            PhotonNetwork.Instantiate(bulletFactory.name, pos, rot);  // �Ѿ� ����
        }

        // ��Ÿ�� ����
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);  // 10�� ���
        canShoot = true;  // ��Ÿ�� ���� �� �ٽ� �߻� ����
    }

    [PunRPC]
    void SetTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }

    [PunRPC]
    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        Instantiate(rpcBulletFactory, position, rotation);
    }

    [PunRPC]
    void CreateImpact(Vector3 position)
    {
        GameObject impact = Instantiate(impactFactory);
        impact.transform.position = position;
    }


    [PunRPC]
    void CreateCube(Vector3 position)
    {
        Instantiate(cubeFactory, position, Quaternion.identity);
    }

    // �׾���?
    bool isDie;
    public void OnDie()
    {
        isDie = true;
    }

    public void ChangeTurn(bool turn)
    {
        photonView.RPC(nameof(RpcChangeTurn), photonView.Owner, turn);
    }

    // isMyTurn �� �������ִ� �Լ�
    [PunRPC]
    public void RpcChangeTurn(bool turn)
    {
        isMyTurn = turn;
    }
}