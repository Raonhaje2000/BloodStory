using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class PlayerController : MonoBehaviour
    {
        Transform cameraPosition; // ī�޶� �̵� ��ġ

        float playerMoveSpeed;    // �÷��̾� �̵� �ӵ�
        float playerRotateSpeed;  // �÷��̾� ȸ�� �ӵ�

        void Start()
        {
            // ������Ʈ �ҷ����� �� �ʱ�ȭ
            cameraPosition = GameObject.Find("CameraPosition").transform;

            playerMoveSpeed = 5.0f;
            playerRotateSpeed = 120.0f;
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

            if (v != 0) transform.Translate(v * Vector3.forward * playerMoveSpeed * Time.deltaTime); // �̵�Ű�� ������ �ִ� ��� Ű �Է� ���⿡ ���� �̵�
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
    }
}
