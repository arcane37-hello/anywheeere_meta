using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviourPun
{
    // 큐브 Prefab
    public GameObject cubeFactory;

    // Impact Prefab
    public GameObject impactFactory;

    // RigidBody 로 움직이는 총알 Prefab
    public GameObject bulletFactory;
    // 총알 Prefab
    public GameObject rpcBulletFactory;
    // 총구 Transform
    public Transform firePos;
    // Animator
    Animator anim;

    // 스킬 중심점
    public Transform skillCenter;

    // 나의 턴 이니?
    public bool isMyTurn;

    // 쿨타임 관리
    public bool canShoot = true;  // 쿨타임 여부 체크
    public float cooldownTime = 10f;  // 쿨타임 시간 (10초)
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        // HPSystem 가져오자.
        HPSystem hpSystem = GetComponentInChildren<HPSystem>();
        // onDie 변수에 OnDie 함수 설정
        hpSystem.onDie = OnDie;
    }

    void Update()
    {
        // 만약에 내 것이 아니라면
        if (photonView.IsMine == false) return;

        // 마우스의 lockMode 가 None 이면 (마우스 포인터가 활성화 되어 있다면) 함수를 나가자.
        if (Cursor.lockState == CursorLockMode.None)
            return;

        // HP 0 이 되었으면 총 쏘지 못하게
        if (isDie) return;

        // 내 턴이 아니라면 함수를 나가자
        // if (!isMyTurn) return;

        // 마우스 왼쪽 버튼 누르면
        if (Input.GetMouseButtonDown(0))
        {
            // 총쏘는 애니메이션 실행 (Fire 트리거 발생)
            photonView.RPC(nameof(SetTrigger), RpcTarget.All, "Fire");
            // 총알공장에서 총알을 생성, 총구위치 셋팅, 총구회전 셋팅
            PhotonNetwork.Instantiate("Bullet2", firePos.position, firePos.rotation);

            Debug.Log("총알 발사됨");
        }


        // R 키를 누르면
        if (Input.GetKeyDown(KeyCode.R) && canShoot)
        {
            StartCoroutine(ShootWithCooldown());  // 쿨타임을 포함한 발사 처리
        }
    }

    // 총알 발사 및 쿨타임 코루틴
    IEnumerator ShootWithCooldown()
    {
        int maxBulletCnt = 10;  // 생성할 총알의 개수
        float angle = 360.0f / maxBulletCnt;  // 총알이 회전하면서 생성될 각도

        // 총알 발사
        for (int i = 0; i < maxBulletCnt; i++)
        {
            // skillCenter를 중심으로 각도 설정
            skillCenter.localEulerAngles = new Vector3(0, angle * i, 0);
            Vector3 pos = skillCenter.position + skillCenter.forward * 2;  // 총알 생성 위치
            Quaternion rot = Quaternion.LookRotation(Vector3.down, skillCenter.forward);  // 총알 회전값 설정
            PhotonNetwork.Instantiate(bulletFactory.name, pos, rot);  // 총알 생성
        }

        // 쿨타임 시작
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);  // 10초 대기
        canShoot = true;  // 쿨타임 종료 후 다시 발사 가능
    }

    [PunRPC]
    void SetTrigger(string parameter)
    {
        anim.SetTrigger(parameter);
    }

    [PunRPC]
    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        Instantiate(rpcBulletFactory, position, rotation);
    }

    [PunRPC]
    void CreateImpact(Vector3 position)
    {
        GameObject impact = Instantiate(impactFactory);
        impact.transform.position = position;
    }


    [PunRPC]
    void CreateCube(Vector3 position)
    {
        Instantiate(cubeFactory, position, Quaternion.identity);
    }

    // 죽었니?
    bool isDie;
    public void OnDie()
    {
        isDie = true;
    }

    public void ChangeTurn(bool turn)
    {
        photonView.RPC(nameof(RpcChangeTurn), photonView.Owner, turn);
    }

    // isMyTurn 을 변경해주는 함수
    [PunRPC]
    public void RpcChangeTurn(bool turn)
    {
        isMyTurn = turn;
    }
}