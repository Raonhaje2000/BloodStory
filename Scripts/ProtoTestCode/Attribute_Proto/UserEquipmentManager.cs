using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserEquipmentManager : MonoBehaviour
{
    Equipment sword;
    Equipment helmet;
    Equipment armor;
    Equipment pants;
    Equipment boot;

    void Start()
    {
        sword = new Equipment(Equipment.Part.sword, Resources.Load<Sprite>("RSA/Item/Equipment/Item_Icon_Sword"), "강철분검", 3, 0, 10 + 4, 0, 0);
        helmet = new Equipment(Equipment.Part.helmet, Resources.Load<Sprite>("RSA/Item/Equipment/Item_Icon_Helmet"), "혈소판금투구", 0, 20, 0, 2, 0);
        armor = new Equipment(Equipment.Part.armor, Resources.Load<Sprite>("RSA/Item/Equipment/Item_Icon_Armor"), "혈소판금상의", 0, 20, 0, 2, 0);
        pants = new Equipment(Equipment.Part.pants, Resources.Load<Sprite>("RSA/Item/Equipment/Item_Icon_Pants"), "혈소판금하의", 0, 20, 0, 2, 0);
        boot = new Equipment(Equipment.Part.boot, Resources.Load<Sprite>("RSA/Item/Equipment/Item_Icon_Boot"), "혈소판금신발", 0, 10, 0, 2, 10);
    }

    public void Click_UserSword()
    {
        AttributeManager.instance.currEquip = sword;

        AttributeUIManager.instance.SetActiveEquip(true);
        AttributeUIManager.instance.ChangeAttributeEquip(sword);
    }

    public void Click_UserHelmet()
    {
        AttributeManager.instance.currEquip = helmet;

        AttributeUIManager.instance.SetActiveEquip(true);
        AttributeUIManager.instance.ChangeAttributeEquip(helmet);
    }

    public void Click_UserArmor()
    {
        AttributeManager.instance.currEquip = armor;

        AttributeUIManager.instance.SetActiveEquip(true);
        AttributeUIManager.instance.ChangeAttributeEquip(armor);
    }

    public void Click_UserPants()
    {
        AttributeManager.instance.currEquip = pants;

        AttributeUIManager.instance.SetActiveEquip(true);
        AttributeUIManager.instance.ChangeAttributeEquip(pants);
    }

    public void Click_UserBoot()
    {
        AttributeManager.instance.currEquip = boot;

        AttributeUIManager.instance.SetActiveEquip(true);
        AttributeUIManager.instance.ChangeAttributeEquip(boot);
    }
}
