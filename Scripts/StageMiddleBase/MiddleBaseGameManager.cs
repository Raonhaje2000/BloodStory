using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class MiddleBaseGameManager : MonoBehaviour
    {
        public static MiddleBaseGameManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI(); // �߰� ���� �⺻ UI Ȱ��ȭ
            MiddleBaseUIManager.instance.SetScoreAndOxygenTexts(); // ������ ��� �ؽ�Ʈ UI ����
        }

        // ���� ���� ������ Ŭ��
        public void ClickUserInformationIcon()
        {
            // ���� ���� UI Ȱ��ȭ
            MiddleBaseUIManager.instance.ActiveUserInformationUI();
        }

        // ���� �κ��丮 ������ Ŭ��
        public void ClickInventoryIcon()
        {
            // ���� �κ��丮 UI Ȱ��ȭ
            MiddleBaseUIManager.instance.ActiveUserInventoryUI();
        }

        // �Ӽ� �ο� ������ Ŭ��
        public void ClickAttributeIcon()
        {
            // �Ӽ� �ο� UI Ȱ��ȭ
            MiddleBaseUIManager.instance.ActiveAttributeUI();
        }
    }
}