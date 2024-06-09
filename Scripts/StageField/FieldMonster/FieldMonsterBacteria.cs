using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RSA
{
    public abstract class FieldMonsterBacteria : FieldMonster
    {
        // ������ ����
        protected enum State
        { 
            chase = 0, // ����
            afraid = 1 // �̸���
        }

        [SerializeField]
        protected State monsterState; // ���� ������ ����

        protected GameObject player; // �÷��̾�

        protected float afraidMoveSpeed; // �̸��� ������ ���� �̵��ӵ�
        protected bool isAfraid;         // ���� �̸��� �������� Ȯ���ϴ� �÷���

        [SerializeField]
        protected GameObject[] afraidMovePoints; // �̸��� ������ �� ��ȸ�ϴ�(���İ���) ����Ʈ��
        protected int afraidMovePointIndex;      // �̸��� ������ �� ��ȭ�ϴ�(���İ���) ����Ʈ�� �ε���

        public SpriteRenderer miniMapIcon; // ������ �̴ϸ� ������
        protected Color initColor;         // ������ �̴ϸ� ������ �ʱ� ��
        protected Color afraidColor;       // ������ �̴ϸ� ������ �̸��� ������ ��

        protected abstract void Load();       // ���� ������Ʈ �� ������Ʈ �ε�
        protected abstract void Initialize(); // �ʱ�ȭ

        protected abstract void UpdateMonsterState(); // ������ ���� ������Ʈ
        protected abstract void ChaseMonster();       // ���� ���� ������ ���� ó��
        protected abstract void AfraidMonster();      // ���� �̸��� ������ ���� ó��

        // ���� ���� ��ġ�κ��� ���� ����� ��ȸ ����Ʈ ã��
        protected int FindNearAfraidMovePointIndex()
        {
            // ���� ����� �Ÿ��� �� �ε����� �ʱⰪ�� �迭�� ù��° ������ ����
            float nearDistance = Vector3.Distance(gameObject.transform.position, afraidMovePoints[0].transform.position);
            int nearIndex = 0;

            // ���� ����� ��ȸ ����Ʈ ã��
            for (int i = 1; i < afraidMovePoints.Length; i++)
            {
                // ��ȸ ����Ʈ �� �Ÿ� ���
                float distance = Vector3.Distance(gameObject.transform.position, afraidMovePoints[i].transform.position);

                // ���� ���� ����� �Ÿ����� �� ����� ��� �� ����
                if (nearDistance > distance)
                {
                    nearDistance = distance;
                    nearIndex = i;
                }
            }

            // ���� ����� ��ȸ ����Ʈ �ε��� ��ȯ
            return nearIndex;
        }

        // �̸��� ������ �� ��ȸ ����Ʈ�� ���� ���� �̵�
        protected void AroundAfraidMovePoints()
        {
            // ���� ���� ��ġ�κ��� ���� ����� ��ȸ ����Ʈ�� �������� ����
            navMeshAgent.destination = afraidMovePoints[afraidMovePointIndex].transform.position;

            Vector3 dest = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            Vector3 obj = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

            // �������� ���� ������ �Ÿ�(y�� ���� �Ÿ�)�� 1 ������ ���
            if (Vector3.Distance(dest, obj) < 1.0f)
            {
                // ���� ��ȸ ����Ʈ�� �ε��� ����
                afraidMovePointIndex++;

                // ��ȸ ����Ʈ �ε����� ��ȸ ����Ʈ �迭�� ���̸� �Ѿ ��� 0���� �ʱ�ȭ (�ٽ� ��ȸ ����Ʈ ó������ ��ȸ)
                if (afraidMovePointIndex >= afraidMovePoints.Length) afraidMovePointIndex = 0;
            }
        }
    }
}