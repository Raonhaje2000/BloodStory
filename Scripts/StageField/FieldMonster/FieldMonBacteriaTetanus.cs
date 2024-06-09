using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    // �Ļ�ǳ�� (�Ķ���)
    public class FieldMonBacteriaTetanus : FieldMonsterBacteria
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
            targetPoint = GameObject.Find("TetanusTargetPoint").transform;

            miniMapIcon = GameObject.Find("Bacteria_Tetanus_MiniMapIcon").GetComponent<SpriteRenderer>();
            initColor = miniMapIcon.color;
            afraidColor = new Color(0, 0, 255);

            afraidMovePoints = new GameObject[] { GameObject.Find("TetanusMovePoint1"), GameObject.Find("TetanusMovePoint2"),
                                                  GameObject.Find("TetanusMovePoint3"), GameObject.Find("TetanusMovePoint4"),
                                                  GameObject.Find("TetanusMovePoint5"), GameObject.Find("TetanusMovePoint6"),
                                                  GameObject.Find("TetanusMovePoint7"), GameObject.Find("TetanusMovePoint8") };
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