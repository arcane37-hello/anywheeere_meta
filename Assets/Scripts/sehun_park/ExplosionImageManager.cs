using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ExplosionImageManager : MonoBehaviour
{
    // 첫 번째 이미지 (쿨타임 중)
    public Image explosionCooldownImage;

    // 두 번째 이미지 (쿨타임 완료 상태)
    public Image explosionReadyImage;

    // PlayerFire 스크립트와 연계
    private PlayerFire playerFire;

    // 쿨타임 시간
    private float cooldownTime;

    // 현재 쿨타임 상태
    private bool canShoot;

    void Start()
    {
        // PlayerFire 스크립트를 찾아서 연계
        playerFire = FindObjectOfType<PlayerFire>();  // 씬에서 PlayerFire 스크립트 찾기

        if (playerFire == null)
        {
            Debug.LogError("PlayerFire 스크립트를 찾을 수 없습니다.");
            return;
        }

        // PlayerFire의 쿨타임 정보를 가져옴
        cooldownTime = playerFire.cooldownTime;

        // 대쉬 관련 이미지 초기 상태 설정
        explosionReadyImage.gameObject.SetActive(true);  // 준비 상태 이미지 활성화
        explosionCooldownImage.gameObject.SetActive(false);  // 쿨타임 중 이미지 비활성화
    }

    void Update()
    {
        // 쿨타임 여부 PlayerFire에서 가져오기
        canShoot = playerFire.canShoot;

        if (canShoot)
        {
            // 쿨타임이 끝났을 때
            explosionReadyImage.gameObject.SetActive(true);
            explosionCooldownImage.gameObject.SetActive(false);
        }
        else
        {
            // 쿨타임 진행 중일 때
            explosionReadyImage.gameObject.SetActive(false);
            explosionCooldownImage.gameObject.SetActive(true);

            // 쿨타임 진행 상황에 따라 이미지가 차오름
            float cooldownProgress = 1 - (playerFire.cooldownTime / cooldownTime);
            explosionCooldownImage.fillAmount = cooldownProgress;  // 차오름 효과
        }
    }
}