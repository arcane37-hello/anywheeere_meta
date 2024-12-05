using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DashImageManager : MonoBehaviour
{
    // 첫 번째 이미지 (쿨타임 중)
    public Image dashCooldownImage;

    // 두 번째 이미지 (대쉬 가능 상태)
    public Image dashReadyImage;

    // 대쉬와 연동할 PlayerMove를 수동으로 연결
    private PlayerMove playerMove;

    // 대쉬 쿨타임
    private float dashCooldown;

    // 현재 대쉬 상태 (PlayerMove 스크립트에서 가져옴)
    private bool canDash;

    void Start()
    {
        // PlayerMove 스크립트를 씬에서 수동으로 연결해야 함
        playerMove = FindObjectOfType<PlayerMove>();  // 씬에서 PlayerMove 스크립트 찾아서 연결

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove 스크립트를 찾을 수 없습니다.");
            return;
        }

        // 대쉬 쿨타임을 PlayerMove에서 가져오기
        dashCooldown = playerMove.dashCooldown;

        // 대쉬 관련 이미지 초기 상태 설정
        dashReadyImage.gameObject.SetActive(true);  // 대쉬 가능 상태일 때 활성화
        dashCooldownImage.gameObject.SetActive(false);  // 쿨타임 중 이미지 비활성화
    }

    void Update()
    {
        // 대쉬 가능 여부 PlayerMove 스크립트에서 가져오기
        canDash = playerMove.canDash;

        if (canDash)
        {
            // 대쉬가 가능할 때 (대쉬 준비 상태 이미지 표시)
            dashReadyImage.gameObject.SetActive(true);
            dashCooldownImage.gameObject.SetActive(false);
        }
        else
        {
            // 대쉬 쿨타임 중일 때 (쿨타임 이미지 표시)
            dashReadyImage.gameObject.SetActive(false);
            dashCooldownImage.gameObject.SetActive(true);

            // 쿨타임 동안 이미지가 차오르는 느낌으로 보여줌
            float cooldownProgress = 1 - (playerMove.dashTimeLeft / dashCooldown);
            dashCooldownImage.fillAmount = cooldownProgress;  // 이미지의 fillAmount로 차오름 효과 구현
        }
    }
}