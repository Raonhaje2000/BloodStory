using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class PlayerLevelManager : MonoBehaviour
    {
        public static PlayerLevelManager instance;

        TextMeshProUGUI playerLevelText; // 플레이어 레벨 텍스트
        Slider playerExpBar;             // 플레이어 경험치 바

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
            playerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();
            playerExpBar = GameObject.Find("PlayerExpBar").GetComponent<Slider>();
        }

        // 초기화
        void Initialize()
        {
            // 플레이어 레벨 및 경험치바 UI 세팅 (게임 처음 시작 및 레벨업 시 세팅)
            SetPlayerLevelAndExpBar(GameManager.instance.PlayerLevel, GameManager.instance.PlayerExpMax, GameManager.instance.PlayerExp);
        }

        // 플레이어 레벨 및 경험치바 UI 세팅 (게임 처음 시작 및 레벨업 시 세팅)
        public void SetPlayerLevelAndExpBar(int level, float expMax, float expCurrent)
        {
            // 레벨 텍스트 세팅
            playerLevelText.text = string.Format("Lv. {0}", level);

            // 경험치바 세팅
            playerExpBar.maxValue = expMax;
            playerExpBar.minValue = 0.0f;

            playerExpBar.value = expCurrent;
        }
    }
}
