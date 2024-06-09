using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class ProductionManager : MonoBehaviour
    {
        public static ProductionManager instance;

        GameObject messageBoxProgress;  // 제작 진행 메세지 박스
        GameObject minigame;            // 미니게임 창
        GameObject messageBoxSuccess;   // 제작 결과 메세지 박스

        Recipe currentRecipe;           // 현재 선택된 레시피

        float probability;              // 제작 대성공 확률

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트들 불러오기
            LoadObject();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 오브젝트들 불러오기
        void LoadObject()
        {
            messageBoxProgress = GameObject.Find("ProductionMessageBox");
            minigame = GameObject.Find("ProductionMinigame");
            messageBoxSuccess = GameObject.Find("ProductionSuccessMessageBox");
        }

        // 초기화
        void Initialize()
        {
            messageBoxProgress.SetActive(false);
            minigame.SetActive(false);
            messageBoxSuccess.SetActive(false);

            currentRecipe = null;

            // 제작 대성공 확률은 제작 숙련도 레벨 * 2.5와 같음
            probability = GameManager.instance.PlayerProductionLevel * 2.5f;
        }

        // 현재 선택된 레시피 세팅
        public void SetCurrentRecipe(Recipe recipe)
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                // 현재 선택된 레시피를 선택한 레시피로 변경
                currentRecipe = recipe;

                // 현재 선택한 제작 레시피로 제작 UI 세팅
                ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
            }
        }

        // 제작 대성공 확률 업데이트
        public void UpdateProbability()
        {
            probability = GameManager.instance.PlayerProductionLevel * 2.5f;

            // 현재 선택된 레시피 세팅 (제작 대성공 확률이 변경되었기 때문)
            SetCurrentRecipe(currentRecipe);
        }

        // 제작 가능 여부 확인
        bool IsProductionPossible()
        {
            if(currentRecipe != null)
            {
                // 현재 선택된 레시피가 있는 경우

                // 재료 아이템이 필요한 만큼 있는지 확인
                for(int i = 0; i < currentRecipe.ProductionMaterialItems.Length; i++)
                {
                    if(currentRecipe.ProductionMaterialItems[i] != null)
                    {
                        // 레시피에 재료 아이템이 존재하는 경우

                        // 해당 재료 아이템의 요구 개수와 인벤토리 내 보유 개수 계산
                        int needCount = currentRecipe.PoductionMaterialNeedCount[i];
                        int inventoryCount = GameManager.instance.FindItemTotalCount(currentRecipe.ProductionMaterialItems[i]);

                        // 해당 재료 아이템의 보유 개수가 요구 개수보다 부족한 경우 제작 불가능
                        if (needCount > inventoryCount) return false;
                    }
                }

                // 재료 아이템이 필요한 만큼 다 있는 경우 제작 가능
                return true;
            }
            else
            {
                // 현재 선택된 레시피가 없는 경우 제작 불가능
                return false;
            }
        }

        // 아이템 제작 버튼을 클릭 했을 때
        public void ClickProductionButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxSuccess.activeSelf)
            {
                if (IsProductionPossible())
                {
                    // 제작이 가능한 경우
                    // 현재 레시피 정보에 맞춰 제작 진행 메세지 박스 세팅 후 활성화
                    messageBoxProgress.GetComponent<ProductionMessageBox>().SetMessageBoxProgressText(currentRecipe);
                    messageBoxProgress.SetActive(true);
                }
            }
        }

        // 제작 진행 메세지 박스에서 제작을 계속한다고 선택했을 때
        public void ContinueProductionProgressButton()
        {
            // 제작 진행 메세지 박스 비활성화
            messageBoxProgress.SetActive(false);

            // 미니게임 창 활성화 (미니게임 진행)
            minigame.SetActive(true);
        }

        // 제작 진행 메세지 박스에서 제작을 계속하지 않는다고 선택했을 때
        public void CancleProductionProgressButton()
        {
            // 제작 진행 메세지 박스 비활성화
            messageBoxProgress.SetActive(false);
        }

        // 제작을 진행함 (미니게임이 끝남)
        public void ProgressProduction(float additionProbability)
        {
            // 미니게임 창 비활성화
            minigame.SetActive(false);

            // 미니게임으로 추가된 확률을 더해 제작이 대성공한 경우 제작 대성공 처리, 그렇지 않은 경우 성공 처리
            if (IsProductionGreatSuccess(additionProbability)) ProduceGreatSuccess();
            else ProduceSuccess();
        }

        // 제작이 대성공 했는지 확인
        bool IsProductionGreatSuccess(float additionProbability)
        {
            // 제작 대성공 확률에 미니게임으로 추가된 확률을 더함 (최종 대성공 확률)
            float finalProbability = probability + additionProbability;

            // 0부터 100까지의 실수를 랜덤으로 하나 뽑음
            float randNum = Random.Range(0.0f, 100.0f);

            Debug.Log("probability: " + finalProbability + " / randNum: " + randNum);

            // 랜덤으로 뽑은 값이 최종 대성공 확률 이하인 경우 제작 대성공, 아닌 경우 제작 성공
            return (randNum <= finalProbability) ? true : false;
        }

        // 제작 대성공 처리
        void ProduceGreatSuccess()
        {
            // 제작에 필요한 재료 아이템 요구 개수만큼 인벤토리에서 제거
            SubtractMaterialItem();

            InventoryItem producedItem = currentRecipe.FinishedProductItem.CopyInventoryData();

            // 제작 성공으로 획득한 아이템 인벤토리에 추가 (대성공 시 2개 획득)
            AddProducedItem(producedItem, 2);

            // 제작 숙련도 경험치 획득 (대성공 시 숙련도 경험치 2배 획득)
            AddProductionExp(true);

            // 제작 결과 메세지 박스 세팅 및 활성화
            messageBoxSuccess.GetComponent<ProductionMessageBoxSuccess>().SetMessageBoxSuccess(currentRecipe, true);
            messageBoxSuccess.SetActive(true);
        }

        // 제작 성공 처리
        void ProduceSuccess()
        {
            // 제작에 필요한 재료 아이템 요구 개수만큼 인벤토리에서 제거
            SubtractMaterialItem();

            InventoryItem producedItem = currentRecipe.FinishedProductItem.CopyInventoryData();

            // 제작 성공으로 획득한 아이템 인벤토리에 추가 (성공 시 1개 획득)
            AddProducedItem(producedItem, 1);

            // 제작 숙련도 경험치 획득 (성공 시 숙련도 경험치 1배 획득)
            AddProductionExp(false);

            // 제작 결과 메세지 박스 세팅 및 활성화
            messageBoxSuccess.GetComponent<ProductionMessageBoxSuccess>().SetMessageBoxSuccess(currentRecipe, false);
            messageBoxSuccess.SetActive(true);
        }

        // X 버튼을 클릭했을 때
        public void ClickButtonX()
        {
            // 제작 UI 오브젝트 제거 후 중간 거점 기본 UI 활성화
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }

        // 제작 대성공 버튼을 클릭했을 때 (치트키)
        public void ClickGreatSuccessButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                if (IsProductionPossible())
                {
                    // 제작이 가능한 경우
                    // 제작 대성공 처리
                    ProduceGreatSuccess();
                }
            }
        }

        // 재료 충전 버튼을 클릭 했을 때 (치트키)
        public void ClickFillMaterialItemButton()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf)
            {
                if (currentRecipe != null)
                {
                    // 현재 선택된 레시피가 있는 경우

                    // 재료 아이템 별로 요구 개수만큼 인벤토리에 추가
                    for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
                    {
                        // 해당 재료 아이템과 요구 개수 계산
                        InventoryItem item = currentRecipe.ProductionMaterialItems[i];
                        int count = currentRecipe.PoductionMaterialNeedCount[i];

                        // 재료 아이템이 존재 하는 경우 인벤토리에 추가
                        if (item != null) GameManager.instance.AddInventoryItem(item, count);
                    }

                    // 인벤토리 슬롯 및 제작 UI 재세팅
                    UserInventorySlotsManager.instance.SetSlots();
                    ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
                }
            }
        }

        // 제작 성공으로 획득한 아이템 인벤토리에 추가
        void AddProducedItem(InventoryItem producedItem, int count)
        {
            // 획득한 개수 만큼 인벤토리에 추가
            GameManager.instance.AddInventoryItem(producedItem, count);

            // 인벤토리 슬롯 및 제작 UI 재세팅
            UserInventorySlotsManager.instance.SetSlots();
            ProductionUIManager.instance.SetProductionRecipe(currentRecipe, probability);
        }
        
        // 제작에 필요한 재료 아이템 요구 개수만큼 인벤토리에서 제거
        void SubtractMaterialItem()
        {
            for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                if (currentRecipe.ProductionMaterialItems[i] != null)
                {
                    // 해당 재료 아이템 요구 개수
                    int remainCount = currentRecipe.PoductionMaterialNeedCount[i];

                    // 인벤토리에서 해당 재료 아이템을 찾아 요구 개수만큼 제거
                    GameManager.instance.RemoveInventoryItem(currentRecipe.ProductionMaterialItems[i], remainCount);
                }
            }

            // 인벤토리 슬롯 재세팅
            UserInventorySlotsManager.instance.SetSlots();
        }

        // 제작 숙련도 경험치 획득
        void AddProductionExp(bool isDouble)
        {
            // 2배 획득 true 상태면 획득 숙련도 경험치의 2배, 아닌 경우 1배
            float exp = (isDouble) ? currentRecipe.ProductionExp * 2 : currentRecipe.ProductionExp;

            // 제작 숙련도 경험치 획득 및 숙련도 UI 변경
            UserProductionLevelManager.instance.AddPlayerProductionExp(exp);
        }
    }
}
