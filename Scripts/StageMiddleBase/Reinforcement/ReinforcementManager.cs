using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class ReinforcementManager : MonoBehaviour
    {
        // ��ȭ Ÿ�� (�ƹ� Ÿ�Ե� �ƴ�, �⺻ ��ȭ, ���� ��ȭ, ������ ��ȭ)
        public enum ReinforcementType { none = 0, normal = 1, high = 2, highest = 3 }

        // ��ȭ ���� (�ƹ� ���µ� �ƴ�, ��� ���, ��� ����, ��� �϶�)
        public enum ReinforcementState { none = 0, up = 1, keep = 2, down = 3 }

        public static ReinforcementManager instance;

        GameObject messageBoxProgress; // ��ȭ ���� �޼��� �ڽ�
        GameObject minigame;           // �̴ϰ��� â
        GameObject messageBoxResult;   // ��ȭ ��� �޼��� �ڽ�

        ReinforcementValues weaponReinforcementValues;  // ���� ��ȭ ���� ��ġ��
        ReinforcementValues defenceReinforcementValues; // �� ��ȭ ���� ��ġ��

        EquipmentItem currentEquipment;                 // ���� ��ȭ�Ϸ��� ���
        ReinforcementValues currentReinforcementValues; // ���� ��ȭ�Ϸ��� ������ ��ȭ ���� ��ġ

        int index;    // ���� ���� ��ũ�� ��� (�ε����� �ջ�) 
        int indexMax; // ���� ��ũ�� ��� �ִ�ġ (�ε����� ȯ��)

        ReinforcementType equipReinforcementType;   // ��ȭ Ÿ��
        ReinforcementState equipReinforcementState; // ��ȭ ����

        public ReinforcementType EquipReinforcementType
        {
            get { return equipReinforcementType; }
        }

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ������Ʈ �ε�
            LoadObjetcts();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ������Ʈ �ε�
        void LoadObjetcts()
        {
            messageBoxProgress = GameObject.Find("ReinforcementMessageBox");
            minigame = GameObject.Find("ReinforcementMinigame");
            messageBoxResult = GameObject.Find("ReinforcementMessageBoxResult");

            weaponReinforcementValues = Resources.Load<ReinforcementValues>("RSA/ScriptableObjects/ReinforcementValues/Weapon");
            defenceReinforcementValues = Resources.Load<ReinforcementValues>("RSA/ScriptableObjects/ReinforcementValues/Defence");
        }

        // �ʱ�ȭ
        void Initialize()
        {
            // �޼��� �ڽ��� ��Ȱ��ȭ
            messageBoxProgress.SetActive(false);
            minigame.SetActive(false);
            messageBoxResult.SetActive(false);

            currentEquipment = null;
            currentReinforcementValues = null;

            // ��� ��ũ�� ��� �ε����� ȯ��
            // �������� ��ũ�� �������� �̿�
            // (��� ���� = 0, �븻 = 1, ���� = 2, ����ũ = 3, �����帮 = 4)
            index = 0;
            indexMax = ((int)Item.Rank.legend - 1) * 3 + EquipmentItem.ITEM_GRADE_MAX;

            equipReinforcementType = ReinforcementType.none;
            equipReinforcementState = ReinforcementState.none;
        }

        // ���� ���õ� ��� ����
        public void SetCurrentEquipment(EquipmentItem equip)
        {
            if (equip != null)
            {
                // ���� ���õ� ��� ������ ���� �ٲٰ�, ����� ���� ��ȭ ���� ��ġ�� ���� �� ��ȭ ���� ��ġ�� ���
                currentEquipment = equip;
                currentReinforcementValues = (equip.EquipPart == EquipmentItem.Part.sword) ? weaponReinforcementValues : defenceReinforcementValues;

                // ��� ��ȭ ���� ����
                SetEquipmentInfo();
            }
        }

        // ��� ��ȭ ���� ����
        void SetEquipmentInfo()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                // ���� ����� ��ũ�� ����� �ε����� ȯ��
                index = ((int)currentEquipment.ItemRank - 1) * 3 + currentEquipment.ItemGrade;

                equipReinforcementType = ReinforcementType.none;

                // ��ȭ �� ��� ����  ����
                ReinforcementUIManager.instance.SetEquipBeforeInfo(currentEquipment);

                if (index < indexMax)
                {
                    // ��ȭ�� �� ���� �� ���
                    // ��ȭ ���� ��� ������ ���� ��ȭ ������ ����
                    ReinforcementUIManager.instance.SetEquipAfterInfo(currentEquipment, currentReinforcementValues, index);
                    ReinforcementUIManager.instance.SetReinforcementInfo(currentReinforcementValues, index);
                }
                else 
                {
                    // ��ȭ�� �� �Ұ��� �� �� (��ȭ �ִ�ġ �� ��)
                    // ��ȭ ���� ��� ������ ��ȭ �ִ�ġ�� ���� ������ ����
                    ReinforcementUIManager.instance.SetEquipAfterInfoMax(currentEquipment);
                    ReinforcementUIManager.instance.SetReinforcementInfoMax();
                }

                // ��� ��ȭ UI Ȱ��ȭ
                ReinforcementUIManager.instance.SetUIActive(true);
            }
        }

        // �⺻ ��ȭ�� Ŭ������ ���
        public void ClickReinforceNormal()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    // ��ȭ �ܰ谡 �ִ� ��� ������ ���

                    if (IsReinforcementPossible(ReinforcementType.normal))
                    {
                        // �⺻ ��ȭ�� ������ ��� (��ȭ�� ����� ���)

                        // ��ȭ Ÿ���� �⺻ ��ȭ�� ����
                        equipReinforcementType = ReinforcementType.normal;

                        // ���� ��ȭ ������ ���� ��ȭ ���� �޼��� �ڽ� ���� �� �޼��� �ڽ� Ȱ��ȭ
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("��ȭ ����");
                    }
                }
            }
        }

        // ���� ��ȭ�� Ŭ������ ���
        public void ClickReinforceHigh()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    // ��ȭ �ܰ谡 �ִ� ��� ������ ���

                    if (IsReinforcementPossible(ReinforcementType.high))
                    {
                        // ���� ��ȭ�� ������ ��� (��ȭ�� ����� ���)

                        // ��ȭ Ÿ���� ���� ��ȭ�� ����
                        equipReinforcementType = ReinforcementType.high;

                        // ���� ��ȭ ������ ���� ��ȭ ���� �޼��� �ڽ� ���� �� �޼��� �ڽ� Ȱ��ȭ
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("��ȭ ����");
                    }
                }
            }
        }

        // ������ ��ȭ�� Ŭ������ ���
        public void ClickReinforceHighest()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    if (IsReinforcementPossible(ReinforcementType.highest))
                    {
                        // ������ ��ȭ�� ������ ��� (��ȭ�� ����� ���)

                        // ��ȭ Ÿ���� ������ ��ȭ�� ����
                        equipReinforcementType = ReinforcementType.highest;

                        // ���� ��ȭ ������ ���� ��ȭ ���� �޼��� �ڽ� ���� �� �޼��� �ڽ� Ȱ��ȭ
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("��ȭ ����");
                    }
                }
            }
        }

        // ��ȭ ���� �޼��� �ڽ����� ��ȭ�� ����Ѵٰ� �������� ��
        public void ContinueReinforceProgressButton()
        {
            // ��ȭ ���� �޼��� �ڽ� ��Ȱ��ȭ
            messageBoxProgress.SetActive(false);

            // �̴ϰ��� â Ȱ��ȭ (�̴ϰ��� ����)
            minigame.SetActive(true);
        }

        // ��ȭ ���� �޼��� �ڽ����� ��ȭ�� ������� �ʴ´ٰ� �������� ��
        public void CancleReinforceProgressButton()
        {
            // ��ȭ Ÿ���� �ƹ��͵� �ƴ����� �ʱ�ȭ
            equipReinforcementType = ReinforcementType.none;

            // ��ȭ ���� �޼��� �ڽ� ��Ȱ��ȭ
            messageBoxProgress.SetActive(false);
        }

        // ��ȭ�� ������ (�̴ϰ����� ����)
        public void ProgressReinforcement(float additionProbability)
        {
            // �̴ϰ��� â ��Ȱ��ȭ
            minigame.SetActive(false);

            if (IsReinforcepSuccess(additionProbability))
            {
                // �̴ϰ������� �߰��� Ȯ���� ���� ��ȭ�� ������ ���

                // ��ȭ ���� ó��
                ReinforceSuccess(equipReinforcementType);
            }
            else
            {
                // ��ȭ ���� ó��
                ReinforceFail(equipReinforcementType);
            }
        }

        // ��ȭ�� �����ߴ��� Ȯ��
        bool IsReinforcepSuccess(float additionProbability)
        {
            // ����� ���� ��ȭ ���� Ȯ���� �̴ϰ������� �߰��� Ȯ���� ���� (���� ��ȭ Ȯ��)                                           
            float finalProbability = currentReinforcementValues.Probability[index];
            finalProbability += additionProbability;

            // 0���� 100������ �Ǽ��� �������� �ϳ� ����
            float randNum = Random.Range(0.0f, 100.0f);

            Debug.Log("probability: " + finalProbability + " / randNum: " + randNum);                                       

            // �������� ���� ���� ���� ��ȭ ���� Ȯ�� ������ ��� ��ȭ ����, �ƴ� ��� ��ȭ ����
            return (randNum <= finalProbability) ? true : false;
        }

        // ��ȭ ���� ó��
        public void ReinforceSuccess(ReinforcementType type)
        {
            // ��ȭ ��ġ���� ��� �ɷ�ġ ����
            AddReinforcementStatusValues();
            // ���� ��ȭ ���¸� ��� ������� ����
            equipReinforcementState = ReinforcementState.up;

            // ��ȭ ��� ��ŭ ���� ��ȭ���� ����
            DeleteMoney(equipReinforcementType);

            // ���� ���������� ��� ���
            currentEquipment.ItemGrade++;

            // ��� ��ȭ ���� ����
            SetEquipmentInfo();

            // ��ȭ ��� �޼��� �ڽ��� ���� �� �޼��� �ڽ� Ȱ��ȭ
            messageBoxResult.GetComponent<ReinforcementMessageBoxResult>().SetMessageBoxResult(currentEquipment, equipReinforcementState);
            messageBoxResult.SetActive(true);

            // ���� ��ȭ ���� �ƹ��͵� �ƴ����� �ʱ�ȭ
            equipReinforcementState = ReinforcementState.none;
        }

        // ��ȭ ���� ó��
        public void ReinforceFail(ReinforcementType type)
        {
            if (currentEquipment.ItemGrade > 0)
            {
                // ����� ��ȭ ����� 0 �̻��� ���

                // ��ȭ ��ġ��ŭ ��� �ɷ�ġ �϶�
                SubtractReinforcementStatusValues();

                // ���� ��ȭ ���¸� ��� �϶����� ����
                equipReinforcementState = ReinforcementState.down;
            }
            else
            {
                // ����� ��ȭ ����� 0�� ���

                // ���� ��ȭ ���¸� ��� ������ ����
                equipReinforcementState = ReinforcementState.keep;
            }

            // ��ȭ ��� ��ŭ ���� ��ȭ���� ����
            DeleteMoney(equipReinforcementType);

            // ���� ���������� ��� ���
            currentEquipment.ItemGrade--;

            // ��� ��ȭ ���� ����
            SetEquipmentInfo();

            // ��ȭ ��� �޼��� �ڽ��� ���� �� �޼��� �ڽ� Ȱ��ȭ
            messageBoxResult.GetComponent<ReinforcementMessageBoxResult>().SetMessageBoxResult(currentEquipment, equipReinforcementState);
            messageBoxResult.SetActive(true);

            // ���� ��ȭ ���� �ƹ��͵� �ƴ����� �ʱ�ȭ
            equipReinforcementState = ReinforcementState.none;
        }

        // ��ȭ ��ġ���� ��� �ɷ�ġ ���� (���� ����� �Ǿ��� �� ������ ��ġ��ŭ ����)
        void AddReinforcementStatusValues()
        {
            currentEquipment.ReinforcementStatus.AddStatus
                        (currentReinforcementValues.Hp[index + 1], currentReinforcementValues.Attack[index + 1], currentReinforcementValues.Defence[index + 1], currentReinforcementValues.Speed[index + 1]);
        }

        // ��ȭ ��ġ��ŭ ��� �ɷ�ġ �϶� (���� ����� �Ǿ��� �� ������ ��ġ��ŭ ����)
        void SubtractReinforcementStatusValues()
        {
            currentEquipment.ReinforcementStatus.AddStatus
                            (-currentReinforcementValues.Hp[index], -currentReinforcementValues.Attack[index], -currentReinforcementValues.Defence[index], -currentReinforcementValues.Speed[index]);
        }

        // ��ȭ ����
        void DeleteMoney(ReinforcementType type)
        {
            switch (type)
            {
                case ReinforcementType.normal:
                    {
                        // �⺻ ��ȭ�� ���
                        // �ش� ��븸ŭ ��ҿ� ö�� ����
                        GameManager.instance.Oxygen -= currentReinforcementValues.Oxygen[index];
                        GameManager.instance.Iron -= currentReinforcementValues.Iron[index];
                        break;
                    }
                case ReinforcementType.high:
                    {
                        // ���� ��ȭ�� ���
                        // �ش� ��븸ŭ ��ҿ� ö��, �̳׶� ����
                        GameManager.instance.Oxygen -= currentReinforcementValues.Oxygen[index];
                        GameManager.instance.Iron -= currentReinforcementValues.Iron[index];
                        GameManager.instance.Mineral -= currentReinforcementValues.Mineral[index];

                        break;
                    }
                case ReinforcementType.highest:
                    {
                        // ������ ��ȭ�� ���
                        // �ش� ��븸ŭ ��ҿ� ö��, �̳׶� ����
                        GameManager.instance.Oxygen -= currentReinforcementValues.Oxygen[index];
                        GameManager.instance.Iron -= currentReinforcementValues.Iron[index];
                        GameManager.instance.Mineral -= (currentReinforcementValues.Mineral[index] * 2);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // �ݾ��� �����Ǿ����Ƿ� ���� �κ��丮�� ���� ��ȭ �缼��
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();
        }

        // ��ȭ�� �������� Ȯ��
        bool IsReinforcementPossible(ReinforcementType type)
        {
            switch (type)
            {
                case ReinforcementType.normal:
                    {
                        // �⺻ ��ȭ�� ���
                        // ���� ������ ��ҿ� ö���� ��ȭ�� ���̴� ���� �̻��� ��� ture, ��ȭ�� ������ ��� false
                        return (GameManager.instance.Oxygen >= currentReinforcementValues.Oxygen[index]
                                    && GameManager.instance.Iron >= currentReinforcementValues.Iron[index]) ? true : false;
                    }
                case ReinforcementType.high:
                    {
                        // ���� ��ȭ�� ���
                        // ���� ������ ��ҿ� ö��, �̳׶��� ��ȭ�� ���̴� ���� �̻��� ��� ture, ��ȭ�� ������ ��� false
                        return (GameManager.instance.Oxygen >= currentReinforcementValues.Oxygen[index]
                                    && GameManager.instance.Iron >= currentReinforcementValues.Iron[index]
                                        && GameManager.instance.Mineral >= currentReinforcementValues.Mineral[index]) ? true : false;
                    }
                case ReinforcementType.highest:
                    {
                        // ������ ��ȭ�� ���
                        // ���� ������ ��ҿ� ö��, �̳׶��� ��ȭ�� ���̴� ���� �̻��� ��� ture, ��ȭ�� ������ ��� false
                        // ��ȭ�� ���̴� �̳׶��� ���� ��ȭ�� 2��
                        return (GameManager.instance.Oxygen >= currentReinforcementValues.Oxygen[index]
                                    && GameManager.instance.Iron >= currentReinforcementValues.Iron[index]
                                        && GameManager.instance.Mineral >= currentReinforcementValues.Mineral[index] * 2) ? true : false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        // X ��ư�� Ŭ������ ��
        public void ClickButtonX()
        {
            // ��ȭ UI ������Ʈ ���� �� �߰� ���� �⺻ UI Ȱ��ȭ
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }

        // ��ȭ ���� ��ư�� ������ ��� (ġƮŰ)
        public void ClickEquipReinforcementSuccess()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    equipReinforcementType = ReinforcementType.normal;

                    if (IsReinforcementPossible(equipReinforcementType))
                    {
                        ReinforceSuccess(equipReinforcementType);
                    }
                }
            }
        }

        // ��ȭ ���� ��ư�� ������ ��� (ġƮŰ)
        public void ClickEquipReinforcementFail()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    equipReinforcementType = ReinforcementType.normal;

                    if (IsReinforcementPossible(equipReinforcementType))
                    {
                        ReinforceFail(equipReinforcementType);
                    }
                }
            }
        }

        // ��� ��ȭ ���� �ʱ�ȭ ��ư�� ������ ��� (ġƮŰ)
        public void ClickEquipReinforcementReset()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null)
                {
                    currentEquipment.ItemRank = Item.Rank.normal;
                    currentEquipment.ItemGrade = 0;

                    currentEquipment.ReinforcementStatus.ResetStatus();

                    SetEquipmentInfo();
                }
            }
        }

        // ��� ���� ��ư�� ������ ��� (ġƮŰ)
        public void FillOxygenMax()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null)
                {
                    GameManager.instance.Oxygen = GameManager.MONEY_MAX;

                    UserInventoryMoneyManager.instance.SetUserInventoryMoney();
                    SetEquipmentInfo();
                }
            }
        }

        // ö�� ���� ��ư�� ������ ��� (ġƮŰ)
        public void FillIronMax()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null)
                {
                    GameManager.instance.Iron = GameManager.MONEY_MAX;

                    UserInventoryMoneyManager.instance.SetUserInventoryMoney();
                    SetEquipmentInfo();
                }
            }
        }

        // �̳׶� ���� ��ư�� ������ ��� (ġƮŰ)
        public void FillMineralMax()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null)
                {
                    GameManager.instance.Mineral = GameManager.MONEY_MAX;

                    UserInventoryMoneyManager.instance.SetUserInventoryMoney();
                    SetEquipmentInfo();
                }
            }
        }
    }
}
