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

        Recipe[] recipes;               // ���� �����ǵ�
        Image[] buttons;                // ���� ������ ��ư��

        TextMeshProUGUI recipePageNum;  // ������ ������ UI �ؽ�Ʈ

        int recipePageCurrent;          // ���� ������ ������ ��
        int recipePageMax;              // �ִ� ������ ������ ��

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

        // �ʱ�ȭ
        void Initialize()
        {
            // ������ ������ ������ ��ư ������ ������ ��, �������� ������ ������ �ִ� �������� �����ϰ� �������� �ִ� ��� ��+1�� �ִ� �������� ������
            // ���������� �������̹Ƿ�, ������ �κ��� �߸��� ������ ������ �κ��� ������ �������� �ֱ� ����
            int temp = recipes.Length / buttons.Length;
            recipePageMax = (recipes.Length % buttons.Length == 0) ? temp : temp + 1;

            // ���� �������� 1�� �ʱ�ȭ
            recipePageCurrent = 1;

            // ������ ��ư ����
            SetRecipeButtons();

            // ������ ������ UI �ؽ�Ʈ ����
            SetPageText();
        }

        // ������ ������ UI �ؽ�Ʈ ����
        void SetPageText()
        {
            // '���� ������ / �ִ� ������' ����
            recipePageNum.text = recipePageCurrent.ToString() + " / " + recipePageMax.ToString();
        }

        // ������ ��ư ����
        public void SetRecipeButtons()
        {
            // �������� �� ��ư �� ������ ����
            for (int i = 0; i < buttons.Length; i++)
            {
                // �ش� �������� ��ư�� �ش��ϴ� �������� ��Ͽ����� �ε����� '��ư ���� * (���� ������ - 1) + ��ư ��ȣ'�� ����
                int index = buttons.Length * (recipePageCurrent - 1) + i;

                if (index < recipes.Length)
                {
                    // ���� �ε����� ������ ��� �������� ���� ��� (�����ǰ� �ִ� ��ư�� ���)

                    // �ش� ��Ͽ� �ִ� �����Ǹ� ������ ������ ��ư ���� �� ��ư Ȱ��ȭ
                    Recipe recipe = recipes[index];

                    buttons[i].GetComponent<UserRecipeButtonsEvent>().InitializeButtons(recipe);
                    buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    // ���� �ε����� ������ ��� �������� ū ��� (����ִ� ��ư�� ���)
                    // ��ư ��Ȱ��ȭ
                    buttons[i].gameObject.SetActive(false);
                }
            }

            // ������ ������ UI �ؽ�Ʈ ����
            SetPageText();
        }

        // �������� ���� ȭ��ǥ�� Ŭ�� ���� ��
        public void ClickLeftArrow()
        {
            if (recipePageCurrent > 1)
            {
                // ���� �������� 1���� ū ��쿡�� ���� �������� �Ѿ
                recipePageCurrent--;

                // ������ ��ư ����
                // �ش� �������� �ִ� �����Ǹ� �����ֱ� ����
                SetRecipeButtons();
            }
        }

        // �������� ������ ȭ��ǥ�� Ŭ�� ���� ��
        public void ClickRightArrow()
        {
            if (recipePageCurrent < recipePageMax)
            {
                // ���� �������� �ִ� ���������� ���� ��쿡�� ���� �������� �Ѿ
                recipePageCurrent++;

                // ������ ��ư ����
                // �ش� �������� �ִ� �����Ǹ� �����ֱ� ����
                SetRecipeButtons();
            }
        }
    }
}
