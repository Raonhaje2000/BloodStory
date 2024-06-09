using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class PlayerRedAnimation : MonoBehaviour
    {                                                                                                    
        Animator animator;

        private void Awake()
        {
            // ������Ʈ �ҷ�����
            LoadComponent();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ������Ʈ �ҷ�����
        void LoadComponent()
        {
            animator = GetComponent<Animator>();                                                                       
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // �ȱ� �ִϸ��̼����� ����
            SetWalkAnimation();
        }

        // �ȱ� �ִϸ��̼����� ����
        public void SetWalkAnimation()
        {
            animator.SetBool("walk", true);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", false);                                                         
            animator.SetBool("die", false);
        }

        // �޸��� �ִϸ��̼����� ����
        public void SetRunAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
            animator.SetBool("whiteCall", false);
            animator.SetBool("die", false);
        }

        // ������ ȣ�� �ִϸ��̼����� ����
        public void SetWhiteCallAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", true);
            animator.SetBool("die", false);
        }

        // ���� �ִϸ��̼����� ����
        public void SetDieAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", false);
            animator.SetBool("die", true);
        }
    }
}