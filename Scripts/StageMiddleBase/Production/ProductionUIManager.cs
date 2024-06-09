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
 
        TextMeshProUGUI productionRecipeNameText;         // ���� ������ ���� �ؽ�Ʈ
 
        Image productionItemIconImage;                    // �������� ���� ������ ������ �̹���
        TextMeshProUGUI productionItemNameText;           // �������� ���� ������ �̸� �ؽ�Ʈ
        TextMeshProUGUI productionItemDescriptionText;    // �������� ���� ������ ����(����) �ؽ�Ʈ

        Image[] productionMaterialIconImages;             // ��� �����۵��� ������ �̹�����
        TextMeshProUGUI[] productionMaterialText;         // ��� �����۵��� '�䱸 ����/���� ����' �ؽ�Ʈ��

        TextMeshProUGUI productionProbabilityTextValue;   // �뼺�� Ȯ�� �ؽ�Ʈ
        TextMeshProUGUI productionSkillValueTextValue;    // ���۽� ȹ���ϴ� ���� ���õ� ����ġ �ؽ�Ʈ

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

        // �ʱ�ȭ
        void Initialize()
        {
            // ���� ���� UI ��Ȱ��ȭ
            SetProductionActive(false);
        }

        // ���� ���� UI Ȱ��ȭ/��Ȱ��ȭ
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

        // ���� ������ ���� �����ǿ� ���� ���� UI ����
        public void SetProductionRecipe(Recipe recipe, float probability)
        {
            // ���� ���� UI Ȱ��ȭ
            SetProductionActive(true);

            productionRecipeNameText.text = recipe.FinishedProductItem.ItemName + " ������";

            productionItemIconImage.sprite = recipe.FinishedProductItem.ItemIcon;
            productionItemNameText.text = recipe.FinishedProductItem.ItemName;
            productionItemDescriptionText.text = recipe.FinishedProductItem.ItemTooltip;

            // �ʿ��� ��� ������ ĭ ����
            for (int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                if(recipe.ProductionMaterialItems[i] != null)
                {
                    // ��� �������� �����ϴ� ���
                    // ��� ������ �����ܰ� '�䱸 ����/���� ����' ���·� �ؽ�Ʈ ����
                    productionMaterialIconImages[i].sprite = recipe.ProductionMaterialItems[i].ItemIcon;
                    productionMaterialText[i].text = recipe.PoductionMaterialNeedCount[i].ToString() + " / " + GameManager.instance.FindItemTotalCount(recipe.ProductionMaterialItems[i]).ToString();
                }
                else
                {
                    // ��� �������� �������� �ʴ� ��� (��� ������ ������ 3������ ��, 4��° ĭ ���� ��)
                    // �ش� ĭ�� �����ܰ� �ؽ�Ʈ ��Ȱ��ȭ
                    productionMaterialIconImages[i].gameObject.SetActive(false);
                    productionMaterialText[i].gameObject.SetActive(false);
                }
            }

            // ���� �뼺�� Ȯ�� �� ȹ�� ���õ� ��ġ �ؽ�Ʈ ����
            productionProbabilityTextValue.text = probability.ToString("0.0");
            productionSkillValueTextValue.text = recipe.ProductionExp.ToString();
        }
    }
}
