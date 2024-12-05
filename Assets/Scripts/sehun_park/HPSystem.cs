using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviourPun
{
    // �ִ� HP
    public float maxHP;
    // ���� HP
    public float currHP;
    // HPBar Image
    public Image hpBar;
    // HP �� 0 �� �Ǿ��� �� ȣ��Ǵ� �Լ��� ���� ����
    public Action onDie;

    void Start()
    {
        InitHP();
    }

    void Update()
    {
    }

    public void InitHP()
    {
        // ���� HP �� �ִ� HP �� ����
        currHP = maxHP;
    }

    public void UpdateHP(float value)
    {
        photonView.RPC(nameof(RpcUpdateHP), RpcTarget.All, value);
    }

    // HP ���� �Լ�
    [PunRPC]
    public void RpcUpdateHP(float value)
    {
        // ���� HP �� value ��ŭ ������.
        currHP += value;

        // HPBar Image ����
        if (hpBar != null)
        {
            hpBar.fillAmount = currHP / maxHP;
        }

        // ���࿡ ���� HP �� 0���� �۰ų� ������
        if (currHP <= 0)
        {
            print(gameObject.name + "�� HP �� 0�Դϴ�.");

            if (onDie != null)
            {
                onDie();
            }
            else
            {
                // �⺻������ ���� ó���Ǵ� �� ����
                Destroy(gameObject);
            }

            //if (gameObject.layer == LayerMask.NameToLayer("Player"))
            //{
            //    //�÷��̾� ���� ó��
            //    PlayerFire pf = GetComponentInParent<PlayerFire>();
            //    pf.OnDie();
            //}
            //else if (gameObject.layer == LayerMask.NameToLayer("ObstacleCube"))
            //{
            //    //ť�� ���� ó��
            //    ObstacleCube cube = GetComponent<ObstacleCube>();
            //    cube.OnDie();
            //}
            //else if(gameObject.layer == LayerMask.NameToLayer("Enemy"))
            //{
            //    //�� ���� ó��
            //}
        }
    }
}
