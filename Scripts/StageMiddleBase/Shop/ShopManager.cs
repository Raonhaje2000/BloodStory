using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager instance;

        const int SHOP_TABS_MAX = 3;          // ���� ���� �ִ�ġ
        public const int SHOP_SLOTS_MAX = 8;  // ���� ������ �ִ�ġ

        // ���� ���õ� ��
        public enum Tab { potion = 0, material = 1, repurchase = 2 }

        Tab currentTab; // ���� ���õ� ��

        GameObject purchaseMessageBox;      // ���� �޼��� �ڽ�
        GameObject repurchaseMessageBox;    // �籸�� �޼��� �ڽ�
        GameObject sellingMessageBox;       // �Ǹ� �޼��� �ڽ�
        GameObject durabilityFixMessageBox; // ������ ���� �޼��� �ڽ�

        List<InventoryItem> potionItems;     // ���� ������ (ȸ�� ������)
        List<InventoryItem> materialItems;   // ���� ��� ������
        List<InventoryItem> repurchaseItems; // �籸�� ������
        List<int> repurchaseItemsCount;      // �籸�� ������ ����

        Image[] tabBackground; // �� ��� �̹���

        Color initTabColor;   // �ʱ� ���� ��
        Color selectTabColor; // ���õ� ���� ��

        GameObject[] shopItemSlots; // ���� ������ ���Ե�
        Image[] shopItemIcon;       // ���� �������� ������ �̹�����
        TextMeshProUGUI[] shopItemCountText;  // ���� �������� ���� �ؽ�Ʈ
        TextMeshProUGUI[] shopItemNameText;   // ���� �������� �̸� �ؽ�Ʈ
        TextMeshProUGUI[] shopItemPriceText;  // ���� �������� ���� �ؽ�Ʈ

        TextMeshProUGUI shopNpcText; // ���� NPC�� ��ǳ�� �ؽ�Ʈ
        string[] npcTexts; // ���� NPC�� ����

        public List<InventoryItem> RepurchaseItems
        {
            get { return repurchaseItems; }
            set { repurchaseItems = value; }
        }

        public List<int> RepurchaseItemsCount
        { 
            get { return repurchaseItemsCount; }
            set { repurchaseItemsCount = value; }
        }

        private void Awake()
        {
            if (instance == null) instance = this;

            LoadItemsObjects(); // ���� ������ ������Ʈ �ҷ�����
            LoadObjects();      // ���� ������Ʈ �ҷ�����
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������ ������Ʈ �ҷ�����
        void LoadItemsObjects()
        {
            potionItems = new List<InventoryItem>() { Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/LowPotion"),
                                                      Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/IntermediatePotion") };
             
            materialItems = new List<InventoryItem>() { Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Bottle"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Glucose"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Soil"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Gunpowder"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Straw"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Rope"),
                                                        Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Water")      };

            repurchaseItems = new List<InventoryItem>();
            repurchaseItemsCount = new List<int>();
        }

        // ���� ������Ʈ �ҷ�����
        void LoadObjects()
        {
            purchaseMessageBox = GameObject.Find("PurchaseMessageBox");
            repurchaseMessageBox = GameObject.Find("RepurchaseMessageBox");
            sellingMessageBox = GameObject.Find("SellingMessageBox");
            durabilityFixMessageBox = GameObject.Find("DurabilityFixMessageBox");

            tabBackground = new Image[] { GameObject.Find("ShopPotionTabBackground").GetComponent<Image>(),
                                          GameObject.Find("ShopMaterialTabBackground").GetComponent<Image>(),
                                          GameObject.Find("ShopRepuchaseTabBackground").GetComponent<Image>() };

            shopNpcText = GameObject.Find("ShopNPCSpeechBubbleText").GetComponent<TextMeshProUGUI>();

            // oo1, oo2 ������ ������Ʈ �̸��� �پ� �����Ƿ� for���� ����� ������Ʈ �̸��� �����Ͽ� �ش� ������Ʈ�� �ҷ���
            shopItemSlots = new GameObject[SHOP_SLOTS_MAX];

            for(int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                string name = "ShopItem" + i;
                shopItemSlots[i] = GameObject.Find(name);
            }

            shopItemIcon = new Image[SHOP_SLOTS_MAX];

            for(int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                string name = "ShopItemIcon" + i;
                shopItemIcon[i] = GameObject.Find(name).GetComponent<Image>();
            }

            shopItemCountText = new TextMeshProUGUI[SHOP_SLOTS_MAX];

            for(int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                string name = "ShopItemCount" + i;
                shopItemCountText[i] = GameObject.Find(name).GetComponent<TextMeshProUGUI>();
            }

            shopItemNameText = new TextMeshProUGUI[SHOP_SLOTS_MAX];

            for (int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                string name = "ShopItemNameText" + i;
                shopItemNameText[i] = GameObject.Find(name).GetComponent<TextMeshProUGUI>();
            }

            shopItemPriceText = new TextMeshProUGUI[SHOP_SLOTS_MAX];

            for (int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                string name = "ShopItemPriceText" + i;
                shopItemPriceText[i] = GameObject.Find(name).GetComponent<TextMeshProUGUI>();
            }
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // �޼��� �ڽ��� ��Ȱ��ȭ
            purchaseMessageBox.SetActive(false);
            repurchaseMessageBox.SetActive(false);
            sellingMessageBox.SetActive(false);
            durabilityFixMessageBox.SetActive(false);

            // ���� ���� ���� ������ �ʱ�ȭ
            currentTab = Tab.potion;

            // Color Ŭ������ 0�� 1������ �ε� �Ҽ��� ���� �����Ƿ�, �������� 255f�� ������� ��
            initTabColor = new Color(216 / 255f, 216 / 255f, 216 / 255f, 255 / 255f);
            selectTabColor = new Color(198 / 255f, 198 / 255f, 198 / 255f, 255 / 255f);

            // ���� ���� ����
            SetTebBackGround();

            // NPC ��� ����
            npcTexts = new string[] { "�ܼ� ���ɿ� ���� ��ǰ, ȯ���� �Ұ����մϴ�. \n�� �и� ���ߴ�?", "���. ��� �󸶳� �־�?", "�츮 ������ ������ ǰ���� �ְ��̴� \n�ǽ����� �������." };
            SetNPCText();

            // ������ ���� ����
            SetItemSlots();
        }

        // NPC ��� ����
        void SetNPCText()
        {
            // ��� ��� �� �������� �ϳ� �̾Ƽ� NPC ��� �ؽ�Ʈ ����
            int index = Random.Range(0, npcTexts.Length);
            shopNpcText.text = npcTexts[index];
        }

        // ���� ���� ����
        void SetTebBackGround()
        {
            for(int i = 0; i < SHOP_TABS_MAX; i++)
            {
                // ���� ���õ� ���̸� ���õ� ���� ������ ���� �ƴ� ���̸� �ʱ� ���� ������ ���� (�������� ���ڷ� �Ǿ������� �̿���)
                if (i == (int)currentTab) tabBackground[i].color = selectTabColor;
                else tabBackground[i].color = initTabColor;
            }
        }

        // ������ ���� ����
        public void SetItemSlots()
        {
            // ���õ� �ǿ� ���� ������ ���� ����
            switch (currentTab)
            {
                case Tab.potion:
                    {
                        // ���� ���� ���
                        // ����(ȸ�� ������)��� ������ �����ϰ� ������ ���� �ؽ�Ʈ ��Ȱ��ȭ
                        SetTabItemSlotsWithList(potionItems);
                        SetItemCountActive(false);
                        break;
                    }
                case Tab.material:
                    {
                        // ���� ��� ���� ���
                        // ��� �����۵�� ������ �����ϰ� ������ ���� �ؽ�Ʈ ��Ȱ��ȭ
                        SetTabItemSlotsWithList(materialItems);
                        SetItemCountActive(false);
                        break;
                    }
                case Tab.repurchase:
                    {
                        // �籸�� ���� ���
                        // �籸�� ������(������ �Ǹ��ߴ� ������)��� ������ ����
                        SetTabItemSlotsWithList(repurchaseItems);
                        break;
                    }
            }
        }

        // ������ ���� �ؽ�Ʈ Ȱ��ȭ ����
        void SetItemCountActive(bool active)
        {
            for(int i = 0; i < shopItemCountText.Length; i++)
            {
                shopItemCountText[i].gameObject.SetActive(active);
            }
        }

        // �ش� �����۵�� ���� ������ ����
        void SetTabItemSlotsWithList(List<InventoryItem> itemsList)
        {
            for(int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                if (i < itemsList.Count)
                {
                    // ���� ������ ���� �ε����� ������ ��� �������� ���� ��� (���� ���Կ� �������� �ִ� ���)
                    shopItemSlots[i].GetComponent<ShopSlotsEvent>().InitializeSlots(itemsList[i], i);

                    // �ش� ���������� ���� �����ܰ� ������ �̸� ����
                    shopItemIcon[i].sprite = itemsList[i].ItemIcon;
                    shopItemNameText[i].text = itemsList[i].ItemName;

                    // �籸�� ���� �ƴ� ��� �������� ������ ���� �Ǹ� �������� ����, �籸�� ���� ��� �籸�� �������� ����
                    shopItemPriceText[i].text = (currentTab != Tab.repurchase) ? itemsList[i].PurchasePrice.ToString() : itemsList[i].RepurchasePrice.ToString();

                    if (currentTab == Tab.repurchase)
                    {
                        // ���� ���� �籸�� ���� ���
                        // ���� ������ ���� ���� �� Ȱ��ȭ
                        shopItemCountText[i].text = repurchaseItemsCount[i].ToString();
                        shopItemCountText[i].gameObject.SetActive(true);
                    }

                    // �ش� ���� Ȱ��ȭ
                    SetActiveSlots(i, true);
                }
                else
                {
                    // ���� ������ ���� �ε����� ������ ��� �������� ū ��� (���� ���Կ� �������� ���� ���)

                    // �ش� ������ �������� null�� ó���ϰ� ������ �ε����� -1�� ����
                    shopItemSlots[i].GetComponent<ShopSlotsEvent>().InitializeSlots(null, -1);

                    // �ش� ������ ������ ���� �� ���� ��Ȱ��ȭ
                    shopItemCountText[i].gameObject.SetActive(false);
                    SetActiveSlots(i, false);
                }
            }
        }

        // ���� Ȱ��ȭ
        void SetActiveSlots(int index, bool active)
        {
            shopItemIcon[index].gameObject.SetActive(active);
            shopItemNameText[index].gameObject.SetActive(active);
            shopItemPriceText[index].gameObject.SetActive(active);
        }

        // �������� �������� �����ϴ� ���
        public void PurchaseItem(InventoryItem item, int slotIndex)
        {
            if (currentTab != Tab.repurchase)
            {
                // ���� ���� �籸�� ���� �ƴ� ���
                // ���� �޼��� �ڽ� ���� �� Ȱ��ȭ
                purchaseMessageBox.GetComponent<ShopPurchaseMessageBox>().SetPurchaseMessageBox(item);
                purchaseMessageBox.SetActive(true);
            }
            else
            {
                // ���� ���� �籸�� ���� ���
                // �籸�� �޼��� �ڽ� ���� �� Ȱ��ȭ (�̶� �ش� �������� ���� �ε����� �Ѱܾ� �籸�� ��Ͽ��� ������ ���Ű� ������)
                repurchaseMessageBox.GetComponent<ShopRepurchaseMessageBox>().SetRepurchaseMessageBox(item, slotIndex);
                repurchaseMessageBox.SetActive(true);
            }
        }

        // �÷��̾ �������� �Ǹ��ϴ� ���
        public void SellItem(InventoryItem item)
        {
            // �Ǹ� �޼��� �ڽ� ���� �� Ȱ��ȭ
            sellingMessageBox.GetComponent<ShopSellingMessageBox>().SetSellingMessageBox(item);
            sellingMessageBox.SetActive(true);
        }

        // ���� ���� Ŭ���� ���
        public void ClickPotionTab()
        {
            // ���� ���� ���� ������ ����
            currentTab = Tab.potion;

            // �� ���濡 ���� ���� ������ ������ ���� ����
            SetTebBackGround();
            SetItemSlots();
        }

        // ��� ���� Ŭ���� ���
        public void ClickMaterialTab()
        {
            // ���� ���� ���� ��� ������ ����
            currentTab = Tab.material;

            // �� ���濡 ���� ���� ������ ������ ���� ����
            SetTebBackGround();
            SetItemSlots();
        }

        // �籸�� ���� Ŭ���� ���
        public void ClickRepuchaseTab()
        {
            // ���� ���� �籸�� ������ ����
            currentTab = Tab.repurchase;

            // �� ���濡 ���� ���� ������ ������ ���� ����
            SetTebBackGround();
            SetItemSlots();
        }

        // ������ ���� ��ư�� Ŭ���� ���
        public void ClickDurabilityFix()
        {
            // ������ ���� �޽��� �ڽ� ���� ���� �� Ȱ��ȭ
            durabilityFixMessageBox.GetComponent<DurabilityFixMessageBox>().SetDurabilityFixMessageBox();
            durabilityFixMessageBox.SetActive(true);
        }

        // ��� ���� ��ư�� Ŭ���� ���
        public void ClickOxygenMax()
        {
            // ��� �������� ���� ��ȭ �ִ�ġ�� ����
            GameManager.instance.Oxygen = GameManager.MONEY_MAX;
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();
        }

        // X ��ư�� Ŭ���� ���
        public void ClickButtonX()
        {
            // �߰� ���� UI�� Ȱ��ȭ �ϰ� �ش� UI ������Ʈ ����(�ݱ�)
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
            Destroy(gameObject);
        }
    }
}
