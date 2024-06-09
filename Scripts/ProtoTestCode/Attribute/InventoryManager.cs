using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA_Proto
{
    public class InventoryManager : MonoBehaviour
    {
        void Start()
        {

        }

        public void Click_InvenVitaminIconA()
        {
            if (AttributeManager.instance.currEquip != null && AttributeManager.instance.currEquip.Attributes.Count < AttributeManager.instance.currEquip.AttributeLimit)
            {
                AttributeManager.instance.currEquip.Attributes.Add(new Attribute(Attribute.Vitamin.A));
                AttributeManager.instance.currEquip.AttributeValues.Add(-1);
            }
        }

        public void Click_InvenVitaminIconB()
        {
            if (AttributeManager.instance.currEquip != null && AttributeManager.instance.currEquip.Attributes.Count < AttributeManager.instance.currEquip.AttributeLimit)
            {
                AttributeManager.instance.currEquip.Attributes.Add(new Attribute(Attribute.Vitamin.B));
                AttributeManager.instance.currEquip.AttributeValues.Add(-1);
            }
        }

        public void Click_InvenVitaminIconC()
        {
            if (AttributeManager.instance.currEquip != null && AttributeManager.instance.currEquip.Attributes.Count < AttributeManager.instance.currEquip.AttributeLimit)
            {
                AttributeManager.instance.currEquip.Attributes.Add(new Attribute(Attribute.Vitamin.C));
                AttributeManager.instance.currEquip.AttributeValues.Add(-1);
            }
        }

        public void Click_InvenVitaminIconD()
        {
            if (AttributeManager.instance.currEquip != null && AttributeManager.instance.currEquip.Attributes.Count < AttributeManager.instance.currEquip.AttributeLimit)
            {
                AttributeManager.instance.currEquip.Attributes.Add(new Attribute(Attribute.Vitamin.D));
                AttributeManager.instance.currEquip.AttributeValues.Add(-1);
            }
        }
    }
}