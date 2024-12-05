using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    // ȸ�� �ӷ�
    public float rotSpeed = 200;

    // ȸ�� ��
    float rotX;
    float rotY;

    // ȸ�� ���� ����
    public bool useRotX;
    public bool useRotY;

    // PhotonView
    public PhotonView pv;

    void Start()
    {

    }

    void Update()
    {
        // ���࿡ �����̶�� 
        if (pv.IsMine)
        {
            // ���콺�� lockMode �� None �̸� (���콺 �����Ͱ� Ȱ��ȭ �Ǿ� �ִٸ�) �Լ��� ������.
            if (Cursor.lockState == CursorLockMode.None)
                return;

            // 1. ���콺�� �������� �޾ƿ���.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            // 2. ȸ�� ���� ���� (����)
            if (useRotX) rotX += my * rotSpeed * Time.deltaTime;
            if (useRotY) rotY += mx * rotSpeed * Time.deltaTime;

            // rotX �� ���� ����(�ּҰ�, �ִ밪)
            rotX = Mathf.Clamp(rotX, -80, 80);

            // 3. ������ ȸ�� ���� ���� ȸ�� ������ ����
            transform.localEulerAngles = new Vector3(-rotX, rotY, 0);
        }
    }
}
