using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ProductionUIManager : MonoBehaviour
    {
        public static ProductionUIManager instance;
 
        TextMeshProUGUI productionRecipeNameText;         // 제작 레시피 제목 텍스트
 
        Image productionItemIconImage;                    // 제작했을 때의 아이템 아이콘 이미지
        TextMeshProUGUI productionItemNameText;           // 제작했을 때의 아이템 이름 텍스트
        TextMeshProUGUI productionItemDescriptionText;    // 제작했을 때의 아이템 설명(툴팁) 텍스트

        Image[] productionMaterialIconImages;             // 재료 아이템들의 아이콘 이미지들
        TextMeshProUGUI[] productionMaterialText;         // 재료 아이템들의 '요구 개수/보유 개수' 텍스트들

        TextMeshProUGUI productionProbabilityTextValue;   // 대성공 확률 텍스트
        TextMeshProUGUI productionSkillValueTextValue;    // 제작시 획득하는 제작 숙련도 경험치 텍스트

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
            productionRecipeNameText = GameObject.Find("ProductionRecipeNameText").GetComponent<TextMeshProUGUI>();

            productionItemIconImage = GameObject.Find("ProductionItemIconImage").GetComponent<Image>();
            productionItemNameText = GameObject.Find("ProductionItemNameText").GetComponent<TextMeshProUGUI>();
            productionItemDescriptionText = GameObject.Find("ProductionItemDescriptionText").GetComponent<TextMeshProUGUI>();

            productionMaterialIconImages = new Image[] { GameObject.Find("ProductionMaterialIconImage1").GetComponent<Image>(),
                                                         GameObject.Find("ProductionMaterialIconImage2").GetComponent<Image>(),
                                                         GameObject.Find("ProductionMaterialIconImage3").GetComponent<Image>(),
                                                         GameObject.Find("ProductionMaterialIconImage4").GetComponent<Image>() };

            productionMaterialText = new TextMeshProUGUI[] { GameObject.Find("ProductionMaterialText1Value").GetComponent<TextMeshProUGUI>(),
                                                             GameObject.Find("ProductionMaterialText2Value").GetComponent<TextMeshProUGUI>(),
                                                             GameObject.Find("ProductionMaterialText3Value").GetComponent<TextMeshProUGUI>(),
                                                             GameObject.Find("ProductionMaterialText4Value").GetComponent<TextMeshProUGUI>() };

            productionProbabilityTextValue = GameObject.Find("ProductionProbabilityTextValue").GetComponent<TextMeshProUGUI>();
            productionSkillValueTextValue = GameObject.Find("ProductionSkillValueTextValue").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            // 제작 관련 UI 비활성화
            SetProductionActive(false);
        }

        // 제작 관련 UI 활성화/비활성화
        void SetProductionActive(bool active)
        {
            productionRecipeNameText.gameObject.SetActive(active);

            productionItemIconImage.gameObject.SetActive(active);
            productionItemNameText.gameObject.SetActive(active);
            productionItemDescriptionText.gameObject.SetActive(active);

            for(int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                productionMaterialIconImages[i].gameObject.SetActive(active);
                productionMaterialText[i].gameObject.SetActive(active);
            }

            productionProbabilityTextValue.gameObject.SetActive(active);
            productionSkillValueTextValue.gameObject.SetActive(active);
        }

        // 현재 선택한 제작 레시피에 따라 제작 UI 세팅
        public void SetProductionRecipe(Recipe recipe, float probability)
        {
            // 제작 관련 UI 활성화
            SetProductionActive(true);

            productionRecipeNameText.text = recipe.FinishedProductItem.ItemName + " 레시피";

            productionItemIconImage.sprite = recipe.FinishedProductItem.ItemIcon;
            productionItemNameText.text = recipe.FinishedProductItem.ItemName;
            productionItemDescriptionText.text = recipe.FinishedProductItem.ItemTooltip;

            // 필요한 재료 아이템 칸 세팅
            for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                if(recipe.ProductionMaterialItems[i] != null)
                {
                    // 재료 아이템이 존재하는 경우
                    // 재료 아이템 아이콘과 '요구 개수/보유 개수' 형태로 텍스트 세팅
                    productionMaterialIconImages[i].sprite = recipe.ProductionMaterialItems[i].ItemIcon;
                    productionMaterialText[i].text = recipe.PoductionMaterialNeedCount[i].ToString() + " / " + GameManager.instance.FindItemTotalCount(recipe.ProductionMaterialItems[i]).ToString();
                }
                else
                {
                    // 재료 아이템이 존재하지 않는 경우 (재료 아이템 종류가 3가지일 때, 4번째 칸 세팅 시)
                    // 해당 칸의 아이콘과 텍스트 비활성화
                    productionMaterialIconImages[i].gameObject.SetActive(false);
                    productionMaterialText[i].gameObject.SetActive(false);
                }
            }

            // 제작 대성공 확률 및 획득 숙련도 수치 텍스트 세팅
            productionProbabilityTextValue.text = probability.ToString("0.0");
            productionSkillValueTextValue.text = recipe.ProductionExp.ToString();
        }
    }
}
