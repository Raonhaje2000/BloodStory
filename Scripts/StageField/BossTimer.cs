using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class BossTimer : MonoBehaviour
    {
        GameObject homeEntranceWall; // �ʵ� �߾ӿ� �ִ� ���� �� �Ա��� ��

        float timeMax;     // �������� ���� �ð� �ִ�ġ
        float timeCurrent; // �������� ���� �ð�

        bool isTimerStop;  // Ÿ�̸Ӱ� ������� Ȯ���ϴ� �÷���

        private void Awake()
        {
            // ���� ������Ʈ �ҷ�����
            LoadObject();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        void Update()
        {
            if (!isTimerStop) UpdateCurrentTime(); // Ÿ�̸Ӱ� ������ �ʾ��� ��� Ÿ�̸� ������Ʈ
            else BossGen();                        // Ÿ�̸Ӱ� ������ ��� ���� ����
        }

        // ���� ������Ʈ �ҷ�����
        void LoadObject()
        {
            homeEntranceWall = GameObject.Find("HomeEntranceWall");
        }

        // �ʱ�ȭ
        void Initialize()
        {
            timeMax = 30.0f;       // 30��
            timeCurrent = timeMax;

            FieldUIManager.instance.InitSetBossTimer(timeMax, 0.0f);
            FieldUIManager.instance.SetActiveBossMiniMapIcon(false);

            isTimerStop = false;
        }

        // Ÿ�̸� ������Ʈ
        void UpdateCurrentTime()
        {
            // ��ü �ð����� �귯�� �ð��� ��� ���ָ� ���� ���� �ð��� ���
            timeCurrent -= Time.deltaTime;

            if (timeCurrent < 0)
            {
                // ���� ���� �ð��� 0�� �Ǿ��� ��� Ÿ�̸� ���� üũ
                timeCurrent = 0;

                isTimerStop = true;
            }

            // ���� �ð��� ���� ���� ���� Ÿ�̸� UI ������Ʈ
            FieldUIManager.instance.SetBossTimer(timeCurrent);
        }

        // ���� ����
        void BossGen()
        {
            // �ʵ� �߾ӿ� �ִ� ���� �� �Ա��� ���� ��Ȱ��ȭ�ϰ� �̴ϸʿ� ���� ������ ����
            homeEntranceWall.SetActive(false);
            FieldUIManager.instance.SetActiveBossMiniMapIcon(true);
        }
    }
}
