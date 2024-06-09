using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class UserProductionRecipeManager : MonoBehaviour
    {
        public static UserProductionRecipeManager instance;

        Recipe[] recipes;               // 제작 레시피들
        Image[] buttons;                // 제작 레시피 버튼들

        TextMeshProUGUI recipePageNum;  // 레시피 페이지 UI 텍스트

        int recipePageCurrent;          // 현재 레시피 페이지 수
        int recipePageMax;              // 최대 레시피 페이지 수

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
            recipes = new Recipe[] { Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/AdvancedPotionRecipe"),
                                     Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/MudGrenadeRecipe"),
                                     Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/ScarecrowRecipe"),
                                     Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/FinestPotionRecipe"),
                                     Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/ElectricGrenadeRecipe"),
                                     Resources.Load<Recipe>("RSA/ScriptableObjects/Recipe/WoodenScarecrowRecipe")   };

            buttons = new Image[] { GameObject.Find("ProductionRecipeButton0").GetComponent<Image>(), GameObject.Find("ProductionRecipeButton1").GetComponent<Image>(),
                                    GameObject.Find("ProductionRecipeButton2").GetComponent<Image>(), GameObject.Find("ProductionRecipeButton3").GetComponent<Image>() };

            recipePageNum = GameObject.Find("RecipePageNum").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            // 레시피 개수를 레시피 버튼 개수로 나눴을 때, 나머지가 없으면 몫으로 최대 페이지를 설정하고 나머지가 있는 경우 몫+1로 최대 페이지를 설정함
            // 정수끼리의 나눗셈이므로, 나머지 부분이 잘리기 때문에 나머지 부분을 여분의 페이지에 넣기 위함
            int temp = recipes.Length / buttons.Length;
            recipePageMax = (recipes.Length % buttons.Length == 0) ? temp : temp + 1;

            // 현재 페이지를 1로 초기화
            recipePageCurrent = 1;

            // 레시피 버튼 세팅
            SetRecipeButtons();

            // 레시피 페이지 UI 텍스트 변경
            SetPageText();
        }

        // 레시피 페이지 UI 텍스트 변경
        void SetPageText()
        {
            // '현재 페이지 / 최대 페이지' 형태
            recipePageNum.text = recipePageCurrent.ToString() + " / " + recipePageMax.ToString();
        }

        // 레시피 버튼 세팅
        public void SetRecipeButtons()
        {
            // 레시피의 각 버튼 별 레시피 세팅
            for (int i = 0; i < buttons.Length; i++)
            {
                // 해당 페이지의 버튼에 해당하는 레시피의 목록에서의 인덱스는 '버튼 개수 * (현재 페이지 - 1) + 버튼 번호'와 같음
                int index = buttons.Length * (recipePageCurrent - 1) + i;

                if (index < recipes.Length)
                {
                    // 구한 인덱스가 레시피 목록 개수보다 작은 경우 (레시피가 있는 버튼일 경우)

                    // 해당 목록에 있는 레시피를 가져와 레시피 버튼 세팅 후 버튼 활성화
                    Recipe recipe = recipes[index];

                    buttons[i].GetComponent<UserRecipeButtonsEvent>().InitializeButtons(recipe);
                    buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    // 구한 인덱스가 레시피 목록 개수보다 큰 경우 (비어있는 버튼일 경우)
                    // 버튼 비활성화
                    buttons[i].gameObject.SetActive(false);
                }
            }

            // 레시피 페이지 UI 텍스트 변경
            SetPageText();
        }

        // 페이지의 왼쪽 화살표를 클릭 했을 때
        public void ClickLeftArrow()
        {
            if (recipePageCurrent > 1)
            {
                // 현재 페이지가 1보다 큰 경우에만 이전 페이지로 넘어감
                recipePageCurrent--;

                // 레시피 버튼 세팅
                // 해당 페이지에 있는 레시피를 보여주기 위함
                SetRecipeButtons();
            }
        }

        // 페이지의 오른쪽 화살표를 클릭 했을 때
        public void ClickRightArrow()
        {
            if (recipePageCurrent < recipePageMax)
            {
                // 현재 페이지가 최대 페이지보다 작은 경우에만 다음 페이지로 넘어감
                recipePageCurrent++;

                // 레시피 버튼 세팅
                // 해당 페이지에 있는 레시피를 보여주기 위함
                SetRecipeButtons();
            }
        }
    }
}
