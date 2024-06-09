using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSA
{
    public class NextStagePortal : MonoBehaviour
    {
        const int STAGE_MAX = 4; // 최대 스테이지 수

        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (GameManager.instance.Stage < 4)
                {
                    GameManager.instance.Stage++;
                    GameManager.instance.gameState = GameManager.State.fieldStage;
                    SceneManager.LoadScene("Field");
                }
            }
        }
    }
}
