using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public abstract class FieldMonsterBacteria : FieldMonster
    {
        // 몬스터의 상태
        protected enum State
        { 
            chase = 0, // 추적
            afraid = 1 // 겁먹음
        }

        [SerializeField]
        protected State monsterState; // 현재 몬스터의 상태

        protected GameObject player; // 플레이어

        protected float afraidMoveSpeed; // 겁먹음 상태일 때의 이동속도
        protected bool isAfraid;         // 현재 겁먹음 상태인지 확인하는 플래그

        [SerializeField]
        protected GameObject[] afraidMovePoints; // 겁먹음 상태일 때 배회하는(거쳐가는) 포인트들
        protected int afraidMovePointIndex;      // 겁먹음 상태일 때 배화하는(거쳐가는) 포인트의 인덱스

        public SpriteRenderer miniMapIcon; // 몬스터의 미니맵 아이콘
        protected Color initColor;         // 몬스터의 미니맵 아이콘 초기 색
        protected Color afraidColor;       // 몬스터의 미니맵 아이콘 겁먹음 상태의 색

        protected abstract void Load();       // 관련 컴포넌트 및 오브젝트 로드
        protected abstract void Initialize(); // 초기화

        protected abstract void UpdateMonsterState(); // 몬스터의 상태 업데이트
        protected abstract void ChaseMonster();       // 몬스터 추적 상태일 때의 처리
        protected abstract void AfraidMonster();      // 몬스터 겁먹음 상태일 때의 처리

        // 현재 몬스터 위치로부터 가장 가까운 배회 포인트 찾기
        protected int FindNearAfraidMovePointIndex()
        {
            // 가장 가까운 거리와 그 인덱스의 초기값은 배열의 첫번째 값으로 지정
            float nearDistance = Vector3.Distance(gameObject.transform.position, afraidMovePoints[0].transform.position);
            int nearIndex = 0;

            // 가장 가까운 배회 포인트 찾기
            for (int i = 1; i < afraidMovePoints.Length; i++)
            {
                // 배회 포인트 별 거리 계산
                float distance = Vector3.Distance(gameObject.transform.position, afraidMovePoints[i].transform.position);

                // 현재 가장 가까운 거리보다 더 가까울 경우 값 갱신
                if (nearDistance > distance)
                {
                    nearDistance = distance;
                    nearIndex = i;
                }
            }

            // 가장 가까운 배회 포인트 인덱스 반환
            return nearIndex;
        }

        // 겁먹음 상태일 때 배회 포인트를 따라 몬스터 이동
        protected void AroundAfraidMovePoints()
        {
            // 현재 몬스터 위치로부터 가장 가까운 배회 포인트를 목적지로 지정
            navMeshAgent.destination = afraidMovePoints[afraidMovePointIndex].transform.position;

            Vector3 dest = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            Vector3 obj = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

            // 목적지와 몬스터 사이의 거리(y축 동일 거리)가 1 이하인 경우
            if (Vector3.Distance(dest, obj) < 1.0f)
            {
                // 다음 배회 포인트로 인덱스 변경
                afraidMovePointIndex++;

                // 배회 포인트 인덱스가 배회 포인트 배열의 길이를 넘어설 경우 0으로 초기화 (다시 배회 포인트 처음부터 배회)
                if (afraidMovePointIndex >= afraidMovePoints.Length) afraidMovePointIndex = 0;
            }
        }
    }
}