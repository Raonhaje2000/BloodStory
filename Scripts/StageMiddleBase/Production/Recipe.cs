using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "RSA/Recipe")]
    public class Recipe : ScriptableObject
    {
        public const int PRODUCTION_ITEMS_MAX = 4;                                                              // ���� ��� ������ ������ �ִ�ġ

        [SerializeField] InventoryItem finishedProductItem;                                                     // ���۵� ���� ������ (�ϼ�ǰ)

        [SerializeField] ProductionItem[] productionMaterialItems = new ProductionItem[PRODUCTION_ITEMS_MAX];   // ��� ������ ���
        [SerializeField] int[] productionMaterialNeedCount = new int[PRODUCTION_ITEMS_MAX];                     // ��� �������� �䱸 ����

        [SerializeField] float possibleProductionLevel;                                                         // ���� ������ ���� ���õ� ���� (������ ���� ��� ���� �Ұ�)
        [SerializeField] float productionExp;                                                                   // ���۽� ȹ���ϴ� ���õ� ��ġ

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
