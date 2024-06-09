using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    public class FieldMonsterVirusRhinovirus : FieldMonster
    {
        [SerializeField]
        GameObject[] routePoints; // �̵� ��� ����Ʈ
        int routePointIndex;      // �̵� ��� ����Ʈ �ε���

        private void Awake()
        {
            // ���� ������Ʈ �� ������Ʈ �ε�
            Load();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        void Update()
        {
            // ���� ���� ������Ʈ
            AroundRoutePoints();
        }

        // ���� ������Ʈ �� ������Ʈ �ε�
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

        // �ʱ�ȭ
        void Initialize()
        {
            monsterLevel = GameManager.instance.PlayerLevel;

            moveSpeed = 3.0f + (0.1f * (monsterLevel - 1.0f));

            routePointIndex = FindNearRoutePointIndex();
            targetPoint = routePoints[routePointIndex].transform;

            navMeshAgent.destination = targetPoint.position;
            navMeshAgent.speed = moveSpeed;
        }

        // ���� ���� ��ġ�κ��� ���� ����� �̵� ��� ����Ʈ ã��
        int FindNearRoutePointIndex()
        {
            // ���� ����� �Ÿ��� �� �ε����� �ʱⰪ�� �迭�� ù��° ������ ����
            float nearDistance = Vector3.Distance(gameObject.transform.position, routePoints[0].transform.position);
            int nearIndex = 0;

            // ���� ����� �̵� ��� ����Ʈ ã��
            for (int i = 1; i < routePoints.Length; i++)
            {
                // �̵� ��� ����Ʈ �� �Ÿ� ���
                float distance = Vector3.Distance(gameObject.transform.position, routePoints[i].transform.position);

                // ���� ���� ����� �Ÿ����� �� ����� ��� �� ����
                if (nearDistance > distance)
                {
                    nearDistance = distance;
                    nearIndex = i;
                }
            }

            // ���� ����� �̵� ��� ����Ʈ �ε��� ��ȯ
            return nearIndex;
        }

        // �̵� ��� ����Ʈ�� ���� ���� �̵�
        void AroundRoutePoints()
        {
            // ���� ���� ��ġ�κ��� ���� ����� ��� �̵� ����Ʈ�� �������� ����
            navMeshAgent.destination = routePoints[routePointIndex].transform.position;

            Vector3 dest = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            Vector3 obj = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

            // �������� ���� ������ �Ÿ�(y�� ���� �Ÿ�)�� 1 ������ ���
            if (Vector3.Distance(dest, obj) < 1.0f)
            {
                // ���� �̵� ��� ����Ʈ�� �ε��� ����
                routePointIndex++;

                // �̵� ��� ����Ʈ �ε����� �̵� ��� ����Ʈ �迭�� ���̸� �Ѿ ��� 0���� �ʱ�ȭ (�ٽ� �̵� ��� ����Ʈ ó������ �̵�)
                if (routePointIndex >= routePoints.Length) routePointIndex = 0;
            }
        }
    }
}