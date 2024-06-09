using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class AttributeManager : MonoBehaviour
    {
        public static AttributeManager instance;
        UserInventorySlotsEvent inventorySlots; // 이벤트가 발생한 유저 인벤토리 슬롯

        GameObject attributeUI;                 // 속성 부여 UI
        GameObject messageBox;                  // 속성 부여 메세지 박스

        int ATTRIBUTE_TYPE_COUNT = 4;           // 부여 가능한 속성의 가지 수 (비타민 종류)

        EquipmentItem currentEquipment;         // 속성을 부여할 장비 아이템
        int[] attributeMaxValue;                // 속성을 부여했을 때의 최대 수치

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트들 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 오브젝트들 불러오기
        void LoadObjects()
        {
            attributeUI = GameObject.Find("AttributeUI");
            messageBox = GameObject.Find("AttributeMessageBox");
        }

        // 초기화
        void Initialize()
        {
            inventorySlots = null;

            attributeUI.SetActive(false);
            messageBox.SetActive(false);

            currentEquipment = null;
            attributeMaxValue = new int[] { 4, 7, 10, 20 };
        }

        // 현재 속성 부여가 가능한 장비 아이템 세팅 (장비 아이템 슬롯에서 선택한 장비)
        public void SetCurrentEquipment(EquipmentItem equipItem)
        {
            AttributeUIManager.instance.SetAttributeItemEquip(equipItem);
            currentEquipment = equipItem;
        }

        // 속성 부여가 가능한지 판단
        public bool IsInstallationPossible(AttributeItem attriItem)
        {
            if(currentEquipment != null && currentEquipment.EquipAttributeInstallation.VitaminsQuality[(int)attriItem.AttributeItmeType] < 100)
            {
                // 장비가 선택된 상태에서 해당 장비에 부여된 속성의 품질이 100이하인 경우
                // 속성 부여 아이템의 타입은 열거형으로, 해당 속성이 장비에 부여되는 인덱스는 해당 아이템 타입의 정수값과 같음
                // Ex) 비타민 D의 아이템 타입의 정수값은 0이고, 장비에 부여되는 인덱스는 0임

                // 장비 아이템 랭크가 해당 속성 부여 아이템 타입보다 높은 경우 속성 부여 가능, 아닌 경우 속성 부여 불가능
                // 장비 아이템 랭크의 열거형은 { 노말 = 1, 레어 = 2, 유니크 = 3, 레전드 = 4 } 
                // 속성 아이템 타입의 열거형은 { 비타민 D = 0, 비타민 C = 1, 비타민 B = 2, 비타민 A = 3 }
                // Ex) 장비 아이템 랭크가 유니크일때, 속성 아이템 비타민 B까지는 사용 가능, 비타민 A는 사용 불가능
                return ((int)currentEquipment.ItemRank > (int)attriItem.AttributeItmeType) ? true : false;
            }

            // 장비가 선택되지 않았거나, 속성의 품질이 100인 경우 속성 부여 불가능
            return false;
        }

        // 인벤토리에서 속성 부여 아이템을 클릭했을 경우
        public void InstallAttribute(AttributeItem attriItem, UserInventorySlotsEvent slots)
        {
            // 해당 속성 아이템으로 속성이 부여될 인덱스와 해당 속성 아이템이 있는 아이템 슬롯 저장
            int index = (int)attriItem.AttributeItmeType;
            inventorySlots = slots;

            // 장착된 비타민 여부에 따라 메세지 박스 텍스트 변경
            if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] < 0) // 장착된 비타민이 없을 경우
                messageBox.GetComponent<AttributeMessageBox>().SetMessageBoxText(attriItem, false); 
            else                                                                        // 장착된 비타민이 있을 경우
                messageBox.GetComponent<AttributeMessageBox>().SetMessageBoxText(attriItem, true);

            // 속성 부여 메세지 박스 활성화
            messageBox.SetActive(true);
        }

        // 속성 부여 취소
        public void CancleInstallAttribute()
        {
            // 속성 부여 메세지 박스 비활성화
            messageBox.SetActive(false);
        }

        // 속성 부여 진행
        public void ContinueInstallAttribute(AttributeItem attriItem)
        {
            // 속성 아이템으로 부여되는 품질과 속성이 부여될 인덱스 저장
            int quality = attriItem.Quality;
            int index = (int)attriItem.AttributeItmeType;

            if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] < 0)
            {
                // 장착된 비타민이 없을 경우

                // 사용한 비타민의 품질 그대로 적용
                currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] = quality;
            }
            else
            {
                // 장착된 비타민이 있을 경우

                // 사용한 비타민의 품질만큼 품질 증가
                currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] += quality;

                // 증가된 품질의 합이 100 초과인 경우 품질 100으로 고정
                if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] > 100)
                    currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] = 100;

                // 변경된 품질 저장
                quality = currentEquipment.EquipAttributeInstallation.VitaminsQuality[index];
            }

            // 품질에 따른 속성 부여 능력치 값 계산 후 저장
            int value = GetAttributeValue(quality, index);
            currentEquipment.EquipAttributeInstallation.VitaminsValue[index] = value;

            // 장비의 속성 부여로 증가된 능력치 업데이트
            UpdateEquipmentAttributeState(value);

            // 속성 부여 UI 재세팅
            AttributeUIManager.instance.SetAttributeItemEquip(currentEquipment);

            // 속성 부여 메세지 박스 비활성화
            messageBox.SetActive(false);

            // 사용한 속성 아이템 인벤토리에서 제거
            inventorySlots.SendMessage("UseAtAttributeUI");
        }

        // 품질에 따른 속성 부여 능력치 값 계산
        int GetAttributeValue(int quality, int index)
        {
            // 품질이 25씩 줄어들수록 부여 가능한 능력치 최대값에서 점차 감소
            int valueMax = attributeMaxValue[index];

            if (quality > 75) return valueMax;
            else if (quality > 50) return valueMax - 1;
            else if (quality > 25) return valueMax - 2;
            else if (quality > 0) return valueMax - 3;
            else return 0;
        }

        // 장비의 속성 부여로 증가된 능력치 업데이트
        void UpdateEquipmentAttributeState(int value)
        {
            // 해당 장비 아이템에서 속성 부여로 증가되는 능력치 체크 후, 증가되는 값만큼 증가
            switch (currentEquipment.EquipAttributeInstallation.AttributeType)
            {
                case AttributeInstallation.Type.hp:
                    {
                        currentEquipment.AttributeStatus.Hp += value;
                        break;
                    }
                case AttributeInstallation.Type.attack:
                    {
                        currentEquipment.AttributeStatus.Attack += value;
                        break;
                    }
                case AttributeInstallation.Type.defence:
                    {
                        currentEquipment.AttributeStatus.Defence += value;
                        break;
                    }
                case AttributeInstallation.Type.speed:
                    {
                        currentEquipment.AttributeStatus.Speed += value;
                        break;
                    }
            }
        }

        // 장비 UI의 속성 버튼을 클릭 했을 때
        public void ClickEquipmentAttributeButton()
        {
            // 선택된 장비 초기화 및 속성 부여 UI 초기화 후 활성화
            currentEquipment = null;
            AttributeUIManager.instance.Initialize();
            attributeUI.SetActive(true);
            messageBox.SetActive(false);
        }

        // 속성 부여창의 X 버튼을 클릭 했을 때
        public void ClickAttributeUIButtonX()
        {
            // 속성 부여 UI 비활성화 및 현재 선택된 장비 초기화
            attributeUI.SetActive(false);
            messageBox.SetActive(false);
            currentEquipment = null;
        }

        // 속성 초기화 버튼을 눌렀을 경우
        public void ResetEquipAttribute()
        {
            if (currentEquipment != null)
            {
                for (int i = 0; i < ATTRIBUTE_TYPE_COUNT; i++)
                {
                    // 장비 아이템의 모든 속성 품질을 -1, 속성값을 0으로 초기화
                    currentEquipment.EquipAttributeInstallation.VitaminsQuality[i] = -1;
                    currentEquipment.EquipAttributeInstallation.VitaminsValue[i] = 0;

                    // 장비 아이템의 속성 부여로 증가된 능력치 0으로 초기화
                    currentEquipment.AttributeStatus.ResetStatus();

                    // 속성 부여 UI 재세팅
                    AttributeUIManager.instance.SetAttributeItemEquip(currentEquipment);
                }
            }
        }

        // 속성 부여 메세지 박스 활성화 여부 세팅
        public void SetMessageBox(bool active)
        {
            messageBox.SetActive(active);
        }

        // 장비 UI의 X 버튼을 클릭 했을 때
        public void ClickButtonX()
        {
            // 해당 UI 제거 및 기본 UI 활성화
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }
    }
}
