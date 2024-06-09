using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedController : MonoBehaviour
{
    Transform cameraPosition; // ī�޶� �̵� ��ġ

    float playerMoveSpeed;    // �÷��̾� �̵� �ӵ�
    float playerRotateSpeed;  // �÷��̾� ȸ�� �ӵ�

    float autoMoveSpeed; // �ڵ� �̵� �ӵ�

    float playerMoveSpeedInit; // �÷��̾� �̵� �ӵ� �ʱⰪ
    float autoMoveSpeedInit;   // �ڵ� �̵� �ӵ� �ʱⰪ

    void Start()
    {
        // ������Ʈ �ҷ����� �� �ʱ�ȭ
        cameraPosition = GameObject.Find("CameraPosition").transform;

        playerMoveSpeed = 5.0f;
        playerRotateSpeed = 120.0f;

        autoMoveSpeed = 2.0f;

        playerMoveSpeedInit = playerMoveSpeed;
        autoMoveSpeedInit = autoMoveSpeed;
    }

    void Update()
    {
        PlayerRedMove();   // �÷��̾� �̵� ó��
        PlayerRedRotate(); // �÷��̾� ȸ�� ó��
    }

    private void LateUpdate()
    {
        // ���� ī�޶� ��ġ�� ������ ī�޶� �̵� ��ġ�� ����
        Camera.main.transform.SetPositionAndRotation(cameraPosition.position, cameraPosition.rotation);
    }

    // �÷��̾� �̵� ó��
    void PlayerRedMove()
    {
        // ����Ű ���� �Ǵ� W, SŰ�� �̵�
        float v = Input.GetAxis("Vertical");

        if (v != 0) transform.Translate(v * Vector3.forward * playerMoveSpeed * Time.deltaTime); // �̵�Ű�� ������ �ִ� ��� Ű �Է¿� ���� �̵�
        else transform.Translate(Vector3.forward * autoMoveSpeed * Time.deltaTime);              // �̵�Ű�� ������ ���� ���� ��� ������ �ڵ� �̵�
    }

    // �÷��̾� ȸ�� ó��
    void PlayerRedRotate()
    {
        // ����Ű �¿� �Ǵ� A, DŰ�� �̵�
        float h = Input.GetAxis("Horizontal");

        if (h != 0)
        {
            // ȸ��Ű�� ������ �ִ� ��� Ű �Է¿� ���� �¿� ȸ��
            float angle = h * playerRotateSpeed * Time.deltaTime;                                                 

            transform.Rotate(Vector3.up, angle);
        }
    }

    // �÷��̾� �̵��ӵ� ���� (�ۼ�Ʈ ����)
    public void ChangePlayerMoveSpeed(float percent)
    {
        // Ű�Է� �̵��ӵ��� �ڵ� �̵��ӵ� ����
        playerMoveSpeed += playerMoveSpeed * (percent / 100.0f);
        autoMoveSpeed += autoMoveSpeed * (percent / 100.0f);
        //Debug.Log("Move: " + playerMoveSpeed + "AutoMove: " + autoMoveSpeed);                                  
    }

    // �÷��̾� �̵��ӵ� �ʱⰪ���� ����
    public void ChangePlayerMoveSpeedInit()
    {
        // Ű�Է� �̵��ӵ��� �ڵ� �̵��ӵ� �ʱⰪ���� ����
        playerMoveSpeed = playerMoveSpeedInit;
        autoMoveSpeed = autoMoveSpeedInit;
    }

    // �÷��̾� ����
    public void PlayerMoveStop()
    {
        // �÷��̾� Ű�Է� �̵��ӵ��� �ڵ� �̵��ӵ� 0���� ����
        playerMoveSpeed = 0.0f;
        autoMoveSpeed = 0.0f;
    }
}