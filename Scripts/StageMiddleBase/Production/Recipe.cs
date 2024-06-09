using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "RSA/Recipe")]
    public class Recipe : ScriptableObject
    {
        public const int PRODUCTION_ITEMS_MAX = 4;                                                              // 제작 재료 아이템 종류의 최대치

        [SerializeField] InventoryItem finishedProductItem;                                                     // 제작된 최종 아이템 (완성품)

        [SerializeField] ProductionItem[] productionMaterialItems = new ProductionItem[PRODUCTION_ITEMS_MAX];   // 재료 아이템 목록
        [SerializeField] int[] productionMaterialNeedCount = new int[PRODUCTION_ITEMS_MAX];                     // 재료 아이템의 요구 개수

        [SerializeField] float possibleProductionLevel;                                                         // 제작 가능한 제작 숙련도 레벨 (레벨이 낮은 경우 제작 불가)
        [SerializeField] float productionExp;                                                                   // 제작시 획득하는 숙련도 수치

        public InventoryItem FinishedProductItem
        {
            get { return finishedProductItem; }
        }

        public ProductionItem[] ProductionMaterialItems
        {
            get { return productionMaterialItems; }
        }

        public int[] PoductionMaterialNeedCount
        {
            get { return productionMaterialNeedCount; }
        }

        public float PossibleProductionLevel
        {
            get { return possibleProductionLevel; }
        }

        public float ProductionExp
        { 
            get { return productionExp; }
        }
    }
}
