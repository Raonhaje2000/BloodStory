using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "EquipmentItem", menuName = "RSA/EquipmentItem")]
    public class EquipmentItem : Item
    {
        public const int ITEM_GRADE_MAX = 2; // �� ��ũ�� �������� �ִ� ���

        // ��� ������ ����
        public enum Part { sword = 0, helmet = 1, armor = 2, pants = 3, boot = 4 }

        [SerializeField] Part equipPart; // ��� ������ ����

        [SerializeField] int itemGrade;  // ������ ���

        [SerializeField] Status initStatus;          // �������� �⺻ �ɷ�ġ
        [SerializeField] Status reinforcementStatus; // ��� ��ȭ�� ����� �ɷ�ġ
        [SerializeField] Status attributeStatus;     // �Ӽ� �ο��� ����� �ɷ�ġ

        [SerializeField] AttributeInstallation equipAttributeInstallation; // ��� �ο��� �Ӽ� ���� (ǰ��, �ο� ��ġ ��)

        public Part EquipPart
        {
            get { return equipPart; }
        }

        public int ItemGrade
        {
            get { return itemGrade; }
            set
            {
                itemGrade = value;

                if (itemGrade > ITEM_GRADE_MAX)
                {
                    // ������ ����� �� ��ũ�� ������ �ִ� ����� �Ѿ ���
                    if (this.ItemRank != Rank.legend)
                    {
                        // ������ ��ũ�� �����尡 �ƴ� �� ��ũ ���� �� ��� 0���� �ʱ�ȭ
                        ItemRank++;
                        itemGrade = 0;
                    }
                    else
                    {
                        // ������ ��ũ�� �������̸� �ִ� ��ġ�� 2�� ����
                        itemGrade = ITEM_GRADE_MAX;
                    }
                }
                else if (itemGrade < 0)
                {
                    itemGrade = 0;
                }
            }
        }

        public Status InitStatus
        {
            get { return initStatus; }
        }
        
        public Status ReinforcementStatus
        {
            get { return reinforcementStatus; }
            set { reinforcementStatus = value; }
        }

        public Status AttributeStatus
        {
            get { return attributeStatus; }
            set { attributeStatus = value; }
        }

        public AttributeInstallation EquipAttributeInstallation
        { 
            get { return equipAttributeInstallation; }
            set { equipAttributeInstallation = value; }
        }
    }
}
