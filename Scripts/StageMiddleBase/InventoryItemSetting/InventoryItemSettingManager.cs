using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class InventoryItemSettingManager : MonoBehaviour
    {
        const int ITEM_SETTING_MAX_COUNT = 3;  // 세팅 가능한 아이템 슬롯의 최대 개수

        public static InventoryItemSettingManager instance;

        Image[] redItems;   // 적혈구가 사용하는 아이템 아이콘
        Image[] whiteItems; // 백혈구가 사용하는 아이템 아이콘

        TextMeshProUGUI[] redItemsCount;   // 적혈구가 사용하는 아이템 개수 텍스트
        TextMeshProUGUI[] whiteItemsCount; // 백혈구가 사용하는 아이템 개수 텍스트

        FieldItem[] fieldItems;    // 필드에서 사용하는 아이템 (= 적혈구가 사용하는 아이템)
        InventoryItem[] bossItems; // 보스맵에서 사용하는 아이템 (= 백혈구가 사용하는 아이템)

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
            // 각 배열에 들어가는 오브젝트 이름이 ㅇㅇ1, ㅇㅇ2, ㅇㅇ3 식으로 되어있기 때문에 for문을 통해 맨 뒤 숫자만 바꿔가며 오브젝트를 불러옴
            redItems = new Image[ITEM_SETTING_MAX_COUNT];

            for(int i = 0; i < ITEM_SETTING_MAX_COUNT; i++)
            {
                string name = "InventoryItemSettingRedPlayerItemIcon" + (i + 1);

                redItems[i] = GameObject.Find(name).GetComponent<Image>();

            }

            whiteItems = new Image[ITEM_SETTING_MAX_COUNT];

            for(int i = 0; i < ITEM_SETTING_MAX_COUNT; i++)
            {
                string name = "InventoryItemSettingWhitePlayerItemIcon" + (i + 1);

                whiteItems[i] = GameObject.Find(name).GetComponent<Image>();
            }

            redItemsCount = new TextMeshProUGUI[ITEM_SETTING_MAX_COUNT];

            for(int i = 0; i < ITEM_SETTING_MAX_COUNT; i++)
            {
                string name = "InventoryItemSettingRedPlayerItemCountText" + (i + 1);

                redItemsCount[i] = GameObject.Find(name).GetComponent<TextMeshProUGUI>();
            }

            whiteItemsCount = new TextMeshProUGUI[ITEM_SETTING_MAX_COUNT];

            for (int i = 0; i < ITEM_SETTING_MAX_COUNT; i++)
            {
                string name = "InventoryItemSettingWhitePlayerItemCountText" + (i + 1);

                whiteItemsCount[i] = GameObject.Find(name).GetComponent<TextMeshProUGUI>();
            }

            fieldItems = GameManager.instance.FieldItems;
            bossItems = GameManager.instance.BossItems;
        }

        // 초기화
        void Initialize()
        {
            for(int i = 0; i < fieldItems.Length; i++)
            {
                if (fieldItems[i] != null)
                {
                    // 필드 아이템이 있으면 해당 아이템에 맞춰 슬롯 아이콘과 아이템 개수 텍스트 변경 (배열이기 때문에 null 체크)
                    redItems[i].sprite = fieldItems[i].ItemIcon;
                    redItemsCount[i].text = fieldItems[i].InvenCurrentCount.ToString();
                }
            }

            // 백혈구 아이템 슬롯 세팅
            SetWhiteItemSlot();
        }

        // 백혈구 아이템 슬롯 세팅
        public void SetWhiteItemSlot()
        {
            for (int i = 0; i < bossItems.Length; i++)
            {
                // 백혈구 아이템 슬롯은 아이템을 넣었다 뺐다 할 수 있기 때문에 null 체크와 슬롯 활성 여부 필수
                if (bossItems[i] != null)
                {
                    // 보스 아이템이 있으면 해당 아이템에 맞춰 슬롯 아이콘과 아이템 개수 텍스트 변경
                    whiteItems[i].sprite = bossItems[i].ItemIcon;
                    whiteItemsCount[i].text = bossItems[i].InvenCurrentCount.ToString();

                    // 슬롯 활성화
                    whiteItems[i].gameObject.SetActive(true);
                    whiteItemsCount[i].gameObject.SetActive(true);

                    // 슬롯에 해당 아이템 등록
                    whiteItems[i].gameObject.GetComponent<InventoryItemSettingEvent>().SetItemSettingSlot(bossItems[i]);
                }
                else
                {
                    // 슬롯 비활성화
                    whiteItems[i].gameObject.SetActive(false);
                    whiteItemsCount[i].gameObject.SetActive(false);

                    // 슬롯에 해당 아이템을 null로 등록
                    whiteItems[i].gameObject.GetComponent<InventoryItemSettingEvent>().SetItemSettingSlot(null);
                }
            }
        }

        // 백혈구 아이템 슬롯에 아이템 등록
        // (아이템 세팅 슬롯에 유저 인벤토리에서 클릭한 아이템 등록)
        public void AddItemSettingSlot(InventoryItem item)
        {
            // 백혈구 아이템 슬롯에서 비어있는 슬롯 찾기
            int index = FindEmptySettingSlot();

            // 비어있는 아이템 슬롯이 있는 경우 보스맵에서 사용하는 아이템 목록에 해당 아이템 추가
            if (index != -1) GameManager.instance.AddBossItem(item, index);

            // 백혈구 아이템 슬롯 세팅
            SetWhiteItemSlot();
        }

        // 백혈구 아이템 슬롯에서 아이템 제거
        public void RemoveItemSettingSlot(InventoryItem item)
        {
            // 보스맵에서 사용하는 아이템 목록에서 해당 아이템을 찾아옴
            int index = GameManager.instance.FindBossItem(item);

            // 목록에서 해당 아이템을 비움(제거함)
            bossItems[index] = null;

            // 백혈구 아이템 슬롯 세팅
            SetWhiteItemSlot();
        }
        
        // 백혈구 아이템 슬롯에서 비어있는 슬롯 찾기
        int FindEmptySettingSlot()
        {
            for(int i = 0; i < bossItems.Length; i++)
            {
                // 백혈구가 사용하는 아이템이 비어있는(없는) 경우 해당 인덱스 반환
                if (bossItems[i] == null) return i;
            }

            // 다 차있는 경우 -1 반환
            return -1;
        }

        // X 버튼을 클릭한 경우
        public void ClickButtonX()
        {
            // 해당 UI 제거 (닫기)
            Destroy(gameObject);
        }
    }
}