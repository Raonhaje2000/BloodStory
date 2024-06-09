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

        // 획득 점수, 획득 산소 관련
        TextMeshProUGUI scoreText;  // 획득 점수 텍스트
        TextMeshProUGUI oxygenText; // 획득 산소 텍스트

        // 보스 등장 타이머 관련
        Slider bossTimer;               // 보스 등장 타이머 바
        TextMeshProUGUI bossTimerText;  // 보스 등장 타이머 텍스트
        GameObject bossMiniMapIcon;     // 보스 등장 아이콘

        // 필드에서 사용하는 아이템(이하 필드 아이템) 관련
        public Image[] itemsIcon;  // 필드 아이템의 슬롯 아이콘
        GameObject[] itemsTexts;    // 필드 아이템의 활성화 시 텍스트 오브젝트 (입력키, 남은 개수)
        GameObject[] itemsCoolTime; // 필드 아이템의 비활성화 시 텍스트 오브젝트 (쿨타임)
        TextMeshProUGUI[] itemsCountText;    // 필드 아이템의 남은 개수 텍스트
        TextMeshProUGUI[] itemsCoolTimeText; // 필드 아이템의 쿨타임 텍스트

        TextMeshProUGUI unbeatableButtonText; // 무적 버튼 텍스트

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트들 불러오기
            LoadObjects();
        }

        void Start()
        {
            SetUnbeatableButton(false);
        }

        // 관련 오브젝트들 불러오기
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

        // 점수 텍스트 세팅 (5자리 형태로 표기)
        public void SetScoreText(int score)
        {
            scoreText.text = score.ToString("00000");
        }

        // 산소 텍스트 세팅 (5자리 형태로 표기)
        public void SetOxygenText(int oxygen)
        {
            oxygenText.text = oxygen.ToString("00000");
        }

        // 보스 등장 타이머 초기화
        public void InitSetBossTimer(float maxValue, float minValue)
        {
            // 보스 등장 타이머 바 세팅
            bossTimer.maxValue = maxValue;
            bossTimer.minValue = minValue;
            bossTimer.value = maxValue;

            // 보스 등장 타이머 텍스트 세팅 (00:00 형태로 표기)
            int min = (int)Mathf.Floor(maxValue / 60.0f);
            int sec = (int)Mathf.Floor(maxValue % 60.0f);

            bossTimerText.text = min.ToString("00") + " : " + sec.ToString("00");
        }

        // 보스 등장 타이머 세팅
        public void SetBossTimer(float time)
        {
            // 넘겨받은 시간으로 보스 타이머를 세팅함
            bossTimer.value = time;

            int min = (int)Mathf.Floor(time / 60.0f);
            int sec = (int)Mathf.Floor(time % 60.0f);

            bossTimerText.text = min.ToString("00") + " : " + sec.ToString("00");
        }

        // 보스 등장 미니맵 아이콘 활성/비활성화 세팅
        public void SetActiveBossMiniMapIcon(bool active)
        {
            bossMiniMapIcon.SetActive(active);
        }

        // 필드 아이템 아이콘 세팅
        public void SetItemIcon(int index, Sprite icon)
        {
            itemsIcon[index].sprite = icon;
        }

        // 필드 아이템 텍스트 오브젝트 세팅
        public void SetActiveItem(int index, bool usePossible)
        {
            itemsTexts[index].SetActive(usePossible);     // 필드 아이템 활성화 시 텍스트 오브젝트
            itemsCoolTime[index].SetActive(!usePossible); // 필드 아이템 비활성화 시 텍스트 오브젝트
        }

        // 필드 아이템 남은 개수 텍스트 세팅
        public void SetItemCountText(int index, int count)
        {
            itemsCountText[index].text = count.ToString();
        }

        // 필드 아이템 쿨타임 텍스트 세팅
        public void SetItemCoolTimeText(int index, int coolTime)
        {
            itemsCoolTimeText[index].text = coolTime.ToString("00");                                                    
        }

        public void SetUnbeatableButton(bool isUnbeatable)
        {
            unbeatableButtonText.text = (isUnbeatable) ? "무적 해제" : "무적 설정";
        }
    }
}