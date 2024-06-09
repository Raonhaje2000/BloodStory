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

        const int ATTRIBUTE_COUNT = 4;                     // �ο��Ǵ� �Ӽ��� �ִ� ������

        Image attributeEquipImageIcon;                     // ��� ������ ������ �̹���

        TextMeshProUGUI attributeEquipNameText;            // ��� ������ �̸� �ؽ�Ʈ
        TextMeshProUGUI attributeEquipGradeTextValue;      // ��� ������ ��� �ؽ�Ʈ

        TextMeshProUGUI attributeEquipDesHpValue;          // ��� ������ ü�� �ؽ�Ʈ
        TextMeshProUGUI attributeEquipDesAttributeHp;      // �Ӽ����� ������ ��� ������ ü�� �ؽ�Ʈ

        TextMeshProUGUI attributeEquipDesAttackValue;      // ��� ������ ���ݷ� �ؽ�Ʈ
        TextMeshProUGUI attributeEquipDesAttributeAttack;  // �Ӽ����� ������ ��� ������ ���ݷ� �ؽ�Ʈ

        TextMeshProUGUI attributeEquipDesDefenseValue;     // ��� ������ ���� �ؽ�Ʈ
        TextMeshProUGUI attributeEquipDesAttributeDefense; // �Ӽ����� ������ ��� ������ ���� �ؽ�Ʈ

        TextMeshProUGUI attributeEquipDesSpeedValue;       // ��� ������ �̵��ӵ� �ؽ�Ʈ
        TextMeshProUGUI attributeEquipDesAttributeSpeed;   // �Ӽ����� ������ ��� ������ �̵��ӵ� �ؽ�Ʈ

        TextMeshProUGUI attributeTypeTextValue;            // �Ӽ� �ο��� �����Ǵ� �ɷ�ġ ���� �ؽ�Ʈ

        Image[] attributeVitaminsInactive;                 // �Ӽ� �ο� ������ (��Ÿ��) ��Ȱ��ȭ �̹�����
        TextMeshProUGUI[] attributeVitaminsQualityText;    // �Ӽ� �ο��� ��Ÿ���� ǰ�� �ؽ�Ʈ��
        TextMeshProUGUI[] attributeVitaminsValueText;      // �Ӽ� �ο��� ������ �ɷ�ġ �� �ؽ�Ʈ��

        private void Awake()
        {
            if(instance == null) instance = this;

            // ���� ������Ʈ �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������Ʈ �ҷ�����
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

        // �ʱ�ȭ
        public void Initialize()
        {
            // �Ӽ� �ο� ���� ����
            SetAttributeInfo();
        }

        // �Ӽ� �ο� ���� ����
        void SetAttributeInfo()
        {
            SetAttributeEquipDesActive(false); // UI ���� ��Ȱ��ȭ
            SetAttributeItemInit();            // �ο��� �Ӽ� �ؽ�Ʈ �ʱ�ȭ
        }

        // UI Ȱ��ȭ ���� ����
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

        // ���� ���õ� ��� ������ ���� ����
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

        // �ο��� �Ӽ� �ؽ�Ʈ �ʱ�ȭ
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

        // �Ӽ� �ο��� ��� ���� ����
        public void SetAttributeItemEquip(EquipmentItem equip)
        {
            SetAttributeEquipDesActive(true); // UI ���� Ȱ��ȭ
            SetEquipInfo(equip);              // ���� ���õ� ��� ������ ���� ����
            SetVitamins(equip);               // ���� ���õ� ��� �ο��� �Ӽ�(��Ÿ��) ���� ����
        }

        // ���� ���õ� ��� �ο��� �Ӽ�(��Ÿ��) ���� ����
        public void SetVitamins(EquipmentItem equip)
        {
            for(int i = 0; i < ATTRIBUTE_COUNT; i++)
            {
                if ((int)equip.ItemRank > i)
                {
                    // ���� ����� ��ũ�� �Ӽ� �ο�ĭ ���� ū ��� (���� ��񿡼� �ο� ������ ��Ÿ���� ���)

                    // ��Ÿ�� ��Ȱ��ȭ �̹��� ��Ȱ��ȭ
                    attributeVitaminsInactive[i].gameObject.SetActive(false);

                    if (equip.EquipAttributeInstallation.VitaminsQuality[i] >= 0)
                    {
                        // �Ӽ��� �ο��Ǿ� ǰ���� 0 �̻��� ���
                        // �ش� ǰ���� �Ӽ������� �ؽ�Ʈ ����
                        attributeVitaminsQualityText[i].text = equip.EquipAttributeInstallation.VitaminsQuality[i].ToString();
                        attributeVitaminsValueText[i].text = "+" + equip.EquipAttributeInstallation.VitaminsValue[i].ToString();
                    }
                    else
                    {
                        // �Ӽ��� �ο������ʾ� ǰ���� -1�� ���
                        // ??? ���� �ؽ�Ʈ ����
                        attributeVitaminsQualityText[i].text = "???";
                        attributeVitaminsValueText[i].text = "+??";
                    }
                }
                else
                {
                    // ���� ����� ��ũ�� �Ӽ� �ο�ĭ ���� ���� ��� (���� ��񿡼� �ο� �Ұ����� ��Ÿ���� ���)

                    // ��Ÿ�� ��Ȱ��ȭ �̹��� Ȱ��ȭ
                    // -���� �ؽ�Ʈ ����
                    attributeVitaminsInactive[i].gameObject.SetActive(true);
                    attributeVitaminsQualityText[i].text = "-";
                    attributeVitaminsValueText[i].text = "-";
                }
            }

        }

        // �ش� ��� �������� ��ũ ���ڿ� ���ϱ�
        string GetRankString(EquipmentItem.Rank rank)
        {
            switch (rank)
            {
                case Item.Rank.normal:
                    return "�븻";
                case Item.Rank.rare:
                    return "����";
                case Item.Rank.unique:
                    return "����ũ";
                case Item.Rank.legend:
                    return "������";
                default:
                    return "��� ����";
            }
        }

        // �ش� ��� �����ۿ��� �Ӽ� �ο��� �����Ǵ� �ɷ�ġ ���ڿ� ���ϱ�
        string GetAttibuteTypeString(AttributeInstallation.Type type)
        {
            switch (type)
            {
                case AttributeInstallation.Type.hp:
                    return "ü��";
                case AttributeInstallation.Type.attack:
                    return "���ݷ�";
                case AttributeInstallation.Type.defence:
                    return "����";
                case AttributeInstallation.Type.speed:
                    return "�̵��ӵ�";
                default:
                    return "????";
            }
        }
    }
}
