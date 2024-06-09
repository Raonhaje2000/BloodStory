using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ProductionMessageBox : MonoBehaviour
    {
        TextMeshProUGUI productionMessageBoxTypeText;                   // ������ ������ ������ ����ϴ� �ؽ�Ʈ

        Image[] productionMessageBoxMaterialsIcon;                      // ��� �����۵��� ������ �̹�����
        TextMeshProUGUI[] productionMessageBoxMaterialsNeedText;        // ������ ��� �����۵��� �䱸 ���� �ؽ�Ʈ
        TextMeshProUGUI[] productionMessageBoxMaterialsInventoryText;   // ������ ��� �����۵��� ���� �κ��丮 �� ���� ���� �ؽ�Ʈ

        GameObject productionMessageBoxMaterial4;

        private void Awake()
        {
            // ���� ������Ʈ�� �ҷ�����
            LoadObejects();
        }

        void Start()
        {

        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObejects()
        {
            productionMessageBoxTypeText = GameObject.Find("ProductionMessageBoxTypeText").GetComponent<TextMeshProUGUI>();

            productionMessageBoxMaterialsIcon = new Image[] { GameObject.Find("ProductionMessageBoxMaterial1Icon").GetComponent<Image>(),
                                                              GameObject.Find("ProductionMessageBoxMaterial2Icon").GetComponent<Image>(),
                                                              GameObject.Find("ProductionMessageBoxMaterial3Icon").GetComponent<Image>(),
                                                              GameObject.Find("ProductionMessageBoxMaterial4Icon").GetComponent<Image>() };

            productionMessageBoxMaterialsNeedText = new TextMeshProUGUI[] { GameObject.Find("ProductionMessageBoxMaterial1NeedText").GetComponent<TextMeshProUGUI>(),
                                                                            GameObject.Find("ProductionMessageBoxMaterial2NeedText").GetComponent<TextMeshProUGUI>(),
                                                                            GameObject.Find("ProductionMessageBoxMaterial3NeedText").GetComponent<TextMeshProUGUI>(),
                                                                            GameObject.Find("ProductionMessageBoxMaterial4NeedText").GetComponent<TextMeshProUGUI>() };

            productionMessageBoxMaterialsInventoryText = new TextMeshProUGUI[] { GameObject.Find("ProductionMessageBoxMaterial1InventoryText").GetComponent<TextMeshProUGUI>(),
                                                                                 GameObject.Find("ProductionMessageBoxMaterial2InventoryText").GetComponent<TextMeshProUGUI>(),
                                                                                 GameObject.Find("ProductionMessageBoxMaterial3InventoryText").GetComponent<TextMeshProUGUI>(),
                                                                                 GameObject.Find("ProductionMessageBoxMaterial4InventoryText").GetComponent<TextMeshProUGUI>() };

            productionMessageBoxMaterial4 = GameObject.Find("ProductionMessageBoxMaterials4");
        }

        void Initialize()
        {

        }

        // ���� ������ ���� �޼��� �ڽ� ����
        public void SetMessageBoxProgressText(Recipe recipe)
        {
            productionMessageBoxMaterial4.SetActive(true);

            productionMessageBoxTypeText.text = string.Format("\'{0}\' ��/�� �����ұ�?", recipe.FinishedProductItem.ItemName);

            for(int i = 0; i < Recipe.PRODUCTION_ITEMS_MAX; i++)
            {
                if (recipe.ProductionMaterialItems[i] != null)
                {
                    productionMessageBoxMaterialsIcon[i].sprite = recipe.ProductionMaterialItems[i].ItemIcon;
                    productionMessageBoxMaterialsNeedText[i].text = recipe.PoductionMaterialNeedCount[i].ToString();
                    productionMessageBoxMaterialsInventoryText[i].text = GameManager.instance.FindItemTotalCount(recipe.ProductionMaterialItems[i]).ToString();
                }
                else
                {
                    productionMessageBoxMaterial4.SetActive(false);
                }
            }
        }

        // ������ �����ϰڴٴ� ��ư�� Ŭ������ ��
        public void ClickYesButton()
        {
            // ���� ���� ó��
            ProductionManager.instance.ContinueProductionProgressButton();
        }

        // ������ �������� �ʰڴٴ� ��ư�� Ŭ������ ��
        public void ClickNoButton()
        {
            // ���� ��� ó��
            ProductionManager.instance.CancleProductionProgressButton();
        }
    }
}
