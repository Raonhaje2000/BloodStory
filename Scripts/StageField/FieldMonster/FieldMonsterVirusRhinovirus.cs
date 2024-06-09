using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    public class FieldMonsterVirusRhinovirus : FieldMonster
    {
        [SerializeField]
        GameObject[] routePoints; // 이동 경로 포인트
        int routePointIndex;      // 이동 경로 포인트 인덱스

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
            AroundRoutePoints();
        }

        // 관련 컴포넌트 및 오브젝트 로드
        void Load()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            routePoints = new GameObject[] { GameObject.Find("RhinovirusMovePoint1"), GameObject.Find("RhinovirusMovePoint2"), GameObject.Find("RhinovirusMovePoint3"),
                                             GameObject.Find("RhinovirusMovePoint4"), GameObject.Find("RhinovirusMovePoint5"), GameObject.Find("RhinovirusMovePoint6"),
                                             GameObject.Find("RhinovirusMovePoint7"), GameObject.Find("RhinovirusMovePoint8"), GameObject.Find("RhinovirusMovePoint9"),
                                             GameObject.Find("RhinovirusMovePoint10"), GameObject.Find("RhinovirusMovePoint11"), GameObject.Find("RhinovirusMovePoint12"),
                                             GameObject.Find("RhinovirusMovePoint13"), GameObject.Find("RhinovirusMovePoint14"), GameObject.Find("RhinovirusMovePoint15"),
                                             GameObject.Find("RhinovirusMovePoint16"), GameObject.Find("RhinovirusMovePoint17"), GameObject.Find("RhinovirusMovePoint18")};


        }

        // 초기화
        void Initialize()
        {
            monsterLevel = GameManager.instance.PlayerLevel;

            moveSpeed = 3.0f + (0.1f * (monsterLevel - 1.0f));

            routePointIndex = FindNearRoutePointIndex();
            targetPoint = routePoints[routePointIndex].transform;

            navMeshAgent.destination = targetPoint.position;
            navMeshAgent.speed = moveSpeed;
        }

        // 현재 몬스터 위치로부터 가장 가까운 이동 경로 포인트 찾기
        int FindNearRoutePointIndex()
        {
            // 가장 가까운 거리와 그 인덱스의 초기값은 배열의 첫번째 값으로 지정
            float nearDistance = Vector3.Distance(gameObject.transform.position, routePoints[0].transform.position);
            int nearIndex = 0;

            // 가장 가까운 이동 경로 포인트 찾기
            for (int i = 1; i < routePoints.Length; i++)
            {
                // 이동 경로 포인트 별 거리 계산
                float distance = Vector3.Distance(gameObject.transform.position, routePoints[i].transform.position);

                // 현재 가장 가까운 거리보다 더 가까울 경우 값 갱신
                if (nearDistance > distance)
                {
                    nearDistance = distance;
                    nearIndex = i;
                }
            }

            // 가장 가까운 이동 경로 포인트 인덱스 반환
            return nearIndex;
        }

        // 이동 경로 포인트를 따라 몬스터 이동
        void AroundRoutePoints()
        {
            // 현재 몬스터 위치로부터 가장 가까운 경로 이동 포인트를 목적지로 지정
            navMeshAgent.destination = routePoints[routePointIndex].transform.position;

            Vector3 dest = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            Vector3 obj = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

            // 목적지와 몬스터 사이의 거리(y축 동일 거리)가 1 이하인 경우
            if (Vector3.Distance(dest, obj) < 1.0f)
            {
                // 다음 이동 경로 포인트로 인덱스 변경
                routePointIndex++;

                // 이동 경로 포인트 인덱스가 이동 경로 포인트 배열의 길이를 넘어설 경우 0으로 초기화 (다시 이동 경로 포인트 처음부터 이동)
                if (routePointIndex >= routePoints.Length) routePointIndex = 0;
            }
        }
    }
}