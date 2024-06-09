using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public class Item : ScriptableObject
    {
        // 아이템 타입
        public enum Type { equipment = 0, field = 1, recovery = 2, attack = 3, aggro = 4, money = 5, attributeMaterial = 6, ProductionMaterial }

        // 아이템 랭크
        public enum Rank { none = 0, normal = 1, rare = 2, unique = 3, legend = 4 };

        [SerializeField] protected Sprite itemIcon;    // 아이템 아이콘

        [SerializeField] protected int itemId;         // 아이템 아이디
        [SerializeField] protected string itemName;    // 아이템 이름
        [SerializeField] protected string itemTooltip; // 아이템 툴팁

        [SerializeField] protected Type itemType;  // 아이템 타입
        [SerializeField] protected Rank itemRank;  // 아이템 랭크

        public Sprite ItemIcon
        {
            get { return itemIcon; }
        }

        public int ItemId
        { 
            get { return itemId; } 
        }

        public string ItemName
        {
            get { return itemName; }
        }

        public string ItemTooltip
        {
            get { return itemTooltip; }
        }

        public Type ItemType
        {
            get { return itemType; }
        }

        public Rank ItemRank
        {
            get { return itemRank; }
            set { itemRank = value; }
        }
    }
}
