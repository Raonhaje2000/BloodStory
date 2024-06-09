using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public abstract class UsingItem : Item
    {
        [SerializeField] protected int invenMaxCount;         // 인벤토리 슬롯에서의 최대 보유 개수 (해당 범위를 넘을 경우 새로운 묶음 생성)
        [SerializeField] protected int invenCurrentCount = 0; // 인벤토리 슬롯에서의 현재 보유 개수 

        public int InvenMaxCount
        { 
            get { return invenMaxCount; } 
        }

        public int InvenCurrentCount
        {
            get { return invenCurrentCount; }
            set { invenCurrentCount = value; }
        }
    }
}
