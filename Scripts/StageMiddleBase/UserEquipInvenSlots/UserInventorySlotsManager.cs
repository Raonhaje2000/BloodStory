using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class UserInventorySlotsManager : MonoBehaviour
    {
        public static UserInventorySlotsManager instance;

        int inventoryItemCount;      // 인벤토리 내의 아이템 개수

        GameObject[] inventorySlots; // 인벤토리 슬롯들
        int inventoryPageMax;        // 인벤토리의 최대 페이지 수
        int inventoryPageCurrent;    // 인벤토리의 현재 페이지 수

        TextMeshProUGUI inventoryPageNum;  // 페이지 텍스트 UI

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
            inventorySlots = new GameObject[] { GameObject.Find("InventorySlotIcon0"), GameObject.Find("InventorySlotIcon1"), GameObject.Find("InventorySlotIcon2"),
                                                GameObject.Find("InventorySlotIcon3"), GameObject.Find("InventorySlotIcon4"), GameObject.Find("InventorySlotIcon5"),
                                                GameObject.Find("InventorySlotIcon6"), GameObject.Find("InventorySlotIcon7"), GameObject.Find("InventorySlotIcon8"),
                                                GameObject.Find("InventorySlotIcon9"), GameObject.Find("InventorySlotIcon10"), GameObject.Find("InventorySlotIcon11") };

            inventoryPageNum = GameObject.Find("PageNum").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            // 인벤토리 페이지 초기화
            InitializeInvenPage();

            // 인벤토리 슬롯 세팅
            SetSlots();
        }

        void InitializeInvenPage()
        {
            // 인벤토리에 있는 아이템 개수를 받아옴
            inventoryItemCount = GameManager.instance.InventoryItems.Count;

            // 인벤토리 아이템 개수를 인벤토리 슬롯 개수로 나눴을 때, 나머지가 없으면 몫으로 최대 페이지를 설정하고 나머지가 있는 경우 몫+1로 최대 페이지를 설정함
            // 정수끼리의 나눗셈이므로, 나머지 부분이 잘리기 때문에 나머지 부분을 여분의 페이지에 넣기 위함
            int temp = inventoryItemCount / inventorySlots.Length;
            inventoryPageMax = (inventoryItemCount % inventorySlots.Length == 0) ? temp : temp + 1;

            // 현재 페이지를 1로 초기화
            inventoryPageCurrent = 1;

            // 인벤토리 페이지 UI 변경
            SetInvenPageUI();
        }

        // 인벤토리 페이지 UI 변경
        void SetInvenPageUI()
        {
            // 현재 페이지 / 최대 페이지 형태
            inventoryPageNum.text = inventoryPageCurrent.ToString() + " / " + inventoryPageMax.ToString();
        }

        // 인벤토리 슬롯 세팅
        public void SetSlots()
        {
            // 인벤토리에 있는 아이템 개수를 받아옴
            inventoryItemCount = GameManager.instance.InventoryItems.Count;

            // 인벤토리의 각 슬롯별 아이템 세팅
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // 해당 페이지의 슬롯에 해당하는 아이템의 인벤토리 내에서의 인덱스는 인벤토리 슬롯 개수 * (현재 페이지 - 1) + 슬롯 번호와 같음
                // Ex) 2페이지 1번째 칸 = 13번째 아이템 = 인덱스 12 (배열은 0부터 시작하기 때문)
                int index = inventorySlots.Length * (inventoryPageCurrent - 1) + i;

                if(index < inventoryItemCount)
                {
                    // 구한 인덱스가 인벤토리에 있는 아이템 개수보다 작은 경우 (아이템이 있는 슬롯일 경우)

                    // 해당 인벤토리에 있는 아이템을 가져와 인벤토리 슬롯 세팅 후 슬롯 활성화
                    InventoryItem item = GameManager.instance.InventoryItems[index];

                    inventorySlots[i].GetComponent<UserInventorySlotsEvent>().InitializeSlots(item);
                    inventorySlots[i].SetActive(true);
                }
                else
                {
                    // 구한 인덱스가 인벤토리에 있는 아이템 개수보다 큰 경우 (비어있는 슬롯일 경우)
                    // 해당 슬롯의 아이템을 null로 바꾸고, 슬롯 비활성화
                    inventorySlots[i].GetComponent<UserInventorySlotsEvent>().InitializeSlots(null);
                    inventorySlots[i].SetActive(false);
                }
            }

            // 인벤토리 페이지 UI 변경
            SetInvenPageUI();
        }

        // 페이지의 왼쪽 화살표를 클릭 했을 때
        public void ClickLeftArrow()
        {
            if(inventoryPageCurrent > 1)
            {
                // 현재 페이지가 1보다 큰 경우에만 이전 페이지로 넘어감
                inventoryPageCurrent--;

                // 인벤토리 슬롯 세팅
                // 해당 페이지에 있는 아이템을 보여주기 위함
                SetSlots();
            }
        }

        // 페이지의 오른쪽 화살표를 클릭 했을 때
        public void ClickRightArrow()
        {
            if(inventoryPageCurrent < inventoryPageMax)
            {
                // 현재 페이지가 최대 페이지보다 작은 경우에만 다음 페이지로 넘어감
                inventoryPageCurrent++;

                // 인벤토리 슬롯 세팅
                // 해당 페이지에 있는 아이템을 보여주기 위함
                SetSlots();
            }
        }
    }
}
