using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RSA
{
    public class InventoryItemSettingEvent : MonoBehaviour, IPointerClickHandler
    {
        InventoryItem settingItem; // ���õ� �κ��丮 ������

        void Start()
        {

        }

        // ���� ������ ���� �ʱ�ȭ
        public void SetItemSettingSlot(InventoryItem item)
        {
            // �ش� �κ��丮 �������� ���� ������ ���Կ� ���
            settingItem = item;
        }

        // ���콺 Ŭ�� �̺�Ʈ ó��
        public void OnPointerClick(PointerEventData eventData)
        {
            if(settingItem != null)
            {
                // ���õ� �κ��丮 �������� �����ϴ� ���
                // �ش� ���Կ��� ������ ���� (null�� �ʱ�ȭ)
                InventoryItemSettingManager.instance.RemoveItemSettingSlot(settingItem);
                settingItem = null;
            }
        }
    }
}
