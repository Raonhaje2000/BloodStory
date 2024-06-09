using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA_Proto
{
    public class AttributeManager : MonoBehaviour
    {
        public static AttributeManager instance;

        public Equipment currEquip;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            currEquip = null;
        }

        void Update()
        {
            if (currEquip != null)
            {
                UpdateAttributeUI();
            }
            else
            {
                AttributeUIManager.instance.SetActiveEquip(false);
                AttributeUIManager.instance.SetActiveAttribute(false);
            }
        }

        void UpdateAttributeUI()
        {
            List<Attribute> currAttributes = currEquip.Attributes;
            List<int> currAttributeValues = currEquip.AttributeValues;

            if (currAttributes.Count == 0)
            {
                AttributeUIManager.instance.SetActiveAttribute(false);
            }
            else
            {
                for (int i = 0; i < currAttributes.Count; i++)
                {
                    if (currAttributeValues[i] == -1) // 아직 속성값 부여하지 않음
                    {
                        int index = Random.Range(0, currAttributes[i].AttriValue.Length);
                        currAttributeValues[i] = currAttributes[i].AttriValue[index];

                        AddAttributeValue(currAttributeValues[i]);
                        AttributeUIManager.instance.ChangeAttributeEquip(currEquip);
                    }

                    if (i == 0)
                    {
                        AttributeUIManager.instance.ChangeAttributeSlot1(currAttributes[i].Icon, "+ " + currAttributeValues[i].ToString());
                    }
                    else if (i == 1)
                    {
                        AttributeUIManager.instance.ChangeAttributeSlot2(currAttributes[i].Icon, "+ " + currAttributeValues[i].ToString());
                    }
                    else if (i == 2)
                    {
                        AttributeUIManager.instance.ChangeAttributeSlot3(currAttributes[i].Icon, "+ " + currAttributeValues[i].ToString());
                    }
                    else
                    {
                        AttributeUIManager.instance.ChangeAttributeSlot4(currAttributes[i].Icon, "+ " + currAttributeValues[i].ToString());
                    }
                }
            }
        }

        void AddAttributeValue(int value)
        {
            switch (currEquip.EquipPart)
            {
                case Equipment.Part.sword:
                    {
                        currEquip.AttributeAttack += value;

                        break;
                    }
                case Equipment.Part.helmet:
                case Equipment.Part.armor:
                    {
                        currEquip.AttributeDefence += value;

                        break;
                    }
                case Equipment.Part.pants:
                case Equipment.Part.boot:
                    {
                        currEquip.AttributeHp += value;

                        break;
                    }
            }

        }
    }
}