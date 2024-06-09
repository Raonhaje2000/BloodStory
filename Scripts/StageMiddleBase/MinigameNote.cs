using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

namespace RSA
{
    public class MinigameNote : MonoBehaviour
    {
        enum JudgementType { bad = 0, good = 1, perfect = 2 }  // 노트의 판정 타입
        const int KEY_TYPE_MAX = 3;                            // 노트에서 사용 가능한 키의 최대 가지수

        TextMeshProUGUI minigameKey;      // 현재 생성된 노트의 키 텍스트
        GameObject minigameRing;          // 현재 생성된 노트의 판정 링

        KeyCode[] usingKey;               // 사용 가능한 키 목록
        string[] usingKeyText;            // 사용 가능한 키 목록에 따른 키 텍스트

        float perfectRange;               // Perfect 판정 범위
        float goodRange;                  // Good 판정 범위
        float badRange;                   // Bad 판정 범위

        Color perfectBlueOutlineColor;    // Perfect 테두리 색
        Color goodGreenOutlineColor;      // Good 테두리 색
        Color badRedOutlineColor;         // Bad 테두리 색

        TextMeshProUGUI judgementText;    // 노트 판정 텍스트

        Vector3 initScale;                // 초기 판정 링 크기 (중앙 원과 같은 크기)
        Vector3 startScale;               // 시작할 때의 판정 링 크기 (확대했을 떄의 크기)

        int keyIndex;                     // 사용 가능한 키 목록 중에서 뽑은 키 (눌러야 하는 키)
        float ringSpeed;                  // 판정 링이 줄어드는 속도
        float time;                       // 노트가 생성된 이후 흐른 시간 (노트의 크기 변화를 위함)

        bool isKeyDown;                   // 키가 눌렸는지 확인하는 플래그

        private void Awake()
        {
            // 관련 오브젝트들 불러오기
            LoadObejcts();
        }

        void Start()
        {
            // 초기화
            Initialize();

            // 노트 초기화
            InitSetNote();
        }

        void Update()
        {
            if (!isKeyDown)
            {
                // 키를 누르지 않았을 경우

                // 판정 링의 크기 업데이트
                UpdateRingSize();

                // 눌러야 하는 키를 눌렀을 경우 판정 링 크기에 따른 결과 판정
                JudgeNote();
            }
        }

        // 노트 속도 (줄어드는 속도) 세팅
        public void SetSpeed(float speed)
        {
            // Awake, SetSpeed, Start, Update 순서로 실행됨

            ringSpeed = speed;
        }

        void LoadObejcts()
        {

            minigameRing = transform.Find("MinigameNoteRing").gameObject;

            minigameKey = transform.Find("MinigameNoteCircleKey").GetComponent<TextMeshProUGUI>();
            
            judgementText = transform.Find("MinigameNoteJudgementText").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            usingKey = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.C };
            usingKeyText = new string[] { "Z", "X", "C" };

            perfectRange = 0.09f;
            goodRange = 0.25f;
            badRange = 0.95f;

            initScale = minigameRing.transform.localScale;
            startScale = initScale * 2.5f;

            perfectBlueOutlineColor = new Color32(91, 155, 213, 255);
            goodGreenOutlineColor = new Color32(112, 173, 71, 255);
            badRedOutlineColor = new Color32(192, 0, 0, 255);

            judgementText.gameObject.SetActive(false);

            isKeyDown = false;
        }

        // 노트 초기화
        public void InitSetNote()
        {
            minigameRing.transform.localScale = startScale;

            // 키 목록에서 눌러야 하는 키 랜덤으로 하나 결정
            keyIndex = Random.Range(0, KEY_TYPE_MAX);

            time = 0.0f;

            minigameKey.text = usingKeyText[keyIndex];
        }

