using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopRepurchaseMessageBox : MonoBehaviour
    {
        TextMeshProUGUI npcItmeText;     // NPC�� ������ ��� �ؽ�Ʈ
        TextMeshProUGUI npcCountText;    // NPC�� ���� ��� �ؽ�Ʈ

        Image itemIcon;                  // �������� ������
        TextMeshProUGUI itemTotalCount;  // �������� �� �籸�� ����
        TextMeshProUGUI itemName;        // ������ �̸�
        TextMeshProUGUI itemTotalPrice;  // �������� �� �籸�� ����

        InventoryItem currentItem;       // ���� �籸���Ϸ��� ������
        int repurchaseCount;             // �籸���Ϸ��� ������ ����
        int currentSlotIndex;            // ���� �籸���Ϸ��� �������� ���� �ε���

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
            npcItmeText = GameObject.Find("RepurchaseMessageBoxNPCItmeText").GetComponent<TextMeshProUGUI>();
            npcCountText = GameObject.Find("RepurchaseMessageBoxNPCCountText").GetComponent<TextMeshProUGUI>();

            itemIcon = GameObject.Find("RepurchaseMessageBoxItemIconImage").GetComponent<Image>();
            itemTotalCount = GameObject.Find("RepurchaseMessageBoxItemCountText").GetComponent<TextMeshProUGUI>();
            itemName = GameObject.Find("RepurchaseMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
            itemTotalPrice = GameObject.Find("RepurchaseMessageBoxItemPriceText").GetComponent<TextMeshProUGUI>();
        }

        // �籸�� �޼��� �ڽ� ����
        public void SetRepurchaseMessageBox(InventoryItem item, int slotIndex)
        {
            currentItem = item;
            currentSlotIndex = slotIndex;
            repurchaseCount = ShopManager.instance.RepurchaseItemsCount[slotIndex];

            npcItmeText.text = string.Format("���� \'{0}\'�� �籸�� �Ұž�?", item.ItemName);
            npcCountText.text = string.Format("�� {0}�� �ִµ� �� ������.", repurchaseCount);

            itemIcon.sprite = item.ItemIcon;
            itemTotalCount.text = repurchaseCount.ToString();
            itemName.text = item.ItemName;
            itemTotalPrice.text = item.RepurchasePrice.ToString();
        }

        // �籸���Ѵٴ� ��ư�� Ŭ���� ���
        public void ClickButtonYes()
        {
            if (currentItem.RepurchasePrice * repurchaseCount <= GameManager.instance.Oxygen)
            {
                // ������ ��Ұ� �ش� �������� �� �籸�� ���� �̻��� ���

                // �ش� ������ �κ��丮�� ��� �� ������ ��Ҹ� �籸�� �ݾ׸�ŭ ����
                GameManager.instance.AddInventoryItem(currentItem, repurchaseCount);
                GameManager.instance.Oxygen -= (currentItem.RepurchasePrice * repurchaseCount);

                // ������ �籸�� ������ ��Ͽ��� �ش� ������ ���� (�ش� �������� ���� ��Ͽ����� ����)
                ShopManager.instance.RepurchaseItems.RemoveAt(currentSlotIndex);
                ShopManager.instance.RepurchaseItemsCount.RemoveAt(currentSlotIndex);

                // ���� �κ��丮 ���԰� ���� ���� ��ȭ ����
                UserInventorySlotsManager.instance.SetSlots();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // ���� ������ ���� ����
                ShopManager.instance.SetItemSlots();

                // �ش� �޼��� â ��Ȱ��ȭ
                gameObject.SetActive(false);
            }
            else
            {
                // ������ ��Ұ� �ش� �������� �� �籸�� ���ݺ��� ���� ���
                Debug.Log("��ȭ ����");
            }
        }

        // �籸������ �ʴ´ٴ� ��ư�� Ŭ���� ���
        public void ClickButtonNo()
        {
            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
