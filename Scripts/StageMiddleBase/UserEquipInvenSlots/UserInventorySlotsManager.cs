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

        int inventoryItemCount;      // �κ��丮 ���� ������ ����

        GameObject[] inventorySlots; // �κ��丮 ���Ե�
        int inventoryPageMax;        // �κ��丮�� �ִ� ������ ��
        int inventoryPageCurrent;    // �κ��丮�� ���� ������ ��

        TextMeshProUGUI inventoryPageNum;  // ������ �ؽ�Ʈ UI

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
            inventorySlots = new GameObject[] { GameObject.Find("InventorySlotIcon0"), GameObject.Find("InventorySlotIcon1"), GameObject.Find("InventorySlotIcon2"),
                                                GameObject.Find("InventorySlotIcon3"), GameObject.Find("InventorySlotIcon4"), GameObject.Find("InventorySlotIcon5"),
                                                GameObject.Find("InventorySlotIcon6"), GameObject.Find("InventorySlotIcon7"), GameObject.Find("InventorySlotIcon8"),
                                                GameObject.Find("InventorySlotIcon9"), GameObject.Find("InventorySlotIcon10"), GameObject.Find("InventorySlotIcon11") };

            inventoryPageNum = GameObject.Find("PageNum").GetComponent<TextMeshProUGUI>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // �κ��丮 ������ �ʱ�ȭ
            InitializeInvenPage();

            // �κ��丮 ���� ����
            SetSlots();
        }

        void InitializeInvenPage()
        {
            // �κ��丮�� �ִ� ������ ������ �޾ƿ�
            inventoryItemCount = GameManager.instance.InventoryItems.Count;

            // �κ��丮 ������ ������ �κ��丮 ���� ������ ������ ��, �������� ������ ������ �ִ� �������� �����ϰ� �������� �ִ� ��� ��+1�� �ִ� �������� ������
            // ���������� �������̹Ƿ�, ������ �κ��� �߸��� ������ ������ �κ��� ������ �������� �ֱ� ����
            int temp = inventoryItemCount / inventorySlots.Length;
            inventoryPageMax = (inventoryItemCount % inventorySlots.Length == 0) ? temp : temp + 1;

            // ���� �������� 1�� �ʱ�ȭ
            inventoryPageCurrent = 1;

            // �κ��丮 ������ UI ����
            SetInvenPageUI();
        }

        // �κ��丮 ������ UI ����
        void SetInvenPageUI()
        {
            // ���� ������ / �ִ� ������ ����
            inventoryPageNum.text = inventoryPageCurrent.ToString() + " / " + inventoryPageMax.ToString();
        }

        // �κ��丮 ���� ����
        public void SetSlots()
        {
            // �κ��丮�� �ִ� ������ ������ �޾ƿ�
            inventoryItemCount = GameManager.instance.InventoryItems.Count;

            // �κ��丮�� �� ���Ժ� ������ ����
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                // �ش� �������� ���Կ� �ش��ϴ� �������� �κ��丮 �������� �ε����� �κ��丮 ���� ���� * (���� ������ - 1) + ���� ��ȣ�� ����
                // Ex) 2������ 1��° ĭ = 13��° ������ = �ε��� 12 (�迭�� 0���� �����ϱ� ����)
                int index = inventorySlots.Length * (inventoryPageCurrent - 1) + i;

                if(index < inventoryItemCount)
                {
                    // ���� �ε����� �κ��丮�� �ִ� ������ �������� ���� ��� (�������� �ִ� ������ ���)

                    // �ش� �κ��丮�� �ִ� �������� ������ �κ��丮 ���� ���� �� ���� Ȱ��ȭ
                    InventoryItem item = GameManager.instance.InventoryItems[index];

                    inventorySlots[i].GetComponent<UserInventorySlotsEvent>().InitializeSlots(item);
                    inventorySlots[i].SetActive(true);
                }
                else
                {
                    // ���� �ε����� �κ��丮�� �ִ� ������ �������� ū ��� (����ִ� ������ ���)
                    // �ش� ������ �������� null�� �ٲٰ�, ���� ��Ȱ��ȭ
                    inventorySlots[i].GetComponent<UserInventorySlotsEvent>().InitializeSlots(null);
                    inventorySlots[i].SetActive(false);
                }
            }

            // �κ��丮 ������ UI ����
            SetInvenPageUI();
        }

        // �������� ���� ȭ��ǥ�� Ŭ�� ���� ��
        public void ClickLeftArrow()
        {
            if(inventoryPageCurrent > 1)
            {
                // ���� �������� 1���� ū ��쿡�� ���� �������� �Ѿ
                inventoryPageCurrent--;

                // �κ��丮 ���� ����
                // �ش� �������� �ִ� �������� �����ֱ� ����
                SetSlots();
            }
        }

        // �������� ������ ȭ��ǥ�� Ŭ�� ���� ��
        public void ClickRightArrow()
        {
            if(inventoryPageCurrent < inventoryPageMax)
            {
                // ���� �������� �ִ� ���������� ���� ��쿡�� ���� �������� �Ѿ
                inventoryPageCurrent++;

                // �κ��丮 ���� ����
                // �ش� �������� �ִ� �������� �����ֱ� ����
                SetSlots();
            }
        }
    }
}
