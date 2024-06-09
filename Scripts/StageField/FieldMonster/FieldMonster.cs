using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    public abstract class FieldMonster : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;

        protected float monsterLevel;    // ���� ����

        protected Transform targetPoint; // AI ���� ��ǥ ��ġ                                                

        protected float moveSpeed;       // �̵� �ӵ�
    }
}