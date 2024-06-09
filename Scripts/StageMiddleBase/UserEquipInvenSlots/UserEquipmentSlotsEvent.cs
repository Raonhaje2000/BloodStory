using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserEquipmentSlotsEvent : MonoBehaviour, IPointerClickHandler
    {
        EquipmentItem equipItem; // �ش� ������ ��� ������

        public EquipmentItem EquipItem
        {
            get { return equipItem; }
            set { equipItem = value; }
        }

        // ���� �ʱ�ȭ
        public void InitializedSlots(EquipmentItem item)
        {
            // �ش� ���������� ���Կ� ���
            equipItem = item;

            // ���� �̹��� ����
            GetComponent<Image>().sprite = item.ItemIcon;
        }

        // ���콺 Ŭ�� �̺�Ʈ ó��
        public void OnPointerClick(PointerEventData eventData)
        {
            // ���콺 ������ Ŭ���� �������� �����ϵ��� ��
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                // ��ȭ ������ ��� Ŭ���� �������� ��ȭ �Ϸ��� ���� �����
                if (ReinforcementManager.instance != null) ReinforcementManager.instance.SetCurrentEquipment(equipItem);

                // �Ӽ� �ο� ������ ��� Ŭ���� �������� �Ӽ� �ο��Ϸ��� ���� �����
                if (AttributeManager.instance != null) AttributeManager.instance.SetCurrentEquipment(equipItem);
            }
        }
    }
}
