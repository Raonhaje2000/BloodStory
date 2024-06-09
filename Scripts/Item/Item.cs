using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public class Item : ScriptableObject
    {
        // ������ Ÿ��
        public enum Type { equipment = 0, field = 1, recovery = 2, attack = 3, aggro = 4, money = 5, attributeMaterial = 6, ProductionMaterial }

        // ������ ��ũ
        public enum Rank { none = 0, normal = 1, rare = 2, unique = 3, legend = 4 };

        [SerializeField] protected Sprite itemIcon;    // ������ ������

        [SerializeField] protected int itemId;         // ������ ���̵�
        [SerializeField] protected string itemName;    // ������ �̸�
        [SerializeField] protected string itemTooltip; // ������ ����

        [SerializeField] protected Type itemType;  // ������ Ÿ��
        [SerializeField] protected Rank itemRank;  // ������ ��ũ

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
