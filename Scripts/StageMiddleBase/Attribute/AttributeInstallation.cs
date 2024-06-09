using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AttributeInstallation", menuName = "RSA/AttributeInstallation")]
    public class AttributeInstallation : ScriptableObject
    {
        // �Ӽ� �ο� Ÿ��
        public enum Type { hp = 0, attack = 1, defence = 2, speed = 3 }

        [SerializeField] Type attributeType;    // �Ӽ� �ο� Ÿ��

        [SerializeField] int[] vitaminsQuality; // �Ӽ� �ο� ������ ��Ÿ���� ǰ�� ����
        [SerializeField] int[] vitaminsValue;   // �Ӽ� �ο� ������ ��Ÿ�ο� ���� �ο��� �Ӽ���

        public Type AttributeType
        {
            get { return attributeType; }
        }

        public int[] VitaminsQuality
        {
            get { return vitaminsQuality; }
            set { vitaminsQuality = value; }
        }

        public int[] VitaminsValue
        { 
            get { return vitaminsValue; }
            set { vitaminsValue = value; }
        }
    }
}
