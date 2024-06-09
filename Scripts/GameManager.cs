using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace RSA
{
    // 씬 여러 곳에서 공통적으로 쓰이는 데이터 모아놓고 처리 함수 만듦
    // 씬이 전환되어도 살아있어야 하는 데이터들 관리
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        // 게임 상태
        public enum State
        {
            title = 0,      // 타이틀
            fieldStage = 1, // 필드맵
            bossStage = 2,  // 보스맵
            middleBase = 3, // 중간 거점
            uiIActive = 4   // UI 상호작용
        }

        // UI 상태
        public enum UIState
        { 
            none = 0,           // 아무것도 아님
            sub = 1,            // 서브 UI (캐릭터 정보, 인벤토리 등 기존 창 위에 뜨는 UI)
            reinforcement = 2,  // 강화 UI
            attribute = 3,      // 속성 UI
            production = 4,     // 제작 UI
            shop = 5            // 상점 UI
        }

        public State gameState;     // 게임 상태
        public UIState gameUIState; // UI 상태

        public const int MONEY_MAX = 99999; // 게임 재화의 최대 보유량

        // 스테이지 관련 데이터
        [SerializeField] int stage; // 현재 스테이지

        // 플레이어 레벨 관련 데이터
        int playerLevel;    // 플레이어 레벨
        float playerExp;    // 플레이어 경험치
        float playerExpMax; // 플레이어 경험치 통 최대치
 
        int playerProductionLevel; // 제작 숙련도 레벨
        float playerProductionExp; // 제작 숙련도 경험치

        // 캐릭터 능력치
        Status playerStatus; // 캐릭터 능력치

        // 플레이어 점수 관련 데이터
        int score; // 전체 획득 점수

        // 플레이어 게임 재화 관련 데이터
        int oxygen;  // 산소 보유량
        int iron;    // 철분 보유량
        int mineral; // 미네랄 보유량

        // 플레이어 내구도 관련 데이터
        float durabilityMax;     // 내구도의 최대치

        float durabilityWeapon;  // 현재 무기 내구도
        float durabilityDefence; // 현재 방어구 내구도

        // 플레이어 아이템 관련 데이터
        [SerializeField]
        List<EquipmentItem> equipmentItems; // 플레이어가 장착한 장비 아이템

        [SerializeField]
        List<InventoryItem> inventoryItems; // 플레이어의 인벤토리에 있는 아이템

        [SerializeField]
        FieldItem[] fieldItems;             // 플레이어가 필드에서 사용하는 아이템

        [SerializeField]
        InventoryItem[] bossItems;          // 플레이어가 보스맵에서 사용하는 아이템

        public int Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        public int PlayerLevel
        {
            get { return playerLevel; }
        }

        public float PlayerExp
        {
            get { return playerExp; }
            set { playerExp = value; }
        }

        public float PlayerExpMax
        {
            get { return playerExpMax; }
            set { playerExpMax = value; }
        }

        public int PlayerProductionLevel
        {
            get { return playerProductionLevel; }
            set { playerProductionLevel = value; }
        }

        public float PlayerProductionExp
        {
            get { return playerProductionExp; }
            set { playerProductionExp = value; }
        }

        public Status PlayerStatus
        {
            get { return playerStatus; }
            set { playerStatus = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int Oxygen
        {
            get { return oxygen; }
            set
            { 
                oxygen = value;

                Mathf.Clamp(oxygen, 0, MONEY_MAX);
            }
        }

        public int Iron
        {
            get { return iron; }
            set { iron = value; }
        }

        public int Mineral
        {
            get { return mineral; }
            set { mineral = value; }
        }

        public float DurabilityWeapon
        {
            get { return durabilityWeapon; }
            set { durabilityWeapon = value; }
        }

        public float DurabilityDefence
        { 
            get { return durabilityDefence; }
            set { durabilityDefence = value; }
        }

        public float DurabilityMax
        {
            get { return durabilityMax; }
        }

        public List<EquipmentItem> EquipmentItems
        {
            get { return equipmentItems; }
            set { equipmentItems = value; }
        }

        public List<InventoryItem> InventoryItems
        { 
            get { return inventoryItems; }
            set { inventoryItems = value; }
        }
        public FieldItem[] FieldItems
        {
            get { return fieldItems; }
            set { fieldItems = value; }
        }

        public InventoryItem[] BossItems
        {
            get { return bossItems; }
            set { bossItems = value; }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // 다음 씬으로 넘어가도 오브젝트 파괴되지 않고 유지
                // 게임 내에서 공통으로 쓰이는 데이터 유지
                DontDestroyOnLoad(gameObject);
            }
            else if(instance != this)
            {
                // 이미 기존 씬에 해당 오브젝트가 남아있을 경우 제거
                Destroy(gameObject);
            }

            stage = 1;

            // 초기화
            Initialize(); 

            // 플레이어 아이템 불러오기
            LoadFieldItem(); // 필드에서 쓰이는 아이템 불러오기
            LoadEquipment(); // 장비 아이템 불러오기
            LoadInventory(); // 인벤토리 아이템 불러오기
            LoadBossItem();  // 보스맵에서 쓰이는 아이템 불러오기
        }

        void Start()
        {
            // 플레이어 레벨에 맞춰 경험치통 최대치 세팅
            SetPlayerExpMax();
        }

        // 초기화
        void Initialize()
        {
            gameState = State.title;
            gameUIState = UIState.none;

            playerLevel = 1;
            playerExp = 0.0f;

            playerProductionLevel = 2;
            playerProductionExp = 185.0f;

            playerStatus = Resources.Load<Status>("RSA/ScriptableObjects/Status/PlayerStatus");
            playerStatus.ResetStatus();

            score = 0;

            oxygen = 500;
            iron = 300;
            mineral = 300;

            durabilityMax = 100.0f;

            //durabilityWeapon = durabilityMax;
            //durabilityDefence = durabilityMax;

            durabilityWeapon = 30.0f;
            durabilityDefence = 20.0f;

        }

        // 필드에서 쓰이는 아이템 불러오기
        void LoadFieldItem()
        {
            // 필드 아이템 ScriptableObject들 불러오기 
            FieldItem[] loadFieldItems = Resources.LoadAll<FieldItem>("RSA/ScriptableObjects/Item/FieldItem");

            // 필드 아이템 종류에 맞춰 배열 크기 고정 (필드 아이템 종류 = 3개)
            fieldItems = new FieldItem[loadFieldItems.Length];

            for (int i = 0; i < fieldItems.Length; i++)
            {
                // 필드 아이템 데이터 복사해서 저장하고 현재 보유 개수 3개로 초기화
                fieldItems[i] = loadFieldItems[i].CopyFieldItemData();
                fieldItems[i].InvenCurrentCount = 3;
            }
        }

        // 장비 아이템 불러오기
        void LoadEquipment()
        {
            // 장비 아이템 ScriptableObject들 불러오기
            EquipmentItem[] equipmentItemsArray = Resources.LoadAll<EquipmentItem>("RSA/ScriptableObjects/Item/EquipmentItem");
            
            // 배열을 리스트로 변환해서 저장
            equipmentItems.AddRange(equipmentItemsArray);
        }

        // 인벤토리 아이템 불러오기
        void LoadInventory()
        {
            // 인벤토리 아이템 ScriptableObject 불러온 후, 데이터 복사해서 저장하고 현재 보유 개수 수정
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/FinestPotion").CopyInventoryData(), 99);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttributeItem/VitaminB").CopyInventoryData(), 8);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttributeItem/VitaminC").CopyInventoryData(), 10);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/FinestPotion").CopyInventoryData(), 99);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttributeItem/VitaminA").CopyInventoryData(), 5);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttributeItem/VitaminD").CopyInventoryData(), 10);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Ginseng").CopyInventoryData(), 5);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Glucose").CopyInventoryData(), 40);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Water").CopyInventoryData(), 1000);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Bottle").CopyInventoryData(), 20);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/GoldPine").CopyInventoryData(), 20);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Gunpowder").CopyInventoryData(), 30);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/IonBattery").CopyInventoryData(), 5);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Rope").CopyInventoryData(), 10);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Soil").CopyInventoryData(), 20);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/ProductionItem/Straw").CopyInventoryData(), 20);

            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttackItem/ElectricGrenade").CopyInventoryData(), 3);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AggroItem/WoodenScarecrow").CopyInventoryData(), 1);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/LowPotion").CopyInventoryData(), 20);
        }

        // 보스맵에서 쓰이는 아이템 불러오기
        void LoadBossItem()
        {
            // 보스 아이템 ScriptableObject 불러온 후, 데이터 복사해서 인벤토리에 저장하고 현재 보유 개수 수정
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/IntermediatePotion").CopyInventoryData(), 10);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttackItem/MudGrenade").CopyInventoryData(), 5);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AggroItem/Scarecrow").CopyInventoryData(), 1);

            // 보스 아이템 목록에 저장할 아이템의 인벤토리 내에서의 시작 인덱스 (인벤토리 뒤에서 3개 아이템)
            int startIndex = inventoryItems.Count - 3;

            // 보스 아이템 목록은 최대 3개로 고정
            bossItems = new InventoryItem[3];

            // 인벤토리 내의 아이템을 보스 아이템 목록에 등록
            for(int i = 0; i < bossItems.Length; i++)
            {
                AddBossItem(inventoryItems[startIndex + i], i);
            }
        }

        // 인벤토리에 아이템 추가
        public void AddInventoryItem(InventoryItem item, int count)
        {
            int index = FindInventoryItem(item); // 중복 확인을 위해 뒤에서부터 인덱스 체크

            if (index >= 0)
            {
                // 아이템을 갖고 있는 경우 현재 보유 개수에 추가하는 개수 더하기
                inventoryItems[index].InvenCurrentCount += count;

                if (inventoryItems[index].InvenCurrentCount > inventoryItems[index].InvenMaxCount)
                {
                    // 아이템 묶음 최대 보유 개수보다 많은 경우 (묶음을 새로 만들어야 하는 경우)

                    // 새로운 묶음을 만들었을 때 남아있는 개수 계산
                    int remainCount = inventoryItems[index].InvenCurrentCount - inventoryItems[index].InvenMaxCount;

                    // 현재 아이템 묶음의 현재 보유 개수를 묶음 최대 보유 개수로 수정
                    inventoryItems[index].InvenCurrentCount = inventoryItems[index].InvenMaxCount;

                    // 남아있는 개수가 묶음 최대 보유 개수보다 적어질 때(더이상 새로운 묶음을 안만들어도 될 때)까지 반복
                    while (remainCount > item.InvenMaxCount)
                    {
                        // 새로운 묶음을 만들기 위해 아이템 데이터를 복사하여 새로운 아이템을 만듦
                        InventoryItem cloneItem = item.CopyInventoryData();

                        // 새로운 아이템을 인벤토리에 저장 후, 현재 보유 개수를 묶음 최대 보유 개수로 수정 
                        inventoryItems.Add(cloneItem);
                        inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = item.InvenMaxCount;

                        // 남은 개수에서 묶음을 만든 개수만큼 뺌
                        remainCount -= item.InvenMaxCount;
                    }

                    // 남아있는 개수가 묶음 최대 보유 개수보다 적어 더이상 새로운 묶음을 안만들어도 될 경우
                    // 새로운 묶음을 만들기 위해 아이템 데이터를 복사하여 새로운 아이템을 만듦
                    InventoryItem cloneRemainItem = item.CopyInventoryData();

                    // 새로운 아이템을 인벤토리에 저장 후, 현재 보유 개수를 남아있는 개수로 수정
                    inventoryItems.Add(cloneRemainItem);
                    inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = remainCount;
                }
            }
            else
            {
                // 아이템을 갖고 있지 않은 경우

                // 추가하려는 개수가 묶음 수량보다 적어질 때(더이상 새로운 묶음을 안만들어도 될 때)까지 반복
                while (count > item.InvenMaxCount)
                {
                    // 새로운 묶음을 만들기 위해 아이템 데이터를 복사하여 새로운 아이템을 만듦
                    InventoryItem cloneItem = item.CopyInventoryData();
                    
                    // 새로운 아이템을 인벤토리에 저장 후, 현재 보유 개수를 묶음 최대 보유 개수로 수정 
                    inventoryItems.Add(cloneItem);
                    inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = item.InvenMaxCount;

                    // 추가하려는 개수에서 묶음을 만든 개수만큼 뺌
                    count -= item.InvenMaxCount;
                }

                // 아이템을 인벤토리에 저장 후, 현재 보유 개수를 추가하려는 개수로 변경
                inventoryItems.Add(item);
                inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = count;
            }
        }

        // 인벤토리에서 제거 (뒤에서부터 순차 제거)
        public void RemoveInventoryItem(InventoryItem item, int count)
        {
            // 제거하려는 개수가 0이 될 때(다 제거했을 때)까지 반복
            while (count > 0)
            {
                // 인벤토리 내에서 제거하려는 아이템의 인덱스를 찾아, 해당 아이템의 현재 보유 개수를 구함
                int index = FindInventoryItem(item);
                int indexItemCount = GameManager.instance.InventoryItems[index].InvenCurrentCount;

                if (count < indexItemCount)
                {
                    // 현재 보유 개수가 제거하려는 개수보다 많은 경우
                    // 현재 보유 개수에서 제거하려는 개수만큼 뺌
                    GameManager.instance.InventoryItems[index].InvenCurrentCount -= count;
                    count = 0;
                }
                else if (count == indexItemCount)
                {
                    // 현재 보유 개수가 제거하려는 개수와 같은 경우
                    // 해당 아이템 제거
                    GameManager.instance.InventoryItems.RemoveAt(index);
                    count = 0;
                }
                else
                {
                    // 현재 보유 개수가 제거하려는 개수보다 적은 경우
                    // 제거하려는 개수에서 현재 보유 개수만큼 뺌
                    count -= indexItemCount;
                    GameManager.instance.InventoryItems.RemoveAt(index);
                }
            }
        }

        // 인벤토리 내에서 해당하는 아이템의 총 보유 개수 구하기
        public int FindItemTotalCount(InventoryItem item)
        {
            // 총 보유 개수 0으로 초기화
            int sum = 0;

            // 인벤토리 내의 아이템을 하나씩 확인하며 해당하는 아이템을 찾으면 현재 보유 개수를 더해나감
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].ItemId == item.ItemId)
                {
                    sum += inventoryItems[i].InvenCurrentCount;
                }
            }

            // 총 보유 개수 반환
            return sum;
        }

        // 인벤토리 내에서 해당하는 아이템의 인덱스 찾기
        public int FindInventoryItem(InventoryItem item)
        {
            // 인벤토리 내의 아이템을 하나씩 확인하며 해당되는 아이템을 찾으면 인덱스 반환
            // 뒤에서부터 찾는 이유는 해당 함수가 아이템을 제거할 때 주로 쓰이므로 뒤에서부터 제거하기 위함
            for(int i = inventoryItems.Count - 1; i >= 0; i--)
            {
                if(inventoryItems[i].ItemId == item.ItemId)
                {
                    return i;
                }
            }

            // 인벤토리 내에 해당되는 아이템이 없는 경우 -1 반환
            return -1;
        }

        // 보스맵에서 쓰이는 아이템 등록(저장)
        public void AddBossItem(InventoryItem item, int index)
        {
            // 보스맵에서 쓰이는 아이템에 등록이 안되어 있는 아이템만 등록 가능 (중복 아이템 등록 방지)
            if (FindBossItem(item) == -1) bossItems[index] = item;
        }

        // 보스맵에서 쓰이는 아이템 목록에서 해당 아이템 찾기
        public int FindBossItem(InventoryItem item)
        {
            // 보스맵에서 쓰이는 아이템 목록을 하나씩 확인하며 해당되는 아이템을 찾으면 인덱스 반환
            for (int i = bossItems.Length - 1; i >= 0; i--)
            {
                if (bossItems[i] != null && bossItems[i].ItemId == item.ItemId)
                {
                    return i;
                }
            }

            // 보스맵에서 쓰이는 아이템 목록 내에 해당되는 아이템이 없는 경우 -1 반환
            return -1;
        }

        // 플레이어 레벨에 맞춰 경험치통 최대치 세팅
        void SetPlayerExpMax()
        {
            // 플레이어 경험치통 최대치를 계산한 후 세팅
            playerExpMax = GetPlayerExpMax();
        }


        // 플레이어 경험치통 최대치를 계산
        float GetPlayerExpMax()
        {
            // (기본 제공 수치 * (레벨-1)^2) / 25 + 추가 제공 수치 + (레벨-1)^2
            return 50 * Mathf.Pow((playerLevel - 1), 2) / 25.0f + 50 + Mathf.Pow((playerLevel - 1), 2);
        }

        // 플레이어의 경험치 획득 처리
        public void PlayerAddExp(float exp)
        {
            // 현재 경험치에 획득한 경험치를 더함
            playerExp += exp;

            if(playerExp >= playerExpMax)
            {
                // 플레이어의 현재 경험치가 최대 경험치를 넘어선 경우

                // 플레이어 레벨 1 증가 후(레벨업), 현재 경험치 0으로 초기화(넘어선 경험치는 삭제 처리)
                playerLevel++;
                playerExp = 0.0f;

                // 플레이어 레벨에 맞춰 경험치통 최대치 재세팅
                playerExpMax = GetPlayerExpMax();

                // 플레이어의 능력치 증가 및 필드 아이템 추가 획득(레벨업 보상)
                AddLevelUpPlayerStatus();
                AddLevelUpFieldItems();
            }

            // 플레이어 레벨 및 경험치바 UI 세팅
            PlayerLevelManager.instance.SetPlayerLevelAndExpBar(playerLevel, playerExpMax, playerExp);
        }

        // 필드 아이템 추가 획득(레벨업 보상)
        public void AddLevelUpFieldItems()
        {
            for (int i = 0; i < fieldItems.Length; i++)
            {
                // 각 필드 아이템 별로 1개씩 추가 획득
                fieldItems[i].InvenCurrentCount++;

                // 현재 보유 개수가 최대 보유 개수를 넘어선 경우, 최대 보유 개수로 수정
                if (fieldItems[i].InvenCurrentCount > fieldItems[i].InvenMaxCount)
                    fieldItems[i].InvenCurrentCount = fieldItems[i].InvenMaxCount;

                // 현재 필드에 있는 경우 해당 아이템 슬롯 UI의 남은 개수 텍스트 변경
                if(gameState == State.fieldStage)
                    FieldUIManager.instance.SetItemCountText(i, fieldItems[i].InvenCurrentCount);
            }
        }

        // 플레이어의 능력치 증가
        public void AddLevelUpPlayerStatus()
        {
            // 체력 5, 공격력 1, 방어력 0.5, 이동속도 1 증가
            playerStatus.AddStatus(5.0f, 1.0f, 0.5f, 0.1f);
        }
    }
}