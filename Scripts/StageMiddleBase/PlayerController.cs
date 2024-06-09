using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class PlayerController : MonoBehaviour
    {
        Transform cameraPosition; // 카메라 이동 위치

        float playerMoveSpeed;    // 플레이어 이동 속도
        float playerRotateSpeed;  // 플레이어 회전 속도

        void Start()
        {
            // 오브젝트 불러오기 및 초기화
            cameraPosition = GameObject.Find("CameraPosition").transform;

            playerMoveSpeed = 5.0f;
            playerRotateSpeed = 120.0f;
        }

        void Update()
        {
            PlayerRedMove();   // 플레이어 이동 처리
            PlayerRedRotate(); // 플레이어 회전 처리
        }

        private void LateUpdate()
        {
            // 메인 카메라 위치를 지정된 카메라 이동 위치로 변경
            Camera.main.transform.SetPositionAndRotation(cameraPosition.position, cameraPosition.rotation);
        }

        // 플레이어 이동 처리
        void PlayerRedMove()
        {
            // 방향키 상하 또는 W, S키로 이동
            float v = Input.GetAxis("Vertical");

            if (v != 0) transform.Translate(v * Vector3.forward * playerMoveSpeed * Time.deltaTime); // 이동키를 누르고 있는 경우 키 입력 방향에 따라 이동
        }

        // 플레이어 회전 처리
        void PlayerRedRotate()
        {
            // 방향키 좌우 또는 A, D키로 이동
            float h = Input.GetAxis("Horizontal");

            if (h != 0)
            {
                // 회전키를 누르고 있는 경우 키 입력에 따라 좌우 회전
                float angle = h * playerRotateSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, angle);
            }
        }
    }
}
