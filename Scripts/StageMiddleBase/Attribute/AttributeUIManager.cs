using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class AttributeUIManager : MonoBehaviour
    {
        public static AttributeUIManager instance;

        const int ATTRIBUTE_COUNT = 4;                     // 부여되는 속성의 최대 가지수

        Image attributeEquipImageIcon;                     // 장비 아이템 아이콘 이미지

        TextMeshProUGUI attributeEquipNameText;            // 장비 아이템 이름 텍스트
        TextMeshProUGUI attributeEquipGradeTextValue;      // 장비 아이템 등급 텍스트

        TextMeshProUGUI attributeEquipDesHpValue;          // 장비 아이템 체력 텍스트
        TextMeshProUGUI attributeEquipDesAttributeHp;      // 속성으로 증가된 장비 아이템 체력 텍스트

        TextMeshProUGUI attributeEquipDesAttackValue;      // 장비 아이템 공격력 텍스트
        TextMeshProUGUI attributeEquipDesAttributeAttack;  // 속성으로 증가된 장비 아이템 공격력 텍스트

        TextMeshProUGUI attributeEquipDesDefenseValue;     // 장비 아이템 방어력 텍스트
        TextMeshProUGUI attributeEquipDesAttributeDefense; // 속성으로 증가된 장비 아이템 방어력 텍스트

        TextMeshProUGUI attributeEquipDesSpeedValue;       // 장비 아이템 이동속도 텍스트
        TextMeshProUGUI attributeEquipDesAttributeSpeed;   // 속성으로 증가된 장비 아이템 이동속도 텍스트

        TextMeshProUGUI attributeTypeTextValue;            // 속성 부여로 증가되는 능력치 종류 텍스트

        Image[] attributeVitaminsInactive;                 // 속성 부여 아이템 (비타민) 비활성화 이미지들
        TextMeshProUGUI[] attributeVitaminsQualityText;    // 속성 부여된 비타민의 품질 텍스트들
        TextMeshProUGUI[] attributeVitaminsValueText;      // 속성 부여로 증가된 능력치 값 텍스트들

        private void Awake()
        {
            if(instance == null) instance = this;

            // 관련 오브젝트 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 오브젝트 불러오기
        void LoadObjects()
        {
            attributeEquipImageIcon = GameObject.Find("AttributeEquipImageIcon").GetComponent<Image>();

            attributeEquipNameText = GameObject.Find("AttributeEquipNameText").GetComponent<TextMeshProUGUI>();
            attributeEquipGradeTextValue = GameObject.Find("AttributeEquipGradeTextValue").GetComponent<TextMeshProUGUI>();

            attributeEquipDesHpValue = GameObject.Find("AttributeEquipDesHpValue").GetComponent<TextMeshProUGUI>();
            attributeEquipDesAttributeHp = GameObject.Find("AttributeEquipDesAttributeHp").GetComponent<TextMeshProUGUI>();

            attributeEquipDesAttackValue = GameObject.Find("AttributeEquipDesAttackValue").GetComponent<TextMeshProUGUI>();
            attributeEquipDesAttributeAttack = GameObject.Find("AttributeEquipDesAttributeAttack").GetComponent<TextMeshProUGUI>();

            attributeEquipDesDefenseValue = GameObject.Find("AttributeEquipDesDefenseValue").GetComponent<TextMeshProUGUI>();
            attributeEquipDesAttributeDefense = GameObject.Find("AttributeEquipDesAttributeDefense").GetComponent<TextMeshProUGUI>();

            attributeEquipDesSpeedValue = GameObject.Find("AttributeEquipDesSpeedValue").GetComponent<TextMeshProUGUI>();
            attributeEquipDesAttributeSpeed = GameObject.Find("AttributeEquipDesAttributeSpeed").GetComponent<TextMeshProUGUI>();

            attributeTypeTextValue = GameObject.Find("AttributeTypeTextValue").GetComponent<TextMeshProUGUI>();

            attributeVitaminsInactive = new Image[] { GameObject.Find("AttributeVitaminDInactive").GetComponent<Image>(),
                                                      GameObject.Find("AttributeVitaminCInactive").GetComponent<Image>(),
                                                      GameObject.Find("AttributeVitaminBInactive").GetComponent<Image>(),
                                                      GameObject.Find("AttributeVitaminAInactive").GetComponent<Image>() };

            attributeVitaminsQualityText = new TextMeshProUGUI[] { GameObject.Find("AttributeVitaminDQualityText").GetComponent<TextMeshProUGUI>(),
                                                                   GameObject.Find("AttributeVitaminCQualityText").GetComponent<TextMeshProUGUI>(),
                                                                   GameObject.Find("AttributeVitaminBQualityText").GetComponent<TextMeshProUGUI>(),
                                                                   GameObject.Find("AttributeVitaminAQualityText").GetComponent<TextMeshProUGUI>() };

            attributeVitaminsValueText = new TextMeshProUGUI[] { GameObject.Find("AttributeVitaminDValueText").GetComponent<TextMeshProUGUI>(),
                                                                 GameObject.Find("AttributeVitaminCValueText").GetComponent<TextMeshProUGUI>(),
                                                                 GameObject.Find("AttributeVitaminBValueText").GetComponent<TextMeshProUGUI>(),
                                                                 GameObject.Find("AttributeVitaminAValueText").GetComponent<TextMeshProUGUI>() };
        }

        // 초기화
        public void Initialize()
        {
            // 속성 부여 정보 세팅
            SetAttributeInfo();
        }

        // 속성 부여 정보 세팅
        void SetAttributeInfo()
        {
            SetAttributeEquipDesActive(false); // UI 내용 비활성화
            SetAttributeItemInit();            // 부여된 속성 텍스트 초기화
        }

        // UI 활성화 여부 세팅
        void SetAttributeEquipDesActive(bool active)
        {
            attributeEquipImageIcon.gameObject.SetActive(active);

            attributeEquipNameText.gameObject.SetActive(active);
            attributeEquipGradeTextValue.gameObject.SetActive(active);

            attributeEquipDesHpValue.gameObject.SetActive(active);
            attributeEquipDesAttributeHp.gameObject.SetActive(active);

            attributeEquipDesAttackValue.gameObject.SetActive(active);
            attributeEquipDesAttributeAttack.gameObject.SetActive(active);

            attributeEquipDesDefenseValue.gameObject.SetActive(active);
            attributeEquipDesAttributeDefense.gameObject.SetActive(active);

            attributeEquipDesSpeedValue.gameObject.SetActive(active);
            attributeEquipDesAttributeSpeed.gameObject.SetActive(active);
        }

        // 현재 선택된 장비 아이템 정보 세팅
        void SetEquipInfo(EquipmentItem equip)
        {
            attributeEquipImageIcon.sprite = equip.ItemIcon;

            attributeEquipNameText.text = equip.ItemName;
            attributeEquipGradeTextValue.text = GetRankString(equip.ItemRank);

            attributeEquipDesHpValue.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp).ToString();
            attributeEquipDesAttributeHp.text = string.Format("(+{0})", equip.AttributeStatus.Hp);

            attributeEquipDesAttackValue.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack).ToString();
            attributeEquipDesAttributeAttack.text = string.Format("(+{0})", equip.AttributeStatus.Attack);

            attributeEquipDesDefenseValue.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence).ToString();
            attributeEquipDesAttributeDefense.text = string.Format("(+{0})", equip.AttributeStatus.Defence);

            attributeEquipDesSpeedValue.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed).ToString();
            attributeEquipDesAttributeSpeed.text = string.Format("(+{0})", equip.AttributeStatus.Speed);

            attributeTypeTextValue.text = GetAttibuteTypeString(equip.EquipAttributeInstallation.AttributeType);
        }

        // 부여된 속성 텍스트 초기화
        void SetAttributeItemInit()
        {
            attributeTypeTextValue.text = "????";

            for(int i = 0; i < ATTRIBUTE_COUNT; i++)
            {
                attributeVitaminsInactive[i].gameObject.SetActive(true);
                attributeVitaminsQualityText[i].text = "-";
                attributeVitaminsValueText[i].text = "-";
            }
        }

        // 속성 부여할 장비 정보 세팅
        public void SetAttributeItemEquip(EquipmentItem equip)
        {
            SetAttributeEquipDesActive(true); // UI 내용 활성화
            SetEquipInfo(equip);              // 현재 선택된 장비 아이템 정보 세팅
            SetVitamins(equip);               // 현재 선택된 장비에 부여된 속성(비타민) 정보 세팅
        }

        // 현재 선택된 장비에 부여된 속성(비타민) 정보 세팅
        public void SetVitamins(EquipmentItem equip)
        {
            for(int i = 0; i < ATTRIBUTE_COUNT; i++)
            {
                if ((int)equip.ItemRank > i)
                {
                    // 현재 장비의 랭크가 속성 부여칸 보다 큰 경우 (현재 장비에서 부여 가능한 비타민일 경우)

                    // 비타민 비활성화 이미지 비활성화
                    attributeVitaminsInactive[i].gameObject.SetActive(false);

                    if (equip.EquipAttributeInstallation.VitaminsQuality[i] >= 0)
                    {
                        // 속성이 부여되어 품질이 0 이상인 경우
                        // 해당 품질과 속성값으로 텍스트 세팅
                        attributeVitaminsQualityText[i].text = equip.EquipAttributeInstallation.VitaminsQuality[i].ToString();
                        attributeVitaminsValueText[i].text = "+" + equip.EquipAttributeInstallation.VitaminsValue[i].ToString();
                    }
                    else
                    {
                        // 속성이 부여되지않아 품질이 -1인 경우
                        // ??? 으로 텍스트 세팅
                        attributeVitaminsQualityText[i].text = "???";
                        attributeVitaminsValueText[i].text = "+??";
                    }
                }
                else
                {
                    // 현재 장비의 랭크가 속성 부여칸 보다 작은 경우 (현재 장비에서 부여 불가능한 비타민일 경우)

                    // 비타민 비활성화 이미지 활성화
                    // -으로 텍스트 세팅
                    attributeVitaminsInactive[i].gameObject.SetActive(true);
                    attributeVitaminsQualityText[i].text = "-";
                    attributeVitaminsValueText[i].text = "-";
                }
            }

        }

        // 해당 장비 아이템의 랭크 문자열 구하기
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

        // 해당 장비 아이템에서 속성 부여로 증가되는 능력치 문자열 구하기
        string GetAttibuteTypeString(AttributeInstallation.Type type)
        {
            switch (type)
            {
                case AttributeInstallation.Type.hp:
                    return "체력";
                case AttributeInstallation.Type.attack:
                    return "공격력";
                case AttributeInstallation.Type.defence:
                    return "방어력";
                case AttributeInstallation.Type.speed:
                    return "이동속도";
                default:
                    return "????";
            }
        }
    }
}
