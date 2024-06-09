using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class ProductionManager : MonoBehaviour
    {
        public static ProductionManager instance;

        GameObject messageBoxProgress;  // ���� ���� �޼��� �ڽ�
        GameObject minigame;            // �̴ϰ��� â
        GameObject messageBoxSuccess;   // ���� ��� �޼��� �ڽ�

        Recipe currentRecipe;           // ���� ���õ� ������

        float probability;              // ���� �뼺�� Ȯ��

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ�� �ҷ�����
            LoadObject();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObject()
        {
            messageBoxProgress = GameObject.Find("ProductionMessageBox");
            minigame = GameObject.Find("ProductionMinigame");
            messageBoxSuccess = GameObject.Find("ProductionSuccessMessageBox");
        }

        // �ʱ�ȭ
        void Initialize()
        {
            messageBoxProgress.SetActive(false);
            minigame.SetActive(false);
            messageBoxSuccess.SetActive(false);

            currentRecipe = null;

            // ���� �뼺�� Ȯ���� ���� ���õ� ���� * 2.5�� ����
            probability = GameManager.instance.PlayerProductionLevel * 2.5f;
        }

        // ���� ���õ� ������ ����
        public void SetCurrentRecipe(Recipe recipe)
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                // ���� ���õ� �����Ǹ� ������ �����Ƿ� ����
                currentRecipe = recipe;

                // ���� ������ ���� �����Ƿ� ���� UI ����
                ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
            }
        }

        // ���� �뼺�� Ȯ�� ������Ʈ
        public void UpdateProbability()
        {
            probability = GameManager.instance.PlayerProductionLevel * 2.5f;

            // ���� ���õ� ������ ���� (���� �뼺�� Ȯ���� ����Ǿ��� ����)
            SetCurrentRecipe(currentRecipe);
        }

        // ���� ���� ���� Ȯ��
        bool IsProductionPossible()
        {
            if(currentRecipe != null)
            {
                // ���� ���õ� �����ǰ� �ִ� ���

                // ��� �������� �ʿ��� ��ŭ �ִ��� Ȯ��
                for(int i = 0; i < currentRecipe.ProductionMaterialItems.Length; i++)
                {
                    if(currentRecipe.ProductionMaterialItems[i] != null)
                    {
                        // �����ǿ� ��� �������� �����ϴ� ���

                        // �ش� ��� �������� �䱸 ������ �κ��丮 �� ���� ���� ���
                        int needCount = currentRecipe.PoductionMaterialNeedCount[i];
                        int inventoryCount = GameManager.instance.FindItemTotalCount(currentRecipe.ProductionMaterialItems[i]);

                        // �ش� ��� �������� ���� ������ �䱸 �������� ������ ��� ���� �Ұ���
                        if (needCount > inventoryCount) return false;
                    }
                }

                // ��� �������� �ʿ��� ��ŭ �� �ִ� ��� ���� ����
                return true;
            }
            else
            {
                // ���� ���õ� �����ǰ� ���� ��� ���� �Ұ���
                return false;
            }
        }

        // ������ ���� ��ư�� Ŭ�� ���� ��
        public void ClickProductionButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxSuccess.activeSelf)
            {
                if (IsProductionPossible())
                {
                    // ������ ������ ���
                    // ���� ������ ������ ���� ���� ���� �޼��� �ڽ� ���� �� Ȱ��ȭ
                    messageBoxProgress.GetComponent<ProductionMessageBox>().SetMessageBoxProgressText(currentRecipe);
                    messageBoxProgress.SetActive(true);
                }
            }
        }

        // ���� ���� �޼��� �ڽ����� ������ ����Ѵٰ� �������� ��
        public void ContinueProductionProgressButton()
        {
            // ���� ���� �޼��� �ڽ� ��Ȱ��ȭ
            messageBoxProgress.SetActive(false);

            // �̴ϰ��� â Ȱ��ȭ (�̴ϰ��� ����)
            minigame.SetActive(true);
        }

        // ���� ���� �޼��� �ڽ����� ������ ������� �ʴ´ٰ� �������� ��
        public void CancleProductionProgressButton()
        {
            // ���� ���� �޼��� �ڽ� ��Ȱ��ȭ
            messageBoxProgress.SetActive(false);
        }

        // ������ ������ (�̴ϰ����� ����)
        public void ProgressProduction(float additionProbability)
        {
            // �̴ϰ��� â ��Ȱ��ȭ
            minigame.SetActive(false);

            // �̴ϰ������� �߰��� Ȯ���� ���� ������ �뼺���� ��� ���� �뼺�� ó��, �׷��� ���� ��� ���� ó��
            if (IsProductionGreatSuccess(additionProbability)) ProduceGreatSuccess();
            else ProduceSuccess();
        }

        // ������ �뼺�� �ߴ��� Ȯ��
        bool IsProductionGreatSuccess(float additionProbability)
        {
            // ���� �뼺�� Ȯ���� �̴ϰ������� �߰��� Ȯ���� ���� (���� �뼺�� Ȯ��)
            float finalProbability = probability + additionProbability;

            // 0���� 100������ �Ǽ��� �������� �ϳ� ����
            float randNum = Random.Range(0.0f, 100.0f);

            Debug.Log("probability: " + finalProbability + " / randNum: " + randNum);

            // �������� ���� ���� ���� �뼺�� Ȯ�� ������ ��� ���� �뼺��, �ƴ� ��� ���� ����
            return (randNum <= finalProbability) ? true : false;
        }

        // ���� �뼺�� ó��
        void ProduceGreatSuccess()
        {
            // ���ۿ� �ʿ��� ��� ������ �䱸 ������ŭ �κ��丮���� ����
            SubtractMaterialItem();

            InventoryItem producedItem = currentRecipe.FinishedProductItem.CopyInventoryData();

            // ���� �������� ȹ���� ������ �κ��丮�� �߰� (�뼺�� �� 2�� ȹ��)
            AddProducedItem(producedItem, 2);

            // ���� ���õ� ����ġ ȹ�� (�뼺�� �� ���õ� ����ġ 2�� ȹ��)
            AddProductionExp(true);

            // ���� ��� �޼��� �ڽ� ���� �� Ȱ��ȭ
            messageBoxSuccess.GetComponent<ProductionMessageBoxSuccess>().SetMessageBoxSuccess(currentRecipe, true);
            messageBoxSuccess.SetActive(true);
        }

        // ���� ���� ó��
        void ProduceSuccess()
        {
            // ���ۿ� �ʿ��� ��� ������ �䱸 ������ŭ �κ��丮���� ����
            SubtractMaterialItem();

            InventoryItem producedItem = currentRecipe.FinishedProductItem.CopyInventoryData();

            // ���� �������� ȹ���� ������ �κ��丮�� �߰� (���� �� 1�� ȹ��)
            AddProducedItem(producedItem, 1);

            // ���� ���õ� ����ġ ȹ�� (���� �� ���õ� ����ġ 1�� ȹ��)
            AddProductionExp(false);

            // ���� ��� �޼��� �ڽ� ���� �� Ȱ��ȭ
            messageBoxSuccess.GetComponent<ProductionMessageBoxSuccess>().SetMessageBoxSuccess(currentRecipe, false);
            messageBoxSuccess.SetActive(true);
        }

        // X ��ư�� Ŭ������ ��
        public void ClickButtonX()
        {
            // ���� UI ������Ʈ ���� �� �߰� ���� �⺻ UI Ȱ��ȭ
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }

        // ���� �뼺�� ��ư�� Ŭ������ �� (ġƮŰ)
        public void ClickGreatSuccessButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                if (IsProductionPossible())
                {
                    // ������ ������ ���
                    // ���� �뼺�� ó��
                    ProduceGreatSuccess();
                }
            }
        }

        // ��� ���� ��ư�� Ŭ�� ���� �� (ġƮŰ)
        public void ClickFillMaterialItemButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                if (currentRecipe != null)
                {
                    // ���� ���õ� �����ǰ� �ִ� ���

                    // ��� ������ ���� �䱸 ������ŭ �κ��丮�� �߰�
                    for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
                    {
                        // �ش� ��� �����۰� �䱸 ���� ���
                        InventoryItem item = currentRecipe.ProductionMaterialItems[i];
                        int count = currentRecipe.PoductionMaterialNeedCount[i];

                        // ��� �������� ���� �ϴ� ��� �κ��丮�� �߰�
                        if (item != null) GameManager.instance.AddInventoryItem(item, count);
                    }

                    // �κ��丮 ���� �� ���� UI �缼��
                    UserInventorySlotsManager.instance.SetSlots();
                    ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
                }
            }
        }

        // ���� �������� ȹ���� ������ �κ��丮�� �߰�
        void AddProducedItem(InventoryItem producedItem, int count)
        {
            // ȹ���� ���� ��ŭ �κ��丮�� �߰�
            GameManager.instance.AddInventoryItem(producedItem, count);

            // �κ��丮 ���� �� ���� UI �缼��
            UserInventorySlotsManager.instance.SetSlots();
            ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
        }
        
        // ���ۿ� �ʿ��� ��� ������ �䱸 ������ŭ �κ��丮���� ����
        void SubtractMaterialItem()
        {
            for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                if (currentRecipe.ProductionMaterialItems[i] != null)
                {
                    // �ش� ��� ������ �䱸 ����
                    int remainCount = currentRecipe.PoductionMaterialNeedCount[i];

                    // �κ��丮���� �ش� ��� �������� ã�� �䱸 ������ŭ ����
                    GameManager.instance.RemoveInventoryItem(currentRecipe.ProductionMaterialItems[i], remainCount);
                }
            }

            // �κ��丮 ���� �缼��
            UserInventorySlotsManager.instance.SetSlots();
        }

        // ���� ���õ� ����ġ ȹ��
        void AddProductionExp(bool isDouble)
        {
            // 2�� ȹ�� true ���¸� ȹ�� ���õ� ����ġ�� 2��, �ƴ� ��� 1��
            float exp = (isDouble) ? currentRecipe.ProductionExp * 2 : currentRecipe.ProductionExp;

            // ���� ���õ� ����ġ ȹ�� �� ���õ� UI ����
            UserProductionLevelManager.instance.AddPlayerProductionExp(exp);
        }
    }
}
