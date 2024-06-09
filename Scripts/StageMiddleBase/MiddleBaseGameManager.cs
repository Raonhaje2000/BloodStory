using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class MiddleBaseGameManager : MonoBehaviour
    {
        public static MiddleBaseGameManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 초기화
        void Initialize()
        {
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI(); // 중간 거점 기본 UI 활성화
            MiddleBaseUIManager.instance.SetScoreAndOxygenTexts(); // 점수와 산소 텍스트 UI 세팅
        }

        // 유저 정보 아이콘 클릭
        public void ClickUserInformationIcon()
        {
            // 유저 정보 UI 활성화
            MiddleBaseUIManager.instance.ActiveUserInformationUI();
        }

        // 유저 인벤토리 아이콘 클릭
        public void ClickInventoryIcon()
        {
            // 유저 인벤토리 UI 활성화
            MiddleBaseUIManager.instance.ActiveUserInventoryUI();
        }

        // 속성 부여 아이콘 클릭
        public void ClickAttributeIcon()
        {
            // 속성 부여 UI 활성화
            MiddleBaseUIManager.instance.ActiveAttributeUI();
        }
    }
}