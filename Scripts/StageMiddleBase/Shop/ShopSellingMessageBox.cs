using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopSellingMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;    // NPC�� ������ ��� �ؽ�Ʈ
        TextMeshProUGUI npcCountText;   // NPC�� ���� ��� �ؽ�Ʈ

        Image itemIcon;                 // �������� ������
        TextMeshProUGUI itemTotalCount; // �������� �� �Ǹ� ����
        TextMeshProUGUI itemName;       // ������ �̸�
        TextMeshProUGUI itemTotalPrice; // �������� �� �Ǹ� ����

        InventoryItem currentItem;      // ���� �Ǳ����Ϸ��� ������
        int sellingCount;               // �Ǹ��Ϸ��� ������ ����
        int inventoryTotalCount;        // �Ǹ��Ϸ��� �������� �κ��丮�� ������ �� ����

        private void Awake()
        {
            // ���� ������Ʈ �ҷ�����
            LoadObjects();
        }

        void Start()
        {

        }

        // ���� ������Ʈ �ҷ�����
        void LoadObjects()
        {
            npcItmeText = GameObject.Find("SellingMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();
            npcCountText = GameObject.Find("SellingMessageBoxNPCCountText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("SellingMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("SellingMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("SellingMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("SellingMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // �Ǹ� �޼��� �ڽ� ����
        public void SetSellingMessageBox(InventoryItem item)
        {
            currentItem = item;
            sellingCount = 1;
            inventoryTotalCount = GameManager.instance.FindItemTotalCount(item); // �κ��丮 ������ �ش��ϴ� �������� �� ���� ���� ���ϱ�

            npcItmeText.text = string.Format("���� \'{0}\'�� �Ǹ� �Ұž�?", item.ItemName);
            npcCountText.text = string.Format("�κ��丮�� �� {0}�� �־�.", inventoryTotalCount);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = sellingCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.SellingPrice.ToString();
        }

        // �����Ϸ��� ������ ������ �� ���� ���� ����
        public void ChangeCountAndPrice()
        {
            itemTotalCount.text = sellingCount.ToString();
            itemTotalPrice.text = (currentItem.SellingPrice * sellingCount).ToString();
        }

        // 10�� ���� ��ư�� Ŭ���� ���
        public void ClickSub10()
        {
            // �� �Ǹ� ������ 10 ���ҽ�Ű�� 1 �̸��� �� ��� 1�� ����
            sellingCount -= 10;
            if (sellingCount < 1) sellingCount = 1;

            // �Ǹ��Ϸ��� ������ ������ �� �Ǹ� ���� ����
            ChangeCountAndPrice();
        }

        public void ClickSub1()
        {
            // �� �Ǹ� ������ 1 ���ҽ�Ű�� 1 �̸��� �� ��� 1�� ����
            sellingCount -= 1;
            if (sellingCount < 1) sellingCount = 1;

            // �Ǹ��Ϸ��� ������ ������ �� �Ǹ� ���� ����
            ChangeCountAndPrice();
        }

        public void ClickAdd1()
        {
            // �� �Ǹ� ������ 1 ������Ű�� �κ��丮�� ������ �� �������� �ʰ��� �� ��� �κ��丮�� ������ �� ������ ����
            sellingCount += 1;
            if (sellingCount > inventoryTotalCount) sellingCount = inventoryTotalCount;

            // �Ǹ��Ϸ��� ������ ������ �� �Ǹ� ���� ����
            ChangeCountAndPrice();
        }

        public void ClickAdd10()
        {
            // �� �Ǹ� ������ 10 ������Ű�� �κ��丮�� ������ �� �������� �ʰ��� �� ��� �κ��丮�� ������ �� ������ ����
            sellingCount += 10;
            if (sellingCount > inventoryTotalCount) sellingCount = inventoryTotalCount;

            // �Ǹ��Ϸ��� ������ ������ �� �Ǹ� ���� ����
            ChangeCountAndPrice();
        }

        // �Ǹ��Ѵٴ� ��ư�� Ŭ���� ���
        public void ClickButtonYes()
        {
            // �κ��丮���� �ش� �������� �Ǹ��� ������ŭ �����ϰ� �� �Ǹ� �ݾ׸�ŭ ������ ��ҷ� ����
            GameManager.instance.RemoveInventoryItem(currentItem, sellingCount);
            GameManager.instance.Oxygen += (currentItem.SellingPrice * sellingCount);

            if (ShopManager.instance.RepurchaseItems.Count == ShopManager.SHOP_SLOTS_MAX)
            {
                // �籸�� ������ ��� ���� ���� �ִ� ���� �Ǵ� ��� ��� �� ���� ������ ����
                // �ֱٿ� �Ǹ��� ������ ���� 8���� ���̵��� �ϱ� ����
                ShopManager.instance.RepurchaseItems.RemoveAt(0);
                ShopManager.instance.RepurchaseItemsCount.RemoveAt(0);
            }

            // �籸�� ������ ��Ͽ� �ش� ������ ��� �� ���� ��Ͽ� �Ǹ��� ���� ����
            ShopManager.instance.RepurchaseItems.Add(currentItem);
            ShopManager.instance.RepurchaseItemsCount.Add(sellingCount);

            // ���� �κ��丮 ���԰� ���� ���� ��ȭ ����
            UserInventorySlotsManager.instance.SetSlots();
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();

            // ���� ������ ���� ����
            ShopManager.instance.SetItemSlots();

            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }

        // �Ǹ����� �ʴ´ٴ� ��ư�� Ŭ���� ���
        public void ClickButtonNo()
        {
            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}