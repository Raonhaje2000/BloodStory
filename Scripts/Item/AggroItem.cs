using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AggroItem", menuName = "RSA/AggroItem")]
    public class AggroItem : InventoryItem
    {
        [SerializeField] float coolTime; // 쿨타임
        bool isUsePossible;              // 사용 가능 여부 (쿨타임이 아닌지)

        [SerializeField] float duration; // 효과 지속 시간

        [SerializeField] float itemHp;   // 몬스터에게 입히는 데미지

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

        // 인벤토리 아이템 데이터 복사 (새로운 묶음 생성을 위함)
        public override InventoryItem CopyInventoryData()
        {
            // 새로운 아이템 생성
            AggroItem item = ScriptableObject.CreateInstance<AggroItem>();

            // 생성된 아이템에 기존 데이터 복사
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

            // 데이터가 복사된 새로운 아이템 반환
            return item;
        }
    }
}
