using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class DurabilityFixMessageBox : MonoBehaviour
    {
        TextMeshProUGUI weaponCostDurabilityValue;       // �����ؾ��ϴ� ���� ������ ��ġ �ؽ�Ʈ
        TextMeshProUGUI weaponCostDurabilityMoney;       // ���� ������ ���� ��� �ؽ�Ʈ
        TextMeshProUGUI weaponCostDurabilityTotalMoney;  // ���� ������ ���� �� ��� �ؽ�Ʈ

        TextMeshProUGUI defenceCostDurabilityValue;      // �����ؾ��ϴ� �� ������ ��ġ �ؽ�Ʈ
        TextMeshProUGUI defenceCostDurabilityMoney;      // �� ������ ���� ��� �ؽ�Ʈ
        TextMeshProUGUI defenceCostDurabilityTotalMoney; // �� ������ ���� �� ��� �ؽ�Ʈ

        TextMeshProUGUI totalText; // ����� �� ������ ���� �� ��� �ؽ�Ʈ

        int weaponMoney;  // ���� ������ ���� ���
        int defenceMoney; // �� ������ ���� ���

        int totalMoney;   // ����� �� ������ ���� �� ��� 

        private void Awake()
        {
            LoadObjects(); // ���� ������Ʈ �ҷ�����
            Initialize();  // �ʱ�ȭ
        }

        void Start()
        {

        }

        // ���� ������Ʈ �ҷ�����
        void LoadObjects()
        {
            weaponCostDurabilityValue = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityValue").GetComponent<TextMeshProUGUI>();
            weaponCostDurabilityMoney = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityMoney").GetComponent<TextMeshProUGUI>();
            weaponCostDurabilityTotalMoney = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();

            defenceCostDurabilityValue = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityValue").GetComponent<TextMeshProUGUI>();
            defenceCostDurabilityMoney = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityMoney").GetComponent<TextMeshProUGUI>();
            defenceCostDurabilityTotalMoney = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();

            totalText = GameObject.Find("DurabilityFixMessageBoxTotalCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            weaponMoney = 2;
            defenceMoney = 10;
        }

        // ������ ���� �޼��� �ڽ� ����
        public void SetDurabilityFixMessageBox()
        {
            // �����ؾ��ϴ� ���� ������ ��ġ�� �����ؾ��ϴ� �� ������ ��ġ
            float weaponValue = GameManager.instance.DurabilityMax - GameManager.instance.DurabilityWeapon;
            float defenceValue = GameManager.instance.DurabilityMax - GameManager.instance.DurabilityDefence;

            // ���⸦ �����ϴµ� ��� �� ���� ���� �����ϴµ� ��� �� ���
            int weaponTotalMoney = (int)weaponValue * weaponMoney;
            int defenceTotalMoney = (int)defenceValue * weaponMoney;

            totalMoney = weaponTotalMoney + defenceTotalMoney;

            // �޼��� �ڽ� �ؽ�Ʈ ����
            weaponCostDurabilityValue.text = weaponValue.ToString();
            weaponCostDurabilityMoney.text = weaponMoney.ToString();
            weaponCostDurabilityTotalMoney.text = weaponTotalMoney.ToString();

            defenceCostDurabilityValue.text = defenceValue.ToString();
            defenceCostDurabilityMoney.text = defenceMoney.ToString();
            defenceCostDurabilityTotalMoney.text = defenceTotalMoney.ToString();

            totalText.text = totalMoney.ToString();
        }

        // �������� �����Ѵٴ� ��ư�� Ŭ���� ���
        public void ClickButtonYes()
        {
            if (totalMoney <= GameManager.instance.Oxygen)
            {
                // �� ���� �ݾ��� ��� ������ ������ ���

                // ����� ���� �������� �ִ�ġ�� ����
                GameManager.instance.DurabilityWeapon = GameManager.instance.DurabilityMax;
                GameManager.instance.DurabilityDefence = GameManager.instance.DurabilityMax;

                // �� ���� �ݾ� ��ŭ ������ ��� ����
                GameManager.instance.Oxygen -= totalMoney;

                // ������ UI �� ���� �κ��丮�� ���� ��ȭ ����
                DurabilityManager.instance.SetDurability();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // �ش� �޼��� â ��Ȱ��ȭ
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("��ȭ ����");
            }
        }

        // �������� �������� �ʰڴٴ� ��ư�� Ŭ���� ���
        public void ClickButtonNo()
        {
            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
