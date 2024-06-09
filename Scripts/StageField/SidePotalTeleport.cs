using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class SidePotalTeleport : MonoBehaviour
    {
        // 충돌 체크
        private void OnCollisionEnter(Collision collision)
        {
            // 포탈에 플레이어가 닿으면 반대쪽 포탈로 이동시킴
            // - 한 포탈의 위치가 (x, y, z)이면 다른 포탈의 위치는 (-x, y, z)
            if (collision.gameObject.tag == "Player")
            {
                // 플레이어와 충돌 했을 경우

                // 플레이어의 회전은 유지한 상태에서 위치를 반대쪽 포탈 위치로 이동시킴
                // x축 값에만 -처리를 하는 이유는 한 포탈의 위치가 (x, y, z)이면 다른 포탈의 위치는 (-x, y, z)이기 때문
                Vector3 playerPosition = collision.gameObject.transform.position;
                collision.gameObject.transform.position = new Vector3(-playerPosition.x, playerPosition.y, playerPosition.z);
            }
        }
    }
}
