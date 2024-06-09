using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class Shield : MonoBehaviour
    {
        // ���� ������Ʈ �浹 üũ
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Monster" || other.gameObject.tag == "MonsterVirus")                                                
            {
                // ���Ϳ� �浹�� ���

                // ���� ������Ʈ ��Ȱ��ȭ
                gameObject.SetActive(false);

                // 0.5�� �� ���� ȿ�� ���� (���尡 ������ڸ��� �״� �� ������)
                //Invoke("RemoveShildEffect", 0.5f);
            }
        }

        // ���� ȿ�� ����
        void RemoveShildEffect()
        {
            // �÷��̾��� ���� Ȱ��ȭ �÷��׸� false�� ����
            GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(false);
        }
    }
}