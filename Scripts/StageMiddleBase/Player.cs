using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class Player : MonoBehaviour
    {
        // 플레이어의 상태
        enum State
        { 
            idle = 0, // 기본
            move = 1  // 움직임
        }

        State playerState; // 현재 플레이어의 상태

        Animator animator; // 애니메이션 컴포넌트

        private void Awake()
        {
            // 컴포넌트 불러오기
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        void Update()
        {
            // 플레이어의 상태 업데이트
            switch (playerState)
            {
                case State.idle:
                    {
                        UpdateInit(); // 기본 상태일 떄의 처리
                        break;
                    }
                case State.move:
                    {
                        UpdateMove(); // 움직임 상태일 때의 처리
                        break;
                    }
            }

        }

        // 초기화
        void Initialize()
        {
            playerState = State.idle;
        }

        // 기본 상태일 떄의 처리
        void UpdateInit()
        {       
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // 이동키(방향키 또는 W, S, A, D키) 입력이 있을 경우
                // 애니메이션과 플레이어 상태를 움직임 상태로 전이
                SetMoveAnimation();
                playerState = State.move;
            }
            else
            {
                // 이동키 입력이 없을 경우
                // 기본 상태 유지
                playerState = State.idle;
            }
        }

        // 움직임 상태일 때의 처리
        void UpdateMove()
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                // 이동키 입력이 있을 경우
                // 움직임 상태 유지
                playerState = State.move;
            }
            else
            {
                // 이동키 입력이 없을 경우
                // 애니메이션과 플레이어 상태를 기본 상태로 전이
                SetIdleAnimation();
                playerState = State.idle;
            }
        }

        // 기본 애니메이션으로 세팅
        void SetIdleAnimation()
        {
            animator.SetBool("idle", true);
            animator.SetBool("walk", false);
        }

        // 움직임 애니메이션으로 세팅
        void SetMoveAnimation()
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", true);
        }

        // 플레이어 충돌 처리
        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                // 충돌하고 있는 상태에서 G키를 눌렀을 경우
                if(other.tag == "Reinforcement")
                {
                    // 강화 NPC 영역 내에 있을 때, 강화 UI 활성화
                    MiddleBaseUIManager.instance.ActivReinforcementUI();
                }
                else if(other.tag == "Production")
                {
                    // 제작 NPC 영역 내에 있을 때, 제작 UI 활성화
                    MiddleBaseUIManager.instance.ActiveProductionUI();
                }
                else if(other.tag == "Shop")
                {
                    // 상점 NPC 영역 내에 있을 때, 상점 UI 활성화
                    MiddleBaseUIManager.instance.ActiveShopUI();
                }
            }
        }
    }
}