using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    // 녹농균 (초록색)
    public class FieldMonBacteriaAeruginosa : FieldMonsterBacteria
    {
        private void Awake()
        {
            // 관련 컴포넌트 및 오브젝트 로드
            Load();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        void Update()
        {
            // 몬스터 상태 업데이트
            UpdateMonsterState();
        }

        // 관련 컴포넌트 및 오브젝트 로드
        protected override void Load()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            player = GameObject.FindWithTag("Player");
            targetPoint = GameObject.Find("AeruginosaTargetPoint").transform;

            miniMapIcon = GameObject.Find("Bacteria_Aeruginosa_MiniMapIcon").GetComponent<SpriteRenderer>();
            initColor = miniMapIcon.color;
            afraidColor = new Color(0, 0, 255);

            afraidMovePoints = new GameObject[] { GameObject.Find("AeruginosaMovePoint1"),
                                                  GameObject.Find("AeruginosaMovePoint2"),
                                                  GameObject.Find("AeruginosaMovePoint3"),
                                                  GameObject.Find("AeruginosaMovePoint4") };
        }

        // 초기화
        protected override void Initialize()
        {
            monsterLevel = GameManager.instance.PlayerLevel;

            monsterState = State.chase;

            moveSpeed = 3.0f + (0.1f * (monsterLevel - 1.0f));
            afraidMoveSpeed = moveSpeed / 2.0f;

            isAfraid = false;

            navMeshAgent.destination = targetPoint.position;
            navMeshAgent.speed = moveSpeed;
            miniMapIcon.color = initColor;
        }

        // 몬스터의 상태 업데이트
        protected override void UpdateMonsterState()
        {
            // 플레이어의 상태가 백혈구 호출 상태이면 겁먹음 상태 플래그 ture, 아닌 경우 false
            isAfraid = (player.GetComponent<PlayerRed>().playerRedState == PlayerRed.State.WhiteCall) ? true : false;

            // 몬스터의 상태
            switch (monsterState)
            {
                case State.chase:
                    {
                        ChaseMonster(); // 추적 상태일 때의 처리
                        break;
                    }
                case State.afraid:
                    {
                        AfraidMonster(); // 겁먹음 상태일 때의 처리
                        break;
                    }
            }
        }

        // 몬스터 추적 상태일 때의 처리
        protected override void ChaseMonster()
        {
            // 몬스터의 속도를 기본 이동 속도로 바꾸고 미니맵 아이콘 색을 초기 색으로 변경
            navMeshAgent.speed = moveSpeed;
            miniMapIcon.color = initColor;

            // 플레이어 추적
            navMeshAgent.destination = targetPoint.position;

            if (isAfraid)
            {
                // 몬스터가 겁먹음 상태인 경우, 현재 몬스터 위치로부터 가장 가까운 배회포인트 찾기                                 
                // 몬스터의 상태를 겁먹음 상태로 전이
                afraidMovePointIndex = FindNearAfraidMovePointIndex();
                monsterState = State.afraid;
            }
        }

        // 몬스터 겁먹음 상태일 때의 처리
        protected override void AfraidMonster()
        {
            // 몬스터의 속도를 겁먹음 이동 속도로 바꾸고 미니맵 아이콘 색을 겁먹음 색으로 변경                                            
            navMeshAgent.speed = afraidMoveSpeed;
            miniMapIcon.color = afraidColor;

            // 배회 포인트를 따라 몬스터 이동
            AroundAfraidMovePoints();

            // 몬스터가 겁먹음 상태가 아닌 경우 추적 상태로 전이
            if (!isAfraid) monsterState = State.chase;
        }
    }
}