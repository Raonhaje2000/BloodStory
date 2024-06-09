using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ProductionMessageBoxSuccess : MonoBehaviour
    {
        TextMeshProUGUI itemDesText;     // 제작한 아이템에 종류와 성공/대성공 여부를 언급하는 텍스트
        TextMeshProUGUI expText;         // 획득한 숙련도를 언급하는 텍스트

        Image itemIconImage;             // 제작한 아이템의 아이콘 이미지
        TextMeshProUGUI itemCountText;   // 제작한 아이템의 개수 텍스트

        TextMeshProUGUI itemNameText;    // 제작한 아이템의 이름 텍스트

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
            itemDesText = GameObject.Find("ProductionSuccessMessageBoxItemDesText").GetComponent<TextMeshProUGUI>();
            expText = GameObject.Find("ProductionSuccessMessageBoxExpText").GetComponent<TextMeshProUGUI>();

            itemIconImage = GameObject.Find("ProductionSuccessMessageBoxSuccessItemIcon").GetComponent<Image>();
            itemCountText = GameObject.Find("ProductionSuccessMessageBoxSuccessItemCountText").GetComponent<TextMeshProUGUI>();

            itemNameText = GameObject.Find("ProductionSuccessMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
        }

        // 성공/대성공에 따라 메세지 박스 세팅 (대성공인 경우 숙련도와 제작으로 획득 가능한 아이템 개수 2배)
        public void SetMessageBoxSuccess(Recipe recipe, bool isGreatSuccess)
        {
            string successType = (!isGreatSuccess) ? "성공" : "대성공";
            float exp = (!isGreatSuccess) ? recipe.ProductionExp : recipe.ProductionExp * 2;
            int count = (!isGreatSuccess) ? 1 : 2;

            itemDesText.text = string.Format("\'{0}\' 제작에 {1}했어!", recipe.FinishedProductItem.ItemName, successType);
            expText.text = string.Format("숙련도가 {0} 증가했어.", exp);

            itemIconImage.sprite = recipe.FinishedProductItem.ItemIcon;
            itemCountText.text = count.ToString();

            itemNameText.text = recipe.FinishedProductItem.ItemName;
        }

        // X 버튼을 눌렀을 때
        public void ClickButtonX()
        {
            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }
    }
}
