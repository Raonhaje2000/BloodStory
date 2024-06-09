using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace RSA
{
    // �� ���� ������ ���������� ���̴� ������ ��Ƴ��� ó�� �Լ� ����
    // ���� ��ȯ�Ǿ ����־�� �ϴ� �����͵� ����
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        // ���� ����
        public enum State
        {
            title = 0,      // Ÿ��Ʋ
            fieldStage = 1, // �ʵ��
            bossStage = 2,  // ������
            middleBase = 3, // �߰� ����
            uiIActive = 4   // UI ��ȣ�ۿ�
        }

        // UI ����
        public enum UIState
        { 
            none = 0,           // �ƹ��͵� �ƴ�
            sub = 1,            // ���� UI (ĳ���� ����, �κ��丮 �� ���� â ���� �ߴ� UI)
            reinforcement = 2,  // ��ȭ UI
            attribute = 3,      // �Ӽ� UI
            production = 4,     // ���� UI
            shop = 5            // ���� UI
        }

        public State gameState;     // ���� ����
        public UIState gameUIState; // UI ����

        public const int MONEY_MAX = 99999; // ���� ��ȭ�� �ִ� ������

        // �������� ���� ������
        [SerializeField] int stage; // ���� ��������

        // �÷��̾� ���� ���� ������
        int playerLevel;    // �÷��̾� ����
        float playerExp;    // �÷��̾� ����ġ
        float playerExpMax; // �÷��̾� ����ġ �� �ִ�ġ
 
        int playerProductionLevel; // ���� ���õ� ����
        float playerProductionExp; // ���� ���õ� ����ġ

        // ĳ���� �ɷ�ġ
        Status playerStatus; // ĳ���� �ɷ�ġ

        // �÷��̾� ���� ���� ������
        int score; // ��ü ȹ�� ����

        // �÷��̾� ���� ��ȭ ���� ������
        int oxygen;  // ��� ������
        int iron;    // ö�� ������
        int mineral; // �̳׶� ������

        // �÷��̾� ������ ���� ������
        float durabilityMax;     // �������� �ִ�ġ

        float durabilityWeapon;  // ���� ���� ������
        float durabilityDefence; // ���� �� ������

        // �÷��̾� ������ ���� ������
        [SerializeField]
        List<EquipmentItem> equipmentItems; // �÷��̾ ������ ��� ������

        [SerializeField]
        List<InventoryItem> inventoryItems; // �÷��̾��� �κ��丮�� �ִ� ������

        [SerializeField]
        FieldItem[] fieldItems;             // �÷��̾ �ʵ忡�� ����ϴ� ������

        [SerializeField]
        InventoryItem[] bossItems;          // �÷��̾ �����ʿ��� ����ϴ� ������

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

                // ���� ������ �Ѿ�� ������Ʈ �ı����� �ʰ� ����
                // ���� ������ �������� ���̴� ������ ����
                DontDestroyOnLoad(gameObject);
            }
            else if(instance != this)
            {
                // �̹� ���� ���� �ش� ������Ʈ�� �������� ��� ����
                Destroy(gameObject);
            }

            stage = 1;

            // �ʱ�ȭ
            Initialize(); 

            // �÷��̾� ������ �ҷ�����
            LoadFieldItem(); // �ʵ忡�� ���̴� ������ �ҷ�����
            LoadEquipment(); // ��� ������ �ҷ�����
            LoadInventory(); // �κ��丮 ������ �ҷ�����
            LoadBossItem();  // �����ʿ��� ���̴� ������ �ҷ�����
        }

        void Start()
        {
            // �÷��̾� ������ ���� ����ġ�� �ִ�ġ ����
            SetPlayerExpMax();
        }

        // �ʱ�ȭ
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

        // �ʵ忡�� ���̴� ������ �ҷ�����
        void LoadFieldItem()
        {
            // �ʵ� ������ ScriptableObject�� �ҷ����� 
            FieldItem[] loadFieldItems = Resources.LoadAll<FieldItem>("RSA/ScriptableObjects/Item/FieldItem");

            // �ʵ� ������ ������ ���� �迭 ũ�� ���� (�ʵ� ������ ���� = 3��)
            fieldItems = new FieldItem[loadFieldItems.Length];

            for (int i = 0; i < fieldItems.Length; i++)
            {
                // �ʵ� ������ ������ �����ؼ� �����ϰ� ���� ���� ���� 3���� �ʱ�ȭ
                fieldItems[i] = loadFieldItems[i].CopyFieldItemData();
                fieldItems[i].InvenCurrentCount = 3;
            }
        }

        // ��� ������ �ҷ�����
        void LoadEquipment()
        {
            // ��� ������ ScriptableObject�� �ҷ�����
            EquipmentItem[] equipmentItemsArray = Resources.LoadAll<EquipmentItem>("RSA/ScriptableObjects/Item/EquipmentItem");
            
            // �迭�� ����Ʈ�� ��ȯ�ؼ� ����
            equipmentItems.AddRange(equipmentItemsArray);
        }

        // �κ��丮 ������ �ҷ�����
        void LoadInventory()
        {
            // �κ��丮 ������ ScriptableObject �ҷ��� ��, ������ �����ؼ� �����ϰ� ���� ���� ���� ����
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

        // �����ʿ��� ���̴� ������ �ҷ�����
        void LoadBossItem()
        {
            // ���� ������ ScriptableObject �ҷ��� ��, ������ �����ؼ� �κ��丮�� �����ϰ� ���� ���� ���� ����
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/RecoveryItem/IntermediatePotion").CopyInventoryData(), 10);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AttackItem/MudGrenade").CopyInventoryData(), 5);
            AddInventoryItem(Resources.Load<InventoryItem>("RSA/ScriptableObjects/Item/AggroItem/Scarecrow").CopyInventoryData(), 1);

            // ���� ������ ��Ͽ� ������ �������� �κ��丮 �������� ���� �ε��� (�κ��丮 �ڿ��� 3�� ������)
            int startIndex = inventoryItems.Count - 3;

            // ���� ������ ����� �ִ� 3���� ����
            bossItems = new InventoryItem[3];

            // �κ��丮 ���� �������� ���� ������ ��Ͽ� ���
            for(int i = 0; i < bossItems.Length; i++)
            {
                AddBossItem(inventoryItems[startIndex + i], i);
            }
        }

        // �κ��丮�� ������ �߰�
        public void AddInventoryItem(InventoryItem item, int count)
        {
            int index = FindInventoryItem(item); // �ߺ� Ȯ���� ���� �ڿ������� �ε��� üũ

            if (index >= 0)
            {
                // �������� ���� �ִ� ��� ���� ���� ������ �߰��ϴ� ���� ���ϱ�
                inventoryItems[index].InvenCurrentCount += count;

                if (inventoryItems[index].InvenCurrentCount > inventoryItems[index].InvenMaxCount)
                {
                    // ������ ���� �ִ� ���� �������� ���� ��� (������ ���� ������ �ϴ� ���)

                    // ���ο� ������ ������� �� �����ִ� ���� ���
                    int remainCount = inventoryItems[index].InvenCurrentCount - inventoryItems[index].InvenMaxCount;

                    // ���� ������ ������ ���� ���� ������ ���� �ִ� ���� ������ ����
                    inventoryItems[index].InvenCurrentCount = inventoryItems[index].InvenMaxCount;

                    // �����ִ� ������ ���� �ִ� ���� �������� ������ ��(���̻� ���ο� ������ �ȸ��� �� ��)���� �ݺ�
                    while (remainCount > item.InvenMaxCount)
                    {
                        // ���ο� ������ ����� ���� ������ �����͸� �����Ͽ� ���ο� �������� ����
                        InventoryItem cloneItem = item.CopyInventoryData();

                        // ���ο� �������� �κ��丮�� ���� ��, ���� ���� ������ ���� �ִ� ���� ������ ���� 
                        inventoryItems.Add(cloneItem);
                        inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = item.InvenMaxCount;

                        // ���� �������� ������ ���� ������ŭ ��
                        remainCount -= item.InvenMaxCount;
                    }

                    // �����ִ� ������ ���� �ִ� ���� �������� ���� ���̻� ���ο� ������ �ȸ��� �� ���
                    // ���ο� ������ ����� ���� ������ �����͸� �����Ͽ� ���ο� �������� ����
                    InventoryItem cloneRemainItem = item.CopyInventoryData();

                    // ���ο� �������� �κ��丮�� ���� ��, ���� ���� ������ �����ִ� ������ ����
                    inventoryItems.Add(cloneRemainItem);
                    inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = remainCount;
                }
            }
            else
            {
                // �������� ���� ���� ���� ���

                // �߰��Ϸ��� ������ ���� �������� ������ ��(���̻� ���ο� ������ �ȸ��� �� ��)���� �ݺ�
                while (count > item.InvenMaxCount)
                {
                    // ���ο� ������ ����� ���� ������ �����͸� �����Ͽ� ���ο� �������� ����
                    InventoryItem cloneItem = item.CopyInventoryData();
                    
                    // ���ο� �������� �κ��丮�� ���� ��, ���� ���� ������ ���� �ִ� ���� ������ ���� 
                    inventoryItems.Add(cloneItem);
                    inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = item.InvenMaxCount;

                    // �߰��Ϸ��� �������� ������ ���� ������ŭ ��
                    count -= item.InvenMaxCount;
                }

                // �������� �κ��丮�� ���� ��, ���� ���� ������ �߰��Ϸ��� ������ ����
                inventoryItems.Add(item);
                inventoryItems[inventoryItems.Count - 1].InvenCurrentCount = count;
            }
        }

        // �κ��丮���� ���� (�ڿ������� ���� ����)
        public void RemoveInventoryItem(InventoryItem item, int count)
        {
            // �����Ϸ��� ������ 0�� �� ��(�� �������� ��)���� �ݺ�
            while (count > 0)
            {
                // �κ��丮 ������ �����Ϸ��� �������� �ε����� ã��, �ش� �������� ���� ���� ������ ����
                int index = FindInventoryItem(item);
                int indexItemCount = GameManager.instance.InventoryItems[index].InvenCurrentCount;

                if (count < indexItemCount)
                {
                    // ���� ���� ������ �����Ϸ��� �������� ���� ���
                    // ���� ���� �������� �����Ϸ��� ������ŭ ��
                    GameManager.instance.InventoryItems[index].InvenCurrentCount -= count;
                    count = 0;
                }
                else if (count == indexItemCount)
                {
                    // ���� ���� ������ �����Ϸ��� ������ ���� ���
                    // �ش� ������ ����
                    GameManager.instance.InventoryItems.RemoveAt(index);
                    count = 0;
                }
                else
                {
                    // ���� ���� ������ �����Ϸ��� �������� ���� ���
                    // �����Ϸ��� �������� ���� ���� ������ŭ ��
                    count -= indexItemCount;
                    GameManager.instance.InventoryItems.RemoveAt(index);
                }
            }
        }

        // �κ��丮 ������ �ش��ϴ� �������� �� ���� ���� ���ϱ�
        public int FindItemTotalCount(InventoryItem item)
        {
            // �� ���� ���� 0���� �ʱ�ȭ
            int sum = 0;

            // �κ��丮 ���� �������� �ϳ��� Ȯ���ϸ� �ش��ϴ� �������� ã���� ���� ���� ������ ���س���
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].ItemId == item.ItemId)
                {
                    sum += inventoryItems[i].InvenCurrentCount;
                }
            }

            // �� ���� ���� ��ȯ
            return sum;
        }

        // �κ��丮 ������ �ش��ϴ� �������� �ε��� ã��
        public int FindInventoryItem(InventoryItem item)
        {
            // �κ��丮 ���� �������� �ϳ��� Ȯ���ϸ� �ش�Ǵ� �������� ã���� �ε��� ��ȯ
            // �ڿ������� ã�� ������ �ش� �Լ��� �������� ������ �� �ַ� ���̹Ƿ� �ڿ������� �����ϱ� ����
            for(int i = inventoryItems.Count - 1; i >= 0; i--)
            {
                if(inventoryItems[i].ItemId == item.ItemId)
                {
                    return i;
                }
            }

            // �κ��丮 ���� �ش�Ǵ� �������� ���� ��� -1 ��ȯ
            return -1;
        }

        // �����ʿ��� ���̴� ������ ���(����)
        public void AddBossItem(InventoryItem item, int index)
        {
            // �����ʿ��� ���̴� �����ۿ� ����� �ȵǾ� �ִ� �����۸� ��� ���� (�ߺ� ������ ��� ����)
            if (FindBossItem(item) == -1) bossItems[index] = item;
        }

        // �����ʿ��� ���̴� ������ ��Ͽ��� �ش� ������ ã��
        public int FindBossItem(InventoryItem item)
        {
            // �����ʿ��� ���̴� ������ ����� �ϳ��� Ȯ���ϸ� �ش�Ǵ� �������� ã���� �ε��� ��ȯ
            for (int i = bossItems.Length - 1; i >= 0; i--)
            {
                if (bossItems[i] != null && bossItems[i].ItemId == item.ItemId)
                {
                    return i;
                }
            }

            // �����ʿ��� ���̴� ������ ��� ���� �ش�Ǵ� �������� ���� ��� -1 ��ȯ
            return -1;
        }

        // �÷��̾� ������ ���� ����ġ�� �ִ�ġ ����
        void SetPlayerExpMax()
        {
            // �÷��̾� ����ġ�� �ִ�ġ�� ����� �� ����
            playerExpMax = GetPlayerExpMax();
        }


        // �÷��̾� ����ġ�� �ִ�ġ�� ���
        float GetPlayerExpMax()
        {
            // (�⺻ ���� ��ġ * (����-1)^2) / 25 + �߰� ���� ��ġ + (����-1)^2
            return 50 * Mathf.Pow((playerLevel - 1), 2) / 25.0f + 50 + Mathf.Pow((playerLevel - 1), 2);
        }

        // �÷��̾��� ����ġ ȹ�� ó��
        public void PlayerAddExp(float exp)
        {
            // ���� ����ġ�� ȹ���� ����ġ�� ����
            playerExp += exp;

            if(playerExp >= playerExpMax)
            {
                // �÷��̾��� ���� ����ġ�� �ִ� ����ġ�� �Ѿ ���

                // �÷��̾� ���� 1 ���� ��(������), ���� ����ġ 0���� �ʱ�ȭ(�Ѿ ����ġ�� ���� ó��)
                playerLevel++;
                playerExp = 0.0f;

                // �÷��̾� ������ ���� ����ġ�� �ִ�ġ �缼��
                playerExpMax = GetPlayerExpMax();

                // �÷��̾��� �ɷ�ġ ���� �� �ʵ� ������ �߰� ȹ��(������ ����)
                AddLevelUpPlayerStatus();
                AddLevelUpFieldItems();
            }

            // �÷��̾� ���� �� ����ġ�� UI ����
            PlayerLevelManager.instance.SetPlayerLevelAndExpBar(playerLevel, playerExpMax, playerExp);
        }

        // �ʵ� ������ �߰� ȹ��(������ ����)
        public void AddLevelUpFieldItems()
        {
            for (int i = 0; i < fieldItems.Length; i++)
            {
                // �� �ʵ� ������ ���� 1���� �߰� ȹ��
                fieldItems[i].InvenCurrentCount++;

                // ���� ���� ������ �ִ� ���� ������ �Ѿ ���, �ִ� ���� ������ ����
                if (fieldItems[i].InvenCurrentCount > fieldItems[i].InvenMaxCount)
                    fieldItems[i].InvenCurrentCount = fieldItems[i].InvenMaxCount;

                // ���� �ʵ忡 �ִ� ��� �ش� ������ ���� UI�� ���� ���� �ؽ�Ʈ ����
                if(gameState == State.fieldStage)
                    FieldUIManager.instance.SetItemCountText(i, fieldItems[i].InvenCurrentCount);
            }
        }

        // �÷��̾��� �ɷ�ġ ����
        public void AddLevelUpPlayerStatus()
        {
            // ü�� 5, ���ݷ� 1, ���� 0.5, �̵��ӵ� 1 ����
            playerStatus.AddStatus(5.0f, 1.0f, 0.5f, 0.1f);
        }
    }
}