using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class AttributeMessageBox : MonoBehaviour
    {
        AttributeItem vitaminItem;                       // ���Ǵ� �Ӽ� ������ (��Ÿ��)

        TextMeshProUGUI attributeMessageBoxTextVitamin;  // ��Ÿ�� ���� �ؽ�Ʈ
        TextMeshProUGUI attributeMessageBoxTextIsExist;  // �ο��� �Ӽ��� �����ϴ����� ���� �ؽ�Ʈ

        string existText;     // ������ ���� �ؽ�Ʈ ��
        string nonexistText;  // �������� ���� ���� �ؽ�Ʈ ��

        private void Awake()
        {
            LoadObejcts(); // ���� ������Ʈ�� �ҷ�����
            Initialize();  // �ʱ�ȭ
        }

        void Start()
        {
            //Initialize();
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObejcts()
        {
            attributeMessageBoxTextVitamin = GameObject.Find("AttributeMessageBoxTextVitamin").GetComponent<TextMeshProUGUI>();
            attributeMessageBoxTextIsExist = GameObject.Find("AttributeMessageBoxTextIsExist").GetComponent<TextMeshProUGUI>();
        }

        // �ʱ�ȭ
        void Initialize()
        {
            vitaminItem = null;

            existText = "�̹� ������ ��Ÿ�� ������";
            nonexistText = "���� �����ϴ� ��Ÿ�� ������";
        }

        // �Ӽ� �ο� �޼��� �ڽ� ����
        public void SetMessageBoxText(AttributeItem vitamin, bool isExist)
        {
            vitaminItem = vitamin;

            attributeMessageBoxTextVitamin.text = string.Format("\'{0}\'�� �����ұ�?", vitaminItem.ItemName);
            attributeMessageBoxTextIsExist.text = (isExist) ? existText : nonexistText;
        }

        // �Ӽ� �ο��� �����ϰڴٴ� ��ư�� Ŭ������ ��
        public void ClickYesButton()
        {
            // �Ӽ� �ο� ����
            AttributeManager.instance.ContinueInstallAttribute(vitaminItem);
        }

        // �Ӽ� �ο��� �������� �ʰڴٴ� ��ư�� Ŭ������ ��
        public void ClickNoButton()
        {
            // �Ӽ� �ο� ���
            AttributeManager.instance.CancleInstallAttribute();
        }
    }
}
