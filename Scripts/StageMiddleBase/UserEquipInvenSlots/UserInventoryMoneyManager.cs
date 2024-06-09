using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RSA
{
    public class UserInventoryMoneyManager : MonoBehaviour
    {
        public static UserInventoryMoneyManager instance;

        TextMeshProUGUI inventoryOxygenText;  // 인벤토리에 있는 산소 텍스트
        TextMeshProUGUI inventoryIronText;    // 인벤토리에 있는 철분 텍스트
        TextMeshProUGUI inventoryMineralText; // 인벤토리에 있는 미네랄 텍스트

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
            inventoryOxygenText = GameObject.Find("InventoryOxygenText").GetComponent<TextMeshProUGUI>();
            inventoryIronText = GameObject.Find("InventoryIronText").GetComponent<TextMeshProUGUI>();
            inventoryMineralText = GameObject.Find("InventoryMineralText").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            // 유저 인벤토리의 게임 재화 세팅 (텍스트 UI 변경)
            SetUserInventoryMoney();
        }

        // 유저 인벤토리의 게임 재화 세팅 (텍스트 UI 변경)
        public void SetUserInventoryMoney()
        {
            // 현재 보유한 게임 재화만큼 텍스트 UI 변경
            inventoryOxygenText.text = GameManager.instance.Oxygen.ToString();
            inventoryIronText.text = GameManager.instance.Iron.ToString();
            inventoryMineralText.text = GameManager.instance.Mineral.ToString();
        }
    }
}
