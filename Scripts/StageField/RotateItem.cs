using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class RotateItem : MonoBehaviour
    {
        float rotateSpeed; // 아이템 회전 속도

        void Start()
        {
            rotateSpeed = 90.0f; // 회전 속도 초기화
        }

        void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); // y축을 기준으로 아이템 계속 회전               
        }
    }
}