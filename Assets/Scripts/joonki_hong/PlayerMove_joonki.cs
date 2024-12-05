using UnityEngine;

public class PlayerMove_joonki : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Ŀ���� �߾ӿ� �����ϰ� ����ϴ�.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���콺 �Է��� �޽��ϴ�.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���Ʒ�(��ġ) ȸ���� ����մϴ�.
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ������ �������� �ʵ��� ����

        // ī�޶� ȸ���� �����մϴ�.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
