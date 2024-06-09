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
        GameObject[] equipmentSlots; // UI 상의 장비 아이템 슬롯들

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 장비 아이템 슬롯 초기화
            InitializeSlots();
        }

        // 관련 오브젝트들 불러오기
        void LoadObjects()
        {
            equipmentSlots = new GameObject[] { GameObject.Find("EquipmentSlots_SwordIcon"),    // 무기 [0]
                                                GameObject.Find("EquipmentSlots_HelmetIcon"),   // 모자 [1]
                                                GameObject.Find("EquipmentSlots_ArmorIcon"),    // 상의 [2]
                                                GameObject.Find("EquipmentSlots_PantsIcon"),    // 하의 [3]
                                                GameObject.Find("EquipmentSlots_BootIcon")   }; // 신발 [4]
        }

        // 장비 아이템 슬롯 초기화
        void InitializeSlots()
        {
            for(int i = 0; i < equipmentSlots.Length; i++)
            {
                // 장비 부위별 슬롯 순서에 맞춰 장비 아이템 등록

                // 장비 아이템을 받아와서 해당 아이템의 부위 확인 후 해당 부위 슬롯에 장비 아이템 등록
                // 열거형 데이터 EquipPart가 정수인 것을 이용함 { 무기 = 0, 모자 = 1, 상의 = 2, 하의 = 3, 신발 = 4}
                EquipmentItem currEquip = GameManager.instance.EquipmentItems[i];
                int index = (int)currEquip.EquipPart;

                equipmentSlots[index].GetComponent<UserEquipmentSlotsEvent>().InitializedSlots(currEquip);
            }
        }
    }
}
