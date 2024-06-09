using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ReinforcementMessageBoxResult : MonoBehaviour
    {
        Transform reinforcementEffectPoint;             // ��ȭ ����Ʈ�� ������ ��ġ
 
        GameObject successEffect;                       // ���� ����Ʈ ������
        GameObject failEffect;                          // ���� ����Ʈ ������

        TextMeshProUGUI resultSuccessOrFailText;        // ���� �Ǵ� ���������� �����ִ� �ؽ�Ʈ
        TextMeshProUGUI resultEquipStateText;           // ����� ���� (��� ���, ����, �϶�)�� �����ִ� �ؽ�Ʈ

        Image resultEquipmentIconImage;                 // ����� ������ �̹���
        TextMeshProUGUI resultEquipmentNameText;        // ����� �̸� �ؽ�Ʈ
        TextMeshProUGUI resultEquipmentGradeText;       // ����� ��� �ؽ�Ʈ

        TextMeshProUGUI resultEquipmentDesHpValue;      // ����� ü�� ��ġ
        TextMeshProUGUI resultEquipmentDesAttackValue;  // ����� ���ݷ� ��ġ
        TextMeshProUGUI resultEquipmentDesDefenceValue; // ����� ���� ��ġ
        TextMeshProUGUI resultEquipmentDesSpeedValue;   // ����� �̵� �ӵ� ��ġ

        GameObject effect;                              // ��ȭ ����Ʈ

        private void Awake()
        {
            // ���õ� ������Ʈ �ҷ�����
            LoadObejcts();
        }

        void Start()
        {

        }

        // ���õ� ������Ʈ �ҷ�����
        void LoadObejcts()
        {
            reinforcementEffectPoint = GameObject.Find("ReinforcementMessageBoxResultPoint").transform;

            successEffect = Resources.Load<GameObject>("RSA/Prefabs/Effect/SuccessEffect");
            failEffect = Resources.Load<GameObject>("RSA/Prefabs/Effect/FailEffect");

            resultSuccessOrFailText = GameObject.Find("ReinforcementMessageBoxResultSuccessOrFailText").GetComponent<TextMeshProUGUI>();
            resultEquipStateText = GameObject.Find("ReinforcementMessageBoxResultEquipStateText").GetComponent<TextMeshProUGUI>();

            resultEquipmentIconImage = GameObject.Find("ReinforcementMessageBoxResultEquipmentIconImage").GetComponent<Image>();
            resultEquipmentNameText = GameObject.Find("ReinforcementMessageBoxResultEquipmentNameText").GetComponent<TextMeshProUGUI>();
            resultEquipmentGradeText = GameObject.Find("ReinforcementMessageBoxResultEquipmentGradeText").GetComponent<TextMeshProUGUI>();

            resultEquipmentDesHpValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesHpValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesAttackValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesAttackValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesDefenceValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesDefenceValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesSpeedValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesSpeedValue").GetComponent<TextMeshProUGUI>();

            effect = null;
        }


        // ��ȭ ��� �޼��� �ڽ� ����
        public void SetMessageBoxResult(EquipmentItem equip, ReinforcementManager.ReinforcementState state)
        {
            // ��ȭ ����Ʈ(��ƼŬ) ����
            effect = (state == ReinforcementManager.ReinforcementState.up)
                ? Instantiate(successEffect, reinforcementEffectPoint.position, reinforcementEffectPoint.rotation)
                    : Instantiate(failEffect, reinforcementEffectPoint.position, reinforcementEffectPoint.rotation);
            effect.transform.SetParent(reinforcementEffectPoint);
            effect.transform.SetAsLastSibling();

            string stateString
                = (state == ReinforcementManager.ReinforcementState.up) ? "����߾�"
                    : (state == ReinforcementManager.ReinforcementState.keep) ? "�����ƾ�" : "�϶��߾�";

            if (state == ReinforcementManager.ReinforcementState.up) resultSuccessOrFailText.text = "��! ��ȭ �����̾�!";
            else resultSuccessOrFailText.text = "�̾���. ��ȭ �����߾�.";

            resultEquipStateText.text = "��� �������� ����� " + stateString + ".";

            resultEquipmentIconImage.sprite = equip.ItemIcon;
            resultEquipmentNameText.text = equip.ItemName;
            resultEquipmentGradeText.text = string.Format("{0}(+{1})", GetRankString(equip.ItemRank), equip.ItemGrade);

            resultEquipmentDesHpValue.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            resultEquipmentDesAttackValue.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            resultEquipmentDesDefenceValue.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            resultEquipmentDesSpeedValue.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // X ��ư�� ������ ��
        public void ClickButtonX()
        {
            // ����Ʈ ������Ʈ ����
            Destroy(effect.gameObject);

            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }

        // ��ũ�� ���� ���ڿ� ���ϱ�
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
