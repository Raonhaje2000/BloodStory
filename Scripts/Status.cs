using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    [CreateAssetMenu(fileName = "Status", menuName = "RSA/Status")]
    public class Status : ScriptableObject
    {
        [SerializeField] float hp;      // 체력
        [SerializeField] float attack;  // 공격력
        [SerializeField] float defence; // 방어력
        [SerializeField] float speed;   // 이동속도

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

        // 추가된 능력치 적용(저장)
        public void AddStatus(float hp, float attack, float defence, float speed)
        {
            this.hp += hp;
            this.attack += attack;
            this.defence += defence;
            this.speed += speed;
        }

        // 적용(저장)된 능력치 초기화
        public void ResetStatus()
        {
            this.hp = 0;
            this.attack = 0;
            this.defence = 0;
            this.speed = 0;
        }

        // 적용(저장)된 능력치 한번에 받아오기
        public float[] GetStatusHpAttackDefenceSpeed()
        {
            float[] values = { hp, attack, defence, speed };

            return values;
        }
    }
}