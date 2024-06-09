using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AggroItem", menuName = "RSA/AggroItem")]
    public class AggroItem : InventoryItem
    {
        [SerializeField] float coolTime; // ��Ÿ��
        bool isUsePossible;              // ��� ���� ���� (��Ÿ���� �ƴ���)

        [SerializeField] float duration; // ȿ�� ���� �ð�

        [SerializeField] float itemHp;   // ���Ϳ��� ������ ������

        public float CoolTime
        {
            get { return coolTime; }
        }

        public bool IsUsePossible
        {
            get { return isUsePossible; }
            set { isUsePossible = value; }
        }

        public float Duration
        {
            get { return duration; }
        }

        public float ItemHp
        {
            get { return itemHp; }
        }

        // �κ��丮 ������ ������ ���� (���ο� ���� ������ ����)
        public override InventoryItem CopyInventoryData()
        {
            // ���ο� ������ ����
            AggroItem item = ScriptableObject.CreateInstance<AggroItem>();

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

            item.coolTime = this.coolTime;
            item.duration = this.duration;

            item.itemHp = this.itemHp;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }
    }
}
