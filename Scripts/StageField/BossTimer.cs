using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class BossTimer : MonoBehaviour
    {
        GameObject homeEntranceWall; // 필드 중앙에 있는 몬스터 집 입구의 벽

        float timeMax;     // 출현까지 남은 시간 최대치
        float timeCurrent; // 출현까지 남은 시간

        bool isTimerStop;  // 타이머가 멈췄는지 확인하는 플레그

        private void Awake()
        {
            // 관련 오브젝트 불러오기
            LoadObject();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        void Update()
        {
            if (!isTimerStop) UpdateCurrentTime(); // 타이머가 멈추지 않았을 경우 타이머 업데이트
            else BossGen();                        // 타이머가 멈췄을 경우 보스 출현
        }

        // 관련 오브젝트 불러오기
        void LoadObject()
        {
            homeEntranceWall = GameObject.Find("HomeEntranceWall");
        }

        // 초기화
        void Initialize()
        {
            timeMax = 30.0f;       // 30초
            timeCurrent = timeMax;

            FieldUIManager.instance.InitSetBossTimer(timeMax, 0.0f);
            FieldUIManager.instance.SetActiveBossMiniMapIcon(false);

            isTimerStop = false;
        }

        // 타이머 업데이트
        void UpdateCurrentTime()
        {
            // 전체 시간에서 흘러간 시간을 계속 빼주며 현재 남은 시간을 계산
            timeCurrent -= Time.deltaTime;

            if (timeCurrent < 0)
            {
                // 현재 남은 시간이 0이 되었을 경우 타이머 멈춤 체크
                timeCurrent = 0;

                isTimerStop = true;
            }

            // 남은 시간에 맞춰 보스 출현 타이머 UI 업데이트
            FieldUIManager.instance.SetBossTimer(timeCurrent);
        }

        // 보스 출현
        void BossGen()
        {
            // 필드 중앙에 있는 몬스터 집 입구의 벽을 비활성화하고 미니맵에 보스 아이콘 띄우기
            homeEntranceWall.SetActive(false);
            FieldUIManager.instance.SetActiveBossMiniMapIcon(true);
        }
    }
}
