using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "RecoveryItem", menuName = "RSA/RecoveryItem")]
    public class RecoveryItem : InventoryItem
    {
        [SerializeField] float coolTime;    // ��Ÿ��
        bool isUsePossible;                 // ��� ���� ���� (��Ÿ���� �ƴ���)

        [SerializeField] float recoveryHp;  // ȸ���ϴ� ü�� ��ġ

        public float CoolTime
        {
            get { return coolTime; }
        }

        public bool IsUsePossible
        {
            get { return isUsePossible; }
            set { isUsePossible = value; }
        }

        public float RecoveryHp
        {
            get { return recoveryHp; }
        }

        // �κ��丮 ������ ������ ���� (���ο� ���� ������ ����)
        public override InventoryItem CopyInventoryData()
        {
            // ���ο� ������ ����
            RecoveryItem item = ScriptableObject.CreateInstance<RecoveryItem>();

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
            item.recoveryHp = this.recoveryHp;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }
    }
}
