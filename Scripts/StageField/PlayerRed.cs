using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSA
{
    public class PlayerRed : MonoBehaviour
    {
        // 플레이어의 상태
        public enum State
        {
            Walk = 0,       // 걷기
            Run = 1,        // 달리기
            WhiteCall = 2,  // 백혈구 호출
            Die = 3         // 죽음
        }

        public State playerRedState; // 현재 플레이어의 상태

        float whiteCallTime; // 백혈구 호출 지속 시간

        bool isWhiteCall; // 백혈구 호출 상태인지 여부 (백혈구 호출 아이템을 먹었는지)
        bool isShield;    // 쉴드 여부

        bool isUnbeatable; // 현재 무적 상태인지 여부

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
            Initialized(); // 초기화
        }

        void Start()
        {
            //Initialized();
        }

        void Update()
        {
            UpdatePlayerRedState(); // 플레이어의 상태 업데이트
        }

        // 초기화
        void Initialized()
        {
            playerRedState = State.Walk;

            whiteCallTime = 15.0f;

            isWhiteCall = false;
            isShield = false;

            isUnbeatable = false;
        }

        // 플레이어의 상태 업데이트
        void UpdatePlayerRedState()                                         
        {
            switch (playerRedState)
            {
                case State.Walk:
                    {
                        WalkPlayerRed(); // 걷기 상태일 때의 처리
                        break;
                    }
                case State.Run:
                    {
                        RunPlayerRed(); // 달리기 상태일 때의 처리
                        break;
                    }
                case State.WhiteCall:
                    {
                        WhiteCallPlayerRed(); // 백혈구 호출 상태일 때의 처리
                        break;
                    }
                case State.Die:
                    {
                        DiePlayerRed(); // 죽음 상태일 때의 처리
                        break;
                    }
            }
        }

        // 걷기 상태일 때의 처리
        void WalkPlayerRed()
        {
            if (isWhiteCall)
            {
                // 백혈구 호출 아이템을 먹어 백혈구 호출 상태가 된 경우
                // 애니메이션과 플레이어 상태를 백혈구 호출 상태로 전이
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else
            {
                if (Input.GetAxis("Vertical") == 0)
                {
                    // 이동키 입력이 없을 경우
                    // 걷기 상태 유지
                    playerRedState = State.Walk;
                }
                else
                {
                    // 이동키 입력이 있을 경우
                    // 애니메이션과 플레이어 상태를 달리기 상태로 전이
                    GetComponent<PlayerRedAnimation>().SetRunAnimation();
                    playerRedState = State.Run;
                }
            }
        }

        void RunPlayerRed()
        {
            if (isWhiteCall)
            {
                // 백혈구 호출 아이템을 먹어 백혈구 호출 상태가 된 경우
                // 애니메이션과 플레이어 상태를 백혈구 호출 상태로 전이
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    // 이동키 입력이 있을 경우
                    // 달리기 상태 유지
                    playerRedState = State.Run;
                }
                else
                {
                    // 이동키 입력이 없을 경우
                    // 애니메이션과 플레이어 상태를 걷기 상태로 전이
                    GetComponent<PlayerRedAnimation>().SetWalkAnimation();
                    playerRedState = State.Walk;
                }
            }
        }

        // 백혈구 호출 상태일 때의 처리
        void WhiteCallPlayerRed()
        {
            // 백혈구 호출 지속 시간이 끝난 후 관련 데이터 처리                                                         
            Invoke("FinishWhilteCall", whiteCallTime);
        }

        // 죽음 상태일 때의 처리
        void DiePlayerRed()
        {
            // 플레이어의 이동을 멈춤
            GetComponent<PlayerRedController>().PlayerMoveStop();

            Debug.Log("플레이어 사망");
        }

        // 백혈구 호출 지속 시간이 끝난 후 관련 데이터 처리
        void FinishWhilteCall()
        {
            // 증가되었던 플레이어의 이동 속도를 원래대로 돌림
            GetComponent<PlayerRedController>().ChangePlayerMoveSpeedInit();                                          

            isWhiteCall = false;

            // 애니메이션과 플레이어 상태를 걷기 상태로 전이
            GetComponent<PlayerRedAnimation>().SetWalkAnimation();
            playerRedState = State.Walk;
        }

        // 쉴드 활성화 세팅
        public void SetShieldActive(bool active)
        {
            isShield = active;
        }

        // 플레이어 충돌 관련 처리
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Oxygen")
            {
                // 산소 아이템과 충돌한 경우
                // 해당 산소 아이템 비활성화 후 산소 획득 처리
                other.gameObject.SetActive(false);

                FieldGameManager.instance.GetOxygen();
            }
            else if(other.gameObject.tag == "Reset")
            {
                // 리셋 아이템과 충돌한 경우
                // 해당 리셋 아이템 비활성화 후 필드 아이템 생성 처리
                other.gameObject.SetActive(false);

                FieldGenManager.instance.GenerateFieldItems();
            }
            else if(other.gameObject.tag == "WhiteCall")
            {
                // 백혈구 호출 아이템과 충돌한 경우
                // 해당 백혈구 호출 아이템 비활성화 후, 플레이어 이동속도 20% 증가
                other.gameObject.SetActive(false);

                GetComponent<PlayerRedController>().ChangePlayerMoveSpeed(20.0f);
                isWhiteCall = true;

                // 애니메이션과 플레이어 상태를 백혈구 호출 상태로 전이
                GetComponent<PlayerRedAnimation>().SetWhiteCallAnimation();
                playerRedState = State.WhiteCall;
            }
            else if(other.gameObject.tag == "Monster" || other.gameObject.tag == "MonsterVirus")
            {
                // 필드 몬스터와 충돌한 경우

                if((playerRedState == State.WhiteCall || isShield) && GameManager.instance.DurabilityWeapon > 0)
                {
                    // 현재 백혈구 호출 상태이거나, 쉴드가 있는 상태에서 무기 내구도가 남아있는 경우

                    if (other.gameObject.tag == "MonsterVirus")
                    {
                        // 바이러스형 몬스터의 경우 해당 몬스터 제거 후 현재 생성 수 감소
                        Destroy(other.gameObject);
                        GameObject.Find("FieldMonsterGenManager").GetComponent<FieldMonsterGenManager>().KillVirus();
                    }
                    else
                    {
                        // 세균형 몬스터의 경우 해당 몬스터 비활성화
                        other.gameObject.SetActive(false);
                    }

                    GameManager.instance.PlayerAddExp(50.0f);  // 플레이어 경험치 50 획득
                    FieldGameManager.instance.KillMonster();   // 몬스터 처치했을 때의 처리
                    DurabilityManager.instance.UseWeapon();    // 무기 내구도 감소
                }
                else
                {
                    // 현재 백혈구 호출 상태, 쉴드가 있는 상태가 아니거나 무기 내구도가 없는 경우

                    if (!isUnbeatable) // 무적 상태가 아닐 때만
                    {
                        // 애니메이션과 플레이어 상태를 죽음 상태로 전이
                        GetComponent<PlayerRedAnimation>().SetDieAnimation();
                        playerRedState = State.Die;
                    }
                }
            }
            else if(other.gameObject.tag == "MiddlePotal")
            {
                // 중앙 포탈과 출돌한 경우

                // 보스맵 씬으로 넘어감
                GameManager.instance.gameState = GameManager.State.bossStage;
                SceneManager.LoadScene("Boss");
            }
        }
    }
}
