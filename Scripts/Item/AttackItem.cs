using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AttackItem", menuName = "RSA/AttackItem")]
    public class AttackItem : InventoryItem
    {
        // ����� ����
        public enum DebuffType { none = 0, slow = 1, stun = 2 }

        [SerializeField] DebuffType itemDebuffType; // ����� ����

        [SerializeField] float coolTime; // ��Ÿ��
        bool isUsePossible;              // ��� ���� ���� (��Ÿ���� �ƴ���)

        [SerializeField] float duration; // ȿ�� ���� �ð�

        [SerializeField] float damage;   // ���Ϳ��� ������ ������

        public DebuffType ItemDebuffType
        {
            get { return itemDebuffType; }
        }

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

        public float Damage
        {
            get { return damage; }
        }

        // �κ��丮 ������ ������ ���� (���ο� ���� ������ ����)
        public override InventoryItem CopyInventoryData()
        {
            // ���ο� ������ ����
            AttackItem item = ScriptableObject.CreateInstance<AttackItem>();

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

            item.itemDebuffType = this.itemDebuffType;

            item.coolTime = this.coolTime;
            item.duration = this.duration;

            item.damage = this.damage;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }
    }
}
