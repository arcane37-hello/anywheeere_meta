using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ExplosionImageManager : MonoBehaviour
{
    // ù ��° �̹��� (��Ÿ�� ��)
    public Image explosionCooldownImage;

    // �� ��° �̹��� (��Ÿ�� �Ϸ� ����)
    public Image explosionReadyImage;

    // PlayerFire ��ũ��Ʈ�� ����
    private PlayerFire playerFire;

    // ��Ÿ�� �ð�
    private float cooldownTime;

    // ���� ��Ÿ�� ����
    private bool canShoot;

    void Start()
    {
        // PlayerFire ��ũ��Ʈ�� ã�Ƽ� ����
        playerFire = FindObjectOfType<PlayerFire>();  // ������ PlayerFire ��ũ��Ʈ ã��

        if (playerFire == null)
        {
            Debug.LogError("PlayerFire ��ũ��Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // PlayerFire�� ��Ÿ�� ������ ������
        cooldownTime = playerFire.cooldownTime;

        // �뽬 ���� �̹��� �ʱ� ���� ����
        explosionReadyImage.gameObject.SetActive(true);  // �غ� ���� �̹��� Ȱ��ȭ
        explosionCooldownImage.gameObject.SetActive(false);  // ��Ÿ�� �� �̹��� ��Ȱ��ȭ
    }

    void Update()
    {
        // ��Ÿ�� ���� PlayerFire���� ��������
        canShoot = playerFire.canShoot;

        if (canShoot)
        {
            // ��Ÿ���� ������ ��
            explosionReadyImage.gameObject.SetActive(true);
            explosionCooldownImage.gameObject.SetActive(false);
        }
        else
        {
            // ��Ÿ�� ���� ���� ��
            explosionReadyImage.gameObject.SetActive(false);
            explosionCooldownImage.gameObject.SetActive(true);

            // ��Ÿ�� ���� ��Ȳ�� ���� �̹����� ������
            float cooldownProgress = 1 - (playerFire.cooldownTime / cooldownTime);
            explosionCooldownImage.fillAmount = cooldownProgress;  // ������ ȿ��
        }
    }
}