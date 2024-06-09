using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AttributeItem", menuName = "RSA/AttributeItem")]
    public class AttributeItem : InventoryItem
    {
        // 속성 아이템 종류
        public enum AttributeType { VitaminD = 0, VitaminC = 1, VitaminB = 2, VitaminA = 3 }

        public const int QUALITY_VALUE_MAX = 100; // 품질의 최대 수치

        [SerializeField] AttributeType attributeItmeType; // 속성 아이템 종류

        [SerializeField] int quality; // 아이템 품질

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
                // 속성 부여에 사용되지 않아 품질이 정해지지 않았을 경우 (초기값 -1)
                // 0에서 100사이 값 중 랜덤으로 품질 부여
                if (quality < 0) quality = Random.Range(0, QUALITY_VALUE_MAX + 1);

                return quality;
            }
        }

        // 인벤토리 아이템 데이터 복사 (새로운 묶음 생성을 위함)
        public override InventoryItem CopyInventoryData()
        {
            // 새로운 아이템 생성
            AttributeItem item = ScriptableObject.CreateInstance<AttributeItem>();

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

            item.attributeItmeType = this.attributeItmeType;
            item.quality = -1;

            // 데이터가 복사된 새로운 아이템 반환
            return item;
        }

        // 인벤토리 아이템 사용
        public override void UseInventoryItem()
        {
            // 현재 보유 개수 1 감소
            invenCurrentCount--;
        }
    }
}
