using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ReinforcementMessageBoxProgress : MonoBehaviour
    {
        TextMeshProUGUI reinforcementMessageBoxTypeText;             // 현재 강화 타입 텍스트

        TextMeshProUGUI reinforcementMessageBoxOxygenNeedText;       // 필요한 산소 수 텍스트
        TextMeshProUGUI reinforcementMessageBoxOxygenInventoryText;  // 현재 보유한 산소 수 텍스트

        TextMeshProUGUI reinforcementMessageBoxIronNeedText;         // 필요한 철분 수 텍스트
        TextMeshProUGUI reinforcementMessageBoxIronInventoryText;    // 현재 보유한 철분 수 텍스트

        TextMeshProUGUI reinforcementMessageBoxMineralNeedText;      // 필요한 미네랄 수 텍스트
        TextMeshProUGUI reinforcementMessageBoxMineralInventoryText; // 현재 보유한 미네랄 수 텍스트

        private void Awake()
        {
            // 관련 오브젝트 불러오기
            LoadObejects();
        }

        void Start()
        {

        }

        // 관련 오브젝트 불러오기
        void LoadObejects()
        {
            reinforcementMessageBoxTypeText = GameObject.Find("ReinforcementMessageBoxTypeText").GetComponent<TextMeshProUGUI>();

            reinforcementMessageBoxOxygenNeedText = GameObject.Find("ReinforcementMessageBoxOxygenNeedText").GetComponent<TextMeshProUGUI>();
            reinforcementMessageBoxOxygenInventoryText = GameObject.Find("ReinforcementMessageBoxOxygenInventoryText").GetComponent<TextMeshProUGUI>();

            reinforcementMessageBoxIronNeedText = GameObject.Find("ReinforcementMessageBoxIronNeedText").GetComponent<TextMeshProUGUI>();
            reinforcementMessageBoxIronInventoryText = GameObject.Find("ReinforcementMessageBoxIronInventoryText").GetComponent<TextMeshProUGUI>();

            reinforcementMessageBoxMineralNeedText = GameObject.Find("ReinforcementMessageBoxMineralNeedText").GetComponent<TextMeshProUGUI>();
            reinforcementMessageBoxMineralInventoryText = GameObject.Find("ReinforcementMessageBoxMineralInventoryText").GetComponent<TextMeshProUGUI>();
        }

        void Initialize()
        {

        }

        // 해당 강화 정보에 맞춰 메세지 박스 세팅
        public void SetMessageBoxProgressText(ReinforcementManager.ReinforcementType type, ReinforcementValues values, int index)
        {
            reinforcementMessageBoxTypeText.text = string.Format("정말 \'{0}\' 강화로 할거야?", GetReinforcementTypeString(type));

            reinforcementMessageBoxOxygenNeedText.text = values.Oxygen[index].ToString();
            reinforcementMessageBoxOxygenInventoryText.text = GameManager.instance.Oxygen.ToString();

            reinforcementMessageBoxIronNeedText.text = values.Iron[index].ToString();
            reinforcementMessageBoxIronInventoryText.text = GameManager.instance.Iron.ToString();

            SetMessageBoxMineralTexts(type, values.Mineral[index]);
        }

        // 강화를 진행하겠다는 버튼을 클릭했을 때
        public void ClickYesButton()
        {
            // 강화 진행 처리
            ReinforcementManager.instance.ContinueReinforceProgressButton();
        }

        // 강화를 진행하지 않겠다는 버튼을 클릭했을 때
        public void ClickNoButton()
        {
            // 강화 취소 처리
            ReinforcementManager.instance.CancleReinforceProgressButton();
        }

        // 강화 타입에 따른 문자열 구하기
        string GetReinforcementTypeString(ReinforcementManager.ReinforcementType type)
        {
            switch(type)
            {
                case ReinforcementManager.ReinforcementType.normal:
                    {
                        return "기본 강화";
                    }
                case ReinforcementManager.ReinforcementType.high:
                    {
                        return "집중 강화";
                    }
                case ReinforcementManager.ReinforcementType.highest:
                    {
                        return "초집중 강화";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        // 미네랄 텍스트 변경하기
        void SetMessageBoxMineralTexts(ReinforcementManager.ReinforcementType type, int needCount)
        {
            // 강화 타입에 따른 필요한 미네랄 텍스트 변경하기
            switch (type)
            {
                case ReinforcementManager.ReinforcementType.normal:
                    {
                        reinforcementMessageBoxMineralNeedText.text = "0";
                        break;
                    }
                case ReinforcementManager.ReinforcementType.high:
                    {
                        reinforcementMessageBoxMineralNeedText.text = needCount.ToString();
                        break;
                    }
                case ReinforcementManager.ReinforcementType.highest:
                    {
                        reinforcementMessageBoxMineralNeedText.text = (needCount * 2).ToString();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            reinforcementMessageBoxMineralInventoryText.text = GameManager.instance.Mineral.ToString();
        }
    }
}
