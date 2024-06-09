using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RSA
{
    // Ȳ�������󱸱� (�����)
    public class FieldMonBacteriaAureus : FieldMonsterBacteria
    {
        GameObject monster; // ���� ������Ʈ

        bool isNearPlayer;  // �÷��̾�� ���� �Ÿ� ���Ϸ� ������� Ȯ���ϴ� �÷���

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
            targetPoint = GameObject.Find("AureusTargetPoint").transform;

            monster = this.gameObject;
            isNearPlayer = false;

            miniMapIcon = GameObject.Find("Bacteria_S_Aureus_MiniMapIcon").GetComponent<SpriteRenderer>();
            initColor = miniMapIcon.color;
            afraidColor = new Color(0, 0, 255);

            afraidMovePoints = new GameObject[] { GameObject.Find("AureusMovePoint1"), GameObject.Find("AureusMovePoint2"),
                                                  GameObject.Find("AureusMovePoint3"), GameObject.Find("AureusMovePoint4"), 
                                                  GameObject.Find("AureusMovePoint5"), GameObject.Find("AureusMovePoint6"), 
                                                  GameObject.Find("AureusMovePoint7"), GameObject.Find("AureusMovePoint8") };
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

            Vector3 m = new Vector3(monster.transform.position.x, 0, monster.transform.position.z);
            Vector3 p = new Vector3(player.transform.position.x, 0, player.transform.position.z);

            if (Vector3.Distance(m, p) > 8.0f)
            {
                // ���Ϳ� �÷��̾� ������ �Ÿ�(y�� ���� �Ÿ�)�� 8���� ū ���
                // �÷��̾� ����
                navMeshAgent.destination = targetPoint.position;

                isNearPlayer = false;
            }
            else
            {
                // ���Ϳ� �÷��̾� ������ �Ÿ�(y�� ���� �Ÿ�)�� 8 ������ ���
                // �÷��̾�� ������ �ٰ����� ���� ó��
                ApproachPlayer();
            }

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

        // �÷��̾�� ������ �ٰ����� ���� ó��
        void ApproachPlayer()
        {
            if (!isNearPlayer)
            {
                // �÷��̾�� ����� ���� üũ�� �ȵǾ��µ� �÷��̾�� ������ �ٰ��� ���
                // ���� ���� ��ġ�κ��� ���� ����� ��ȸ����Ʈ ã�� ����� ���� üũ true�� ����
                afraidMovePointIndex = FindNearAfraidMovePointIndex();
                isNearPlayer = true;
            }
            else
            {
                // �÷��̾�� ����� ���� üũ�� �� ���¿��� �÷��̾�� ������ �ٰ��� ���
                // ��ȸ ����Ʈ�� ���� ���� �̵�
                AroundAfraidMovePoints();
            }
        }
    }
}