        // 판정 링의 크기 업데이트
        void UpdateRingSize()
        {
            if (minigameRing.transform.localScale.x >= initScale.x * 0.1f)
            {
                // 판정 링의 크기가 초기 링 크기의 0.1배 이상인 경우

                // 시작할 때의 링 크기에서 링 속도에 따라 크기 점차 감소
                // 노트가 생성 된 후 흐른 시간에 따라 계속 변화함
                minigameRing.transform.localScale = startScale * (1.0f - ringSpeed * time);
                time += Time.deltaTime;
            }
            else
            {
                // 판정 링의 크기가 초기 링 크기의 0.1배 보다 작아진 경우

                // 판정 링 비활성화
                minigameRing.SetActive(false);

                // Bad 판정 세팅
                SetNoteJudgement(JudgementType.bad);
            }     
        }

        // 눌러야 하는 키를 눌렀을 경우 판정 링 크기에 따른 결과 판정
        void JudgeNote()
        {
            if(Input.GetKeyDown(usingKey[keyIndex]))
            {
                // 눌러야 하는 키가 눌린 경우

                if(minigameRing.transform.localScale.x <= (initScale * (1.0f + perfectRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - perfectRange)).x)
                {
                    // 판정 링의 크기가 Perfect 범위 내에 있는 경우
                    // Perfect 판정 세팅
                    SetNoteJudgement(JudgementType.perfect);
                }
                else if(minigameRing.transform.localScale.x <= (initScale * (1.0f + goodRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - goodRange)).x)
                {
                    // 판정 링의 크기가 Good 범위 내에 있는 경우
                    // Good 판정 세팅
                    SetNoteJudgement(JudgementType.good);
                }
                else if (minigameRing.transform.localScale.x <= (initScale * (1.0f + badRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - badRange)).x)
                {
                    // 판정 링의 크기가 Bad 범위 내에 있는 경우
                    // Bad 판정 세팅
                    SetNoteJudgement(JudgementType.bad);
                }
            }
        }

        // 노트 결과 판정 세팅
        void SetNoteJudgement(JudgementType type)
        {
            // 키 눌렀음 체크 (여러번 누르는 것을 방지하기 위함)
            isKeyDown = true;

            // 노트 결과에 따른 판정 텍스트 활성화
            ActiveJudgementText(type);

            // 미니게임 최종 점수에 현재 노트의 점수 추가
            // 열거형의 정수값을 이용함 (Bad = 0점, Good = 1점, Perfect = 2점)
            MinigameManager.instance.AddScore((int)type);

            // 1.5초 후 노트 제거 (노트가 생성되고 Bad 판정이 뜰 때까지 링 크기가 줄어들 수 있도록 대기 시간 이후 비활성화)
            Destroy(gameObject, 1.5f);
        }

        // 노트 결과에 따른 판정 텍스트 활성화
        void ActiveJudgementText(JudgementType type)
        {
            // 노트 판정 텍스트 활성화
            judgementText.gameObject.SetActive(true);
            
            switch (type)
            {
                case JudgementType.bad:
                    {
                        // Bad 판정인 경우
                        // 텍스트를 Bad로 바꾸고, 텍스트 테두리 색을 Bad 테두리 색으로 변경
                        judgementText.text = "Bad";
                        judgementText.outlineColor = badRedOutlineColor;

                        break;
                    }
                case JudgementType.good:
                    {
                        // Good 판정인 경우
                        // 텍스트를 Good으로 바꾸고, 텍스트 테두리 색을 Good 테두리 색으로 변경
                        judgementText.text = "Good";
                        judgementText.outlineColor = goodGreenOutlineColor;

                        break;
                    }
                case JudgementType.perfect:
                    {
                        // Perfect 판정인 경우
                        // 텍스트를 Perfect로 바꾸고, 텍스트 테두리 색을 Perfect 테두리 색으로 변경
                        judgementText.text = "Perfect";
                        judgementText.outlineColor = perfectBlueOutlineColor;

                        break;
                    }
            }
        }
    }
}
