using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class FieldGenManager : MonoBehaviour
    {
        public static FieldGenManager instance;

        GameObject[] oxygens;    // 산소 오브젝트들
        GameObject[] whiteCalls; // 백혈구 호출 오브젝트들
        GameObject reset;        // 리셋 오브젝트

        int oxygenCountMax;      // 필드에 있는 전체 산소 개수
        int oxygenCountCurrent;  // 필드에 있는 현재 산소 개수

        int resetGenOxygenPercent; // 리셋 아이템이 생성되기 위해 획득해야 하는 산소 비율
        int resetGenOxygenCount;   // 리셋 아이템이 생성되기 위해 획득해야 하는 산소 개수

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트들 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 오브젝트들 불러오기
        void LoadObjects()
        {
            oxygens = GameObject.FindGameObjectsWithTag("Oxygen");
            whiteCalls = GameObject.FindGameObjectsWithTag("WhiteCall");                                                                
            reset = GameObject.FindWithTag("Reset");
        }

        // 초기화
        void Initialize()
        {
            reset.SetActive(false); // 리셋 아이템 비활성화

            oxygenCountMax = oxygens.Length;     // 산소 오브젝트 개수만큼 전체 산소 개수 초기화                               
            oxygenCountCurrent = oxygenCountMax;

            resetGenOxygenPercent = 10;
            resetGenOxygenCount = (int)(oxygenCountMax * (1 - (resetGenOxygenPercent / 100.0f)));

            Debug.Log(oxygenCountMax + ", " + (oxygenCountMax-resetGenOxygenCount));
        }

        // 산소 개수 감소
        public void DecreaseOxygen()
        {
            oxygenCountCurrent--;

            if(oxygenCountCurrent <= resetGenOxygenCount)
            {
                // 현재 산소 개수가 리셋 아이템이 생성되기 위해 획득해야 하는 산소 개수 이하인 경우              
                // 리셋 아이템 생성 (활성화)
                GenerateResetItem();
            }
        }

        // 리셋 아이템 생성 (활성화)
        public void GenerateResetItem()
        {
            reset.SetActive(true);
        }

        // 필드 전체 아이템 재생성 (활성화)
        public void GenerateFieldItems()
        {
            oxygenCountCurrent = oxygenCountMax; // 현재 산소 개수를 전체 산소 개수로 초기화                                

            // 모든 산소 오브젝트 활성화
            for (int i = 0; i < oxygens.Length; i++)
            {
                oxygens[i].SetActive(true);
            }

            // 모든 백혈구 호출 오브젝트 활성화
            for(int i = 0; i < whiteCalls.Length; i++)
            {
                whiteCalls[i].SetActive(true);
            }
        }
    }
}