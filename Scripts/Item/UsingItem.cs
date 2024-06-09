using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public abstract class UsingItem : Item
    {
        [SerializeField] protected int invenMaxCount;         // �κ��丮 ���Կ����� �ִ� ���� ���� (�ش� ������ ���� ��� ���ο� ���� ����)
        [SerializeField] protected int invenCurrentCount = 0; // �κ��丮 ���Կ����� ���� ���� ���� 

        public int InvenMaxCount
        { 
            get { return invenMaxCount; } 
        }

        public int InvenCurrentCount
        {
            get { return invenCurrentCount; }
            set { invenCurrentCount = value; }
        }
    }
}
