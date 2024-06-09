using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSA
{
    public class ReinforcementManager : MonoBehaviour
    {
        // 강화 타입 (아무 타입도 아님, 기본 강화, 집중 강화, 초집중 강화)
        public enum ReinforcementType { none = 0, normal = 1, high = 2, highest = 3 }

        // 강화 상태 (아무 상태도 아님, 등급 상승, 등급 유지, 등급 하락)
        public enum ReinforcementState { none = 0, up = 1, keep = 2, down = 3 }

        public static ReinforcementManager instance;

        GameObject messageBoxProgress; // 강화 진행 메세지 박스
        GameObject minigame;           // 미니게임 창
        GameObject messageBoxResult;   // 강화 결과 메세지 박스

        ReinforcementValues weaponReinforcementValues;  // 무기 강화 관련 수치들
        ReinforcementValues defenceReinforcementValues; // 방어구 강화 관련 수치들

        EquipmentItem currentEquipment;                 // 현재 강화하려는 장비
        ReinforcementValues currentReinforcementValues; // 현재 강화하려는 부위의 강화 관련 수치

        int index;    // 현재 무기 랭크와 등급 (인덱스로 합산) 
        int indexMax; // 무기 랭크와 등급 최대치 (인덱스로 환산)

        ReinforcementType equipReinforcementType;   // 강화 타입
        ReinforcementState equipReinforcementState; // 강화 상태

        public ReinforcementType EquipReinforcementType
        {
            get { return equipReinforcementType; }
        }

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 오브젝트 로드
            LoadObjetcts();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 오브젝트 로드
        void LoadObjetcts()
        {
            messageBoxProgress = GameObject.Find("ReinforcementMessageBox");
            minigame = GameObject.Find("ReinforcementMinigame");
            messageBoxResult = GameObject.Find("ReinforcementMessageBoxResult");

            weaponReinforcementValues = Resources.Load<ReinforcementValues>("RSA/ScriptableObjects/ReinforcementValues/Weapon");
            defenceReinforcementValues = Resources.Load<ReinforcementValues>("RSA/ScriptableObjects/ReinforcementValues/Defence");
        }

        // 초기화
        void Initialize()
        {
            // 메세지 박스들 비활성화
            messageBoxProgress.SetActive(false);
            minigame.SetActive(false);
            messageBoxResult.SetActive(false);

            currentEquipment = null;
            currentReinforcementValues = null;

            // 장비 랭크와 등급 인덱스로 환산
            // 열거형인 랭크가 숫자임을 이용
            // (등급 없음 = 0, 노말 = 1, 레어 = 2, 유니크 = 3, 레전드리 = 4)
            index = 0;
            indexMax = ((int)Item.Rank.legend - 1) * 3 + EquipmentItem.ITEM_GRADE_MAX;

            equipReinforcementType = ReinforcementType.none;
            equipReinforcementState = ReinforcementState.none;
        }

        // 현재 선택된 장비 세팅
        public void SetCurrentEquipment(EquipmentItem equip)
        {
            if (equip != null)
            {
                // 현재 선택된 장비를 선택한 장비로 바꾸고, 무기면 무기 강화 관련 수치로 방어구면 방어구 강화 관련 수치로 사용
                currentEquipment = equip;
                currentReinforcementValues = (equip.EquipPart == EquipmentItem.Part.sword) ? weaponReinforcementValues : defenceReinforcementValues;

                // 장비 강화 정보 세팅
                SetEquipmentInfo();
            }
        }

        // 장비 강화 정보 세팅
        void SetEquipmentInfo()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                // 현재 장비의 랭크와 등급을 인덱스로 환산
                index = ((int)currentEquipment.ItemRank - 1) * 3 + currentEquipment.ItemGrade;

                equipReinforcementType = ReinforcementType.none;

                // 강화 전 장비 정보  세팅
                ReinforcementUIManager.instance.SetEquipBeforeInfo(currentEquipment);

                if (index < indexMax)
                {
                    // 강화가 더 가능 한 경우
                    // 강화 후의 장비 정보를 이후 강화 정보로 세팅
                    ReinforcementUIManager.instance.SetEquipAfterInfo(currentEquipment, currentReinforcementValues, index);
                    ReinforcementUIManager.instance.SetReinforcementInfo(currentReinforcementValues, index);
                }
                else 
                {
                    // 강화가 더 불가능 할 때 (강화 최대치 일 때)
                    // 강화 후의 장비 정보를 강화 최대치일 떄의 정보로 세팅
                    ReinforcementUIManager.instance.SetEquipAfterInfoMax(currentEquipment);
                    ReinforcementUIManager.instance.SetReinforcementInfoMax();
                }

                // 장비 강화 UI 활성화
                ReinforcementUIManager.instance.SetUIActive(true);
            }
        }

        // 기본 강화를 클릭했을 경우
        public void ClickReinforceNormal()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    // 강화 단계가 최대 등급 이하인 경우

                    if (IsReinforcementPossible(ReinforcementType.normal))
                    {
                        // 기본 강화가 가능한 경우 (재화가 충분한 경우)

                        // 강화 타입을 기본 강화로 변경
                        equipReinforcementType = ReinforcementType.normal;

                        // 현재 강화 정보에 맞춰 강화 진행 메세지 박스 세팅 후 메세지 박스 활성화
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("재화 부족");
                    }
                }
            }
        }

        // 집중 강화를 클릭했을 경우
        public void ClickReinforceHigh()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    // 강화 단계가 최대 등급 이하인 경우

                    if (IsReinforcementPossible(ReinforcementType.high))
                    {
                        // 집중 강화가 가능한 경우 (재화가 충분한 경우)

                        // 강화 타입을 집중 강화로 변경
                        equipReinforcementType = ReinforcementType.high;

                        // 현재 강화 정보에 맞춰 강화 진행 메세지 박스 세팅 후 메세지 박스 활성화
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("재화 부족");
                    }
                }
            }
        }

        // 초집중 강화를 클릭했을 경우
        public void ClickReinforceHighest()
        {
            if (!messageBoxProgress.activeSelf && !minigame.activeSelf && !messageBoxResult.activeSelf)
            {
                if (currentEquipment != null && index < indexMax)
                {
                    if (IsReinforcementPossible(ReinforcementType.highest))
                    {
                        // 초집중 강화가 가능한 경우 (재화가 충분한 경우)

                        // 강화 타입을 초집중 강화로 변경
                        equipReinforcementType = ReinforcementType.highest;

                        // 현재 강화 정보에 맞춰 강화 진행 메세지 박스 세팅 후 메세지 박스 활성화
                        messageBoxProgress.GetComponent<ReinforcementMessageBoxProgress>().SetMessageBoxProgressText(equipReinforcementType, currentReinforcementValues, index);
                        messageBoxProgress.SetActive(true);
                    }
                    else
                    {
                        Debug.Log("재화 부족");
                    }
                }
            }
        }

        // 강화 진행 메세지 박스에서 강화를 계속한다고 선택했을 때
        public void ContinueReinforceProgressButton()
        {
            // 강화 진행 메세지 박스 비활성화
            messageBoxProgress.SetActive(false);

            // 미니게임 창 활성화 (미니게임 진행)
            minigame.SetActive(true);
        }

        // 강화 진행 메세지 박스에서 강화를 계속하지 않는다고 선택했을 때
        public void CancleReinforceProgressButton()
        {
            // 강화 타입을 아무것도 아님으로 초기화
            equipReinforcementType = ReinforcementType.none;

            // 강화 진행 메세지 박스 비활성화
            messageBoxProgress.SetActive(false);
        }

        // 강화를 진행함 (미니게임이 끝남)
        public void ProgressReinforcement(float additionProbability)
        {
            // 미니게임 창 비활성화
            minigame.SetActive(false);

            if (IsReinforcepSuccess(additionProbability))
            {
                // 미니게임으로 추가된 확률을 더해 강화가 성공한 경우

                // 강화 성공 처리
                ReinforceSuccess(equipReinforcementType);
            }
            else
            {
                // 강화 실패 처리
                ReinforceFail(equipReinforcementType);
            }
        }

        // 강화가 성공했는지 확인
        bool IsReinforcepSuccess(float additionProbability)
        {
            // 장비의 현재 강화 성공 확률에 미니게임으로 추가된 확률을 더함 (최종 강화 확률)                                           
            float finalProbability = currentReinforcementValues.Probability[index];
            finalProbability += additionProbability;

            // 0부터 100까지의 실수를 랜덤으로 하나 뽑음
            float randNum = Random.Range(0.0f, 100.0f);

            Debug.Log("probability: " + finalProbability + " / randNum: " + randNum);                                       

            // 랜덤으로 뽑은 값이 최종 강화 성공 확률 이하인 경우 강화 성공, 아닌 경우 강화 실패
            return (randNum <= finalProbability) ? true : false;
        }

        // 강화 성공 처리
        public void ReinforceSuccess(ReinforcementType type)
        {
            // 강화 수치만금 장비 능력치 증가
            AddReinforcementStatusValues();
            // 현재 강화 상태를 등급 상승으로 변경
            equipReinforcementState = ReinforcementState.up;

            // 강화 비용 만큼 보유 재화에서 차감
            DeleteMoney(equipReinforcementType);

            // 현재 장비아이템의 등급 상승
            currentEquipment.ItemGrade++;

            // 장비 강화 정보 세팅
            SetEquipmentInfo();

            // 강화 결과 메세지 박스를 세팅 후 메세지 박스 활성화
            messageBoxResult.GetComponent<ReinforcementMessageBoxResult>().SetMessageBoxResult(currentEquipment, equipReinforcementState);
            messageBoxResult.SetActive(true);

            // 현재 강화 상태 아무것도 아님으로 초기화
            equipReinforcementState = ReinforcementState.none;
        }

        // 강화 실패 처리
        public void ReinforceFail(ReinforcementType type)
        {
            if (currentEquipment.ItemGrade > 0)
            {
                // 장비의 강화 등급이 0 이상인 경우

                // 강화 수치만큼 장비 능력치 하락
                SubtractReinforcementStatusValues();

                // 현재 강화 상태를 등급 하락으로 변경
                equipReinforcementState = ReinforcementState.down;
            }
            else
            {
                // 장비의 강화 등급이 0인 경우

                // 현재 강화 상태를 등급 유지로 변경
                equipReinforcementState = ReinforcementState.keep;
            }

            // 강화 비용 만큼 보유 재화에서 차감
            DeleteMoney(equipReinforcementType);

            // 현재 장비아이템의 등급 상승
            currentEquipment.ItemGrade--;

            // 장비 강화 정보 세팅
            SetEquipmentInfo();

            // 강화 결과 메세지 박스를 세팅 후 메세지 박스 활성화
            messageBoxResult.GetComponent<ReinforcementMessageBoxResult>().SetMessageBoxResult(currentEquipment, equipReinforcementState);
            messageBoxResult.SetActive(true);

            // 현재 강화 상태 아무것도 아님으로 초기화
            equipReinforcementState = ReinforcementState.none;
        }

        // 강화 수치만금 장비 능력치 증가 (이후 등급이 되었을 때 증가된 수치만큼 증가)
        void AddReinforcementStatusValues()
        {
            currentEquipment.ReinforcementStatus.AddStatus
                        (currentReinforcementValues.Hp[index + 1], currentReinforcementValues.Attack[index + 1], currentReinforcementValues.Defence[index + 1], currentReinforcementValues.Speed[index + 1]);
        }

        // 강화 수치만큼 장비 능력치 하락 (현재 등급이 되었을 때 증가된 수치만큼 감소)
        void SubtractReinforcementStatusValues()
        {
            currentEquipment.ReinforcementStatus.AddStatus
                            (-currentReinforcementValues.Hp[index], -currentReinforcementValues.Attack[index], -currentReinforcementValues.Defence[index], -currentReinforcementValues.Speed[index]);
        }

        // 재화 삭제
        void DeleteMoney(ReinforcementType type)
        {
            switch (type)
            {
                case ReinforcementType.normal:
                    {
                        // 기본 강화의 경우
                        // 해당 비용만큼 산소와 철분 차감
                        GameManager.instance.Oxygen -= currentReinforcementValues.Oxygen[index];
                        GameManager.instance.Iron -= currentReinforcementValues.Iron[index];
                        break;
                    }
                case ReinforcementType.high:
                    {
                        // 집중 강화의 경우
                        // 해당 비용만큼 산소와 철분, 미네랄 차감
                        GameManager.instance.Oxygen -= currentReinforcementValues.Oxygen[index];
                        GameManager.instance.Iron -= currentReinforcementValues.Iron[index];
                        GameManager.instance.Mineral -= currentReinforcementValues.Mineral[index];

                        break;
                    }
                case ReinforcementType.highest:
                    {
                        // 초집중 강화의 경우
                        // 해당 비용만큼 산소와 철분, 미네랄 차감
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

            // 금액이 차감되었으므로 유저 인벤토리의 게임 재화 재세팅
            UserInventoryMoneyManager.instance.SetUserInventoryMoney();
        }

        // 강화가 가능한지 확인
        bool IsReinforcementPossible(ReinforcementType type)
        {
            switch (type)
            {
                case ReinforcementType.normal:
                    {
                        // 기본 강화의 경우
                        // 현재 보유한 산소와 철분이 강화에 쓰이는 수량 이상인 경우 ture, 재화가 부족한 경우 false
                        return (GameManager.instance.Oxygen >= currentReinforcementValues.Oxygen[index]
                                    && GameManager.instance.Iron >= currentReinforcementValues.Iron[index]) ? true : false;
                    }
                case ReinforcementType.high:
                    {
                        // 집중 강화의 경우
                        // 현재 보유한 산소와 철분, 미네랄이 강화에 쓰이는 수량 이상인 경우 ture, 재화가 부족한 경우 false
                        return (GameManager.instance.Oxygen >= currentReinforcementValues.Oxygen[index]
                                    && GameManager.instance.Iron >= currentReinforcementValues.Iron[index]
                                        && GameManager.instance.Mineral >= currentReinforcementValues.Mineral[index]) ? true : false;
                    }
                case ReinforcementType.highest:
                    {
                        // 초집중 강화의 경우
                        // 현재 보유한 산소와 철분, 미네랄이 강화에 쓰이는 수량 이상인 경우 ture, 재화가 부족한 경우 false
                        // 강화에 쓰이는 미네랄은 집중 강화의 2배
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

        // X 버튼을 클릭했을 때
        public void ClickButtonX()
        {
            // 강화 UI 오브젝트 제거 후 중간 거점 기본 UI 활성화
            Destroy(this.gameObject);
            MiddleBaseUIManager.instance.ActiveMiddleBaseInitUI();
        }

        // 강화 성공 버튼을 눌렀을 경우 (치트키)
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

        // 강화 실패 버튼을 눌렀을 경우 (치트키)
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

        // 장비 강화 상태 초기화 버튼을 눌렀을 경우 (치트키)
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

        // 산소 수급 버튼을 눌렀을 경우 (치트키)
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

        // 철분 수급 버튼을 눌렀을 경우 (치트키)
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

        // 미네랄 수급 버튼을 눌렀을 경우 (치트키)
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
