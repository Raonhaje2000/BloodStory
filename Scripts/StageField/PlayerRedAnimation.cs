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
            // 컴포넌트 불러오기
            LoadComponent();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 컴포넌트 불러오기
        void LoadComponent()
        {
            animator = GetComponent<Animator>();                                                                       
        }

        // 초기화
        void Initialize()
        {
            // 걷기 애니메이션으로 세팅
            SetWalkAnimation();
        }

        // 걷기 애니메이션으로 세팅
        public void SetWalkAnimation()
        {
            animator.SetBool("walk", true);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", false);                                                         
            animator.SetBool("die", false);
        }

        // 달리기 애니메이션으로 세팅
        public void SetRunAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
            animator.SetBool("whiteCall", false);
            animator.SetBool("die", false);
        }

        // 백혈구 호출 애니메이션으로 세팅
        public void SetWhiteCallAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", true);
            animator.SetBool("die", false);
        }

        // 죽음 애니메이션으로 세팅
        public void SetDieAnimation()
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("whiteCall", false);
            animator.SetBool("die", true);
        }
    }
}