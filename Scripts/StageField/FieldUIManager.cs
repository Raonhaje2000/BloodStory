using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class FieldUIManager : MonoBehaviour
    {
        public static FieldUIManager instance;

        // ȹ�� ����, ȹ�� ��� ����
        TextMeshProUGUI scoreText;  // ȹ�� ���� �ؽ�Ʈ
        TextMeshProUGUI oxygenText; // ȹ�� ��� �ؽ�Ʈ

        // ���� ���� Ÿ�̸� ����
        Slider bossTimer;               // ���� ���� Ÿ�̸� ��
        TextMeshProUGUI bossTimerText;  // ���� ���� Ÿ�̸� �ؽ�Ʈ
        GameObject bossMiniMapIcon;     // ���� ���� ������

        // �ʵ忡�� ����ϴ� ������(���� �ʵ� ������) ����
        public Image[] itemsIcon;  // �ʵ� �������� ���� ������
        GameObject[] itemsTexts;    // �ʵ� �������� Ȱ��ȭ �� �ؽ�Ʈ ������Ʈ (�Է�Ű, ���� ����)
        GameObject[] itemsCoolTime; // �ʵ� �������� ��Ȱ��ȭ �� �ؽ�Ʈ ������Ʈ (��Ÿ��)
        TextMeshProUGUI[] itemsCountText;    // �ʵ� �������� ���� ���� �ؽ�Ʈ
        TextMeshProUGUI[] itemsCoolTimeText; // �ʵ� �������� ��Ÿ�� �ؽ�Ʈ

        TextMeshProUGUI unbeatableButtonText; // ���� ��ư �ؽ�Ʈ

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ�� �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            SetUnbeatableButton(false);
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObjects()
        {
            scoreText = GameObject.Find("ScoreValue").GetComponent<TextMeshProUGUI>();
            oxygenText = GameObject.Find("OxygenValue").GetComponent<TextMeshProUGUI>();

            bossTimer = GameObject.Find("BossTimer").GetComponent<Slider>();
            bossTimerText = GameObject.Find("BossTimerText").GetComponent<TextMeshProUGUI>();
            bossMiniMapIcon = GameObject.Find("BossMiniMapIcon");

            itemsIcon = new Image[] { GameObject.Find("ItemAIcon").GetComponent<Image>(),
                                       GameObject.Find("ItemBIcon").GetComponent<Image>(),
                                       GameObject.Find("ItemCIcon").GetComponent<Image>() };

            itemsTexts = new GameObject[] { GameObject.Find("ItemATexts"), 
                                            GameObject.Find("ItemBTexts"),
                                            GameObject.Find("ItemCTexts")};

            itemsCoolTime = new GameObject[] { GameObject.Find("ItemACoolTime"),
                                               GameObject.Find("ItemBCoolTime"),
                                               GameObject.Find("ItemCCoolTime") };

            itemsCountText = new TextMeshProUGUI[] { GameObject.Find("ItemACountText").GetComponent<TextMeshProUGUI>(),
                                                     GameObject.Find("ItemBCountText").GetComponent<TextMeshProUGUI>(),
                                                     GameObject.Find("ItemCCountText").GetComponent<TextMeshProUGUI>() };

            itemsCoolTimeText = new TextMeshProUGUI[] { GameObject.Find("ItemACoolTimeText").GetComponent<TextMeshProUGUI>(),
                                                        GameObject.Find("ItemBCoolTimeText").GetComponent<TextMeshProUGUI>(),
                                                        GameObject.Find("ItemCCoolTimeText").GetComponent<TextMeshProUGUI>() };

            unbeatableButtonText = GameObject.Find("UnbeatableButtonText").GetComponent<TextMeshProUGUI>();
        }

        // ���� �ؽ�Ʈ ���� (5�ڸ� ���·� ǥ��)
        public void SetScoreText(int score)
        {
            scoreText.text = score.ToString("00000");
        }

        // ��� �ؽ�Ʈ ���� (5�ڸ� ���·� ǥ��)
        public void SetOxygenText(int oxygen)
        {
            oxygenText.text = oxygen.ToString("00000");
        }

        // ���� ���� Ÿ�̸� �ʱ�ȭ
        public void InitSetBossTimer(float maxValue, float minValue)
        {
            // ���� ���� Ÿ�̸� �� ����
            bossTimer.maxValue = maxValue;
            bossTimer.minValue = minValue;
            bossTimer.value = maxValue;

            // ���� ���� Ÿ�̸� �ؽ�Ʈ ���� (00:00 ���·� ǥ��)
            int min = (int)Mathf.Floor(maxValue / 60.0f);
            int sec = (int)Mathf.Floor(maxValue % 60.0f);

            bossTimerText.text = min.ToString("00") + " : " + sec.ToString("00");
        }

        // ���� ���� Ÿ�̸� ����
        public void SetBossTimer(float time)
        {
            // �Ѱܹ��� �ð����� ���� Ÿ�̸Ӹ� ������
            bossTimer.value = time;

            int min = (int)Mathf.Floor(time / 60.0f);
            int sec = (int)Mathf.Floor(time % 60.0f);

            bossTimerText.text = min.ToString("00") + " : " + sec.ToString("00");
        }

        // ���� ���� �̴ϸ� ������ Ȱ��/��Ȱ��ȭ ����
        public void SetActiveBossMiniMapIcon(bool active)
        {
            bossMiniMapIcon.SetActive(active);
        }

        // �ʵ� ������ ������ ����
        public void SetItemIcon(int index, Sprite icon)
        {
            itemsIcon[index].sprite = icon;
        }

        // �ʵ� ������ �ؽ�Ʈ ������Ʈ ����
        public void SetActiveItem(int index, bool usePossible)
        {
            itemsTexts[index].SetActive(usePossible);     // �ʵ� ������ Ȱ��ȭ �� �ؽ�Ʈ ������Ʈ
            itemsCoolTime[index].SetActive(!usePossible); // �ʵ� ������ ��Ȱ��ȭ �� �ؽ�Ʈ ������Ʈ
        }

        // �ʵ� ������ ���� ���� �ؽ�Ʈ ����
        public void SetItemCountText(int index, int count)
        {
            itemsCountText[index].text = count.ToString();
        }

        // �ʵ� ������ ��Ÿ�� �ؽ�Ʈ ����
        public void SetItemCoolTimeText(int index, int coolTime)
        {
            itemsCoolTimeText[index].text = coolTime.ToString("00");                                                    
        }

        public void SetUnbeatableButton(bool isUnbeatable)
        {
            unbeatableButtonText.text = (isUnbeatable) ? "���� ����" : "���� ����";
        }
    }
}