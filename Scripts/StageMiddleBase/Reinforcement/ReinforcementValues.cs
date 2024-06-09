using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RSA
{
    [CreateAssetMenu(fileName = "ReinforcementValues", menuName = "RSA/ReinforcementValues")]
    public class ReinforcementValues : ScriptableObject
    {
        [SerializeField] float[] hp;           // ��޺� ü�� ���� ��ġ
        [SerializeField] float[] attack;       // ��޺� ���ݷ� ���� ��ġ
        [SerializeField] float[] defence;      // ��޺� ���� ���� ��ġ
        [SerializeField] float[] speed;        // ��޺� �̵��ӵ� ���� ��ġ

        [SerializeField] int[] oxygen;         // ��޺� ��ȭ�ϴµ� �ʿ��� ��� ��
        [SerializeField] int[] iron;           // ��޺� ��ȭ�ϴµ� �ʿ��� ö�� ��
        [SerializeField] float[] probability;  // ��޺� ��ȭ ���� Ȯ��

        [SerializeField] int[] mineral;        // ��޺� ��ȭ�ϴµ� �ʿ��� �̳׶� ��

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