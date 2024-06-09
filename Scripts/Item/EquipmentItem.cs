using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "EquipmentItem", menuName = "RSA/EquipmentItem")]
    public class EquipmentItem : Item
    {
        public const int ITEM_GRADE_MAX = 2; // 각 랭크별 아이템의 최대 등급

        // 장비 아이템 부위
        public enum Part { sword = 0, helmet = 1, armor = 2, pants = 3, boot = 4 }

        [SerializeField] Part equipPart; // 장비 아이템 부위

        [SerializeField] int itemGrade;  // 아이템 등급

        [SerializeField] Status initStatus;          // 아이템의 기본 능력치
        [SerializeField] Status reinforcementStatus; // 장비 강화로 상승한 능력치
        [SerializeField] Status attributeStatus;     // 속성 부여로 상승한 능력치

        [SerializeField] AttributeInstallation equipAttributeInstallation; // 장비에 부여된 속성 상태 (품질, 부여 수치 등)

        public Part EquipPart
        {
            get { return equipPart; }
        }

        public int ItemGrade
        {
            get { return itemGrade; }
            set
            {
                itemGrade = value;

                if (itemGrade > ITEM_GRADE_MAX)
                {
                    // 아이템 등급이 각 랭크별 아이템 최대 등급을 넘어선 경우
                    if (this.ItemRank != Rank.legend)
                    {
                        // 아이템 랭크가 레전드가 아닐 때 랭크 증가 후 등급 0으로 초기화
                        ItemRank++;
                        itemGrade = 0;
                    }
                    else
                    {
                        // 아이템 랭크가 레전드이면 최대 수치인 2로 고정
                        itemGrade = ITEM_GRADE_MAX;
                    }
                }
                else if (itemGrade < 0)
                {
                    itemGrade = 0;
                }
            }
        }

        public Status InitStatus
        {
            get { return initStatus; }
        }
        
        public Status ReinforcementStatus
        {
            get { return reinforcementStatus; }
            set { reinforcementStatus = value; }
        }

        public Status AttributeStatus
        {
            get { return attributeStatus; }
            set { attributeStatus = value; }
        }

        public AttributeInstallation EquipAttributeInstallation
        { 
            get { return equipAttributeInstallation; }
            set { equipAttributeInstallation = value; }
        }
    }
}
