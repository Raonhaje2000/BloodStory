using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace RSA
{
    // �ش� ������������ ���Ǵ� ������ ����
    public class FieldGameManager : MonoBehaviour
    {
        public static FieldGameManager instance;

        int currentStage;          // ���� ��������
        int stageEnterPlayerLevel; // �������� ���Խ� �÷��̾� ����

        int stageScore;            // ���� �������� ����
        int stageOxygen;           // ���� �������� ���


        int oxygenScore;  // ��� ȹ������ ��� ����
        int monsterScore; // ���� óġ�� ��� ����

        float stageDurabilityWeapon;  // ���� �������������� ���⳻����
        float stageDurabilityDefence; // ���� �������������� �� ������

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
            // �ʱ�ȭ
            Initialize();
        }

        void Initialize()
        {
            // �ʱ�ȭ (GameManager���� ���� �����͸� �Ѱܹ��� )
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

        // ��� �߰�
        void AddOxygen()
        {
            stageOxygen++;
            GameManager.instance.Oxygen++;

            // �������� ��ü ȹ�� ��Ҵ� 99999�� �ѱ� �� ����
            if (GameManager.instance.Oxygen > 99999) GameManager.instance.Oxygen = 99999;
        }

        // ���� �߰� (���� ������ŭ ������)
        void AddScore(int addValue)
        {
            stageScore += addValue;
            GameManager.instance.Score += addValue;

            // �������� ��ü ȹ�� ������ 99999�� �ѱ� �� ����
            if (GameManager.instance.Score > 99999) GameManager.instance.Score = 99999;                                    
        }

        // ��� ȹ������ ��
        public void GetOxygen()
        {
            AddOxygen();           // ��� �߰�
            AddScore(oxygenScore); // ���� �߰� (���� ������ŭ ������)

            FieldGenManager.instance.DecreaseOxygen();

            // UI ����
            FieldUIManager.instance.SetOxygenText(GameManager.instance.Oxygen);
            FieldUIManager.instance.SetScoreText(GameManager.instance.Score);
        }

        // ���͸� óġ���� ��
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
                // ���� �÷��̾ ���� ������ ��� ���� ����
                player.GetComponent<PlayerRed>().IsUnbeatable = false;
                FieldUIManager.instance.SetUnbeatableButton(false);
            }
            else
            {
                // ���� �÷��̾ ���� ������ ��� ���� ����
                player.GetComponent<PlayerRed>().IsUnbeatable = true;
                FieldUIManager.instance.SetUnbeatableButton(true);
            }
        }
    }
}