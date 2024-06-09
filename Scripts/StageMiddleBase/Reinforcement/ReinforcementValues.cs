using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RSA
{
    [CreateAssetMenu(fileName = "ReinforcementValues", menuName = "RSA/ReinforcementValues")]
    public class ReinforcementValues : ScriptableObject
    {
        [SerializeField] float[] hp;           // 등급별 체력 증가 수치
        [SerializeField] float[] attack;       // 등급별 공격력 증가 수치
        [SerializeField] float[] defence;      // 등급별 방어력 증가 수치
        [SerializeField] float[] speed;        // 등급별 이동속도 증가 수치

        [SerializeField] int[] oxygen;         // 등급별 강화하는데 필요한 산소 수
        [SerializeField] int[] iron;           // 등급별 강화하는데 필요한 철분 수
        [SerializeField] float[] probability;  // 등급별 강화 성공 확률

        [SerializeField] int[] mineral;        // 등급별 강화하는데 필요한 미네랄 수

        public float[] Hp
        {
            get { return hp; }
        }

        public float[] Attack
        {
            get { return attack; }
        }

        public float[] Defence
        {
            get { return defence; }
        }

        public float[] Speed
        {
            get { return speed; }
        }

        public int[] Oxygen
        {
            get { return oxygen; }
        }

        public int[] Iron
        {
            get { return iron; }
        }

        public float[] Probability
        {
            get { return probability; }
        }

        public int[] Mineral
        { 
            get { return mineral; }
        }
    }
}