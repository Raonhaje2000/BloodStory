using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace RSA
{
    // 해당 스테이지에서 사용되는 데이터 관리
    public class FieldGameManager : MonoBehaviour
    {
        public static FieldGameManager instance;

        int currentStage;          // 현재 스테이지
        int stageEnterPlayerLevel; // 스테이지 진입시 플레이어 레벨

        int stageScore;            // 현재 스테이지 점수
        int stageOxygen;           // 현재 스테이지 산소


        int oxygenScore;  // 산소 획득으로 얻는 점수
        int monsterScore; // 몬스터 처치로 얻는 점수

        float stageDurabilityWeapon;  // 현재 스테이지에서의 무기내구도
        float stageDurabilityDefence; // 현재 스테이지에서의 방어구 내구도

        public int StageEnterPlayerLevel
        {
            get { return stageEnterPlayerLevel; }
        }

        public float StageDurabilityWeapon
        {
            get { return stageDurabilityWeapon; }
            set { stageDurabilityWeapon = value; }
        }

        public float StageDurabilityDefence
        {
            get { return stageDurabilityDefence; }
            set { stageDurabilityDefence = value; }
        }

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        void Initialize()
        {
            // 초기화 (GameManager에서 관련 데이터를 넘겨받음 )
            currentStage = GameManager.instance.Stage;
            stageEnterPlayerLevel = GameManager.instance.PlayerLevel;

            stageScore = 0;
            FieldUIManager.instance.SetScoreText(GameManager.instance.Score);

            oxygenScore = 10 * currentStage;
            monsterScore = 100 * currentStage;

            stageOxygen = 0;
            FieldUIManager.instance.SetOxygenText(GameManager.instance.Oxygen);

            stageDurabilityWeapon = GameManager.instance.DurabilityWeapon;
            stageDurabilityDefence = GameManager.instance.DurabilityDefence;

            stageOxygen = 0;
        }

        // 산소 추가
        void AddOxygen()
        {
            stageOxygen++;
            GameManager.instance.Oxygen++;

            // 스테이지 전체 획득 산소는 99999를 넘길 수 없음
            if (GameManager.instance.Oxygen > 99999) GameManager.instance.Oxygen = 99999;
        }

        // 점수 추가 (얻은 점수만큼 더해줌)
        void AddScore(int addValue)
        {
            stageScore += addValue;
            GameManager.instance.Score += addValue;

            // 스테이지 전체 획득 점수는 99999를 넘길 수 없음
            if (GameManager.instance.Score > 99999) GameManager.instance.Score = 99999;                                    
        }

        // 산소 획득했을 때
        public void GetOxygen()
        {
            AddOxygen();           // 산소 추가
            AddScore(oxygenScore); // 점수 추가 (얻은 점수만큼 더해줌)

            FieldGenManager.instance.DecreaseOxygen();

            // UI 변경
            FieldUIManager.instance.SetOxygenText(GameManager.instance.Oxygen);
            FieldUIManager.instance.SetScoreText(GameManager.instance.Score);
        }

        // 몬스터를 처치했을 때
        public void KillMonster()
        {
            AddScore(monsterScore);
            FieldUIManager.instance.SetScoreText(GameManager.instance.Score);
        }

        public void ClickUnbeatableButton()
        {
            GameObject player = GameObject.Find("Player");
            bool isUnbeatable = player.GetComponent<PlayerRed>().IsUnbeatable;

            if (isUnbeatable)
            {
                // 현재 플레이어가 무적 상태인 경우 무적 해제
                player.GetComponent<PlayerRed>().IsUnbeatable = false;
                FieldUIManager.instance.SetUnbeatableButton(false);
            }
            else
            {
                // 현재 플레이어가 무적 상태인 경우 무적 설정
                player.GetComponent<PlayerRed>().IsUnbeatable = true;
                FieldUIManager.instance.SetUnbeatableButton(true);
            }
        }
    }
}