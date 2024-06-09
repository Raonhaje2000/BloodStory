using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA_Proto
{
    public class Attribute : MonoBehaviour
    {
        public enum Vitamin { A = 0, B = 1, C = 2, D = 3 };
        // º”º∫ æ∆¿Ã≈€ ¡§∫∏
        Vitamin vita;

        Sprite icon;
        string name;

        int[] attriValue;

        public Attribute(Vitamin vita)
        {
            switch (vita)
            {
                case Vitamin.A:
                    {
                        icon = Resources.Load<Sprite>("RSA/Item/Attribute/Item_Icon_VitaminA");
                        name = "∫Ò≈∏πŒ A";
                        attriValue = new int[] { 17, 18, 19, 20 };

                        break;
                    }
                case Vitamin.B:
                    {
                        icon = Resources.Load<Sprite>("RSA/Item/Attribute/Item_Icon_VitaminB");
                        name = "∫Ò≈∏πŒ B";
                        attriValue = new int[] { 7, 8, 9, 10 };

                        break;
                    }
                case Vitamin.C:
                    {
                        icon = Resources.Load<Sprite>("RSA/Item/Attribute/Item_Icon_VitaminC");
                        name = "∫Ò≈∏πŒ C";
                        attriValue = new int[] { 4, 5, 6, 7 };

                        break;
                    }
                case Vitamin.D:
                    {
                        icon = Resources.Load<Sprite>("RSA/Item/Attribute/Item_Icon_VitaminD");
                        name = "∫Ò≈∏πŒ D";
                        attriValue = new int[] { 1, 2, 3, 4 };

                        break;
                    }
            }
        }

        public Vitamin Vita
        {
            get { return vita; }
            set { vita = value; }
        }

        public Sprite Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int[] AttriValue
        {
            get { return attriValue; }
            set { attriValue = value; }
        }
    }
}
