using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace RSA
{
    public class UserProductionLevelManager : MonoBehaviour
    {
        public static UserProductionLevelManager instance;

        const int PLAYER_PRODUCTION_LEVEL_MAX = 4;            // 제작 숙련도 레벨의 최대치

        TextMeshProUGUI productionSkillLevelCurrentLvValue;   // 현재 제작 숙련도 레벨 텍스트
        TextMeshProUGUI productionSkillLevelNextValue;        // 다음 레벨업까지 필요한 숙련도 경험치 텍스트

        Slider productionSkillLevelBar;                       // 숙련도 경험치 바

        int playerProductionLevel;     // 플레이어의 현재 숙련도 레벨
        float playerProductionExp;     // 플레이어의 현재 숙련도 경험치
        float playerProductionExpMax;  // 플레이어의 숙련도 경험치 최대치

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
            productionSkillLevelCurrentLvValue = GameObject.Find("ProductionSkillLevelCurrentLvValue").GetComponent<TextMeshProUGUI>();
            productionSkillLevelNextValue = GameObject.Find("ProductionSkillLevelNextValue").GetComponent<TextMeshProUGUI>();

            productionSkillLevelBar = GameObject.Find("ProductionSkillLevelBar").GetComponent<Slider>();
        }

        // 초기화
        void Initialize()
        {
            playerProductionLevel = GameManager.instance.PlayerProductionLevel;
            playerProductionExp = GameManager.instance.PlayerProductionExp;

            // 숙련도 최대치는 현재 레벨 * 100과 같음
            playerProductionExpMax = playerProductionLevel * 100;

            // 숙련도 경험치 UI 세팅
            SetUserProductionLevelUI();
        }

        // 숙련도 경험치 UI 세팅
        void SetUserProductionLevelUI()
        {
            productionSkillLevelCurrentLvValue.text = playerProductionLevel.ToString();
            productionSkillLevelNextValue.text = (playerProductionExpMax - playerProductionExp).ToString();

            // 숙련도 경험치 바 UI 세팅
            SetLevelBar();
        }

        // 숙련도 경험치 바 UI 세팅
        void SetLevelBar()
        {
            productionSkillLevelBar.maxValue = playerProductionExpMax;
            productionSkillLevelBar.minValue = 0.0f;

            productionSkillLevelBar.value = playerProductionExp;
        }

        // 제작 숙련도 경험치 획득
        public void AddPlayerProductionExp(float value)
        {
            if(playerProductionLevel < PLAYER_PRODUCTION_LEVEL_MAX)
            {
                // 현재 숙련도 레벨이 최대 레벨보다 낮은 경우

                // 획득한 경험치 추가
                playerProductionExp += value;

                if (playerProductionExp >= playerProductionExpMax)
                {
                    // 현재 숙련도 경험치가 숙련도 경험치 최대치 이상인 경우

                    // 현재 숙련도 경험치에서 최대치를 뺀 후 숙련도 레벨업 (초과된 숙련도 수치 다음 레벨로 이전)
                    playerProductionExp -= playerProductionExpMax;
                    playerProductionLevel++;

                    // 숙련도 최대치 재세팅
                    playerProductionExpMax = playerProductionLevel * 100;

                    if (playerProductionLevel == PLAYER_PRODUCTION_LEVEL_MAX)
                    {
                        // 최대 레벨을 달성한 경우
                        // 숙련도 레벨과 경험치 최대치로 유지
                        playerProductionLevel = PLAYER_PRODUCTION_LEVEL_MAX;
                        playerProductionExp = 100.0f;
                        playerProductionExpMax = 100.0f;
                    }
                }

                // 플레이어의 숙련도 레벨과 경험치 저장
                GameManager.instance.PlayerProductionLevel = playerProductionLevel;
                GameManager.instance.PlayerProductionExp = playerProductionExp;

                // 숙련도 경험치 UI 세팅
                SetUserProductionLevelUI();

                // 레시피 버튼 세팅
                UserProductionRecipeManager.instance.SetRecipeButtons();

                // 제작 대성공 확률 업데이트
                ProductionManager.instance.UpdateProbability();
            }
        }
    }
}
