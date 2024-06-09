using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace RSA
{
    public class MinigameManager : MonoBehaviour
    {
        public static MinigameManager instance;

        const int NOTE_COUNT_MAX = 3;      // 생성되는 노트의 최대 개수

        GameObject minigameNotePrefab;     // 미니게임 노트 프리팹

        [SerializeField]
        Transform minigameNotesGenPoint;   // 미니게임 노트 생성 포인트

        float genMargin;                   // 생성 범위 - 여유 공간
        float genWidthHalf;                // 생성 범위 - 전체 너비 절반 (기준점에서 음수 양수로 따지므로)
        float genHightHalf;                // 생성 범위 - 전체 높이 절반 (기준점에서 음수 양수로 따지므로)

        float noteDelay;                   // 노트 생성 딜레이

        public int currentNoteCount;       // 현재 생성된 노트 수
        int minigameScore;                 // 미니게임 최종 점수 (증가되는 확률)

        bool isNoteDelay;                  // 노트 생성 딜레이인지 확인하는 플래그
        bool isMinigameFinish;             // 미니게임이 끝났는지 확인하는 플래그

        private void Awake()
        {
            if (instance == null) instance = this;

            //LoadObejcts();
            //Initialize();
        }

        void Start()
        {
            // 관련 오브젝트들 불러오기
            LoadObejcts();

            // 초기화
            Initialize();
        }

        void Update()
        {
            if(currentNoteCount <= NOTE_COUNT_MAX)
            {
                // 현재 노트 생성 개수가 노트 생성 최대치 이하인 경우
                // 같을 때까지 대기하도록 하는건 마지막 노트 생성 이후 미니게임이 바로 종료되는 것을 막기 위함

                // 노트 생성 및 생성 딜레이 시간동안 대기
                StartCoroutine(WaitBeforeNote());
            }
            else
            {
                // 현재 노트 생성 개수가 노트 생성 최대치보다 많은 경우

                // 노트 생성 중지
                StopCoroutine("WaitBeforeNote");

                if(!isMinigameFinish)
                {
                    // 미니게임이 끝났다고 처리되지 않은 경우 (결과 처리를 한번만 하기 위함)
                    // 미니게임 종료 체크 후 미니게임 결과 처리
                    isMinigameFinish = true;
                    FinishMinigame();
                }
            }
        }

        // 관련 오브젝트들 불러오기
        void LoadObejcts()
        {
            minigameNotePrefab = Resources.Load<GameObject>("RSA/Prefabs/Minigame/MinigameNote");
            minigameNotesGenPoint = GameObject.Find("MinigameNotesGenPoint").transform;
        }

        // 초기화
        void Initialize()
        {
            genMargin = 45.0f;
            genWidthHalf = 100.0f - genMargin;
            genHightHalf = 60.0f - genMargin;

            noteDelay = 2.0f;

            currentNoteCount = 0;
            minigameScore = 0;

            isNoteDelay = false;
            isMinigameFinish = false;
        }

        void GenerateNote()
        {
            if (currentNoteCount < NOTE_COUNT_MAX)
            {
                // 현재 생성된 노트 수가 최대 노트 생성 수 보다 적은 경우

                // UI 상에서의 노트 생성 위치 랜덤으로 결정
                // 가로와 세로를 각각 20등분 한 뒤 그 중 한 교차점을 뽑음
                int x = Random.Range(-10, 11);
                int y = Random.Range(-10, 11);

                float positionX = genWidthHalf * (float)x / 10.0f;
                float positionY = genHightHalf * (float)y / 10.0f;

                Vector3 randomPosition = new Vector3(positionX, positionY, 0.0f);

                // 랜덤으로 정한 위치에 노트 생성
                GameObject note = Instantiate(minigameNotePrefab, randomPosition, transform.rotation);
                note.transform.SetParent(minigameNotesGenPoint, false);
                //note.transform.SetAsLastSibling();

                // 생성된 노트의 속도 설정
                note.GetComponent<MinigameNote>().SetSpeed(SetNoteSpeed()); // 1.4f 1.2f 1.0f
            }
        }

        // 미니게임 노트 속도 변경
        float SetNoteSpeed()
        {
            if (ReinforcementManager.instance != null)
            {
                // 현재 강화 UI인 경우
                switch (ReinforcementManager.instance.EquipReinforcementType)                                                                          
                {
                    case ReinforcementManager.ReinforcementType.normal:
                        {
                            // 현재 강화 타입이 기본 강화일 때
                            return 1.4f;
                        }
                    case ReinforcementManager.ReinforcementType.high:
                        {
                            // 현재 강화 타입이 집중 강화일 때
                            return 1.2f;
                        }
                    case ReinforcementManager.ReinforcementType.highest:
                        {
                            // 현재 강화 타입이 초집중 강화일 때
                            return 1.0f;
                        }
                    default:
                        {
                            return 0.0f;
                        }
                }
            }
            else
            {
                // 현재 강화 UI가 아닌 경우 (제작 UI인 경우)
                return 1.4f;
            }
        }

        // 노트 생성 및 생성 딜레이 시간동안 대기
        IEnumerator WaitBeforeNote()
        {
            if(!isNoteDelay)
            {
                isNoteDelay = true;

                // 기본 노트 생성 딜레이에 랜덤 시간만큼 더함 (노트가 불규칙하게 생성되도록)
                float randomTime = Random.Range(-0.5f, 2.0f);
                float delay = noteDelay + randomTime;

                yield return new WaitForSeconds(delay);

                // 노트 생성 후 현재 생성된 노트 수 증가
                GenerateNote();
                currentNoteCount++;

                isNoteDelay = false;
            }
        }

        // 미니게임 결과 처리
        void FinishMinigame()
        {
            if (ReinforcementManager.instance != null)
            {
                // 현재 강화 UI가 켜져있는 경우
                // 미니게임 최종 점수를 넘겨주며 강화 진행
                ReinforcementManager.instance.ProgressReinforcement(minigameScore);
            }

            if(ProductionManager.instance != null)
            {
                // 현재 제작 UI가 켜져있는 경우
                // 미니게임 최종 점수를 넘겨주며 제작 진행
                ProductionManager.instance.ProgressProduction(minigameScore);
            }

            // 초기화 (강화나 제작 시 미니게임을 다시 진행하기 위함)
            Initialize();
        }

        // 최종 점수 추가
        public void AddScore(int noteScore)
        {
            // 미니게임 최종 점수에 노트 별 점수 추가
            minigameScore += noteScore;
        }
    }
}
