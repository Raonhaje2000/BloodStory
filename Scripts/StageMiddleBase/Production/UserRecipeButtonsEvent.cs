using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace RSA
{
    public class UserRecipeButtonsEvent : MonoBehaviour, IPointerClickHandler
    {
        Recipe itemRecipe;                 // ������ ������

        public Recipe ItemRecipe
        { 
            get { return itemRecipe; }
            set { itemRecipe = value; }
        }

        void Start()
        {

        }

        // ������ ��ư �ʱ�ȭ
        public void InitializeButtons(Recipe recipe)
        {
            // �ش� �����Ǹ� ��ư�� ���
            itemRecipe = recipe;
            
            if(itemRecipe.PossibleProductionLevel <= GameManager.instance.PlayerProductionLevel)
            {
                // �������� �䱸 ���� ���õ� ������ �÷��̾��� ���� ���õ� ���� ������ ���

                // ������ ��ư�� �ؽ�Ʈ�� ������ ������ �̸����� ǥ�� 
                GetComponentInChildren<TextMeshProUGUI>().text = recipe.FinishedProductItem.ItemName;
            }
            else
            {
                // �������� �䱸 ���� ���õ� ������ �÷��̾��� ���� ���õ� �������� �� ���� ���

                // ������ ��ư�� �ؽ�Ʈ�� ???���� ǥ��
                GetComponentInChildren<TextMeshProUGUI>().text = "???";
            }
        }

        // ���콺 Ŭ�� �̺�Ʈ ó��
        public void OnPointerClick(PointerEventData eventData)
        {
            if(ProductionManager.instance != null)
            {
                // ���� UI�� Ȱ��ȭ �� ������ ��

                if(itemRecipe.PossibleProductionLevel <= GameManager.instance.PlayerProductionLevel)
                {
                    // �������� �䱸 ���� ���õ� ������ �÷��̾��� ���� ���õ� ���� ������ ���
                    // ���� UI�� �ش� �����ǿ� ���� ����
                    ProductionManager.instance.SetCurrentRecipe(itemRecipe);
                }
            }
        }
    }
}
