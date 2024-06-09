using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RSA
{
    public class UserInventoryMoneyManager : MonoBehaviour
    {
        public static UserInventoryMoneyManager instance;

        TextMeshProUGUI inventoryOxygenText;  // �κ��丮�� �ִ� ��� �ؽ�Ʈ
        TextMeshProUGUI inventoryIronText;    // �κ��丮�� �ִ� ö�� �ؽ�Ʈ
        TextMeshProUGUI inventoryMineralText; // �κ��丮�� �ִ� �̳׶� �ؽ�Ʈ

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
            inventoryOxygenText = GameObject.Find("InventoryOxygenText").GetComponent<TextMeshProUGUI>();
            inventoryIronText = GameObject.Find("InventoryIronText").GetComponent<TextMeshProUGUI>();
            inventoryMineralText = GameObject.Find("InventoryMineralText").GetComponent<TextMeshProUGUI>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // ���� �κ��丮�� ���� ��ȭ ���� (�ؽ�Ʈ UI ����)
            SetUserInventoryMoney();
        }

        // ���� �κ��丮�� ���� ��ȭ ���� (�ؽ�Ʈ UI ����)
        public void SetUserInventoryMoney()
        {
            // ���� ������ ���� ��ȭ��ŭ �ؽ�Ʈ UI ����
            inventoryOxygenText.text = GameManager.instance.Oxygen.ToString();
            inventoryIronText.text = GameManager.instance.Iron.ToString();
            inventoryMineralText.text = GameManager.instance.Mineral.ToString();
        }
    }
}
