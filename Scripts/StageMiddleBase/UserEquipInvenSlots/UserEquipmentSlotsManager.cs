using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public class UserEquipmentSlotsManager : MonoBehaviour
    {
        public static UserEquipmentSlotsManager instance;

        [SerializeField]
        GameObject[] equipmentSlots; // UI ���� ��� ������ ���Ե�

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // ��� ������ ���� �ʱ�ȭ
            InitializeSlots();
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObjects()
        {
            equipmentSlots = new GameObject[] { GameObject.Find("EquipmentSlots_SwordIcon"),    // ���� [0]
                                                GameObject.Find("EquipmentSlots_HelmetIcon"),   // ���� [1]
                                                GameObject.Find("EquipmentSlots_ArmorIcon"),    // ���� [2]
                                                GameObject.Find("EquipmentSlots_PantsIcon"),    // ���� [3]
                                                GameObject.Find("EquipmentSlots_BootIcon")   }; // �Ź� [4]
        }

        // ��� ������ ���� �ʱ�ȭ
        void InitializeSlots()
        {
            for(int i = 0; i < equipmentSlots.Length; i++)
            {
                // ��� ������ ���� ������ ���� ��� ������ ���

                // ��� �������� �޾ƿͼ� �ش� �������� ���� Ȯ�� �� �ش� ���� ���Կ� ��� ������ ���
                // ������ ������ EquipPart�� ������ ���� �̿��� { ���� = 0, ���� = 1, ���� = 2, ���� = 3, �Ź� = 4}
                EquipmentItem currEquip = GameManager.instance.EquipmentItems[i];
                int index = (int)currEquip.EquipPart;

                equipmentSlots[index].GetComponent<UserEquipmentSlotsEvent>().InitializedSlots(currEquip);
            }
        }
    }
}
