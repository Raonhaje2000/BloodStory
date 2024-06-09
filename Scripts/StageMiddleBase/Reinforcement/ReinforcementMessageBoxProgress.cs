using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ReinforcementMessageBoxProgress : MonoBehaviour
    {
        TextMeshProUGUI reinforcementMessageBoxTypeText;             // ���� ��ȭ Ÿ�� �ؽ�Ʈ

        TextMeshProUGUI reinforcementMessageBoxOxygenNeedText;       // �ʿ��� ��� �� �ؽ�Ʈ
        TextMeshProUGUI reinforcementMessageBoxOxygenInventoryText;  // ���� ������ ��� �� �ؽ�Ʈ

        TextMeshProUGUI reinforcementMessageBoxIronNeedText;         // �ʿ��� ö�� �� �ؽ�Ʈ
        TextMeshProUGUI reinforcementMessageBoxIronInventoryText;    // ���� ������ ö�� �� �ؽ�Ʈ

        TextMeshProUGUI reinforcementMessageBoxMineralNeedText;      // �ʿ��� �̳׶� �� �ؽ�Ʈ
        TextMeshProUGUI reinforcementMessageBoxMineralInventoryText; // ���� ������ �̳׶� �� �ؽ�Ʈ

        private void Awake()
        {
            // ���� ������Ʈ �ҷ�����
            LoadObejects();
        }

        void Start()
        {

        }

        // ���� ������Ʈ �ҷ�����
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

        // �ش� ��ȭ ������ ���� �޼��� �ڽ� ����
        public void SetMessageBoxProgressText(ReinforcementManager.ReinforcementType type, ReinforcementValues values, int index)
        {
            reinforcementMessageBoxTypeText.text = string.Format("���� \'{0}\' ��ȭ�� �Ұž�?", GetReinforcementTypeString(type));

            reinforcementMessageBoxOxygenNeedText.text = values.Oxygen[index].ToString();
            reinforcementMessageBoxOxygenInventoryText.text = GameManager.instance.Oxygen.ToString();

            reinforcementMessageBoxIronNeedText.text = values.Iron[index].ToString();
            reinforcementMessageBoxIronInventoryText.text = GameManager.instance.Iron.ToString();

            SetMessageBoxMineralTexts(type, values.Mineral[index]);
        }

        // ��ȭ�� �����ϰڴٴ� ��ư�� Ŭ������ ��
        public void ClickYesButton()
        {
            // ��ȭ ���� ó��
            ReinforcementManager.instance.ContinueReinforceProgressButton();
        }

        // ��ȭ�� �������� �ʰڴٴ� ��ư�� Ŭ������ ��
        public void ClickNoButton()
        {
            // ��ȭ ��� ó��
            ReinforcementManager.instance.CancleReinforceProgressButton();
        }

        // ��ȭ Ÿ�Կ� ���� ���ڿ� ���ϱ�
        string GetReinforcementTypeString(ReinforcementManager.ReinforcementType type)
        {
            switch(type)
            {
                case ReinforcementManager.ReinforcementType.normal:
                    {
                        return "�⺻ ��ȭ";
                    }
                case ReinforcementManager.ReinforcementType.high:
                    {
                        return "���� ��ȭ";
                    }
                case ReinforcementManager.ReinforcementType.highest:
                    {
                        return "������ ��ȭ";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        // �̳׶� �ؽ�Ʈ �����ϱ�
        void SetMessageBoxMineralTexts(ReinforcementManager.ReinforcementType type, int needCount)
        {
            // ��ȭ Ÿ�Կ� ���� �ʿ��� �̳׶� �ؽ�Ʈ �����ϱ�
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
