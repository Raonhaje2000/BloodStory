using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName ="FieldItem", menuName ="RSA/FieldItem")]
    public class FieldItem : UsingItem
    {
        // 필드 아이템 아이디
        public enum Id { speedUp = 222001, randomTeleport = 223001, shield = 223002 };                           

        [SerializeField] float duration; // 효과 지속 시간
        [SerializeField] float coolTime; // 쿨타임
        bool isUsePossible;    // 사용 가능 여부 (쿨타임이 아닌지)
        bool isEffectContinue; // 효과 지속 여부 (효과가 적용되고 있는지)

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

        // 필드 아이템 데이터 복사
        // ScriptableObject를 불러왔을 때 값 변경이 초기화 되지 않게 하기 위함
        public FieldItem CopyFieldItemData()
        {
            // 새로운 아이템 생성
            FieldItem item = ScriptableObject.CreateInstance<FieldItem>();

            // 생성된 아이템에 기존 데이터 복사
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

            // 데이터가 복사된 새로운 아이템 반환
            return item;
        }
    }
}