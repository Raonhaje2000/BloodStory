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

        // UI�� ����
        public enum UIState { middleBaseInit = 0, userEquipment = 1, userInfo = 2, userInventory = 3, reinforcement = 4, attribute = 5, production = 6, shop = 7 }

        UIState currentUIState;       // ���� UI ����

        GameObject middleBaseInitUI;  // �߰� ���� �⺻ UI

        GameObject userInformationUI; // ���� ĳ���� ���� UI
        GameObject userInventoryUI;   // ���� �κ��丮 ���� UI

        GameObject reinforcementUI;   // ��ȭ UI
        GameObject attributeUI;       // �Ӽ� �ο� UI
        GameObject productionUI;      // ���� UI

        GameObject shopUI;            // ���� UI

        TextMeshProUGUI scoreText;  // ���� �ؽ�Ʈ UI
        TextMeshProUGUI oxygenText; // ��� �ؽ�Ʈ UI

        Transform uiObjectPoint; // UI ������Ʈ�� �����Ǵ� ��ġ (UI ������Ʈ���� �θ� ������Ʈ)

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }
        
        // ���� ������Ʈ �ҷ�����
        void LoadObjects()
        {
            middleBaseInitUI = GameObject.Find("MiddleBaseInitUIObjects");

            userInformationUI = Resources.Load<GameObject>("RSA/Prefabs/UI/UserInfoObjects"); // ���� ���� UI�� �ٸ� ���� ���� & ����
            userInventoryUI = Resources.Load<GameObject>("RSA/Prefabs/UI/InventoryItemSettingUIObjects");

            reinforcementUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ReinforcementUIObjects");
            attributeUI = Resources.Load<GameObject>("RSA/Prefabs/UI/AttributeUIObjects");
            productionUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ProductionUIObjects");

            shopUI = Resources.Load<GameObject>("RSA/Prefabs/UI/ShopUIObjects");

            scoreText = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
            oxygenText = GameObject.Find("OxygenValue").GetComponent<TextMeshProUGUI>();

            uiObjectPoint = GameObject.Find("MiddleBaseUI").transform;
        }

        // �ʱ�ȭ
        void Initialize()
        {
            currentUIState = UIState.middleBaseInit;
        }

        // �߰� ���� �⺻ UI Ȱ��ȭ
        public void ActiveMiddleBaseInitUI()
        {
            // �߰� ���� �⺻ UI�� Ȱ��ȭ
            middleBaseInitUI.gameObject.SetActive(true);

            // UI ���¸� �߰� �������� ����
            currentUIState = UIState.middleBaseInit;
        }

        // ��ȭ UI Ȱ��ȭ
        public void ActivReinforcementUI()
        {
            // �߰� ���� �⺻ UI�� ��Ȱ��ȭ
            middleBaseInitUI.gameObject.SetActive(false);

            // ��ȭ UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(reinforcementUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� ��ȭ�� ����
            currentUIState = UIState.reinforcement;
        }

        // �Ӽ� �ο� UI Ȱ��ȭ
        public void ActiveAttributeUI()
        {
            // �Ӽ� �ο� UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(attributeUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� �Ӽ� �ο��� ����
            currentUIState = UIState.attribute;
        }

        // ���� UI Ȱ��ȭ
        public void ActiveProductionUI()
        {
            // �߰� ���� �⺻ UI ��Ȱ��ȭ
            middleBaseInitUI.gameObject.SetActive(false);

            // �Ӽ� �ο� UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(productionUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� �������� ����
            currentUIState = UIState.production;
        }

        // ���� UI Ȱ��ȭ
        public void ActiveShopUI()
        {
            // �߰� ���� �⺻ UI�� ��Ȱ��ȭ
            middleBaseInitUI.gameObject.SetActive(false);

            // ���� UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(shopUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� �������� ����
            currentUIState = UIState.shop;
        }

        // ���� ���� UI Ȱ��ȭ
        public void ActiveUserInformationUI()
        {           
            // ���� ���� UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(userInformationUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� ���� ������ ����
            currentUIState = UIState.userInfo;
        }

        // ���� �κ��丮 UI Ȱ��ȭ
        public void ActiveUserInventoryUI()
        {
            // ���� �κ��丮 UI ������Ʈ�� �ҷ��� UI ������Ʈ�� �����Ǵ� ��ġ�� �̵�
            GameObject ui = Instantiate(userInventoryUI);
            ui.transform.SetParent(uiObjectPoint.gameObject.transform, false);

            // ������ UI ������Ʈ�� �̸� ����
            SetInstantiateName(ui.gameObject);

            // UI ���¸� ���� �κ��丮�� ����
            currentUIState = UIState.userInventory;
        }

        // ������ ��� �ؽ�Ʈ UI ����
        public void SetScoreAndOxygenTexts()
        {
            scoreText.text = GameManager.instance.Score.ToString("00000");
            oxygenText.text = GameManager.instance.Oxygen.ToString("00000");
        }

        // ������ UI ������Ʈ�� �̸� ����
        public void SetInstantiateName(GameObject obj)
        {
            // ������ UI �ڿ� �ٴ� (Clone) ����
            // ������Ʈ �̸����� ã�� �� ���� ã�� ����
            int index = obj.name.IndexOf("(Clone)");

            obj.name = obj.name.Substring(0, index);
        }
    }
}
