using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public class WhiteCallRemainingTime : MonoBehaviour
    {
        GameObject player; // �÷��̾�

        Slider whiteCallRemainingTimeSlider; // ������ ȣ�� ���� �ð� Ÿ�̸� ��                                           

        float maxTime; // ������ ȣ�� �ִ� �ð�
        float minTime; // ������ ȣ�� �ּ� �ð�

        float currentTime; // ������ ȣ�� ���� ���� �ð�

        private void Awake()
        {
            // ���� ������Ʈ �ҷ�����
            LoadObejcts();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        void Update()
        {
            // Ÿ�̸� ������Ʈ
            UpdateTime();
        }

        // ���� ������Ʈ �ҷ�����
        void LoadObejcts()
        {
            player = GameObject.Find("Player");
            whiteCallRemainingTimeSlider = GameObject.Find("WhiteCallRemainingTime").GetComponent<Slider>();             
        }

        // �ʱ�ȭ
        void Initialize()
        {
            maxTime = player.GetComponent<PlayerRed>().WhiteCallTime;
            minTime = 0.0f;
            currentTime = 0.0f;

            whiteCallRemainingTimeSlider.maxValue = maxTime;
            whiteCallRemainingTimeSlider.minValue = minTime;

            whiteCallRemainingTimeSlider.value = minTime;
        }

        // Ÿ�̸� ������Ʈ
        void UpdateTime()
        {
            if (player.GetComponent<PlayerRed>().playerRedState == PlayerRed.State.WhiteCall)                        
            {
                // ���� �÷��̾��� ���°� ������ ȣ�� ������ ��� Ÿ�̸� ����

                // ��ü �ð����� �귯�� �ð��� ��� ���ָ� ���� ���� �ð��� ���
                currentTime -= Time.deltaTime;
                whiteCallRemainingTimeSlider.value = currentTime;
            }
            else
            {
                // ���� �÷��̾��� ���°� ������ ȣ�� ���°� �ƴ� ���

                // Ÿ�̸� �ʱ�ȭ
                currentTime = maxTime;
            }
        }
    }
}