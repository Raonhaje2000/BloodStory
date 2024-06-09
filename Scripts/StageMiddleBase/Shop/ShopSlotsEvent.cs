using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSA
{
    public class ShopSlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem shopItem; // ���� ������ ������

        int shopSlotIndex;      // ���� ������ �ε���

        // �ش� ���������� ���� ������ ������ ����
        public void InitializeSlots(InventoryItem item, int index)
        {
            shopItem = item;
            shopSlotIndex = index;
        }

        // ���콺 Ŭ�� ó��
        public void OnPointerClick(PointerEventData eventData)
        {
            if (shopItem != null)
            {
                // ���� �������� �ִ� ��� �ش� ������ ����
                ShopManager.instance.PurchaseItem(shopItem, shopSlotIndex);
            }
        }
    }
}
