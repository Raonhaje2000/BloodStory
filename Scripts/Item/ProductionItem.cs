using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "ProductionItem", menuName = "RSA/ProductionItem")]
    public class ProductionItem : InventoryItem
    {
        // 아이템 획득 장소
        public enum GetPlace { shop = 0, boss = 1 }

        [SerializeField] GetPlace itemGetPlace; // 아이템 획득 장소

        public GetPlace ItemGetPlace
        {
            get { return itemGetPlace; }
        }

        // 인벤토리 아이템 데이터 복사 (새로운 묶음 생성을 위함)
        public override InventoryItem CopyInventoryData()
        {
            // 새로운 아이템 생성
            ProductionItem item = ScriptableObject.CreateInstance<ProductionItem>();

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

            item.itemGetPlace = this.itemGetPlace;

            // 데이터가 복사된 새로운 아이템 반환
            return item;
        }
    }
}
