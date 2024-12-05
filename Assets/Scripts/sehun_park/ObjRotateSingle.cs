using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotateSingle : MonoBehaviour
{
    public float rotationSpeed = 100f;  // ȸ�� �ӵ� ����
    private float mouseX, mouseY;       // ���콺 �Է� ��

    void Update()
    {
        // ���콺 �Է��� ����
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // y �� ȸ�� ���� (ī�޶� �ʹ� ���� �Ʒ��� ���ư��� �ʵ���)
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // ī�޶� ȸ�� (���콺 �����ӿ� ���� x, y ������ ȸ��)
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
    }
}