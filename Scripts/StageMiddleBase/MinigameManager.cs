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

        const int NOTE_COUNT_MAX = 3;      // �����Ǵ� ��Ʈ�� �ִ� ����

        GameObject minigameNotePrefab;     // �̴ϰ��� ��Ʈ ������

        [SerializeField]
        Transform minigameNotesGenPoint;   // �̴ϰ��� ��Ʈ ���� ����Ʈ

        float genMargin;                   // ���� ���� - ���� ����
        float genWidthHalf;                // ���� ���� - ��ü �ʺ� ���� (���������� ���� ����� �����Ƿ�)
        float genHightHalf;                // ���� ���� - ��ü ���� ���� (���������� ���� ����� �����Ƿ�)

        float noteDelay;                   // ��Ʈ ���� ������

        public int currentNoteCount;       // ���� ������ ��Ʈ ��
        int minigameScore;                 // �̴ϰ��� ���� ���� (�����Ǵ� Ȯ��)

        bool isNoteDelay;                  // ��Ʈ ���� ���������� Ȯ���ϴ� �÷���
        bool isMinigameFinish;             // �̴ϰ����� �������� Ȯ���ϴ� �÷���

        private void Awake()
        {
            if (instance == null) instance = this;

            //LoadObejcts();
            //Initialize();
        }

        void Start()
        {
            // ���� ������Ʈ�� �ҷ�����
            LoadObejcts();

            // �ʱ�ȭ
            Initialize();
        }

        void Update()
        {
            if(currentNoteCount <= NOTE_COUNT_MAX)
            {
                // ���� ��Ʈ ���� ������ ��Ʈ ���� �ִ�ġ ������ ���
                // ���� ������ ����ϵ��� �ϴ°� ������ ��Ʈ ���� ���� �̴ϰ����� �ٷ� ����Ǵ� ���� ���� ����

                // ��Ʈ ���� �� ���� ������ �ð����� ���
                StartCoroutine(WaitBeforeNote());
            }
            else
            {
                // ���� ��Ʈ ���� ������ ��Ʈ ���� �ִ�ġ���� ���� ���

                // ��Ʈ ���� ����
                StopCoroutine("WaitBeforeNote");

                if(!isMinigameFinish)
                {
                    // �̴ϰ����� �����ٰ� ó������ ���� ��� (��� ó���� �ѹ��� �ϱ� ����)
                    // �̴ϰ��� ���� üũ �� �̴ϰ��� ��� ó��
                    isMinigameFinish = true;
                    FinishMinigame();
                }
            }
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObejcts()
        {
            minigameNotePrefab = Resources.Load<GameObject>("RSA/Prefabs/Minigame/MinigameNote");
            minigameNotesGenPoint = GameObject.Find("MinigameNotesGenPoint").transform;
        }

        // �ʱ�ȭ
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
                // ���� ������ ��Ʈ ���� �ִ� ��Ʈ ���� �� ���� ���� ���

                // UI �󿡼��� ��Ʈ ���� ��ġ �������� ����
                // ���ο� ���θ� ���� 20��� �� �� �� �� �� �������� ����
                int x = Random.Range(-10, 11);
                int y = Random.Range(-10, 11);

                float positionX = genWidthHalf * (float)x / 10.0f;
                float positionY = genHightHalf * (float)y / 10.0f;

                Vector3 randomPosition = new Vector3(positionX, positionY, 0.0f);

                // �������� ���� ��ġ�� ��Ʈ ����
                GameObject note = Instantiate(minigameNotePrefab, randomPosition, transform.rotation);
                note.transform.SetParent(minigameNotesGenPoint, false);
                //note.transform.SetAsLastSibling();

                // ������ ��Ʈ�� �ӵ� ����
                note.GetComponent<MinigameNote>().SetSpeed(SetNoteSpeed()); // 1.4f 1.2f 1.0f
            }
        }

        // �̴ϰ��� ��Ʈ �ӵ� ����
        float SetNoteSpeed()
        {
            if (ReinforcementManager.instance != null)
            {
                // ���� ��ȭ UI�� ���
                switch (ReinforcementManager.instance.EquipReinforcementType)                                                                          
                {
                    case ReinforcementManager.ReinforcementType.normal:
                        {
                            // ���� ��ȭ Ÿ���� �⺻ ��ȭ�� ��
                            return 1.4f;
                        }
                    case ReinforcementManager.ReinforcementType.high:
                        {
                            // ���� ��ȭ Ÿ���� ���� ��ȭ�� ��
                            return 1.2f;
                        }
                    case ReinforcementManager.ReinforcementType.highest:
                        {
                            // ���� ��ȭ Ÿ���� ������ ��ȭ�� ��
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
                // ���� ��ȭ UI�� �ƴ� ��� (���� UI�� ���)
                return 1.4f;
            }
        }

        // ��Ʈ ���� �� ���� ������ �ð����� ���
        IEnumerator WaitBeforeNote()
        {
            if(!isNoteDelay)
            {
                isNoteDelay = true;

                // �⺻ ��Ʈ ���� �����̿� ���� �ð���ŭ ���� (��Ʈ�� �ұ�Ģ�ϰ� �����ǵ���)
                float randomTime = Random.Range(-0.5f, 2.0f);
                float delay = noteDelay + randomTime;

                yield return new WaitForSeconds(delay);

                // ��Ʈ ���� �� ���� ������ ��Ʈ �� ����
                GenerateNote();
                currentNoteCount++;

                isNoteDelay = false;
            }
        }

        // �̴ϰ��� ��� ó��
        void FinishMinigame()
        {
            if (ReinforcementManager.instance != null)
            {
                // ���� ��ȭ UI�� �����ִ� ���
                // �̴ϰ��� ���� ������ �Ѱ��ָ� ��ȭ ����
                ReinforcementManager.instance.ProgressReinforcement(minigameScore);
            }

            if(ProductionManager.instance != null)
            {
                // ���� ���� UI�� �����ִ� ���
                // �̴ϰ��� ���� ������ �Ѱ��ָ� ���� ����
                ProductionManager.instance.ProgressProduction(minigameScore);
            }

            // �ʱ�ȭ (��ȭ�� ���� �� �̴ϰ����� �ٽ� �����ϱ� ����)
            Initialize();
        }

        // ���� ���� �߰�
        public void AddScore(int noteScore)
        {
            // �̴ϰ��� ���� ������ ��Ʈ �� ���� �߰�
            minigameScore += noteScore;
        }
    }
}
