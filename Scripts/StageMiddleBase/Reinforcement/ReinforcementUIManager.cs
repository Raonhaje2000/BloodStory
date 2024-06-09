using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ReinforcementUIManager : MonoBehaviour
    {
        public static ReinforcementUIManager instance;

        TextMeshProUGUI equipmentName;        // 현재 장비 아이템 이름 텍스트

        TextMeshProUGUI beforeEquipmentGrade; // 강화 전 장비 아이템 등급 텍스트
        TextMeshProUGUI afterEquipmentGrade;  // 강화 후 장비 아이템 등급 텍스트

        TextMeshProUGUI probability;          // 강화 확률 텍스트

        Image beforeEquipmentIcon;            // 강화 전 장비 아이템 아이콘 이미지
        Image afterEquipmentIcon;             // 강화 후 장비 아이템 아이콘 이미지
  
        TextMeshProUGUI beforeHp;             // 강화 전 체력 텍스트
        TextMeshProUGUI beforeAttack;         // 강화 전 공격력 텍스트
        TextMeshProUGUI beforeDefence;        // 강화 전 방어력 텍스트
        TextMeshProUGUI beforeSpeed;          // 강화 전 이동속도 텍스트
 
        TextMeshProUGUI afterHp;              // 강화 후 체력 텍스트
        TextMeshProUGUI afterAttack;          // 강화 후 공격력 텍스트
        TextMeshProUGUI afterDefence;         // 강화 후 방어력 텍스트
        TextMeshProUGUI afterSpeed;           // 강화 후 이동속도 텍스트

        TextMeshProUGUI oxygenValue;          // 산소 텍스트
        TextMeshProUGUI ironValue;            // 철분 텍스트

        TextMeshProUGUI highMineralValue;     // 집중 강화 미네랄 텍스트
        TextMeshProUGUI highestMineralValue;  // 초집중 강화 미네랄 텍스트

        TextMeshProUGUI npcText;              // NPC 대사 텍스트
        string[] npcTexts;                    // NPC 대사 텍스트 모음

        
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
            equipmentName = GameObject.Find("ReinforcementBeforeAfterEquipNameValue").GetComponent<TextMeshProUGUI>();

            beforeEquipmentGrade = GameObject.Find("ReinforcementBeforeAfterEquipBeforeGradeValue").GetComponent<TextMeshProUGUI>();
            afterEquipmentGrade = GameObject.Find("ReinforcementBeforeAfterEquipAfterGradeValue").GetComponent<TextMeshProUGUI>();

            probability = GameObject.Find("ReinforcementBeforeAfterEquipProbabilityValue").GetComponent<TextMeshProUGUI>();

            beforeEquipmentIcon = GameObject.Find("ReinforcementBeforeEquipIcon").GetComponent<Image>();
            afterEquipmentIcon = GameObject.Find("ReinforcementAfterEquipIcon").GetComponent<Image>();

            beforeHp = GameObject.Find("ReinforcementBeforeEquipDesHpValue").GetComponent<TextMeshProUGUI>();
            beforeAttack = GameObject.Find("ReinforcementBeforeEquipDesAttackValue").GetComponent<TextMeshProUGUI>();
            beforeDefence = GameObject.Find("ReinforcementBeforeEquipDesDefenseValue").GetComponent<TextMeshProUGUI>();
            beforeSpeed = GameObject.Find("ReinforcementBeforeEquipDesSpeedValue").GetComponent<TextMeshProUGUI>();

            afterHp = GameObject.Find("ReinforcementAfterEquipDesHpValue").GetComponent<TextMeshProUGUI>();
            afterAttack = GameObject.Find("ReinforcementAfterEquipDesAttackValue").GetComponent<TextMeshProUGUI>();
            afterDefence = GameObject.Find("ReinforcementAfterEquipDesDefenseValue").GetComponent<TextMeshProUGUI>();
            afterSpeed = GameObject.Find("ReinforcementAfterEquipDesSpeedValue").GetComponent<TextMeshProUGUI>();

            oxygenValue = GameObject.Find("ReinforcementBeforeAfterOxygenValue").GetComponent<TextMeshProUGUI>();
            ironValue = GameObject.Find("ReinforcementBeforeAfterIronValue").GetComponent<TextMeshProUGUI>();

            highMineralValue = GameObject.Find("ReinforcementHighMineralValue").GetComponent<TextMeshProUGUI>();
            highestMineralValue = GameObject.Find("ReinforcementHighestMineralValue").GetComponent<TextMeshProUGUI>();

            npcText = GameObject.Find("ReinforcementNPCSpeechBubbleText").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            npcTexts = new string[] { "강화의 세계에 온 걸 환영해.",
                                      "너의 운을 시험해 볼래?",
                                      "어이쿠! 손이 미끄러졌네.\n... 농담이야. 화내지 마." };

            // NPC 텍스트 새팅
            SetRandomNpcText();

            SetUIActive(false);
        }

        // NPC 텍스트 세팅
        void SetRandomNpcText()
        {
            // NPC 텍스트 모음 중 한개를 랜덤으로 골라 텍스트 변경
            int index = Random.Range(0, npcTexts.Length);

            npcText.text = npcTexts[index];
        }

        // UI 활성화 여부 세팅
        public void SetUIActive(bool active)
        {
            equipmentName.gameObject.SetActive(active);

            beforeEquipmentGrade.gameObject.SetActive(active);
            afterEquipmentGrade.gameObject.SetActive(active);

            probability.gameObject.SetActive(active);

            beforeEquipmentIcon.gameObject.SetActive(active);
            afterEquipmentIcon.gameObject.SetActive(active);

            beforeHp.gameObject.SetActive(active);
            beforeAttack.gameObject.SetActive(active);
            beforeDefence.gameObject.SetActive(active);
            beforeSpeed.gameObject.SetActive(active);

            afterHp.gameObject.SetActive(active);
            afterAttack.gameObject.SetActive(active);
            afterDefence.gameObject.SetActive(active);
            afterSpeed.gameObject.SetActive(active);

            oxygenValue.gameObject.SetActive(active);
            ironValue.gameObject.SetActive(active);

            highMineralValue.gameObject.SetActive(active);
            highestMineralValue.gameObject.SetActive(active);
        }

        // 강화 전 장비 정보 세팅
        public void SetEquipBeforeInfo(EquipmentItem equip)
        {
            // 장비 이름
            equipmentName.text = equip.ItemName;

            // 장비 등급
            beforeEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade);

            // 장비 아이콘
            beforeEquipmentIcon.sprite = equip.ItemIcon;

            // 장비 능력치 (기본 능력치 + 강화로 추가된 능력치 + 속성 부여로 추가된 능력치)
            beforeHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            beforeAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            beforeDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            beforeSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // 강화 후 장비 정보 세팅 (현재 장비가 더 강화가 가능한 경우)
        public void SetEquipAfterInfo(EquipmentItem equip, ReinforcementValues values, int index)
        {
            // 장비 등급
            // 장비 랭크가 변경되는 경우 다음 랭크로 변경 후 등급을 0으로 초기화
            // 장비 랭크가 변경되지 않는 경우 등급을 1단계 상승
            if (equip.ItemRank != Item.Rank.legend && equip.ItemGrade == 2) afterEquipmentGrade.text = GetRankString(equip.ItemRank + 1) + "(+0)";
            else afterEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade + 1);

            // 장비 아이콘
            afterEquipmentIcon.sprite = equip.ItemIcon;

            // 장비 능력치 (기본 능력치 + 강화로 추가된 능력치 + 속성 부여로 추가된 능력치 + 강화 성공시 추가될 능력치)
            afterHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp + values.Hp[index + 1]).ToString();
            afterAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack + values.Attack[index + 1]).ToString();
            afterDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence + values.Defence[index + 1]).ToString();
            afterSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed + values.Speed[index + 1]).ToString();
        }

        // 강화 후 장비 정보 세팅 (현재 장비가 강화 최대치로 더 강화가 불가능한 경우)
        // 강화 전 장비 정보와 동일하게 세팅
        public void SetEquipAfterInfoMax(EquipmentItem equip)
        {
            // 장비 등급
            afterEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade);

            // 장비 아이콘
            afterEquipmentIcon.sprite = equip.ItemIcon;

            // 장비 능력치 (기본 능력치 + 강화로 추가된 능력치 + 속성 부여로 추가된 능력치)
            afterHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            afterAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            afterDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            afterSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // 강화 정보 세팅 (현재 장비가 더 강화가 가능한 경우)
        public void SetReinforcementInfo(ReinforcementValues values, int index)
        {
            // 강화 확률
            probability.text = values.Probability[index].ToString("00.0");

            // 강화에 소모되는 재화 ('요구랑 / 보유량')
            oxygenValue.text = values.Oxygen[index].ToString("00000") + " / " + GameManager.instance.Oxygen.ToString("00000");
            ironValue.text = values.Iron[index].ToString("00000") + " / " + GameManager.instance.Iron.ToString("00000");

            highMineralValue.text = values.Mineral[index].ToString("00000") + " / " + GameManager.instance.Mineral.ToString("00000");
            highestMineralValue.text = (values.Mineral[index] * 2).ToString("00000") + " / " + GameManager.instance.Mineral.ToString("00000");
        }

        // 강화 정보 세팅 (현재 장비가 강화 최대치로 더 강화가 불가능한 경우)
        // 강화 확률 및 강화에 소모되는 재화 요구량을 0으로 세팅
        public void SetReinforcementInfoMax()
        {
            // 강화 확률
            probability.text = "00.0";

            // 강화에 소모되는 재화 ('요구랑 / 보유량')
            oxygenValue.text = "00000 / " + GameManager.instance.Oxygen.ToString("00000");
            ironValue.text = "00000 / " + GameManager.instance.Iron.ToString("00000");

            highMineralValue.text = "00000 / " + GameManager.instance.Mineral.ToString("00000");
            highestMineralValue.text = "00000 / " + GameManager.instance.Mineral.ToString("00000");
        }

        // 장비 등급에 따른 문자열 반환
        string GetRankString(EquipmentItem.Rank rank)
        {
            switch (rank)
            {
                case Item.Rank.normal:
                    return "노말";
                case Item.Rank.rare:
                    return "레어";
                case Item.Rank.unique:
                    return "유니크";
                case Item.Rank.legend:
                    return "레전드";
                default:
                    return "등급 없음";                 
            }
        }
    }
}
