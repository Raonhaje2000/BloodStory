using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LYK;
using Unity.VisualScripting;

namespace RSA
{
    public class MiddleBaseUIManager : MonoBehaviour
    {
        public static MiddleBaseUIManager instance;

        // UI의 상태
        public enum UIState { middleBaseInit = 0, userEquipment = 1, userInfo = 2, userInventory = 3, reinforcement = 4, attribute = 5, production = 6, shop = 7 }

        UIState currentUIState;       // 현재 UI 상태

        GameObject middleBaseInitUI;  // 중간 거점 기본 UI

        GameObject userInformationUI; // 유저 캐릭터 정보 UI
        GameObject userInventoryUI;   // 유저 인벤토리 정보 UI

        GameObject reinforcementUI;   // 강화 UI
        GameObject attributeUI;       // 속성 부여 UI
        GameObject productionUI;      // 제작 UI

        GameObject shopUI;            // 상점 UI

        TextMeshProUGUI scoreText;  // 점수 텍스트 UI
        TextMeshProUGUI oxygenText; // 산소 텍스트 UI

        Transform uiObjectPoint; // UI 오브젝트가 생성되는 위치 (UI 오브젝트들의 부모 오브젝트)

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }
        
        // 관련 오브젝트 불러오기
        void LoadObjects()
        {
            middleBaseInitUI = GameObject.Find("MiddleBaseInitUIObjects");

            userInformationUI = Resources.Load<GameObject>("RSA/Prefabs/UI/UserInfoObjects"); // 유저 정보 UI는 다른 분이 제작 & 구현
            userInventoryUI = Resources.Load<GameObject>("RSA/Prefabs/UI/InventoryItemSettingUIObjects");

            reinforcementUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ReinforcementUIObjects");
            attributeUI = Resources.Load<GameObject>("RSA/Prefabs/UI/AttributeUIObjects");
            productionUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ProductionUIObjects");

            shopUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ShopUIObjects");

            scoreText = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
            oxygenText = GameObject.Find("OxygenValue").GetComponent<TextMeshProUGUI>();

            uiObjectPoint = GameObject.Find("MiddleBaseUI").transform;
        }

        // 초기화
        void Initialize()
        {
            currentUIState = UIState.middleBaseInit;
        }

        // 중간 거점 기본 UI 활성화
        public void ActiveMiddleBaseInitUI()
        {
            // 중간 거점 기본 UI를 활성화
            middleBaseInitUI.gameObject.SetActive(true);

            // UI 상태를 중간 거점으로 변경
            currentUIState = UIState.middleBaseInit;
        }

        // 강화 UI 활성화
        public void ActivReinforcementUI()
        {
            // 중간 거점 기본 UI를 비활성화
            middleBaseInitUI.gameObject.SetActive(false);

            // 강화 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(reinforcementUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 강화로 변경
            currentUIState = UIState.reinforcement;
        }

        // 속성 부여 UI 활성화
        public void ActiveAttributeUI()
        {
            // 속성 부여 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(attributeUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 속성 부여로 변경
            currentUIState = UIState.attribute;
        }

        // 제작 UI 활성화
        public void ActiveProductionUI()
        {
            // 중간 거점 기본 UI 비활성화
            middleBaseInitUI.gameObject.SetActive(false);

            // 속성 부여 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(productionUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 제작으로 변경
            currentUIState = UIState.production;
        }

        // 상점 UI 활성화
        public void ActiveShopUI()
        {
            // 중간 거점 기본 UI를 비활성화
            middleBaseInitUI.gameObject.SetActive(false);

            // 상점 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(shopUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 상점으로 변경
            currentUIState = UIState.shop;
        }

        // 유저 정보 UI 활성화
        public void ActiveUserInformationUI()
        {           
            // 유저 정보 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(userInformationUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 유저 정보로 변경
            currentUIState = UIState.userInfo;
        }

        // 유저 인벤토리 UI 활성화
        public void ActiveUserInventoryUI()
        {
            // 유저 인벤토리 UI 오브젝트를 불러와 UI 오브젝트가 생성되는 위치로 이동
            GameObject ui = Instantiate(userInventoryUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // 생성된 UI 오브젝트의 이름 변경
            SetInstantiateName(ui.gameObject);

            // UI 상태를 유저 인벤토리로 변경
            currentUIState = UIState.userInventory;
        }

        // 점수와 산소 텍스트 UI 세팅
        public void SetScoreAndOxygenTexts()
        {
            scoreText.text = GameManager.instance.Score.ToString("00000");
            oxygenText.text = GameManager.instance.Oxygen.ToString("00000");
        }

        // 생성된 UI 오브젝트의 이름 변경
        public void SetInstantiateName(GameObject obj)
        {
            // 생성된 UI 뒤에 붙는 (Clone) 제거
            // 오브젝트 이름으로 찾을 때 쉽게 찾기 위함
            int index = obj.name.IndexOf("(Clone)");

            obj.name = obj.name.Substring(0, index);
        }
    }
}
