using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class FieldItemManager : MonoBehaviour
    {
        const int FIELD_ITEM_MAX_COUNT = 3; // 필드 아이템 종류 최대치

        [SerializeField] FieldItem[] fieldItems; // 필드 아이템 정보 배열

        float[] remainingTime; // 슬롯에 있는 아이템 별 남은 대기 시간

        GameObject shield;                       // 쉴드 오브젝트
        public GameObject[] randomTeleportPoint; // 랜덤 텔레포트 이동 포인트들

        private void Awake()
        {
            // 관련 오브젝트들 불러오기
            LoadObjects();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 초기화
        void Update()
        {
            UpdateFieldItem();     // 필드 아이템의 상태 업데이트
            UpdateRemainingTime(); // 필드 아이템의 남은 대기시간 업데이트
        }

        // 관련 오브젝트들 불러오기
        void LoadObjects()
        {
            shield = GameObject.Find("Shield");
            randomTeleportPoint = GameObject.FindGameObjectsWithTag("RandomTeleportPoint");
        }

        // 초기화
        void Initialize()
        {
            fieldItems = GameManager.instance.FieldItems;

            remainingTime = new float[FIELD_ITEM_MAX_COUNT];

            for (int i = 0; i < FIELD_ITEM_MAX_COUNT; i++)
            {
                if (fieldItems != null)
                {
                    FieldUIManager.instance.SetItemIcon(i, fieldItems[i].ItemIcon);
                    FieldUIManager.instance.SetActiveItem(i, true);
                    FieldUIManager.instance.SetItemCountText(i, fieldItems[i].InvenCurrentCount);

                    fieldItems[i].IsUsePossible = true;
                    remainingTime[i] = fieldItems[i].CoolTime;
                }
            }

            shield.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(false);
        }

        // 필드 아이템의 상태 업데이트
        void UpdateFieldItem()
        {
            int index = -1; // 키입력이 없을 때의 인덱스 값은 -1

            if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;      // 키보드의 1키를 눌렀을 때
            else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1; // 키보드의 2키를 눌렀을 때
            else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2; // 키보드의 3키를 눌렀을 때

            // 키 입력이 들어온 경우 눌린 키에 따른 아이템 사용 처리
            if (index != -1) UseItem(index);
        }

        // 눌린 키에 따른 아이템 사용 처리
        void UseItem(int index)
        {
            if (fieldItems[index] != null)
            {
                // 필드 아이템이 존재하는 경우
 
                if (fieldItems[index].IsUsePossible && fieldItems[index].InvenCurrentCount > 0)
                {
                    // 필드 아이템이 현재 사용 가능하고(쿨타임이 아니고), 현재 개수가 0보다 큰 경우(아이템이 남아있는 경우)

                    // 현재 아이템 개수 감소
                    fieldItems[index].InvenCurrentCount--;
                    //GameManager.instance.FieldItems[index].InvenCurrentCount--;

                    // 해당 아이템 슬롯 UI의 남은 개수 텍스트 변경
                    FieldUIManager.instance.SetItemCountText(index, fieldItems[index].InvenCurrentCount);

                    if (fieldItems[index].ItemId == (int)FieldItem.Id.speedUp)
                    {
                        // 해당 아이템이 이동속도 증가 아이템인 경우
                        // 이동속도 증가 아이템 사용
                        UseSpeedUpItem(index);
                    }
                    else if (fieldItems[index].ItemId == (int)FieldItem.Id.randomTeleport)
                    {
                        // 해당 아이템이 랜덤 텔레포트 아이템인 경우
                        // 랜덤 텔레포트 아이템 사용
                        UseRandomTeleportItem();
                    }
                    else if (fieldItems[index].ItemId == (int)FieldItem.Id.shield)
                    {
                        // 해당 아이템이 쉴드 아이템인 경우
                        // 쉴드 아이템 사용
                        UseShieldItem(index);
                    }

                    // 해당 아이템의 사용 가능 여부를 flase로 변경한 뒤, 아이템 쿨타임 UI로 변경
                    fieldItems[index].IsUsePossible = false;
                    FieldUIManager.instance.SetActiveItem(index, false);

                    // 해당 아이템의 쿨타임이 적용되는 동안 대기
                    StartCoroutine(WaitCoolTime(index));
                }
            }
        }

        // 해당 아이템의 쿨타임이 적용되는 동안 대기
        IEnumerator WaitCoolTime(int index)
        {
            if(!fieldItems[index].IsUsePossible)
            {
                // 해당 아이템이 사용 불가능한 경우

                // 해당 아이템의 쿨타임 시간동안 대기
                yield return new WaitForSeconds(fieldItems[index].CoolTime);

                // 쿨타임이 끝나면 아이템 활성화 UI로 변경 후, 해당 아이템의 사용 가능 여부를 true로 변경                                   
                FieldUIManager.instance.SetActiveItem(index, true);
                fieldItems[index].IsUsePossible = true;

                // 해당 아이템의 남은 대기 시간을 쿨타임 시간으로 초기화
                remainingTime[index] = fieldItems[index].CoolTime;
            }
        }

        // 필드 아이템의 남은 대기시간 업데이트
        void UpdateRemainingTime()
        {
            for(int i = 0; i < FIELD_ITEM_MAX_COUNT; i++)
            {
                // 각 아이템 별로 쿨타임 계속 체크

                if(!fieldItems[i].IsUsePossible)
                {
                    // 해당 아이템이 사용 불가능인 경우
                    // 쿨타임 시간에서 흘러간 시간을 계속 빼주며 현재 남은 시간을 계산
                    remainingTime[i] -= Time.deltaTime;

                    // 남은 시간에 따라 아이템 쿨타임 UI의 텍스트를 변경
                    FieldUIManager.instance.SetItemCoolTimeText(i, (int)Mathf.Ceil(remainingTime[i]));
                }
            }
        }

        // 이동속도 증가 아이템 사용
        void UseSpeedUpItem(int index)
        {
            // 해당 아이템의 효과 지속 여부를 true로 변경
            fieldItems[index].IsEffectContinue = true;

            // 이동속도 증가 아이템의 효과 지속 시간에 동안 효과 유지
            StartCoroutine(WaitSpeedUpDurationTime(index));
        }

        // 이동속도 증가 아이템의 효과 지속 시간에 동안 효과 유지
        IEnumerator WaitSpeedUpDurationTime(int index)
        {
            if(fieldItems[index].IsEffectContinue)
            {
                // 해당 아이템의 효과가 지속 중인 경우

                // 플레이어의 이동속도를 30% 증가
                GameObject.Find("Player").GetComponent<PlayerRedController>().ChangePlayerMoveSpeed(30.0f);

                // 해당 아이템의 지속 시간동안 대기
                yield return new WaitForSeconds(fieldItems[index].Duration);

                // 아이템 지속 시간이 끝나면 플레이어의 이동속도를 초기값으로 되돌리고, 효과 지속 여부를 false로 변경                    
                GameObject.Find("Player").GetComponent<PlayerRedController>().ChangePlayerMoveSpeedInit();
                fieldItems[index].IsEffectContinue = false;
            }
        }

        // 랜덤 텔레포트 아이템 사용
        void UseRandomTeleportItem()
        {
            // 랜덤 텔레포트 이동 포인트들 중 한곳을 랜덤으로 뽑아 플레이어의 위치와 회전값 변경                                          
            int randomIndex = Random.Range(0, randomTeleportPoint.Length);

            Transform player = GameObject.Find("Player").transform;

            player.position = randomTeleportPoint[randomIndex].transform.position;
            player.rotation = randomTeleportPoint[randomIndex].transform.rotation;
        }

        // 쉴드 아이템 사용
        void UseShieldItem(int index)
        {
            // 해당 아이템의 효과 지속 여부를 true로 변경
            fieldItems[index].IsEffectContinue = true;

            // 쉴드 아이템의 효과 지속 시간에 동안 효과 유지
            StartCoroutine(WaitShieldDurationTime(index));
        }

        // 쉴드 아이템의 효과 지속 시간에 동안 효과 유지
        IEnumerator WaitShieldDurationTime(int index)
        {
            if(fieldItems[index].IsEffectContinue)
            {
                // 해당 아이템의 효과가 지속 중인 경우

                // 쉴드 활성화 및 플레이어의 쉴드 여부를 true로 변경
                shield.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(true);

                // 해당 아이템의 지속 시간동안 대기
                yield return new WaitForSeconds(fieldItems[index].Duration);

                // 아이템 지속 시간이 끝나면 쉴드 비활성화 및 플레이어의 쉴드 여부와 효과 지속 여부를 false로 변경                      
                shield.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(false);
                fieldItems[index].IsEffectContinue = false;
            }
        }
    }
}