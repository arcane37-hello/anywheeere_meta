using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DashImageManager : MonoBehaviour
{
    // ù ��° �̹��� (��Ÿ�� ��)
    public Image dashCooldownImage;

    // �� ��° �̹��� (�뽬 ���� ����)
    public Image dashReadyImage;

    // �뽬�� ������ PlayerMove�� �������� ����
    private PlayerMove playerMove;

    // �뽬 ��Ÿ��
    private float dashCooldown;

    // ���� �뽬 ���� (PlayerMove ��ũ��Ʈ���� ������)
    private bool canDash;

    void Start()
    {
        // PlayerMove ��ũ��Ʈ�� ������ �������� �����ؾ� ��
        playerMove = FindObjectOfType<PlayerMove>();  // ������ PlayerMove ��ũ��Ʈ ã�Ƽ� ����

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // �뽬 ��Ÿ���� PlayerMove���� ��������
        dashCooldown = playerMove.dashCooldown;

        // �뽬 ���� �̹��� �ʱ� ���� ����
        dashReadyImage.gameObject.SetActive(true);  // �뽬 ���� ������ �� Ȱ��ȭ
        dashCooldownImage.gameObject.SetActive(false);  // ��Ÿ�� �� �̹��� ��Ȱ��ȭ
    }

    void Update()
    {
        // �뽬 ���� ���� PlayerMove ��ũ��Ʈ���� ��������
        canDash = playerMove.canDash;

        if (canDash)
        {
            // �뽬�� ������ �� (�뽬 �غ� ���� �̹��� ǥ��)
            dashReadyImage.gameObject.SetActive(true);
            dashCooldownImage.gameObject.SetActive(false);
        }
        else
        {
            // �뽬 ��Ÿ�� ���� �� (��Ÿ�� �̹��� ǥ��)
            dashReadyImage.gameObject.SetActive(false);
            dashCooldownImage.gameObject.SetActive(true);

            // ��Ÿ�� ���� �̹����� �������� �������� ������
            float cooldownProgress = 1 - (playerMove.dashTimeLeft / dashCooldown);
            dashCooldownImage.fillAmount = cooldownProgress;  // �̹����� fillAmount�� ������ ȿ�� ����
        }
    }
}