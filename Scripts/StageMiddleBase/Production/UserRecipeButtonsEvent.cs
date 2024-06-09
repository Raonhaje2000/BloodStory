using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserRecipeButtonsEvent : MonoBehaviour, IPointerClickHandler
    {
        Recipe itemRecipe;                 // 아이템 레시피

        public Recipe ItemRecipe
        { 
            get { return itemRecipe; }
            set { itemRecipe = value; }
        }

        void Start()
        {

        }

        // 레시피 버튼 초기화
        public void InitializeButtons(Recipe recipe)
        {
            // 해당 레시피를 버튼에 등록
            itemRecipe = recipe;
            
            if(itemRecipe.PossibleProductionLevel <= GameManager.instance.PlayerProductionLevel)
            {
                // 레시피의 요구 제작 숙련도 레벨이 플레이어의 제작 숙련도 레벨 이하인 경우

                // 레시피 버튼의 텍스트를 제작할 아이템 이름으로 표기 
                GetComponentInChildren<TextMeshProUGUI>().text = recipe.FinishedProductItem.ItemName;
            }
            else
            {
                // 레시피의 요구 제작 숙련도 레벨이 플레이어의 제작 숙련도 레벨보다 더 높은 경우

                // 레시피 버튼의 텍스트를 ???으로 표기
                GetComponentInChildren<TextMeshProUGUI>().text = "???";
            }
        }

        // 마우스 클릭 이벤트 처리
        public void OnPointerClick(PointerEventData eventData)
        {
            if(ProductionManager.instance != null)
            {
                // 제작 UI가 활성화 된 상태일 때

                if(itemRecipe.PossibleProductionLevel <= GameManager.instance.PlayerProductionLevel)
                {
                    // 레시피의 요구 제작 숙련도 레벨이 플레이어의 제작 숙련도 레벨 이하인 경우
                    // 제작 UI를 해당 레시피에 맞춰 세팅
                    ProductionManager.instance.SetCurrentRecipe(itemRecipe);
                }
            }
        }
    }
}
