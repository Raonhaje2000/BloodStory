using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ProductionMessageBox : MonoBehaviour
    {
        TextMeshProUGUI productionMessageBoxTypeText;                   // 제작한 아이템 정보를 언급하는 텍스트

        Image[] productionMessageBoxMaterialsIcon;                      // 재료 아이템들의 아이콘 이미지들
        TextMeshProUGUI[] productionMessageBoxMaterialsNeedText;        // 제작할 재료 아이템들의 요구 개수 텍스트
        TextMeshProUGUI[] productionMessageBoxMaterialsInventoryText;   // 제작할 재료 아이템들의 현재 인벤토리 내 보유 개수 텍스트

        GameObject productionMessageBoxMaterial4;

        private void Awake()
        {
            // 관련 오브젝트들 불러오기
            LoadObejects();
        }

        void Start()
        {

        }

        // 관련 오브젝트들 불러오기
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

        // 제작 정보에 맞춰 메세지 박스 세팅
        public void SetMessageBoxProgressText(Recipe recipe)
        {
            productionMessageBoxMaterial4.SetActive(true);

            productionMessageBoxTypeText.text = string.Format("\'{0}\' 을/를 제작할까?", recipe.FinishedProductItem.ItemName);

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

        // 제작을 진행하겠다는 버튼을 클릭했을 때
        public void ClickYesButton()
        {
            // 제작 진행 처리
            ProductionManager.instance.ContinueProductionProgressButton();
        }

        // 제작을 진행하지 않겠다는 버튼을 클릭했을 때
        public void ClickNoButton()
        {
            // 제작 취소 처리
            ProductionManager.instance.CancleProductionProgressButton();
        }
    }
}
