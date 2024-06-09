using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class AttributeMessageBox : MonoBehaviour
    {
        AttributeItem vitaminItem;                       // 사용되는 속성 아이템 (비타민)

        TextMeshProUGUI attributeMessageBoxTextVitamin;  // 비타민 종류 텍스트
        TextMeshProUGUI attributeMessageBoxTextIsExist;  // 부여된 속성이 존재하는지에 대한 텍스트

        string existText;     // 존재할 때의 텍스트 값
        string nonexistText;  // 존재하지 않을 때의 텍스트 값

        private void Awake()
        {
            LoadObejcts(); // 관련 오브젝트들 불러오기
            Initialize();  // 초기화
        }

        void Start()
        {
            //Initialize();
        }

        // 관련 오브젝트들 불러오기
        void LoadObejcts()
        {
            attributeMessageBoxTextVitamin = GameObject.Find("AttributeMessageBoxTextVitamin").GetComponent<TextMeshProUGUI>();
            attributeMessageBoxTextIsExist = GameObject.Find("AttributeMessageBoxTextIsExist").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            vitaminItem = null;

            existText = "이미 장착된 비타민 종류야";
            nonexistText = "새로 장착하는 비타민 종류야";
        }

        // 속성 부여 메세지 박스 세팅
        public void SetMessageBoxText(AttributeItem vitamin, bool isExist)
        {
            vitaminItem = vitamin;

            attributeMessageBoxTextVitamin.text = string.Format("\'{0}\'를 장착할까?", vitaminItem.ItemName);
            attributeMessageBoxTextIsExist.text = (isExist) ? existText : nonexistText;
        }

        // 속성 부여를 진행하겠다는 버튼을 클릭했을 때
        public void ClickYesButton()
        {
            // 속성 부여 진행
            AttributeManager.instance.ContinueInstallAttribute(vitaminItem);
        }

        // 속성 부여를 진행하지 않겠다는 버튼을 클릭했을 때
        public void ClickNoButton()
        {
            // 속성 부여 취소
            AttributeManager.instance.CancleInstallAttribute();
        }
    }
}
