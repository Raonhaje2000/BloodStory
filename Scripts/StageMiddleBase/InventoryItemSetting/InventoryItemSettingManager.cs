using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class InventoryItemSettingManager : MonoBehaviour
    {
        const int ITEM_SETTING_MAX_COUNT = 3;  // ���� ������ ������ ������ �ִ� ����

        public static InventoryItemSettingManager instance;

        Image[] redItems;   // �������� ����ϴ� ������ ������
        Image[] whiteItems; // �������� ����ϴ� ������ ������

        TextMeshProUGUI[] redItemsCount;   // �������� ����ϴ� ������ ���� �ؽ�Ʈ
        TextMeshProUGUI[] whiteItemsCount; // �������� ����ϴ� ������ ���� �ؽ�Ʈ

        FieldItem[] fieldItems;    // �ʵ忡�� ����ϴ� ������ (= �������� ����ϴ� ������)
        InventoryItem[] bossItems; // �����ʿ��� ����ϴ� ������ (= �������� ����ϴ� ������)

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ�� �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObjects()
        {
            // �� �迭�� ���� ������Ʈ �̸��� ����1, ����2, ����3 ������ �Ǿ��ֱ� ������ for���� ���� �� �� ���ڸ� �ٲ㰡�� ������Ʈ�� �ҷ���
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

        // �ʱ�ȭ
        void Initialize()
        {
            for(int i = 0; i < fieldItems.Length; i++)
            {
                if (fieldItems[i] != null)
                {
                    // �ʵ� �������� ������ �ش� �����ۿ� ���� ���� �����ܰ� ������ ���� �ؽ�Ʈ ���� (�迭�̱� ������ null üũ)
                    redItems[i].sprite = fieldItems[i].ItemIcon;
                    redItemsCount[i].text = fieldItems[i].InvenCurrentCount.ToString();
                }
            }

            // ������ ������ ���� ����
            SetWhiteItemSlot();
        }

        // ������ ������ ���� ����
        public void SetWhiteItemSlot()
        {
            for (int i = 0; i < bossItems.Length; i++)
            {
                // ������ ������ ������ �������� �־��� ���� �� �� �ֱ� ������ null üũ�� ���� Ȱ�� ���� �ʼ�
                if (bossItems[i] != null)
                {
                    // ���� �������� ������ �ش� �����ۿ� ���� ���� �����ܰ� ������ ���� �ؽ�Ʈ ����
                    whiteItems[i].sprite = bossItems[i].ItemIcon;
                    whiteItemsCount[i].text = bossItems[i].InvenCurrentCount.ToString();

                    // ���� Ȱ��ȭ
                    whiteItems[i].gameObject.SetActive(true);
                    whiteItemsCount[i].gameObject.SetActive(true);

                    // ���Կ� �ش� ������ ���
                    whiteItems[i].gameObject.GetComponent<InventoryItemSettingEvent>().SetItemSettingSlot(bossItems[i]);
                }
                else
                {
                    // ���� ��Ȱ��ȭ
                    whiteItems[i].gameObject.SetActive(false);
                    whiteItemsCount[i].gameObject.SetActive(false);

                    // ���Կ� �ش� �������� null�� ���
                    whiteItems[i].gameObject.GetComponent<InventoryItemSettingEvent>().SetItemSettingSlot(null);
                }
            }
        }

        // ������ ������ ���Կ� ������ ���
        // (������ ���� ���Կ� ���� �κ��丮���� Ŭ���� ������ ���)
        public void AddItemSettingSlot(InventoryItem item)
        {
            // ������ ������ ���Կ��� ����ִ� ���� ã��
            int index = FindEmptySettingSlot();

            // ����ִ� ������ ������ �ִ� ��� �����ʿ��� ����ϴ� ������ ��Ͽ� �ش� ������ �߰�
            if (index != -1) GameManager.instance.AddBossItem(item, index);

            // ������ ������ ���� ����
            SetWhiteItemSlot();
        }

        // ������ ������ ���Կ��� ������ ����
        public void RemoveItemSettingSlot(InventoryItem item)
        {
            // �����ʿ��� ����ϴ� ������ ��Ͽ��� �ش� �������� ã�ƿ�
            int index = GameManager.instance.FindBossItem(item);

            // ��Ͽ��� �ش� �������� ���(������)
            bossItems[index] = null;

            // ������ ������ ���� ����
            SetWhiteItemSlot();
        }
        
        // ������ ������ ���Կ��� ����ִ� ���� ã��
        int FindEmptySettingSlot()
        {
            for(int i = 0; i < bossItems.Length; i++)
            {
                // �������� ����ϴ� �������� ����ִ�(����) ��� �ش� �ε��� ��ȯ
                if (bossItems[i] == null) return i;
            }

            // �� ���ִ� ��� -1 ��ȯ
            return -1;
        }

        // X ��ư�� Ŭ���� ���
        public void ClickButtonX()
        {
            // �ش� UI ���� (�ݱ�)
            Destroy(gameObject);
        }
    }
}