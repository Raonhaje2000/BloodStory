using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSA
{
    public class PlayerRed : MonoBehaviour
    {
        // �÷��̾��� ����
        public enum State
        {
            Walk = 0,       // �ȱ�
            Run = 1,        // �޸���
            WhiteCall = 2,  // ������ ȣ��
            Die = 3         // ����
        }

        public State playerRedState; // ���� �÷��̾��� ����

        float whiteCallTime; // ������ ȣ�� ���� �ð�

        bool isWhiteCall; // ������ ȣ�� �������� ���� (������ ȣ�� �������� �Ծ�����)
        bool isShield;    // ���� ����

        bool isUnbeatable; // ���� ���� �������� ����

        public float WhiteCallTime
        {
            get { return whiteCallTime; }
        }

        public bool IsUnbeatable
        {
            get { return isUnbeatable; }
            set { isUnbeatable = value; }
        }

        private void Awake()
        {
            Initialized(); // �ʱ�ȭ
        }

        void Start()
        {
            //Initialized();
        }

        void Update()
        {
            UpdatePlayerRedState(); // �÷��̾��� ���� ������Ʈ
        }

        // �ʱ�ȭ
        void Initialized()
        {
            playerRedState = State.Walk;

            whiteCallTime = 15.0f;

            isWhiteCall = false;
            isShield = false;

            isUnbeatable = false;
        }

        // �÷��̾��� ���� ������Ʈ
        void UpdatePlayerRedState()                                         
        {
            switch (playerRedState)
            {
                case State.Walk:
                    {
                        WalkPlayerRed(); // �ȱ� ������ ���� ó��
                        break;
                    }
                case State.Run:
                    {
                        RunPlayerRed(); // �޸��� ������ ���� ó��
                        break;
                    }
                case State.WhiteCall:
                    {
                        WhiteCallPlayerRed(); // ������ ȣ�� ������ ���� ó��
                        break;
                    }
                case State.Die:
                    {
                        DiePlayerRed(); // ���� ������ ���� ó��
                        break;
                    }
            }
        }

        // �ȱ� ������ ���� ó��
        void WalkPlayerRed()
        {
            if (isWhiteCall)
            {
                // ������ ȣ�� �������� �Ծ� ������ ȣ�� ���°� �� ���
                // �ִϸ��̼ǰ� �÷��̾� ���¸� ������ ȣ�� ���·� ����
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else
            {
                if (Input.GetAxis("Vertical") == 0)
                {
                    // �̵�Ű �Է��� ���� ���
                    // �ȱ� ���� ����
                    playerRedState = State.Walk;
                }
                else
                {
                    // �̵�Ű �Է��� ���� ���
                    // �ִϸ��̼ǰ� �÷��̾� ���¸� �޸��� ���·� ����
                    GetComponent<PlayerRedAnimation>().SetRunAnimation();
                    playerRedState = State.Run;
                }
            }
        }

        void RunPlayerRed()
        {
            if (isWhiteCall)
            {
                // ������ ȣ�� �������� �Ծ� ������ ȣ�� ���°� �� ���
                // �ִϸ��̼ǰ� �÷��̾� ���¸� ������ ȣ�� ���·� ����
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    // �̵�Ű �Է��� ���� ���
                    // �޸��� ���� ����
                    playerRedState = State.Run;
                }
                else
                {
                    // �̵�Ű �Է��� ���� ���
                    // �ִϸ��̼ǰ� �÷��̾� ���¸� �ȱ� ���·� ����
                    GetComponent<PlayerRedAnimation>().SetWalkAnimation();
                    playerRedState = State.Walk;
                }
            }
        }

        // ������ ȣ�� ������ ���� ó��
        void WhiteCallPlayerRed()
        {
            // ������ ȣ�� ���� �ð��� ���� �� ���� ������ ó��                                                         
            Invoke("FinishWhilteCall", whiteCallTime);
        }

        // ���� ������ ���� ó��
        void DiePlayerRed()
        {
            // �÷��̾��� �̵��� ����
            GetComponent<PlayerRedController>().PlayerMoveStop();

            Debug.Log("�÷��̾� ���");
        }

        void FinishWhilteCall()
        {
            // �����Ǿ��� �÷��̾��� �̵� �ӵ��� ������� ����
            GetComponent<PlayerRedController>().ChangePlayerMoveSpeedInit();                                          

            CancelInvoke("FinishWhilteCall");
            isWhiteCall = false;

            // �ִϸ��̼ǰ� �÷��̾� ���¸� �ȱ� ���·� ����
            GetComponent<PlayerRedAnimation>().SetWalkAnimation();
            playerRedState = State.Walk;
        }

        // ���� Ȱ��ȭ ����
        public void SetShieldActive(bool active)
        {
            isShield = active;
        }

        // �÷��̾� �浹 ���� ó��
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Oxygen")
            {
                // ��� �����۰� �浹�� ���
                // �ش� ��� ������ ��Ȱ��ȭ �� ��� ȹ�� ó��
                other.gameObject.SetActive(false);

                FieldGameManager.instance.GetOxygen();
            }
            else if(other.gameObject.tag == "Reset")
            {
                // ���� �����۰� �浹�� ���
                // �ش� ���� ������ ��Ȱ��ȭ �� �ʵ� ������ ���� ó��
                other.gameObject.SetActive(false);

                FieldGenManager.instance.GenerateFieldItems();
            }
            else if(other.gameObject.tag == "WhiteCall")
            {
                // ������ ȣ�� �����۰� �浹�� ���
                // �ش� ������ ȣ�� ������ ��Ȱ��ȭ ��, �÷��̾� �̵��ӵ� 20% ����
                other.gameObject.SetActive(false);

                GetComponent<PlayerRedController>().ChangePlayerMoveSpeed(20.0f);
                isWhiteCall = true;

                // �ִϸ��̼ǰ� �÷��̾� ���¸� ������ ȣ�� ���·� ����
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else if(other.gameObject.tag == "Monster" || other.gameObject.tag == "MonsterVirus")
            {
                // �ʵ� ���Ϳ� �浹�� ���

                if((playerRedState == State.WhiteCall || isShield) && GameManager.instance.DurabilityWeapon > 0)
                {
                    // ���� ������ ȣ�� �����̰ų�, ���尡 �ִ� ���¿��� ���� �������� �����ִ� ���

                    if (other.gameObject.tag == "MonsterVirus")
                    {
                        // ���̷����� ������ ��� �ش� ���� ���� �� ���� ���� �� ����
                        Destroy(other.gameObject);
                        GameObject.Find("FieldMonsterGenManager").GetComponent<FieldMonsterGenManager>().KillVirus();
                    }
                    else
                    {
                        // ������ ������ ��� �ش� ���� ��Ȱ��ȭ
                        other.gameObject.SetActive(false);
                    }

                    GameManager.instance.PlayerAddExp(50.0f);  // �÷��̾� ����ġ 50 ȹ��
                    FieldGameManager.instance.KillMonster();   // ���� óġ���� ���� ó��
                    DurabilityManager.instance.UseWeapon();    // ���� ������ ����
                }
                else
                {
                    // ���� ������ ȣ�� ����, ���尡 �ִ� ���°� �ƴϰų� ���� �������� ���� ���

                    if (!isUnbeatable) // ���� ���°� �ƴ� ����
                    {
                        // �ִϸ��̼ǰ� �÷��̾� ���¸� ���� ���·� ����
                        GetComponent<PlayerRedAnimation>().SetDieAnimation();
                        playerRedState = State.Die;
                    }
                }
            }
            else if(other.gameObject.tag == "MiddlePotal")
            {
                // �߾� ��Ż�� �⵹�� ���

                // ������ ������ �Ѿ
                GameManager.instance.gameState = GameManager.State.bossStage;
                SceneManager.LoadScene("Boss");
            }
        }
    }
}