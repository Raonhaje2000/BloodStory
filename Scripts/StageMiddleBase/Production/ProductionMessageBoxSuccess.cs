using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ProductionMessageBoxSuccess : MonoBehaviour
    {
        TextMeshProUGUI itemDesText;     // ������ �����ۿ� ������ ����/�뼺�� ���θ� ����ϴ� �ؽ�Ʈ
        TextMeshProUGUI expText;         // ȹ���� ���õ��� ����ϴ� �ؽ�Ʈ

        Image itemIconImage;             // ������ �������� ������ �̹���
        TextMeshProUGUI itemCountText;   // ������ �������� ���� �ؽ�Ʈ

        TextMeshProUGUI itemNameText;    // ������ �������� �̸� �ؽ�Ʈ

        private void Awake()
        {
            // ���� ������Ʈ�� �ҷ�����
            LoadObejects();
        }

        void Start()
        {

        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObejects()
        {
            itemDesText = GameObject.Find("ProductionSuccessMessageBoxItemDesText").GetComponent<TextMeshProUGUI>();
            expText = GameObject.Find("ProductionSuccessMessageBoxExpText").GetComponent<TextMeshProUGUI>();

            itemIconImage = GameObject.Find("ProductionSuccessMessageBoxSuccessItemIcon").GetComponent<Image>();
            itemCountText = GameObject.Find("ProductionSuccessMessageBoxSuccessItemCountText").GetComponent<TextMeshProUGUI>();

            itemNameText = GameObject.Find("ProductionSuccessMessageBoxItemNameText").GetComponent<TextMeshProUGUI>();
        }

        // ����/�뼺���� ���� �޼��� �ڽ� ���� (�뼺���� ��� ���õ��� �������� ȹ�� ������ ������ ���� 2��)
        public void SetMessageBoxSuccess(Recipe recipe, bool isGreatSuccess)
        {
            string successType = (!isGreatSuccess) ? "����" : "�뼺��";
            float exp = (!isGreatSuccess) ? recipe.ProductionExp : recipe.ProductionExp * 2;
            int count = (!isGreatSuccess) ? 1 : 2;

            itemDesText.text = string.Format("\'{0}\' ���ۿ� {1}�߾�!", recipe.FinishedProductItem.ItemName, successType);
            expText.text = string.Format("���õ��� {0} �����߾�.", exp);

            itemIconImage.sprite = recipe.FinishedProductItem.ItemIcon;
            itemCountText.text = count.ToString();

            itemNameText.text = recipe.FinishedProductItem.ItemName;
        }

        // X ��ư�� ������ ��
        public void ClickButtonX()
        {
            // �ش� �޼��� â ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
