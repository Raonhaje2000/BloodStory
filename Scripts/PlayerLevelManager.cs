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

        TextMeshProUGUI playerLevelText; // �÷��̾� ���� �ؽ�Ʈ
        Slider playerExpBar;             // �÷��̾� ����ġ ��

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
            playerLevelText = GameObject.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();
            playerExpBar = GameObject.Find("PlayerExpBar").GetComponent<Slider>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // �÷��̾� ���� �� ����ġ�� UI ���� (���� ó�� ���� �� ������ �� ����)
            SetPlayerLevelAndExpBar(GameManager.instance.PlayerLevel, GameManager.instance.PlayerExpMax, GameManager.instance.PlayerExp);
        }

        // �÷��̾� ���� �� ����ġ�� UI ���� (���� ó�� ���� �� ������ �� ����)
        public void SetPlayerLevelAndExpBar(int level, float expMax, float expCurrent)
        {
            // ���� �ؽ�Ʈ ����
            playerLevelText.text = string.Format("Lv. {0}", level);

            // ����ġ�� ����
            playerExpBar.maxValue = expMax;
            playerExpBar.minValue = 0.0f;

            playerExpBar.value = expCurrent;
        }
    }
}
