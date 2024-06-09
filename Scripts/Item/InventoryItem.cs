using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class InventoryItem : UsingItem
    {
        // 인벤토리 아이템 아이디
        public enum Id
        {
            lowPotion = 224011, intermediatePotion = 224021, advancedPotion = 224031, finestPotion = 224041,
            mudGrenade = 225001, electricGrenade = 225002, scarecrow = 226011, woodenScarecrow = 226021,
            vitaminD = 228211, vitaminC = 228221, vitaminB = 228231, vitaminA = 228241,
            water = 228301, bottle = 228302, glucose = 228303, soil = 228304, gunpowder = 228305, straw = 228306, rope = 228307,
            ginseng = 228308, ionBattery = 228309, goldPine = 2283010
        }

        [SerializeField] protected int purchasePrice;   // 상점 구매 가격 (상점 -> 플레이어)
        [SerializeField] protected int sellingPrice;    // 상점 판매 가격 (플레이어 -> 상점)
        [SerializeField] protected int repurchasePrice; // 상점 재구매 가격

        public int PurchasePrice
        {
            get { return purchasePrice; }
        }

        public int SellingPrice
        { 
            get { return sellingPrice; }
        }

        public int RepurchasePrice
        {
            get { return repurchasePrice; }
        }

        // 인벤토리 아이템 데이터 복사 (새로운 묶음 생성을 위함)
        public virtual InventoryItem CopyInventoryData()
        {
            return null;
        }

        // 인벤토리 아이템 사용
        public virtual void UseInventoryItem()
        {

        }
    }
}
