using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace RSA
{
    public class UserProductionLevelManager : MonoBehaviour
    {
        public static UserProductionLevelManager instance;

        const int PLAYER_PRODUCTION_LEVEL_MAX = 4;            // ���� ���õ� ������ �ִ�ġ

        TextMeshProUGUI productionSkillLevelCurrentLvValue;   // ���� ���� ���õ� ���� �ؽ�Ʈ
        TextMeshProUGUI productionSkillLevelNextValue;        // ���� ���������� �ʿ��� ���õ� ����ġ �ؽ�Ʈ

        Slider productionSkillLevelBar;                       // ���õ� ����ġ ��

        int playerProductionLevel;     // �÷��̾��� ���� ���õ� ����
        float playerProductionExp;     // �÷��̾��� ���� ���õ� ����ġ
        float playerProductionExpMax;  // �÷��̾��� ���õ� ����ġ �ִ�ġ

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
            productionSkillLevelCurrentLvValue = GameObject.Find("ProductionSkillLevelCurrentLvValue").GetComponent<TextMeshProUGUI>();
            productionSkillLevelNextValue = GameObject.Find("ProductionSkillLevelNextValue").GetComponent<TextMeshProUGUI>();

            productionSkillLevelBar = GameObject.Find("ProductionSkillLevelBar").GetComponent<Slider>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            playerProductionLevel = GameManager.instance.PlayerProductionLevel;
            playerProductionExp = GameManager.instance.PlayerProductionExp;

            // ���õ� �ִ�ġ�� ���� ���� * 100�� ����
            playerProductionExpMax = playerProductionLevel * 100;

            // ���õ� ����ġ UI ����
            SetUserProductionLevelUI();
        }

        // ���õ� ����ġ UI ����
        void SetUserProductionLevelUI()
        {
            productionSkillLevelCurrentLvValue.text = playerProductionLevel.ToString();
            productionSkillLevelNextValue.text = (playerProductionExpMax - playerProductionExp).ToString();

            // ���õ� ����ġ �� UI ����
            SetLevelBar();
        }

        // ���õ� ����ġ �� UI ����
        void SetLevelBar()
        {
            productionSkillLevelBar.maxValue = playerProductionExpMax;
            productionSkillLevelBar.minValue = 0.0f;

            productionSkillLevelBar.value = playerProductionExp;
        }

        // ���� ���õ� ����ġ ȹ��
        public void AddPlayerProductionExp(float value)
        {
            if(playerProductionLevel < PLAYER_PRODUCTION_LEVEL_MAX)
            {
                // ���� ���õ� ������ �ִ� �������� ���� ���

                // ȹ���� ����ġ �߰�
                playerProductionExp += value;

                if (playerProductionExp >= playerProductionExpMax)
                {
                    // ���� ���õ� ����ġ�� ���õ� ����ġ �ִ�ġ �̻��� ���

                    // ���� ���õ� ����ġ���� �ִ�ġ�� �� �� ���õ� ������ (�ʰ��� ���õ� ��ġ ���� ������ ����)
                    playerProductionExp -= playerProductionExpMax;
                    playerProductionLevel++;

                    // ���õ� �ִ�ġ �缼��
                    playerProductionExpMax = playerProductionLevel * 100;

                    if (playerProductionLevel == PLAYER_PRODUCTION_LEVEL_MAX)
                    {
                        // �ִ� ������ �޼��� ���
                        // ���õ� ������ ����ġ �ִ�ġ�� ����
                        playerProductionLevel = PLAYER_PRODUCTION_LEVEL_MAX;
                        playerProductionExp = 100.0f;
                        playerProductionExpMax = 100.0f;
                    }
                }

                // �÷��̾��� ���õ� ������ ����ġ ����
                GameManager.instance.PlayerProductionLevel = playerProductionLevel;
                GameManager.instance.PlayerProductionExp = playerProductionExp;

                // ���õ� ����ġ UI ����
                SetUserProductionLevelUI();

                // ������ ��ư ����
                UserProductionRecipeManager.instance.SetRecipeButtons();

                // ���� �뼺�� Ȯ�� ������Ʈ
                ProductionManager.instance.UpdateProbability();
            }
        }
    }
}
