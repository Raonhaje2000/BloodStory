using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    // ��ű��� (������)
    public class FieldMonBacteriaPneumoniae : FieldMonsterBacteria
    {
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
            UpdateMonsterState();
        }

        // ���� ������Ʈ �� ������Ʈ �ε�
        protected override void Load()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();

            player = GameObject.FindWithTag("Player");
            targetPoint = GameObject.Find("PneumoniaeTargetPoint").transform;

            miniMapIcon = GameObject.Find("Bacteria_Pneumoniae_MiniMapIcon").GetComponent<SpriteRenderer>();
            initColor = miniMapIcon.color;
            afraidColor = new Color(0, 0, 255);

            afraidMovePoints = new GameObject[] { GameObject.Find("PneumoniaeMovePoint1"),
                                                  GameObject.Find("PneumoniaeMovePoint2"),
                                                  GameObject.Find("PneumoniaeMovePoint3"),
                                                  GameObject.Find("PneumoniaeMovePoint4") };
        }

        // �ʱ�ȭ
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

        // ������ ���� ������Ʈ
        protected override void UpdateMonsterState()
        {
            // �÷��̾��� ���°� ������ ȣ�� �����̸� �̸��� ���� �÷��� ture, �ƴ� ��� false
            isAfraid = (player.GetComponent<PlayerRed>().playerRedState == PlayerRed.State.WhiteCall) ? true : false;

            // ������ ����
            switch (monsterState)
            {
                case State.chase:
                    {
                        ChaseMonster(); // ���� ������ ���� ó��
                        break;
                    }
                case State.afraid:
                    {
                        AfraidMonster(); // �̸��� ������ ���� ó��
                        break;
                    }
            }
        }

        // ���� ���� ������ ���� ó��
        protected override void ChaseMonster()
        {
            // ������ �ӵ��� �⺻ �̵� �ӵ��� �ٲٰ� �̴ϸ� ������ ���� �ʱ� ������ ����
            navMeshAgent.speed = moveSpeed;
            miniMapIcon.color = initColor;

            // �÷��̾� ����
            navMeshAgent.destination = targetPoint.position;

            if (isAfraid)
            {
                // ���Ͱ� �̸��� ������ ���, ���� ���� ��ġ�κ��� ���� ����� ��ȸ����Ʈ ã��
                // ������ ���¸� �̸��� ���·� ����
                afraidMovePointIndex = FindNearAfraidMovePointIndex();
                monsterState = State.afraid;
            }
        }

        // ���� �̸��� ������ ���� ó��
        protected override void AfraidMonster()
        {
            // ������ �ӵ��� �̸��� �̵� �ӵ��� �ٲٰ� �̴ϸ� ������ ���� �̸��� ������ ����
            navMeshAgent.speed = afraidMoveSpeed;
            miniMapIcon.color = afraidColor;

            // ��ȸ ����Ʈ�� ���� ���� �̵�
            AroundAfraidMovePoints();

            // ���Ͱ� �̸��� ���°� �ƴ� ��� ���� ���·� ����
            if (!isAfraid) monsterState = State.chase;
        }
    }
}
