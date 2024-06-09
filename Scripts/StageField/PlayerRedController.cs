using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedController : MonoBehaviour
{
    Transform cameraPosition; // 카메라 이동 위치

    float playerMoveSpeed;    // 플레이어 이동 속도
    float playerRotateSpeed;  // 플레이어 회전 속도

    float autoMoveSpeed; // 자동 이동 속도

    float playerMoveSpeedInit; // 플레이어 이동 속도 초기값
    float autoMoveSpeedInit;   // 자동 이동 속도 초기값

    void Start()
    {
        // 오브젝트 불러오기 및 초기화
        cameraPosition = GameObject.Find("CameraPosition").transform;

        playerMoveSpeed = 5.0f;
        playerRotateSpeed = 120.0f;

        autoMoveSpeed = 2.0f;

        playerMoveSpeedInit = playerMoveSpeed;
        autoMoveSpeedInit = autoMoveSpeed;
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

        if (v != 0) transform.Translate(v * Vector3.forward * playerMoveSpeed * Time.deltaTime); // 이동키를 누르고 있는 경우 키 입력에 따라 이동
        else transform.Translate(Vector3.forward * autoMoveSpeed * Time.deltaTime);              // 이동키를 누르고 있지 않은 경우 앞으로 자동 이동
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

    // 플레이어 이동속도 변경 (퍼센트 단위)
    public void ChangePlayerMoveSpeed(float percent)
    {
        // 키입력 이동속도와 자동 이동속도 변경
        playerMoveSpeed += playerMoveSpeed * (percent / 100.0f);
        autoMoveSpeed += autoMoveSpeed * (percent / 100.0f);
        //Debug.Log("Move: " + playerMoveSpeed + "AutoMove: " + autoMoveSpeed);                                  
    }

    // 플레이어 이동속도 초기값으로 변경
    public void ChangePlayerMoveSpeedInit()
    {
        // 키입력 이동속도와 자동 이동속도 초기값으로 변경
        playerMoveSpeed = playerMoveSpeedInit;
        autoMoveSpeed = autoMoveSpeedInit;
    }

    // 플레이어 멈춤
    public void PlayerMoveStop()
    {
        // 플레이어 키입력 이동속도와 자동 이동속도 0으로 변경
        playerMoveSpeed = 0.0f;
        autoMoveSpeed = 0.0f;
    }
}