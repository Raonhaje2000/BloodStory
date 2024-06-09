using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName ="FieldItem", menuName ="RSA/FieldItem")]
    public class FieldItem : UsingItem
    {
        // �ʵ� ������ ���̵�
        public enum Id { speedUp = 222001, randomTeleport = 223001, shield = 223002 };                           

        [SerializeField] float duration; // ȿ�� ���� �ð�
        [SerializeField] float coolTime; // ��Ÿ��
        bool isUsePossible;    // ��� ���� ���� (��Ÿ���� �ƴ���)
        bool isEffectContinue; // ȿ�� ���� ���� (ȿ���� ����ǰ� �ִ���)

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public float CoolTime
        {
            get { return coolTime; }
            set { coolTime = value; }
        }

        public bool IsUsePossible
        {
            get { return isUsePossible; }
            set { isUsePossible = value; }
        }

        public bool IsEffectContinue
        {
            get { return isEffectContinue; }
            set { isEffectContinue = value; }
        }

        // �ʵ� ������ ������ ����
        // ScriptableObject�� �ҷ����� �� �� ������ �ʱ�ȭ ���� �ʰ� �ϱ� ����
        public FieldItem CopyFieldItemData()
        {
            // ���ο� ������ ����
            FieldItem item = ScriptableObject.CreateInstance<FieldItem>();

            // ������ �����ۿ� ���� ������ ����
            item.itemIcon = this.ItemIcon;

            item.itemId = this.itemId;
            item.itemName = this.itemName;
            item.itemTooltip = this.itemTooltip;

            item.itemType = this.itemType;
            item.itemRank = this.itemRank;

            item.invenMaxCount = this.invenMaxCount;
            item.invenCurrentCount = this.invenCurrentCount;

            item.duration = this.duration;
            item.coolTime = this.coolTime;

            item.isUsePossible = this.isUsePossible;
            item.isEffectContinue = this.isEffectContinue;

            // �����Ͱ� ����� ���ο� ������ ��ȯ
            return item;
        }
    }
}