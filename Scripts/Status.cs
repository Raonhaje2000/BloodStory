using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "Status", menuName = "RSA/Status")]
    public class Status : ScriptableObject
    {
        [SerializeField] float hp;      // ü��
        [SerializeField] float attack;  // ���ݷ�
        [SerializeField] float defence; // ����
        [SerializeField] float speed;   // �̵��ӵ�

        public float Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public float Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public float Defence
        {
            get { return defence; }
            set { Attack = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // �߰��� �ɷ�ġ ����(����)
        public void AddStatus(float hp, float attack, float defence, float speed)
        {
            this.hp += hp;
            this.attack += attack;
            this.defence += defence;
            this.speed += speed;
        }

        // ����(����)�� �ɷ�ġ �ʱ�ȭ
        public void ResetStatus()
        {
            this.hp = 0;
            this.attack = 0;
            this.defence = 0;
            this.speed = 0;
        }

        // ����(����)�� �ɷ�ġ �ѹ��� �޾ƿ���
        public float[] GetStatusHpAttackDefenceSpeed()
        {
            float[] values = { hp, attack, defence, speed };

            return values;
        }
    }
}