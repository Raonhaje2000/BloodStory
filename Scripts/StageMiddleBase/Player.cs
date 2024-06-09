using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class Player : MonoBehaviour
    {
        // �÷��̾��� ����
        enum State
        { 
            idle = 0, // �⺻
            move = 1  // ������
        }

        State playerState; // ���� �÷��̾��� ����

        Animator animator; // �ִϸ��̼� ������Ʈ

        private void Awake()
        {
            // ������Ʈ �ҷ�����
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        void Update()
        {
            // �÷��̾��� ���� ������Ʈ
            switch (playerState)
            {
                case State.idle:
                    {
                        UpdateInit(); // �⺻ ������ ���� ó��
                        break;
                    }
                case State.move:
                    {
                        UpdateMove(); // ������ ������ ���� ó��
                        break;
                    }
            }

        }

        // �ʱ�ȭ
        void Initialize()
        {
            playerState = State.idle;
        }

        // �⺻ ������ ���� ó��
        void UpdateInit()
        {       
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // �̵�Ű(����Ű �Ǵ� W, S, A, DŰ) �Է��� ���� ���
                // �ִϸ��̼ǰ� �÷��̾� ���¸� ������ ���·� ����
                SetMoveAnimation();
                playerState = State.move;
            }
            else
            {
                // �̵�Ű �Է��� ���� ���
                // �⺻ ���� ����
                playerState = State.idle;
            }
        }

        // ������ ������ ���� ó��
        void UpdateMove()
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                // �̵�Ű �Է��� ���� ���
                // ������ ���� ����
                playerState = State.move;
            }
            else
            {
                // �̵�Ű �Է��� ���� ���
                // �ִϸ��̼ǰ� �÷��̾� ���¸� �⺻ ���·� ����
                SetIdleAnimation();
                playerState = State.idle;
            }
        }

        // �⺻ �ִϸ��̼����� ����
        void SetIdleAnimation()
        {
            animator.SetBool("idle", true);
            animator.SetBool("walk", false);
        }

        // ������ �ִϸ��̼����� ����
        void SetMoveAnimation()
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", true);
        }

        // �÷��̾� �浹 ó��
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                // �浹�ϰ� �ִ� ���¿��� GŰ�� ������ ���
                if(other.tag == "Reinforcement")
                {
                    // ��ȭ NPC ���� ���� ���� ��, ��ȭ UI Ȱ��ȭ
                    MiddleBaseUIManager.instance.ActivReinforcementUI();
                }
                else if(other.tag == "Production")
                {
                    // ���� NPC ���� ���� ���� ��, ���� UI Ȱ��ȭ
                    MiddleBaseUIManager.instance.ActiveProductionUI();
                }
                else if(other.tag == "Shop")
                {
                    // ���� NPC ���� ���� ���� ��, ���� UI Ȱ��ȭ
                    MiddleBaseUIManager.instance.ActiveShopUI();
                }
            }
        }
    }
}