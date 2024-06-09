using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class FieldItemManager : MonoBehaviour
    {
        const int FIELD_ITEM_MAX_COUNT = 3; // �ʵ� ������ ���� �ִ�ġ

        [SerializeField] FieldItem[] fieldItems; // �ʵ� ������ ���� �迭

        float[] remainingTime; // ���Կ� �ִ� ������ �� ���� ��� �ð�

        GameObject shield;                       // ���� ������Ʈ
        public GameObject[] randomTeleportPoint; // ���� �ڷ���Ʈ �̵� ����Ʈ��

        private void Awake()
        {
            // ���� ������Ʈ�� �ҷ�����
            LoadObjects();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // �ʱ�ȭ
        void Update()
        {
            UpdateFieldItem();     // �ʵ� �������� ���� ������Ʈ
            UpdateRemainingTime(); // �ʵ� �������� ���� ���ð� ������Ʈ
        }

        // ���� ������Ʈ�� �ҷ�����
        void LoadObjects()
        {
            shield = GameObject.Find("Shield");
            randomTeleportPoint = GameObject.FindGameObjectsWithTag("RandomTeleportPoint");
        }

        // �ʱ�ȭ
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

        // �ʵ� �������� ���� ������Ʈ
        void UpdateFieldItem()
        {
            int index = -1; // Ű�Է��� ���� ���� �ε��� ���� -1

            if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;      // Ű������ 1Ű�� ������ ��
            else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1; // Ű������ 2Ű�� ������ ��
            else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2; // Ű������ 3Ű�� ������ ��

            // Ű �Է��� ���� ��� ���� Ű�� ���� ������ ��� ó��
            if (index != -1) UseItem(index);
        }

        // ���� Ű�� ���� ������ ��� ó��
        void UseItem(int index)
        {
            if (fieldItems[index] != null)
            {
                // �ʵ� �������� �����ϴ� ���
 
                if (fieldItems[index].IsUsePossible && fieldItems[index].InvenCurrentCount > 0)
                {
                    // �ʵ� �������� ���� ��� �����ϰ�(��Ÿ���� �ƴϰ�), ���� ������ 0���� ū ���(�������� �����ִ� ���)

                    // ���� ������ ���� ����
                    fieldItems[index].InvenCurrentCount--;
                    //GameManager.instance.FieldItems[index].InvenCurrentCount--;

                    // �ش� ������ ���� UI�� ���� ���� �ؽ�Ʈ ����
                    FieldUIManager.instance.SetItemCountText(index, fieldItems[index].InvenCurrentCount);

                    if (fieldItems[index].ItemId == (int)FieldItem.Id.speedUp)
                    {
                        // �ش� �������� �̵��ӵ� ���� �������� ���
                        // �̵��ӵ� ���� ������ ���
                        UseSpeedUpItem(index);
                    }
                    else if (fieldItems[index].ItemId == (int)FieldItem.Id.randomTeleport)
                    {
                        // �ش� �������� ���� �ڷ���Ʈ �������� ���
                        // ���� �ڷ���Ʈ ������ ���
                        UseRandomTeleportItem();
                    }
                    else if (fieldItems[index].ItemId == (int)FieldItem.Id.shield)
                    {
                        // �ش� �������� ���� �������� ���
                        // ���� ������ ���
                        UseShieldItem(index);
                    }

                    // �ش� �������� ��� ���� ���θ� flase�� ������ ��, ������ ��Ÿ�� UI�� ����
                    fieldItems[index].IsUsePossible = false;
                    FieldUIManager.instance.SetActiveItem(index, false);

                    // �ش� �������� ��Ÿ���� ����Ǵ� ���� ���
                    StartCoroutine(WaitCoolTime(index));
                }
            }
        }

        // �ش� �������� ��Ÿ���� ����Ǵ� ���� ���
        IEnumerator WaitCoolTime(int index)
        {
            if(!fieldItems[index].IsUsePossible)
            {
                // �ش� �������� ��� �Ұ����� ���

                // �ش� �������� ��Ÿ�� �ð����� ���
                yield return new WaitForSeconds(fieldItems[index].CoolTime);

                // ��Ÿ���� ������ ������ Ȱ��ȭ UI�� ���� ��, �ش� �������� ��� ���� ���θ� true�� ����                                   
                FieldUIManager.instance.SetActiveItem(index, true);
                fieldItems[index].IsUsePossible = true;

                // �ش� �������� ���� ��� �ð��� ��Ÿ�� �ð����� �ʱ�ȭ
                remainingTime[index] = fieldItems[index].CoolTime;
            }
        }

        // �ʵ� �������� ���� ���ð� ������Ʈ
        void UpdateRemainingTime()
        {
            for(int i = 0; i < FIELD_ITEM_MAX_COUNT; i++)
            {
                // �� ������ ���� ��Ÿ�� ��� üũ

                if(!fieldItems[i].IsUsePossible)
                {
                    // �ش� �������� ��� �Ұ����� ���
                    // ��Ÿ�� �ð����� �귯�� �ð��� ��� ���ָ� ���� ���� �ð��� ���
                    remainingTime[i] -= Time.deltaTime;

                    // ���� �ð��� ���� ������ ��Ÿ�� UI�� �ؽ�Ʈ�� ����
                    FieldUIManager.instance.SetItemCoolTimeText(i, (int)Mathf.Ceil(remainingTime[i]));
                }
            }
        }

        // �̵��ӵ� ���� ������ ���
        void UseSpeedUpItem(int index)
        {
            // �ش� �������� ȿ�� ���� ���θ� true�� ����
            fieldItems[index].IsEffectContinue = true;

            // �̵��ӵ� ���� �������� ȿ�� ���� �ð��� ���� ȿ�� ����
            StartCoroutine(WaitSpeedUpDurationTime(index));
        }

        // �̵��ӵ� ���� �������� ȿ�� ���� �ð��� ���� ȿ�� ����
        IEnumerator WaitSpeedUpDurationTime(int index)
        {
            if(fieldItems[index].IsEffectContinue)
            {
                // �ش� �������� ȿ���� ���� ���� ���

                // �÷��̾��� �̵��ӵ��� 30% ����
                GameObject.Find("Player").GetComponent<PlayerRedController>().ChangePlayerMoveSpeed(30.0f);

                // �ش� �������� ���� �ð����� ���
                yield return new WaitForSeconds(fieldItems[index].Duration);

                // ������ ���� �ð��� ������ �÷��̾��� �̵��ӵ��� �ʱⰪ���� �ǵ�����, ȿ�� ���� ���θ� false�� ����                    
                GameObject.Find("Player").GetComponent<PlayerRedController>().ChangePlayerMoveSpeedInit();
                fieldItems[index].IsEffectContinue = false;
            }
        }

        // ���� �ڷ���Ʈ ������ ���
        void UseRandomTeleportItem()
        {
            // ���� �ڷ���Ʈ �̵� ����Ʈ�� �� �Ѱ��� �������� �̾� �÷��̾��� ��ġ�� ȸ���� ����                                          
            int randomIndex = Random.Range(0, randomTeleportPoint.Length);

            Transform player = GameObject.Find("Player").transform;

            player.position = randomTeleportPoint[randomIndex].transform.position;
            player.rotation = randomTeleportPoint[randomIndex].transform.rotation;
        }

        // ���� ������ ���
        void UseShieldItem(int index)
        {
            // �ش� �������� ȿ�� ���� ���θ� true�� ����
            fieldItems[index].IsEffectContinue = true;

            // ���� �������� ȿ�� ���� �ð��� ���� ȿ�� ����
            StartCoroutine(WaitShieldDurationTime(index));
        }

        // ���� �������� ȿ�� ���� �ð��� ���� ȿ�� ����
        IEnumerator WaitShieldDurationTime(int index)
        {
            if(fieldItems[index].IsEffectContinue)
            {
                // �ش� �������� ȿ���� ���� ���� ���

                // ���� Ȱ��ȭ �� �÷��̾��� ���� ���θ� true�� ����
                shield.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(true);

                // �ش� �������� ���� �ð����� ���
                yield return new WaitForSeconds(fieldItems[index].Duration);

                // ������ ���� �ð��� ������ ���� ��Ȱ��ȭ �� �÷��̾��� ���� ���ο� ȿ�� ���� ���θ� false�� ����                      
                shield.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerRed>().SetShieldActive(false);
                fieldItems[index].IsEffectContinue = false;
            }
        }
    }
}