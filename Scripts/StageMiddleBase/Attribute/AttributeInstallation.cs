using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "AttributeInstallation", menuName = "RSA/AttributeInstallation")]
    public class AttributeInstallation : ScriptableObject
    {
        // 속성 부여 타입
        public enum Type { hp = 0, attack = 1, defence = 2, speed = 3 }

        [SerializeField] Type attributeType;    // 속성 부여 타입

        [SerializeField] int[] vitaminsQuality; // 속성 부여 아이템 비타민의 품질 상태
        [SerializeField] int[] vitaminsValue;   // 속성 부여 아이템 비타민에 의해 부여된 속성값

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
