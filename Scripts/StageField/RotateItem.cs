using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class RotateItem : MonoBehaviour
    {
        float rotateSpeed; // ������ ȸ�� �ӵ�

        void Start()
        {
            rotateSpeed = 90.0f; // ȸ�� �ӵ� �ʱ�ȭ
        }

        void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); // y���� �������� ������ ��� ȸ��               
        }
    }
}