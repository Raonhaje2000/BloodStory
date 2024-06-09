using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AttributeItem", menuName = "RSA/AttributeItem")]
    public class AttributeItem : InventoryItem
    {
        // �Ӽ� ������ ����
        public enum AttributeType { VitaminD = 0, VitaminC = 1, VitaminB = 2, VitaminA = 3 }

        public const int QUALITY_VALUE_MAX = 100; // ǰ���� �ִ� ��ġ

        [SerializeField] AttributeType attributeItmeType; // �Ӽ� ������ ����

        [SerializeField] int quality; // ������ ǰ��

        public AttributeType AttributeItmeType
        { 
            get { return attributeItmeType; }
        }

        public int ItemTypeIndex
        {
            get
            {
                return (int)AttributeItmeType;
            }
        }

        public int Quality
        {
            get
            {
                // �Ӽ� �ο��� ������ �ʾ� ǰ���� �������� �ʾ��� ��� (�ʱⰪ -1)
                // 0���� 100���� �� �� �������� ǰ�� �ο�
                if (quality < 0) quality = Random.Range(0, QUALITY_VALUE_MAX + 1);

                return quality;
            }
        }

        // �κ��丮 ������ ������ ���� (���ο� ���� ������ ����)
        public override InventoryItem CopyInventoryData()
        {
            // ���ο� ������ ����
            AttributeItem item = ScriptableObject.CreateInstance<AttributeItem>();

            // ������ �����ۿ� ���� ������ ����
            item.itemIcon = this.ItemIcon;

            item.itemId = this.itemId;
            item.itemName = this.itemName;
            item.itemTooltip = this.itemTooltip;

            item.itemType = this.itemType;
            item.itemRank = this.itemRank;

            item.invenMaxCount = this.invenMaxCount;
            item.invenCurrentCount = this.invenCurrentCount;

            item.sellingPrice = this.sellingPrice;
            item.purchasePrice = this.purchasePrice;
            item.repurchasePrice = this.repurchasePrice;

            item.attributeItmeType = this.attributeItmeType;
            item.quality = -1;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }

        // �κ��丮 ������ ���
        public override void UseInventoryItem()
        {
            // ���� ���� ���� 1 ����
            invenCurrentCount--;
        }
    }
}
