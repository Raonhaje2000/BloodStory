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

        TextMeshProUGUI equipmentName;        // ���� ��� ������ �̸� �ؽ�Ʈ

        TextMeshProUGUI beforeEquipmentGrade; // ��ȭ �� ��� ������ ��� �ؽ�Ʈ
        TextMeshProUGUI afterEquipmentGrade;  // ��ȭ �� ��� ������ ��� �ؽ�Ʈ

        TextMeshProUGUI probability;          // ��ȭ Ȯ�� �ؽ�Ʈ

        Image beforeEquipmentIcon;            // ��ȭ �� ��� ������ ������ �̹���
        Image afterEquipmentIcon;             // ��ȭ �� ��� ������ ������ �̹���
  
        TextMeshProUGUI beforeHp;             // ��ȭ �� ü�� �ؽ�Ʈ
        TextMeshProUGUI beforeAttack;         // ��ȭ �� ���ݷ� �ؽ�Ʈ
        TextMeshProUGUI beforeDefence;        // ��ȭ �� ���� �ؽ�Ʈ
        TextMeshProUGUI beforeSpeed;          // ��ȭ �� �̵��ӵ� �ؽ�Ʈ
 
        TextMeshProUGUI afterHp;              // ��ȭ �� ü�� �ؽ�Ʈ
        TextMeshProUGUI afterAttack;          // ��ȭ �� ���ݷ� �ؽ�Ʈ
        TextMeshProUGUI afterDefence;         // ��ȭ �� ���� �ؽ�Ʈ
        TextMeshProUGUI afterSpeed;           // ��ȭ �� �̵��ӵ� �ؽ�Ʈ

        TextMeshProUGUI oxygenValue;          // ��� �ؽ�Ʈ
        TextMeshProUGUI ironValue;            // ö�� �ؽ�Ʈ

        TextMeshProUGUI highMineralValue;     // ���� ��ȭ �̳׶� �ؽ�Ʈ
        TextMeshProUGUI highestMineralValue;  // ������ ��ȭ �̳׶� �ؽ�Ʈ

        TextMeshProUGUI npcText;              // NPC ��� �ؽ�Ʈ
        string[] npcTexts;                    // NPC ��� �ؽ�Ʈ ����

        
        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ�� �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������Ʈ�� �ҷ�����
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

        // �ʱ�ȭ
        void Initialize()
        {
            npcTexts = new string[] { "��ȭ�� ���迡 �� �� ȯ����.",
                                      "���� ���� ������ ����?",
                                      "������! ���� �̲�������.\n... ����̾�. ȭ���� ��." };

            // NPC �ؽ�Ʈ ����
            SetRandomNpcText();

            SetUIActive(false);
        }

        // NPC �ؽ�Ʈ ����
        void SetRandomNpcText()
        {
            // NPC �ؽ�Ʈ ���� �� �Ѱ��� �������� ��� �ؽ�Ʈ ����
            int index = Random.Range(0, npcTexts.Length);

            npcText.text = npcTexts[index];
        }

        // UI Ȱ��ȭ ���� ����
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

        // ��ȭ �� ��� ���� ����
        public void SetEquipBeforeInfo(EquipmentItem equip)
        {
            // ��� �̸�
            equipmentName.text = equip.ItemName;

            // ��� ���
            beforeEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade);

            // ��� ������
            beforeEquipmentIcon.sprite = equip.ItemIcon;

            // ��� �ɷ�ġ (�⺻ �ɷ�ġ + ��ȭ�� �߰��� �ɷ�ġ + �Ӽ� �ο��� �߰��� �ɷ�ġ)
            beforeHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            beforeAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            beforeDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            beforeSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // ��ȭ �� ��� ���� ���� (���� ��� �� ��ȭ�� ������ ���)
        public void SetEquipAfterInfo(EquipmentItem equip, ReinforcementValues values, int index)
        {
            // ��� ���
            // ��� ��ũ�� ����Ǵ� ��� ���� ��ũ�� ���� �� ����� 0���� �ʱ�ȭ
            // ��� ��ũ�� ������� �ʴ� ��� ����� 1�ܰ� ���
            if (equip.ItemRank != Item.Rank.legend && equip.ItemGrade == 2) afterEquipmentGrade.text = GetRankString(equip.ItemRank + 1) + "(+0)";
            else afterEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade + 1);

            // ��� ������
            afterEquipmentIcon.sprite = equip.ItemIcon;

            // ��� �ɷ�ġ (�⺻ �ɷ�ġ + ��ȭ�� �߰��� �ɷ�ġ + �Ӽ� �ο��� �߰��� �ɷ�ġ + ��ȭ ������ �߰��� �ɷ�ġ)
            afterHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp + values.Hp[index + 1]).ToString();
            afterAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack + values.Attack[index + 1]).ToString();
            afterDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence + values.Defence[index + 1]).ToString();
            afterSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed + values.Speed[index + 1]).ToString();
        }

        // ��ȭ �� ��� ���� ���� (���� ��� ��ȭ �ִ�ġ�� �� ��ȭ�� �Ұ����� ���)
        // ��ȭ �� ��� ������ �����ϰ� ����
        public void SetEquipAfterInfoMax(EquipmentItem equip)
        {
            // ��� ���
            afterEquipmentGrade.text = GetRankString(equip.ItemRank) + string.Format("(+{0})", equip.ItemGrade);

            // ��� ������
            afterEquipmentIcon.sprite = equip.ItemIcon;

            // ��� �ɷ�ġ (�⺻ �ɷ�ġ + ��ȭ�� �߰��� �ɷ�ġ + �Ӽ� �ο��� �߰��� �ɷ�ġ)
            afterHp.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            afterAttack.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            afterDefence.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            afterSpeed.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // ��ȭ ���� ���� (���� ��� �� ��ȭ�� ������ ���)
        public void SetReinforcementInfo(ReinforcementValues values, int index)
        {
            // ��ȭ Ȯ��
            probability.text = values.Probability[index].ToString("00.0");

            // ��ȭ�� �Ҹ�Ǵ� ��ȭ ('�䱸�� / ������')
            oxygenValue.text = values.Oxygen[index].ToString("00000") + " / " + GameManager.instance.Oxygen.ToString("00000");
            ironValue.text = values.Iron[index].ToString("00000") + " / " + GameManager.instance.Iron.ToString("00000");

            highMineralValue.text = values.Mineral[index].ToString("00000") + " / " + GameManager.instance.Mineral.ToString("00000");
            highestMineralValue.text = (values.Mineral[index] * 2).ToString("00000") + " / " + GameManager.instance.Mineral.ToString("00000");
        }

        // ��ȭ ���� ���� (���� ��� ��ȭ �ִ�ġ�� �� ��ȭ�� �Ұ����� ���)
        // ��ȭ Ȯ�� �� ��ȭ�� �Ҹ�Ǵ� ��ȭ �䱸���� 0���� ����
        public void SetReinforcementInfoMax()
        {
            // ��ȭ Ȯ��
            probability.text = "00.0";

            // ��ȭ�� �Ҹ�Ǵ� ��ȭ ('�䱸�� / ������')
            oxygenValue.text = "00000 / " + GameManager.instance.Oxygen.ToString("00000");
            ironValue.text = "00000 / " + GameManager.instance.Iron.ToString("00000");

            highMineralValue.text = "00000 / " + GameManager.instance.Mineral.ToString("00000");
            highestMineralValue.text = "00000 / " + GameManager.instance.Mineral.ToString("00000");
        }

        // ��� ��޿� ���� ���ڿ� ��ȯ
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
    }
}
