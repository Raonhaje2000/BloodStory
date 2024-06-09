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

        const int SHOP_TABS_MAX = 3;          // 상점 탭의 최대치
        public const int SHOP_SLOTS_MAX = 8;  // 상점 슬롯의 최대치

        // 현재 선택된 탭
        public enum Tab { potion = 0, material = 1, repurchase = 2 }

        Tab currentTab; // 현재 선택된 탭

        GameObject purchaseMessageBox;      // 구매 메세지 박스
        GameObject repurchaseMessageBox;    // 재구매 메세지 박스
        GameObject sellingMessageBox;       // 판매 메세지 박스
        GameObject durabilityFixMessageBox; // 내구도 수리 메세지 박스

        List<InventoryItem> potionItems;     // 포션 아이템 (회복 아이템)
        List<InventoryItem> materialItems;   // 제작 재료 아이템
        List<InventoryItem> repurchaseItems; // 재구매 아이템
        List<int> repurchaseItemsCount;      // 재구매 아이템 개수

        Image[] tabBackground; // 탭 배경 이미지

        Color initTabColor;   // 초기 탭의 색
        Color selectTabColor; // 선택된 탭의 색

        GameObject[] shopItemSlots; // 상점 아이템 슬롯들
        Image[] shopItemIcon;       // 상점 아이템의 아이콘 이미지들
        TextMeshProUGUI[] shopItemCountText;  // 상점 아이템의 개수 텍스트
        TextMeshProUGUI[] shopItemNameText;   // 상점 아이템의 이름 텍스트
        TextMeshProUGUI[] shopItemPriceText;  // 상점 아이템의 가격 텍스트

        TextMeshProUGUI shopNpcText; // 상점 NPC의 말풍선 텍스트
        string[] npcTexts; // 상점 NPC의 대사들

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

            LoadItemsObjects(); // 관련 아이템 오브젝트 불러오기
            LoadObjects();      // 관련 오브젝트 불러오기
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 아이템 오브젝트 불러오기
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

        // 관련 오브젝트 불러오기
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

            // oo1, oo2 식으로 오브젝트 이름이 붙어 있으므로 for문을 사용해 오브젝트 이름을 지정하여 해당 오브젝트를 불러옴
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

        // 초기화
        void Initialize()
        {
            // 메세지 박스들 비활성화
            purchaseMessageBox.SetActive(false);
            repurchaseMessageBox.SetActive(false);
            sellingMessageBox.SetActive(false);
            durabilityFixMessageBox.SetActive(false);

            // 현재 탭을 물약 탭으로 초기화
            currentTab = Tab.potion;

            // Color 클래스는 0과 1사이의 부동 소숫점 값을 받으므로, 설정값을 255f로 나눠줘야 함
            initTabColor = new Color(216 / 255f, 216 / 255f, 216 / 255f, 255 / 255f);
            selectTabColor = new Color(198 / 255f, 198 / 255f, 198 / 255f, 255 / 255f);

            // 탭의 배경색 세팅
            SetTebBackGround();

            // NPC 대사 세팅
            npcTexts = new string[] { "단순 변심에 의한 반품, 환불은 불가능합니다. \n난 분명 말했다?", "어서와. 산소 얼마나 있어?", "우리 상점의 아이템 품질은 최고이니 \n의심하지 말지어다." };
            SetNPCText();

            // 아이템 슬롯 세팅
            SetItemSlots();
        }

        // NPC 대사 세팅
        void SetNPCText()
        {
            // 대사 목록 중 랜덤으로 하나 뽑아서 NPC 대사 텍스트 세팅
            int index = Random.Range(0, npcTexts.Length);
            shopNpcText.text = npcTexts[index];
        }

        // 탭의 배경색 세팅
        void SetTebBackGround()
        {
            for(int i = 0; i < SHOP_TABS_MAX; i++)
            {
                // 현재 선택된 탭이면 선택된 탭의 색으로 변경 아닌 탭이면 초기 탭의 색으로 변경 (열거형이 숫자로 되어있음을 이용함)
                if (i == (int)currentTab) tabBackground[i].color = selectTabColor;
                else tabBackground[i].color = initTabColor;
            }
        }

        // 아이템 슬롯 세팅
        public void SetItemSlots()
        {
            // 선택된 탭에 따라 아이템 슬롯 변경
            switch (currentTab)
            {
                case Tab.potion:
                    {
                        // 물약 탭의 경우
                        // 물약(회복 아이템)들로 슬롯을 세팅하고 아이템 개수 텍스트 비활성화
                        SetTabItemSlotsWithList(potionItems);
                        SetItemCountActive(false);
                        break;
                    }
                case Tab.material:
                    {
                        // 제작 재료 탭의 경우
                        // 재료 아이템들로 슬롯을 세팅하고 아이템 개수 텍스트 비활성화
                        SetTabItemSlotsWithList(materialItems);
                        SetItemCountActive(false);
                        break;
                    }
                case Tab.repurchase:
                    {
                        // 재구매 탭의 경우
                        // 재구매 아이템(이전에 판매했던 아이템)들로 슬롯을 세팅
                        SetTabItemSlotsWithList(repurchaseItems);
                        break;
                    }
            }
        }

        // 아이템 개수 텍스트 활성화 여부
        void SetItemCountActive(bool active)
        {
            for(int i = 0; i < shopItemCountText.Length; i++)
            {
                shopItemCountText[i].gameObject.SetActive(active);
            }
        }

        // 해당 아이템들로 상점 슬롯을 세팅
        void SetTabItemSlotsWithList(List<InventoryItem> itemsList)
        {
            for(int i = 0; i < SHOP_SLOTS_MAX; i++)
            {
                if (i < itemsList.Count)
                {
                    // 현재 아이템 슬롯 인덱스가 아이템 목록 개수보다 작은 경우 (상점 슬롯에 아이템이 있는 경우)
                    shopItemSlots[i].GetComponent<ShopSlotsEvent>().InitializeSlots(itemsList[i], i);

                    // 해당 아이템으로 슬롯 아이콘과 아이템 이름 변경
                    shopItemIcon[i].sprite = itemsList[i].ItemIcon;
                    shopItemNameText[i].text = itemsList[i].ItemName;

                    // 재구매 탭이 아닌 경우 아이템의 가격을 상점 판매 가격으로 변경, 재구매 탭인 경우 재구매 가격으로 변경
                    shopItemPriceText[i].text = (currentTab != Tab.repurchase) ? itemsList[i].PurchasePrice.ToString() : itemsList[i].RepurchasePrice.ToString();

                    if (currentTab == Tab.repurchase)
                    {
                        // 현재 탭이 재구매 탭인 경우
                        // 슬롯 아이템 개수 설정 및 활성화
                        shopItemCountText[i].text = repurchaseItemsCount[i].ToString();
                        shopItemCountText[i].gameObject.SetActive(true);
                    }

                    // 해당 슬롯 활성화
                    SetActiveSlots(i, true);
                }
                else
                {
                    // 현재 아이템 슬롯 인덱스가 아이템 목록 개수보다 큰 경우 (상점 슬롯에 아이템이 없는 경우)

                    // 해당 슬롯의 아이템을 null로 처리하고 슬롯의 인덱스를 -1로 변경
                    shopItemSlots[i].GetComponent<ShopSlotsEvent>().InitializeSlots(null, -1);

                    // 해당 슬롯의 아이템 개수 및 슬롯 비활성화
                    shopItemCountText[i].gameObject.SetActive(false);
                    SetActiveSlots(i, false);
                }
            }
        }

        // 슬롯 활성화
        void SetActiveSlots(int index, bool active)
        {
            shopItemIcon[index].gameObject.SetActive(active);
            shopItemNameText[index].gameObject.SetActive(active);
            shopItemPriceText[index].gameObject.SetActive(active);
        }

        // 상점에서 아이템을 구매하는 경우
        public void PurchaseItem(InventoryItem item, int slotIndex)
        {
            if (currentTab != Tab.repurchase)
            {
                // 현재 탭이 재구매 탭이 아닌 경우
                // 구매 메세지 박스 세팅 후 활성화
                purchaseMessageBox.GetComponent<ShopPurchaseMessageBox>().SetPurchaseMessageBox(item);
                purchaseMessageBox.SetActive(true);
            }
            else
            {
                // 현재 탭이 재구매 탭인 경우
                // 재구매 메세지 박스 세팅 후 활성화 (이때 해당 아이템의 슬롯 인덱스도 넘겨야 재구매 목록에서 아이템 제거가 가능함)
                repurchaseMessageBox.GetComponent<ShopRepurchaseMessageBox>().SetRepurchaseMessageBox(item, slotIndex);
                repurchaseMessageBox.SetActive(true);
            }
        }

        // 플레이어가 아이템을 판매하는 경우
        public void SellItem(InventoryItem item)
        {
            // 판매 메세지 박스 세팅 후 활성화
            sellingMessageBox.GetComponent<ShopSellingMessageBox>().SetSellingMessageBox(item);
            sellingMessageBox.SetActive(true);
        }

        // 물약 탭을 클릭한 경우
        public void ClickPotionTab()
        {
            // 현재 탭을 물약 탭으로 변경
            currentTab = Tab.potion;

            // 탭 변경에 맞춰 탭의 배경색과 아이템 슬롯 세팅
            SetTebBackGround();
            SetItemSlots();
        }

        // 재료 탭을 클릭한 경우
        public void ClickMaterialTab()
        {
            // 현재 탭을 제작 재료 탭으로 변경
            currentTab = Tab.material;

            // 탭 변경에 맞춰 탭의 배경색과 아이템 슬롯 세팅
            SetTebBackGround();
            SetItemSlots();
        }

        // 재구매 탭을 클릭한 경우
        public void ClickRepuchaseTab()
        {
            // 현재 탭을 재구매 탭으로 변경
            currentTab = Tab.repurchase;

            // 탭 변경에 맞춰 탭의 배경색과 아이템 슬롯 세팅
            SetTebBackGround();
            SetItemSlots();
        }

        // 내구도 수리 버튼을 클릭한 경우
        public void ClickDurabilityFix()
        {
            // 내구도 수리 메시지 박스 내용 세팅 후 활성화
            durabilityFixMessageBox.GetComponent<DurabilityFixMessageBox>().SetDurabilityFixMessageBox();
            durabilityFixMessageBox.SetActive(true);
        }

        // 산소 수급 버튼을 클릭한 경우
        public void ClickOxygenMax()
        {
            // 산소 보유량을 게임 재화 최대치로 변경
            GameManager.instance.Oxygen = GameManager.MONEY_MAX;
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();
        }

        // X 버튼을 클릭한 경우
        public void ClickButtonX()
        {
            // 중간 거점 UI를 활성화 하고 해당 UI 오브젝트 제거(닫기)
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
            Destroy(gameObject);
        }
    }
}
