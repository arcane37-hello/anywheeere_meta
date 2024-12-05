using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �̵� �ӷ�
    public float moveSpeed = 5;

    // �߷�
    float gravity = -9.81f;
    // y �ӷ�
    float yVelocity;
    // ���� �ʱ� �ӷ�
    public float jumpPower = 3;

    // �뽬 ���� ����
    public float dashDistance = 10f;  // �뽬 �Ÿ�
    public float dashDuration = 0.2f; // �뽬 ���� �ð�
    public float dashCooldown = 5f;   // �뽬 ��Ÿ��
    public bool canDash = true;      // �뽬 ���� ����
    private bool isDashing = false;   // �뽬 ������ ����
    private Vector3 dashDirection;    // �뽬 ����
    public float dashTimeLeft;       // ���� �뽬 �ð�

    // ī�޶� 
    public GameObject cam;

    // �������� �Ѿ���� ��ġ��
    Vector3 receivePos;
    // �������� �Ѿ���� ȸ����
    Quaternion receiveRot;
    // ���� �ӷ�
    public float lerpSpeed = 50;

    // animator
    Animator anim;

    // AD Ű �Է� ���� ����
    float h;
    // WS Ű �Է� ���� ����
    float v;

    // LookPos
    public Transform lookPos;

    // �г��� UI
    public TMP_Text nickName;

    [PunRPC]
    void RpcAddPlayer(int order)
    {
        // GameManger ���� photonView �� �Ѱ�����
        Game2Manager.instance.AddPlayer(photonView, order);
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            // ���콺 �����.
            Cursor.lockState = CursorLockMode.Locked;

            // ���� �濡 ���� ������ ��ο��� �˷�����.
            photonView.RPC(nameof(RpcAddPlayer), RpcTarget.AllBuffered, ProjectMgr.Get().orderInRoom);

        }


        // ĳ���� ��Ʈ�ѷ� ��������.
        cc = GetComponent<CharacterController>();
        // �� ���� ���� ī�޶� Ȱ��ȭ��
        cam.SetActive(photonView.IsMine);
        // Animator ��������
        anim = GetComponentInChildren<Animator>();

        // �г��� UI �� �ش�ĳ������ ������ �г��� ����
        nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // �� ���� ���� ��Ʈ�� ����!
        if (photonView.IsMine)
        {
            // ���콺�� lockMode �� None �̸� (���콺 �����Ͱ� Ȱ��ȭ �Ǿ� �ִٸ�) �Լ��� ������.
            if (Cursor.lockState == CursorLockMode.None)
                return;

            // 1 .Ű���� WASD Ű �Է��� ����
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // 2. ������ ������.
            Vector3 dirH = transform.right * h;
            Vector3 dirV = transform.forward * v;
            Vector3 dir = dirH + dirV;

            dir.Normalize();

            // ���࿡ ���� ������ yVelocity �� 0 ���� �ʱ�ȭ
            if (cc.isGrounded)
            {
                yVelocity = 0;
            }

            // ���࿡ Space �ٸ� ������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // yVelocity �� jumpPower �� ����
                yVelocity = jumpPower;
            }

            // yVelocity ���� �߷¿� ���ؼ� �����Ű��.
            yVelocity += gravity * Time.deltaTime;

            #region �������� ���� �ƴѰ�
            // dir.y �� yVelocity ���� ����
            dir.y = yVelocity;

            // 3. �� �������� ��������.
            cc.Move(dir * moveSpeed * Time.deltaTime);
            #endregion

            #region �������� ����
            //dir = dir * moveSpeed;
            //dir.y = yVelocity;
            //cc.Move(dir * Time.deltaTime);
            #endregion

            // �뽬 ó��
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartDash(dir);  // �뽬 ����
            }

            else
            {
                DashMove();
            }
        }

        // ���� Player �ƴ϶��
        else
        {
            // ��ġ ����
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * lerpSpeed);
            // ȸ�� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, Time.deltaTime * lerpSpeed);
        }

        // anim �� �̿��ؼ� h, v ���� ����
        anim.SetFloat("DirH", h);
        anim.SetFloat("DirV", v);
    }
    // �뽬 ���� �޼���
    private void StartDash(Vector3 dir)
    {
        dashDirection = dir;
        dashTimeLeft = dashDuration;
        isDashing = true;
        canDash = false;
        Invoke(nameof(ResetDash), dashCooldown);  // ��Ÿ�� ����
    }

    // �뽬 �̵� �޼���
    public void DashMove()
    {
        if (dashTimeLeft > 0)
        {
            cc.Move(dashDirection * (dashDistance / dashDuration) * Time.deltaTime);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    // �뽬 ��Ÿ�� �ʱ�ȭ �޼���
    private void ResetDash()
    {
        canDash = true;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���࿡ ���� �����͸� ���� �� �ִ� ���¶�� (�� ���̶��)
        if (stream.IsWriting)
        {
            // ���� ��ġ���� ������.
            stream.SendNext(transform.position);
            // ���� ȸ������ ������.
            stream.SendNext(transform.rotation);
            // ���� h ��
            stream.SendNext(h);
            // ���� v ��
            stream.SendNext(v);
            // LookPos �� ��ġ���� ������.
            stream.SendNext(lookPos.position);
        }
        // �����͸� ���� �� �ִ� ���¶�� (�� ���� �Ƴ����)
        else if (stream.IsReading)
        {
            // ��ġ���� ����.
            receivePos = (Vector3)stream.ReceiveNext();
            // ȸ������ ����.
            receiveRot = (Quaternion)stream.ReceiveNext();
            // �������� ���� �Ǵ� h �� ����.
            h = (float)stream.ReceiveNext();
            // �������� ���� �Ǵ� v �� ����.
            v = (float)stream.ReceiveNext();
            // LookPos �� ��ġ���� ����.
            lookPos.position = (Vector3)stream.ReceiveNext();
        }
    }
}
