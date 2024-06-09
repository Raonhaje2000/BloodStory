using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    public abstract class FieldMonster : MonoBehaviour
    {
        protected NavMeshAgent navMeshAgent;

        protected float monsterLevel;    // 몬스터 레벨

        protected Transform targetPoint; // AI 추적 목표 위치                                                

        protected float moveSpeed;       // 이동 속도
    }
}