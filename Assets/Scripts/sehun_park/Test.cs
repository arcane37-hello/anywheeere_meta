using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    CharacterController cc;

    // �߷�
    float gravity = 0f;
    // y �ӷ�
    float yVelocity;
    // ���� �ʱ� �ӷ�
    public float jumpPower = 3;

    public GameObject cam;
    void Start()
    {
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {

            // 1. Ű���� WASD Ű �Է��� ����
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            // 2. ������ ������.
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // ���࿡ ���� ������ yVelocity�� 0���� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // ���࿡ Space �ٸ� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity �� jumpPower�� ����
                yVelocity = jumpPower;
            }

            // yVelocity ���� �߷¿� ���ؼ� �����Ű��.
            yVelocity += gravity * Time.deltaTime;
            // dir.y�� yVelocity ���� ����
            dir.y = yVelocity;

            // �ڽ��� ������ �������� dir ����
            // dir = transform.TransformDirection(dir); ;

            // 3, �� �������� ��������.
            cc.Move(dir * moveSpeed * Time.deltaTime);
        

    }
}