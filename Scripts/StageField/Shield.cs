using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class Shield : MonoBehaviour
    {
        // 쉴드 오브젝트 충돌 체크
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster" || other.gameObject.tag == "MonsterVirus")                                                
            {
                // 몬스터와 충돌한 경우

                // 쉴드 오브젝트 비활성화
                gameObject.SetActive(false);

                // 0.5초 뒤 쉴드 효과 제거 (쉴드가 사라지자마자 죽는 것 방지용)
                //Invoke("RemoveShildEffect", 0.5f);
            }
        }

        // 쉴드 효과 제거
        void RemoveShildEffect()
        {
            // 플레이어의 쉴드 활성화 플래그를 false로 변경
            GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(false);
        }
    }
}