using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserInventorySlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem invenItem; // �κ��丮 ������

        public InventoryItem InvenItem
        { 
            get { return invenItem; }
            set { invenItem = value; }
        }

        // �κ��丮 ���� �ʱ�ȭ
        public void InitializeSlots(InventoryItem item)
        {
            // �ش� �κ��丮 �������� ���Կ� ���
            invenItem = item;

            // �κ��丮 �������� null�� �ƴ� ���
            if (invenItem != null)
            {
                // ���� �̹����� ������ ���� �ؽ�Ʈ ����
                GetComponent<Image>().sprite = item.ItemIcon;
                GetComponentInChildren<TextMeshProUGUI>().text = item.InvenCurrentCount.ToString();
            }
        }

        // ���콺 Ŭ�� �̺�Ʈ ó��
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("������: " + invenItem.ItemName + " / ���� ���� : " + invenItem.InvenCurrentCount);

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if(InventoryItemSettingManager.instance != null
                        && (invenItem.ItemType == Item.Type.recovery || invenItem.ItemType == Item.Type.attack || invenItem.ItemType == Item.Type.aggro))
                {
                    // ������ ���� UI�� Ȱ��ȭ�� ���¿��� ȸ�� ������, ���� ������, ��׷� �������� Ŭ�� �� ���
                    // ������ ���� ���Կ� Ŭ���� ������ ���
                    InventoryItemSettingManager.instance.AddItemSettingSlot(invenItem);
                }
                if (AttributeManager.instance != null && invenItem.ItemType == Item.Type.attributeMaterial)
                {
                    // �Ӽ� �ο� UI�� Ȱ��ȭ �� ���¿��� �Ӽ� �ο� �������� Ŭ���� ���

                    AttributeItem attrbuteiItem = (AttributeItem)invenItem; // ��ڽ�

                    if (AttributeManager.instance.IsInstallationPossible(attrbuteiItem))
                    {
                        // �ش� �Ӽ� �ο� �������� ����� �Ӽ� �ο��� ������ ��� �ش� �Ӽ� �ο� �������� ����� �Ӽ��� �ο���
                        // ���Կ� ���õ� �����͸� �ִ� ������ �������� �����ϱ� �� ������ ������ Ȯ�� �޼����� �����µ�, �����ϰڴٰ� �� ��쿡�� �ش� ������ �Ӽ� �������� �����ϱ� ����
                        AttributeManager.instance.InstallAttribute(attrbuteiItem, this);
                    }
                }
                if(ShopManager.instance != null)
                {
                    // ���� UI�� Ȱ��ȭ �� ���¿��� �������� Ŭ���� ���

                    // �ش� ������ �Ǹ�
                    ShopManager.instance.SellItem(invenItem);
                }
            }
        }

        // ������ �Ӽ� �ο� �������� ������� ��� ���� UI ����
        public void UseAtAttributeUI()
        {
            // �ش� ������ �Ӽ� �ο� ������ ���
            invenItem.UseInventoryItem();

            if (invenItem.InvenCurrentCount == 0)
            {
                // �ش� ������ �Ӽ� �ο� ������ ������ 0�� �Ǿ�����

                // �κ��丮���� �ش� �Ӽ��ο� �������� �ִ� ��ġ�� ã�� �Ӽ� �ο� ������ ����
                int index = GameManager.instance.InventoryItems.IndexOf(invenItem);
                GameManager.instance.InventoryItems.RemoveAt(index);

                // ���� �ʱ�ȭ (������ ������ �� ���� ������ ����)
                UserInventorySlotsManager.instance.SetSlots();
            }
        }
    }
}
