using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotateSingle : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 회전 속도 설정
    private float mouseX, mouseY;       // 마우스 입력 값

    void Update()
    {
        // 마우스 입력을 받음
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // y 축 회전 제한 (카메라가 너무 위나 아래로 돌아가지 않도록)
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // 카메라 회전 (마우스 움직임에 따라 x, y 축으로 회전)
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
    }
}