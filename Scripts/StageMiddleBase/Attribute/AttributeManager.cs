using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class AttributeManager : MonoBehaviour
    {
        public static AttributeManager instance;
        UserInventorySlotsEvent inventorySlots; // �̺�Ʈ�� �߻��� ���� �κ��丮 ����

        GameObject attributeUI;                 // �Ӽ� �ο� UI
        GameObject messageBox;                  // �Ӽ� �ο� �޼��� �ڽ�

        int ATTRIBUTE_TYPE_COUNT = 4;           // �ο� ������ �Ӽ��� ���� �� (��Ÿ�� ����)

        EquipmentItem currentEquipment;         // �Ӽ��� �ο��� ��� ������
        int[] attributeMaxValue;                // �Ӽ��� �ο����� ���� �ִ� ��ġ

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
            attributeUI = GameObject.Find("AttributeUI");
            messageBox = GameObject.Find("AttributeMessageBox");
        }

        // �ʱ�ȭ
        void Initialize()
        {
            inventorySlots = null;

            attributeUI.SetActive(false);
            messageBox.SetActive(false);

            currentEquipment = null;
            attributeMaxValue = new int[] { 4, 7, 10, 20 };
        }

        // ���� �Ӽ� �ο��� ������ ��� ������ ���� (��� ������ ���Կ��� ������ ���)
        public void SetCurrentEquipment(EquipmentItem equipItem)
        {
            AttributeUIManager.instance.SetAttributeItemEquip(equipItem);
            currentEquipment = equipItem;
        }

        // �Ӽ� �ο��� �������� �Ǵ�
        public bool IsInstallationPossible(AttributeItem attriItem)
        {
            if(currentEquipment != null && currentEquipment.EquipAttributeInstallation.VitaminsQuality[(int)attriItem.AttributeItmeType] < 100)
            {
                // ��� ���õ� ���¿��� �ش� ��� �ο��� �Ӽ��� ǰ���� 100������ ���
                // �Ӽ� �ο� �������� Ÿ���� ����������, �ش� �Ӽ��� ��� �ο��Ǵ� �ε����� �ش� ������ Ÿ���� �������� ����
                // Ex) ��Ÿ�� D�� ������ Ÿ���� �������� 0�̰�, ��� �ο��Ǵ� �ε����� 0��

                // ��� ������ ��ũ�� �ش� �Ӽ� �ο� ������ Ÿ�Ժ��� ���� ��� �Ӽ� �ο� ����, �ƴ� ��� �Ӽ� �ο� �Ұ���
                // ��� ������ ��ũ�� �������� { �븻 = 1, ���� = 2, ����ũ = 3, ������ = 4 } 
                // �Ӽ� ������ Ÿ���� �������� { ��Ÿ�� D = 0, ��Ÿ�� C = 1, ��Ÿ�� B = 2, ��Ÿ�� A = 3 }
                // Ex) ��� ������ ��ũ�� ����ũ�϶�, �Ӽ� ������ ��Ÿ�� B������ ��� ����, ��Ÿ�� A�� ��� �Ұ���
                return ((int)currentEquipment.ItemRank > (int)attriItem.AttributeItmeType) ? true : false;
            }

            // ��� ���õ��� �ʾҰų�, �Ӽ��� ǰ���� 100�� ��� �Ӽ� �ο� �Ұ���
            return false;
        }

        // �κ��丮���� �Ӽ� �ο� �������� Ŭ������ ���
        public void InstallAttribute(AttributeItem attriItem, UserInventorySlotsEvent slots)
        {
            // �ش� �Ӽ� ���������� �Ӽ��� �ο��� �ε����� �ش� �Ӽ� �������� �ִ� ������ ���� ����
            int index = (int)attriItem.AttributeItmeType;
            inventorySlots = slots;

            // ������ ��Ÿ�� ���ο� ���� �޼��� �ڽ� �ؽ�Ʈ ����
            if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] < 0) // ������ ��Ÿ���� ���� ���
                messageBox.GetComponent<AttributeMessageBox>().SetMessageBoxText(attriItem, false); 
            else                                                                        // ������ ��Ÿ���� ���� ���
                messageBox.GetComponent<AttributeMessageBox>().SetMessageBoxText(attriItem, true);

            // �Ӽ� �ο� �޼��� �ڽ� Ȱ��ȭ
            messageBox.SetActive(true);
        }

        // �Ӽ� �ο� ���
        public void CancleInstallAttribute()
        {
            // �Ӽ� �ο� �޼��� �ڽ� ��Ȱ��ȭ
            messageBox.SetActive(false);
        }

        // �Ӽ� �ο� ����
        public void ContinueInstallAttribute(AttributeItem attriItem)
        {
            // �Ӽ� ���������� �ο��Ǵ� ǰ���� �Ӽ��� �ο��� �ε��� ����
            int quality = attriItem.Quality;
            int index = (int)attriItem.AttributeItmeType;

            if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] < 0)
            {
                // ������ ��Ÿ���� ���� ���

                // ����� ��Ÿ���� ǰ�� �״�� ����
                currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] = quality;
            }
            else
            {
                // ������ ��Ÿ���� ���� ���

                // ����� ��Ÿ���� ǰ����ŭ ǰ�� ����
                currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] += quality;

                // ������ ǰ���� ���� 100 �ʰ��� ��� ǰ�� 100���� ����
                if (currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] > 100)
                    currentEquipment.EquipAttributeInstallation.VitaminsQuality[index] = 100;

                // ����� ǰ�� ����
                quality = currentEquipment.EquipAttributeInstallation.VitaminsQuality[index];
            }

            // ǰ���� ���� �Ӽ� �ο� �ɷ�ġ �� ��� �� ����
            int value = GetAttributeValue(quality, index);
            currentEquipment.EquipAttributeInstallation.VitaminsValue[index] = value;

            // ����� �Ӽ� �ο��� ������ �ɷ�ġ ������Ʈ
            UpdateEquipmentAttributeState(value);

            // �Ӽ� �ο� UI �缼��
            AttributeUIManager.instance.SetAttributeItemEquip(currentEquipment);

            // �Ӽ� �ο� �޼��� �ڽ� ��Ȱ��ȭ
            messageBox.SetActive(false);

            // ����� �Ӽ� ������ �κ��丮���� ����
            inventorySlots.SendMessage("UseAtAttributeUI");
        }

        // ǰ���� ���� �Ӽ� �ο� �ɷ�ġ �� ���
        int GetAttributeValue(int quality, int index)
        {
            // ǰ���� 25�� �پ����� �ο� ������ �ɷ�ġ �ִ밪���� ���� ����
            int valueMax = attributeMaxValue[index];

            if (quality > 75) return valueMax;
            else if (quality > 50) return valueMax - 1;
            else if (quality > 25) return valueMax - 2;
            else if (quality > 0) return valueMax - 3;
            else return 0;
        }

        // ����� �Ӽ� �ο��� ������ �ɷ�ġ ������Ʈ
        void UpdateEquipmentAttributeState(int value)
        {
            // �ش� ��� �����ۿ��� �Ӽ� �ο��� �����Ǵ� �ɷ�ġ üũ ��, �����Ǵ� ����ŭ ����
            switch (currentEquipment.EquipAttributeInstallation.AttributeType)
            {
                case AttributeInstallation.Type.hp:
                    {
                        currentEquipment.AttributeStatus.Hp += value;
                        break;
                    }
                case AttributeInstallation.Type.attack:
                    {
                        currentEquipment.AttributeStatus.Attack += value;
                        break;
                    }
                case AttributeInstallation.Type.defence:
                    {
                        currentEquipment.AttributeStatus.Defence += value;
                        break;
                    }
                case AttributeInstallation.Type.speed:
                    {
                        currentEquipment.AttributeStatus.Speed += value;
                        break;
                    }
            }
        }

        // ��� UI�� �Ӽ� ��ư�� Ŭ�� ���� ��
        public void ClickEquipmentAttributeButton()
        {
            // ���õ� ��� �ʱ�ȭ �� �Ӽ� �ο� UI �ʱ�ȭ �� Ȱ��ȭ
            currentEquipment = null;
            AttributeUIManager.instance.Initialize();
            attributeUI.SetActive(true);
            messageBox.SetActive(false);
        }

        // �Ӽ� �ο�â�� X ��ư�� Ŭ�� ���� ��
        public void ClickAttributeUIButtonX()
        {
            // �Ӽ� �ο� UI ��Ȱ��ȭ �� ���� ���õ� ��� �ʱ�ȭ
            attributeUI.SetActive(false);
            messageBox.SetActive(false);
            currentEquipment = null;
        }

        // �Ӽ� �ʱ�ȭ ��ư�� ������ ���
        public void ResetEquipAttribute()
        {
            if (currentEquipment != null)
            {
                for (int i = 0; i < ATTRIBUTE_TYPE_COUNT; i++)
                {
                    // ��� �������� ��� �Ӽ� ǰ���� -1, �Ӽ����� 0���� �ʱ�ȭ
                    currentEquipment.EquipAttributeInstallation.VitaminsQuality[i] = -1;
                    currentEquipment.EquipAttributeInstallation.VitaminsValue[i] = 0;

                    // ��� �������� �Ӽ� �ο��� ������ �ɷ�ġ 0���� �ʱ�ȭ
                    currentEquipment.AttributeStatus.ResetStatus();

                    // �Ӽ� �ο� UI �缼��
                    AttributeUIManager.instance.SetAttributeItemEquip(currentEquipment);
                }
            }
        }

        // �Ӽ� �ο� �޼��� �ڽ� Ȱ��ȭ ���� ����
        public void SetMessageBox(bool active)
        {
            messageBox.SetActive(active);
        }

        // ��� UI�� X ��ư�� Ŭ�� ���� ��
        public void ClickButtonX()
        {
            // �ش� UI ���� �� �⺻ UI Ȱ��ȭ
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }
    }
}
