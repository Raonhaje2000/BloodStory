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
        enum JudgementType { bad = 0, good = 1, perfect = 2 }  // ��Ʈ�� ���� Ÿ��
        const int KEY_TYPE_MAX = 3;                            // ��Ʈ���� ��� ������ Ű�� �ִ� ������

        TextMeshProUGUI minigameKey;      // ���� ������ ��Ʈ�� Ű �ؽ�Ʈ
        GameObject minigameRing;          // ���� ������ ��Ʈ�� ���� ��

        KeyCode[] usingKey;               // ��� ������ Ű ���
        string[] usingKeyText;            // ��� ������ Ű ��Ͽ� ���� Ű �ؽ�Ʈ

        float perfectRange;               // Perfect ���� ����
        float goodRange;                  // Good ���� ����
        float badRange;                   // Bad ���� ����

        Color perfectBlueOutlineColor;    // Perfect �׵θ� ��
        Color goodGreenOutlineColor;      // Good �׵θ� ��
        Color badRedOutlineColor;         // Bad �׵θ� ��

        TextMeshProUGUI judgementText;    // ��Ʈ ���� �ؽ�Ʈ

        Vector3 initScale;                // �ʱ� ���� �� ũ�� (�߾� ���� ���� ũ��)
        Vector3 startScale;               // ������ ���� ���� �� ũ�� (Ȯ������ ���� ũ��)

        int keyIndex;                     // ��� ������ Ű ��� �߿��� ���� Ű (������ �ϴ� Ű)
        float ringSpeed;                  // ���� ���� �پ��� �ӵ�
        float time;                       // ��Ʈ�� ������ ���� �帥 �ð� (��Ʈ�� ũ�� ��ȭ�� ����)

        bool isKeyDown;                   // Ű�� ���ȴ��� Ȯ���ϴ� �÷���

        private void Awake()
        {
            // ���� ������Ʈ�� �ҷ�����
            LoadObejcts();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();

            // ��Ʈ �ʱ�ȭ
            InitSetNote();
        }

        void Update()
        {
            if (!isKeyDown)
            {
                // Ű�� ������ �ʾ��� ���

                // ���� ���� ũ�� ������Ʈ
                UpdateRingSize();

                // ������ �ϴ� Ű�� ������ ��� ���� �� ũ�⿡ ���� ��� ����
                JudgeNote();
            }
        }

        // ��Ʈ �ӵ� (�پ��� �ӵ�) ����
        public void SetSpeed(float speed)
        {
            // Awake, SetSpeed, Start, Update ������ �����

            ringSpeed = speed;
        }

        void LoadObejcts()
        {

            minigameRing = transform.Find("MinigameNoteRing").gameObject;

            minigameKey = transform.Find("MinigameNoteCircleKey").GetComponent<TextMeshProUGUI>();
            
            judgementText = transform.Find("MinigameNoteJudgementText").GetComponent<TextMeshProUGUI>();
        }

        // �ʱ�ȭ
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

        // ��Ʈ �ʱ�ȭ
        public void InitSetNote()
        {
            minigameRing.transform.localScale = startScale;

            // Ű ��Ͽ��� ������ �ϴ� Ű �������� �ϳ� ����
            keyIndex = Random.Range(0, KEY_TYPE_MAX);

            time = 0.0f;

            minigameKey.text = usingKeyText[keyIndex];
        }

        // ���� ���� ũ�� ������Ʈ
        void UpdateRingSize()
        {
            if (minigameRing.transform.localScale.x >= initScale.x * 0.1f)
            {
                // ���� ���� ũ�Ⱑ �ʱ� �� ũ���� 0.1�� �̻��� ���

                // ������ ���� �� ũ�⿡�� �� �ӵ��� ���� ũ�� ���� ����
                // ��Ʈ�� ���� �� �� �帥 �ð��� ���� ��� ��ȭ��
                minigameRing.transform.localScale = startScale * (1.0f - ringSpeed * time);
                time += Time.deltaTime;
            }
            else
            {
                // ���� ���� ũ�Ⱑ �ʱ� �� ũ���� 0.1�� ���� �۾��� ���

                // ���� �� ��Ȱ��ȭ
                minigameRing.SetActive(false);

                // Bad ���� ����
                SetNoteJudgement(JudgementType.bad);
            }     
        }

        // ������ �ϴ� Ű�� ������ ��� ���� �� ũ�⿡ ���� ��� ����
        void JudgeNote()
        {
            if(Input.GetKeyDown(usingKey[keyIndex]))
            {
                // ������ �ϴ� Ű�� ���� ���

                if(minigameRing.transform.localScale.x <= (initScale * (1.0f + perfectRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - perfectRange)).x)
                {
                    // ���� ���� ũ�Ⱑ Perfect ���� ���� �ִ� ���
                    // Perfect ���� ����
                    SetNoteJudgement(JudgementType.perfect);
                }
                else if(minigameRing.transform.localScale.x <= (initScale * (1.0f + goodRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - goodRange)).x)
                {
                    // ���� ���� ũ�Ⱑ Good ���� ���� �ִ� ���
                    // Good ���� ����
                    SetNoteJudgement(JudgementType.good);
                }
                else if (minigameRing.transform.localScale.x <= (initScale * (1.0f + badRange)).x && minigameRing.transform.localScale.x >= (initScale * (1.0f - badRange)).x)
                {
                    // ���� ���� ũ�Ⱑ Bad ���� ���� �ִ� ���
                    // Bad ���� ����
                    SetNoteJudgement(JudgementType.bad);
                }
            }
        }

        // ��Ʈ ��� ���� ����
        void SetNoteJudgement(JudgementType type)
        {
            // Ű ������ üũ (������ ������ ���� �����ϱ� ����)
            isKeyDown = true;

            // ��Ʈ ����� ���� ���� �ؽ�Ʈ Ȱ��ȭ
            ActiveJudgementText(type);

            // �̴ϰ��� ���� ������ ���� ��Ʈ�� ���� �߰�
            // �������� �������� �̿��� (Bad = 0��, Good = 1��, Perfect = 2��)
            MinigameManager.instance.AddScore((int)type);

            // 1.5�� �� ��Ʈ ���� (��Ʈ�� �����ǰ� Bad ������ �� ������ �� ũ�Ⱑ �پ�� �� �ֵ��� ��� �ð� ���� ��Ȱ��ȭ)
            Destroy(gameObject, 1.5f);
        }

        // ��Ʈ ����� ���� ���� �ؽ�Ʈ Ȱ��ȭ
        void ActiveJudgementText(JudgementType type)
        {
            // ��Ʈ ���� �ؽ�Ʈ Ȱ��ȭ
            judgementText.gameObject.SetActive(true);
            
            switch (type)
            {
                case JudgementType.bad:
                    {
                        // Bad ������ ���
                        // �ؽ�Ʈ�� Bad�� �ٲٰ�, �ؽ�Ʈ �׵θ� ���� Bad �׵θ� ������ ����
                        judgementText.text = "Bad";
                        judgementText.outlineColor = badRedOutlineColor;

                        break;
                    }
                case JudgementType.good:
                    {
                        // Good ������ ���
                        // �ؽ�Ʈ�� Good���� �ٲٰ�, �ؽ�Ʈ �׵θ� ���� Good �׵θ� ������ ����
                        judgementText.text = "Good";
                        judgementText.outlineColor = goodGreenOutlineColor;

                        break;
                    }
                case JudgementType.perfect:
                    {
                        // Perfect ������ ���
                        // �ؽ�Ʈ�� Perfect�� �ٲٰ�, �ؽ�Ʈ �׵θ� ���� Perfect �׵θ� ������ ����
                        judgementText.text = "Perfect";
                        judgementText.outlineColor = perfectBlueOutlineColor;

                        break;
                    }
            }
        }
    }
}
