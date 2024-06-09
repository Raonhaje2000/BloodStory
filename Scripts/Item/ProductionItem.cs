using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "ProductionItem", menuName = "RSA/ProductionItem")]
    public class ProductionItem : InventoryItem
    {
        // ������ ȹ�� ���
        public enum GetPlace { shop = 0, boss = 1 }

        [SerializeField] GetPlace itemGetPlace; // ������ ȹ�� ���

        public GetPlace ItemGetPlace
        {
            get { return itemGetPlace; }
        }

        // �κ��丮 ������ ������ ���� (���ο� ���� ������ ����)
        public override InventoryItem CopyInventoryData()
        {
            // ���ο� ������ ����
            ProductionItem item = ScriptableObject.CreateInstance<ProductionItem>();

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

            item.itemGetPlace = this.itemGetPlace;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }
    }
}
