using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopPurchaseMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;     // NPC�� ������ ��� �ؽ�Ʈ

        Image itemIcon;                  // �������� ������
        TextMeshProUGUI itemTotalCount;  // �������� �� ���� ����
        TextMeshProUGUI itemName;        // ������ �̸�
        TextMeshProUGUI itemTotalPrice;  // �������� �� ���� ����

        InventoryItem currentItem;       // ���� �����Ϸ��� ������
        int purchaseCount;               // �����Ϸ��� ������ ����

        private void Awake()
        {
            LoadObjects();
        }

        void Start()
        {

        }

        // ���� ������Ʈ �ҷ�����
        void LoadObjects()
        {
            npcItmeText = GameObject.Find("PurchaseMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("PurchaseMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("PurchaseMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("PurchaseMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("PurchaseMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // ���� �޼��� �ڽ� ����
        public void SetPurchaseMessageBox(InventoryItem item)
        {
            currentItem = item;
            purchaseCount = 1; // ���� ������ 1���� ����

            npcItmeText.text = string.Format("���� \'{0}\'�� ���� �Ұž�?", item.ItemName);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = purchaseCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.PurchasePrice.ToString();
        }

        // �����Ϸ��� ������ ������ �� ���� ���� ����
        public void ChangeCountAndPrice()
        {
            itemTotalCount.text = purchaseCount.ToString();
            itemTotalPrice.text = (currentItem.PurchasePrice * purchaseCount).ToString();
        }

        // 10�� ���� ��ư�� Ŭ���� ���
        public void ClickSub10()
        {
            // �� ���� ������ 10 ���ҽ�Ű�� 1 �̸��� �� ��� 1�� ����
            purchaseCount -= 10;
            if (purchaseCount < 1) purchaseCount = 1;

            // �����Ϸ��� ������ ������ �� ���� ���� ����
            ChangeCountAndPrice();
        }

        // 1�� ���� ��ư�� Ŭ���� ���
        public void ClickSub1()
        {
            // �� ���� ������ 10 ���ҽ�Ű�� 1 �̸��� �� ��� 1�� ����
            purchaseCount -= 1;
            if (purchaseCount < 1) purchaseCount = 1;

            // �����Ϸ��� ������ ������ �� ���� ���� ����
            ChangeCountAndPrice();
        }

        // 1�� ���� ��ư�� Ŭ���� ���
        public void ClickAdd1()
        {
            // �� ���� ������ 1 ������Ű�� 99 �ʰ��� �� ��� 99�� ����
            purchaseCount += 1;
            if (purchaseCount > 99) purchaseCount = 99;

            // �����Ϸ��� ������ ������ �� ���� ���� ����
            ChangeCountAndPrice();
        }

        // 10�� ���� ��ư�� Ŭ���� ���
        public void ClickAdd10()
        {
            // �� ���� ������ 10 ������Ű�� 99 �ʰ��� �� ��� 99�� ����
            purchaseCount += 10;
            if (purchaseCount > 99) purchaseCount = 99;

            // �����Ϸ��� ������ ������ �� ���� ���� ����
            ChangeCountAndPrice();
        }

        // �����Ѵٴ� ��ư�� Ŭ���� ���
        public void ClickButtonYes()
        {
            if (currentItem.PurchasePrice * purchaseCount <= GameManager.instance.Oxygen)
            {
                // ������ ��Ұ� �ش� �������� �� ���� ���� �̻��� ���

                // �ش� ������ �κ��丮�� ��� �� ������ ��Ҹ� ���� �ݾ׸�ŭ ����
                GameManager.instance.AddInventoryItem(currentItem, purchaseCount);
                GameManager.instance.Oxygen -= (currentItem.PurchasePrice * purchaseCount);

                // ���� �κ��丮 ���԰� ���� ���� ��ȭ ����
                UserInventorySlotsManager.instance.SetSlots();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // �ش� �޼��� â ��Ȱ��ȭ
                gameObject.SetActive(false);
            }
            else
            {
                // ������ ��Ұ� �ش� �������� �� ���� ���ݺ��� ���� ���
                Debug.Log("��ȭ ����");
            }
        }

        // �������� �ʴ´ٴ� ��ư�� Ŭ���� ���
        public void ClickButtonNo()
        {
            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}