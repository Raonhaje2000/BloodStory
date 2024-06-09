using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class DurabilityFixMessageBox : MonoBehaviour
    {
        TextMeshProUGUI weaponCostDurabilityValue;       // 수리해야하는 무기 내구도 수치 텍스트
        TextMeshProUGUI weaponCostDurabilityMoney;       // 무기 내구도 수리 비용 텍스트
        TextMeshProUGUI weaponCostDurabilityTotalMoney;  // 무기 내구도 수리 총 비용 텍스트

        TextMeshProUGUI defenceCostDurabilityValue;      // 수리해야하는 방어구 내구도 수치 텍스트
        TextMeshProUGUI defenceCostDurabilityMoney;      // 방어구 내구도 수리 비용 텍스트
        TextMeshProUGUI defenceCostDurabilityTotalMoney; // 방어구 내구도 수리 총 비용 텍스트

        TextMeshProUGUI totalText; // 무기와 방어구 내구도 수리 총 비용 텍스트

        int weaponMoney;  // 무기 내구도 수리 비용
        int defenceMoney; // 방어구 내구도 수리 비용

        int totalMoney;   // 무기와 방어구 내구도 수리 총 비용 

        private void Awake()
        {
            LoadObjects(); // 관련 오브젝트 불러오기
            Initialize();  // 초기화
        }

        void Start()
        {

        }

        // 관련 오브젝트 불러오기
        void LoadObjects()
        {
            weaponCostDurabilityValue = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityValue").GetComponent<TextMeshProUGUI>();
            weaponCostDurabilityMoney = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityMoney").GetComponent<TextMeshProUGUI>();
            weaponCostDurabilityTotalMoney = GameObject.Find("DurabilityFixMessageBoxWeaponCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();

            defenceCostDurabilityValue = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityValue").GetComponent<TextMeshProUGUI>();
            defenceCostDurabilityMoney = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityMoney").GetComponent<TextMeshProUGUI>();
            defenceCostDurabilityTotalMoney = GameObject.Find("DurabilityFixMessageBoxDefenceCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();

            totalText = GameObject.Find("DurabilityFixMessageBoxTotalCostDurabilityTotalMoney").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            weaponMoney = 2;
            defenceMoney = 10;
        }

        // 내구도 수리 메세지 박스 세팅
        public void SetDurabilityFixMessageBox()
        {
            // 수리해야하는 무기 내구도 수치와 수리해야하는 방어구 내구도 수치
            float weaponValue = GameManager.instance.DurabilityMax - GameManager.instance.DurabilityWeapon;
            float defenceValue = GameManager.instance.DurabilityMax - GameManager.instance.DurabilityDefence;

            // 무기를 수리하는데 드는 총 비용과 방어구를 수리하는데 드는 총 비용
            int weaponTotalMoney = (int)weaponValue * weaponMoney;
            int defenceTotalMoney = (int)defenceValue * weaponMoney;

            totalMoney = weaponTotalMoney + defenceTotalMoney;

            // 메세지 박스 텍스트 세팅
            weaponCostDurabilityValue.text = weaponValue.ToString();
            weaponCostDurabilityMoney.text = weaponMoney.ToString();
            weaponCostDurabilityTotalMoney.text = weaponTotalMoney.ToString();

            defenceCostDurabilityValue.text = defenceValue.ToString();
            defenceCostDurabilityMoney.text = defenceMoney.ToString();
            defenceCostDurabilityTotalMoney.text = defenceTotalMoney.ToString();

            totalText.text = totalMoney.ToString();
        }

        // 내구도를 수리한다는 버튼을 클릭한 경우
        public void ClickButtonYes()
        {
            if (totalMoney <= GameManager.instance.Oxygen)
            {
                // 총 수리 금액이 산소 보유량 이하인 경우

                // 무기와 방어구의 내구도를 최대치로 수리
                GameManager.instance.DurabilityWeapon = GameManager.instance.DurabilityMax;
                GameManager.instance.DurabilityDefence = GameManager.instance.DurabilityMax;

                // 총 수리 금액 만큼 보유한 산소 감소
                GameManager.instance.Oxygen -= totalMoney;

                // 내구도 UI 및 유저 인벤토리의 게임 재화 세팅
                DurabilityManager.instance.SetDurability();
                UserInventoryMoneyManager.instance.SetUserInventoryMoney();

                // 해당 메세지 창 비활성화
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("재화 부족");
            }
        }

        // 내구도를 수리하지 않겠다는 버튼을 클릭한 경우
        public void ClickButtonNo()
        {
            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }
    }
}
