using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA_Proto
{
    public class AttributeUIManager : MonoBehaviour
    {
        public static AttributeUIManager instance;

        Image equipIcon;
        TextMeshProUGUI equipNameText;
        TextMeshProUGUI equipGradeText;

        TextMeshProUGUI equipStateText1;
        TextMeshProUGUI equipStateText2;
        TextMeshProUGUI equipStateText3;
        TextMeshProUGUI equipStateText4;

        TextMeshProUGUI equipValueText1;
        TextMeshProUGUI equipValueText2;
        TextMeshProUGUI equipValueText3;
        TextMeshProUGUI equipValueText4;

        Image vita1_Icon;
        TextMeshProUGUI Vita1Text;

        Image vita2_Icon;
        TextMeshProUGUI Vita2Text;

        Image vita3_Icon;
        TextMeshProUGUI Vita3Text;

        Image vita4_Icon;
        TextMeshProUGUI Vita4Text;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            equipIcon = GameObject.Find("EquipIcon").GetComponent<Image>();
            equipNameText = GameObject.Find("EquipNameText").GetComponent<TextMeshProUGUI>();
            equipGradeText = GameObject.Find("EquipGradeText").GetComponent<TextMeshProUGUI>();

            equipStateText1 = GameObject.Find("EquipStateText1").GetComponent<TextMeshProUGUI>();
            equipStateText2 = GameObject.Find("EquipStateText2").GetComponent<TextMeshProUGUI>();
            equipStateText3 = GameObject.Find("EquipStateText3").GetComponent<TextMeshProUGUI>();
            equipStateText4 = GameObject.Find("EquipStateText4").GetComponent<TextMeshProUGUI>();

            equipValueText1 = GameObject.Find("EquipValueText1").GetComponent<TextMeshProUGUI>();
            equipValueText2 = GameObject.Find("EquipValueText2").GetComponent<TextMeshProUGUI>();
            equipValueText3 = GameObject.Find("EquipValueText3").GetComponent<TextMeshProUGUI>();
            equipValueText4 = GameObject.Find("EquipValueText4").GetComponent<TextMeshProUGUI>();

            vita1_Icon = GameObject.Find("Vita1_Icon").GetComponent<Image>();
            Vita1Text = GameObject.Find("Vita1Text").GetComponent<TextMeshProUGUI>();
            vita2_Icon = GameObject.Find("Vita2_Icon").GetComponent<Image>();
            Vita2Text = GameObject.Find("Vita2Text").GetComponent<TextMeshProUGUI>();
            vita3_Icon = GameObject.Find("Vita3_Icon").GetComponent<Image>();
            Vita3Text = GameObject.Find("Vita3Text").GetComponent<TextMeshProUGUI>();
            vita4_Icon = GameObject.Find("Vita4_Icon").GetComponent<Image>();
            Vita4Text = GameObject.Find("Vita4Text").GetComponent<TextMeshProUGUI>();
        }

        public void SetActiveEquip(bool active)
        {
            equipIcon.gameObject.SetActive(active);
            equipNameText.gameObject.SetActive(active);
            equipGradeText.gameObject.SetActive(active);

            equipStateText1.gameObject.SetActive(active);
            equipStateText2.gameObject.SetActive(active);
            equipStateText3.gameObject.SetActive(active);
            equipStateText4.gameObject.SetActive(active);

            equipValueText1.gameObject.SetActive(active);
            equipValueText2.gameObject.SetActive(active);
            equipValueText3.gameObject.SetActive(active);
            equipValueText4.gameObject.SetActive(active);
        }

        public void ChangeAttributeEquip(Equipment equip)
        {
            string[] gradeStr = { "노말", "레어", "유니크", "레전드" };

            equipIcon.sprite = equip.Icon;
            equipNameText.text = equip.Name;
            equipGradeText.text = gradeStr[equip.Grade / 3];

            equipStateText1.text = "체력";
            equipStateText2.text = "공격력";
            equipStateText3.text = "방어력";
            equipStateText4.text = "이동속도";

            equipValueText1.text = (": " + equip.Hp.ToString()) + ((equip.AttributeHp <= 0) ? "" : " ( +" + equip.AttributeHp + " )");
            equipValueText2.text = (": " + equip.Attack.ToString()) + ((equip.AttributeAttack <= 0) ? "" : " ( +" + equip.AttributeAttack + " )");
            equipValueText3.text = (": " + equip.Defence.ToString()) + ((equip.AttributeDefence <= 0) ? "" : " ( +" + equip.AttributeDefence + " )");
            equipValueText4.text = (": " + equip.MoveSpeed.ToString()) + ((equip.AttributeMoveSpeed <= 0) ? "" : " ( +" + equip.AttributeMoveSpeed + " )");
        }

        public void SetActiveAttribute(bool active)
        {
            vita1_Icon.gameObject.SetActive(active);
            Vita1Text.gameObject.SetActive(active);

            vita2_Icon.gameObject.SetActive(active);
            Vita2Text.gameObject.SetActive(active);

            vita3_Icon.gameObject.SetActive(active);
            Vita3Text.gameObject.SetActive(active);

            vita4_Icon.gameObject.SetActive(active);
            Vita4Text.gameObject.SetActive(active);
        }

        public void ChangeAttributeSlot1(Sprite icon, string text)
        {
            vita1_Icon.gameObject.SetActive(true);
            Vita1Text.gameObject.SetActive(true);

            vita2_Icon.gameObject.SetActive(false);
            Vita2Text.gameObject.SetActive(false);

            vita3_Icon.gameObject.SetActive(false);
            Vita3Text.gameObject.SetActive(false);

            vita4_Icon.gameObject.SetActive(false);
            Vita4Text.gameObject.SetActive(false);

            vita1_Icon.sprite = icon;
            Vita1Text.text = text;
        }
        public void ChangeAttributeSlot2(Sprite icon, string text)
        {
            vita2_Icon.gameObject.SetActive(true);
            Vita2Text.gameObject.SetActive(true);

            vita3_Icon.gameObject.SetActive(false);
            Vita3Text.gameObject.SetActive(false);

            vita4_Icon.gameObject.SetActive(false);
            Vita4Text.gameObject.SetActive(false);

            vita2_Icon.sprite = icon;
            Vita2Text.text = text;
        }

        public void ChangeAttributeSlot3(Sprite icon, string text)
        {
            vita3_Icon.gameObject.SetActive(true);
            Vita3Text.gameObject.SetActive(true);

            vita4_Icon.gameObject.SetActive(false);
            Vita4Text.gameObject.SetActive(false);

            vita3_Icon.sprite = icon;
            Vita3Text.text = text;
        }

        public void ChangeAttributeSlot4(Sprite icon, string text)
        {
            vita4_Icon.gameObject.SetActive(true);
            Vita4Text.gameObject.SetActive(true);

            vita4_Icon.sprite = icon;
            Vita4Text.text = text;
        }
    }
}