using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class SidePotalTeleport : MonoBehaviour
    {
        // �浹 üũ
        private void OnCollisionEnter(Collision collision)
        {
            // ��Ż�� �÷��̾ ������ �ݴ��� ��Ż�� �̵���Ŵ
            // - �� ��Ż�� ��ġ�� (x, y, z)�̸� �ٸ� ��Ż�� ��ġ�� (-x, y, z)
            if (collision.gameObject.tag == "Player")
            {
                // �÷��̾�� �浹 ���� ���

                // �÷��̾��� ȸ���� ������ ���¿��� ��ġ�� �ݴ��� ��Ż ��ġ�� �̵���Ŵ
                // x�� ������ -ó���� �ϴ� ������ �� ��Ż�� ��ġ�� (x, y, z)�̸� �ٸ� ��Ż�� ��ġ�� (-x, y, z)�̱� ����
                Vector3 playerPosition = collision.gameObject.transform.position;
                collision.gameObject.transform.position = new Vector3(-playerPosition.x, playerPosition.y, playerPosition.z);
            }
        }
    }
}
