using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class FieldGenManager : MonoBehaviour
    {
        public static FieldGenManager instance;

        GameObject[] oxygens;    // ��� ������Ʈ��
        GameObject[] whiteCalls; // ������ ȣ�� ������Ʈ��
        GameObject reset;        // ���� ������Ʈ

        int oxygenCountMax;      // �ʵ忡 �ִ� ��ü ��� ����
        int oxygenCountCurrent;  // �ʵ忡 �ִ� ���� ��� ����

        int resetGenOxygenPercent; // ���� �������� �����Ǳ� ���� ȹ���ؾ� �ϴ� ��� ����
        int resetGenOxygenCount;   // ���� �������� �����Ǳ� ���� ȹ���ؾ� �ϴ� ��� ����

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
            oxygens = GameObject.FindGameObjectsWithTag("Oxygen");
            whiteCalls = GameObject.FindGameObjectsWithTag("WhiteCall");                                                                
            reset = GameObject.FindWithTag("Reset");
        }

        // �ʱ�ȭ
        void Initialize()
        {
            reset.SetActive(false); // ���� ������ ��Ȱ��ȭ

            oxygenCountMax = oxygens.Length;     // ��� ������Ʈ ������ŭ ��ü ��� ���� �ʱ�ȭ                               
            oxygenCountCurrent = oxygenCountMax;

            resetGenOxygenPercent = 10;
            resetGenOxygenCount = (int)(oxygenCountMax * (1 - (resetGenOxygenPercent / 100.0f)));

            Debug.Log(oxygenCountMax + ", " + (oxygenCountMax-resetGenOxygenCount));
        }

        // ��� ���� ����
        public void DecreaseOxygen()
        {
            oxygenCountCurrent--;

            if(oxygenCountCurrent <= resetGenOxygenCount)
            {
                // ���� ��� ������ ���� �������� �����Ǳ� ���� ȹ���ؾ� �ϴ� ��� ���� ������ ���              
                // ���� ������ ���� (Ȱ��ȭ)
                GenerateResetItem();
            }
        }

        // ���� ������ ���� (Ȱ��ȭ)
        public void GenerateResetItem()
        {
            reset.SetActive(true);
        }

        // �ʵ� ��ü ������ ����� (Ȱ��ȭ)
        public void GenerateFieldItems()
        {
            oxygenCountCurrent = oxygenCountMax; // ���� ��� ������ ��ü ��� ������ �ʱ�ȭ                                

            // ��� ��� ������Ʈ Ȱ��ȭ
            for (int i = 0; i < oxygens.Length; i++)
            {
                oxygens[i].SetActive(true);
            }

            // ��� ������ ȣ�� ������Ʈ Ȱ��ȭ
            for(int i = 0; i < whiteCalls.Length; i++)
            {
                whiteCalls[i].SetActive(true);
            }
        }
    }
}