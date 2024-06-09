using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public class WhiteCallRemainingTime : MonoBehaviour
    {
        GameObject player; // 플레이어

        Slider whiteCallRemainingTimeSlider; // 백혈구 호출 남은 시간 타이머 바                                           

        float maxTime; // 백혈구 호출 최대 시간
        float minTime; // 백혈구 호출 최소 시간

        float currentTime; // 백혈구 호출 현재 남은 시간

        private void Awake()
        {
            // 관련 오브젝트 불러오기
            LoadObejcts();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        void Update()
        {
            // 타이머 업데이트
            UpdateTime();
        }

        // 관련 오브젝트 불러오기
        void LoadObejcts()
        {
            player = GameObject.Find("Player");
            whiteCallRemainingTimeSlider = GameObject.Find("WhiteCallRemainingTime").GetComponent<Slider>();             
        }

        // 초기화
        void Initialize()
        {
            maxTime = player.GetComponent<PlayerRed>().WhiteCallTime;
            minTime = 0.0f;
            currentTime = 0.0f;

            whiteCallRemainingTimeSlider.maxValue = maxTime;
            whiteCallRemainingTimeSlider.minValue = minTime;

            whiteCallRemainingTimeSlider.value = minTime;
        }

        // 타이머 업데이트
        void UpdateTime()
        {
            if (player.GetComponent<PlayerRed>().playerRedState == PlayerRed.State.WhiteCall)                        
            {
                // 현재 플레이어의 상태가 백혈구 호출 상태인 경우 타이머 시작

                // 전체 시간에서 흘러간 시간을 계속 빼주며 현재 남은 시간을 계산
                currentTime -= Time.deltaTime;
                whiteCallRemainingTimeSlider.value = currentTime;
            }
            else
            {
                // 현재 플레이어의 상태가 백혈구 호출 상태가 아닌 경우

                // 타이머 초기화
                currentTime = maxTime;
            }
        }
    }
}