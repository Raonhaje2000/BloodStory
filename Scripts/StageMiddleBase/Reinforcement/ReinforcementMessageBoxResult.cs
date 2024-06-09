using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class ReinforcementMessageBoxResult : MonoBehaviour
    {
        Transform reinforcementEffectPoint;             // 강화 이펙트가 나오는 위치
 
        GameObject successEffect;                       // 성공 이펙트 프리팹
        GameObject failEffect;                          // 실패 이펙트 프리팹

        TextMeshProUGUI resultSuccessOrFailText;        // 성공 또는 실패했음을 말해주는 텍스트
        TextMeshProUGUI resultEquipStateText;           // 장비의 상태 (등급 상승, 유지, 하락)을 말해주는 텍스트

        Image resultEquipmentIconImage;                 // 장비의 아이콘 이미지
        TextMeshProUGUI resultEquipmentNameText;        // 장비의 이름 텍스트
        TextMeshProUGUI resultEquipmentGradeText;       // 장비의 등급 텍스트

        TextMeshProUGUI resultEquipmentDesHpValue;      // 장비의 체력 수치
        TextMeshProUGUI resultEquipmentDesAttackValue;  // 장비의 공격력 수치
        TextMeshProUGUI resultEquipmentDesDefenceValue; // 장비의 방어력 수치
        TextMeshProUGUI resultEquipmentDesSpeedValue;   // 장비의 이동 속도 수치

        GameObject effect;                              // 강화 이펙트

        private void Awake()
        {
            // 관련된 오브젝트 불러오기
            LoadObejcts();
        }

        void Start()
        {

        }

        // 관련된 오브젝트 불러오기
        void LoadObejcts()
        {
            reinforcementEffectPoint = GameObject.Find("ReinforcementMessageBoxResultPoint").transform;

            successEffect = Resources.Load<GameObject>("RSA/Prefabs/Effect/SuccessEffect");
            failEffect = Resources.Load<GameObject>("RSA/Prefabs/Effect/FailEffect");

            resultSuccessOrFailText = GameObject.Find("ReinforcementMessageBoxResultSuccessOrFailText").GetComponent<TextMeshProUGUI>();
            resultEquipStateText = GameObject.Find("ReinforcementMessageBoxResultEquipStateText").GetComponent<TextMeshProUGUI>();

            resultEquipmentIconImage = GameObject.Find("ReinforcementMessageBoxResultEquipmentIconImage").GetComponent<Image>();
            resultEquipmentNameText = GameObject.Find("ReinforcementMessageBoxResultEquipmentNameText").GetComponent<TextMeshProUGUI>();
            resultEquipmentGradeText = GameObject.Find("ReinforcementMessageBoxResultEquipmentGradeText").GetComponent<TextMeshProUGUI>();

            resultEquipmentDesHpValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesHpValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesAttackValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesAttackValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesDefenceValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesDefenceValue").GetComponent<TextMeshProUGUI>();
            resultEquipmentDesSpeedValue = GameObject.Find("ReinforcementMessageBoxResultEquipmentDesSpeedValue").GetComponent<TextMeshProUGUI>();

            effect = null;
        }


        // 강화 결과 메세지 박스 세팅
        public void SetMessageBoxResult(EquipmentItem equip, ReinforcementManager.ReinforcementState state)
        {
            // 강화 이펙트(파티클) 생성
            effect = (state == ReinforcementManager.ReinforcementState.up)
                ? Instantiate(successEffect, reinforcementEffectPoint.position, reinforcementEffectPoint.rotation)
                    : Instantiate(failEffect, reinforcementEffectPoint.position, reinforcementEffectPoint.rotation);
            effect.transform.SetParent(reinforcementEffectPoint);
            effect.transform.SetAsLastSibling();

            string stateString
                = (state == ReinforcementManager.ReinforcementState.up) ? "상승했어"
                    : (state == ReinforcementManager.ReinforcementState.keep) ? "유지됐어" : "하락했어";

            if (state == ReinforcementManager.ReinforcementState.up) resultSuccessOrFailText.text = "와! 강화 성공이야!";
            else resultSuccessOrFailText.text = "미안해. 강화 실패했어.";

            resultEquipStateText.text = "장비 아이템의 등급이 " + stateString + ".";

            resultEquipmentIconImage.sprite = equip.ItemIcon;
            resultEquipmentNameText.text = equip.ItemName;
            resultEquipmentGradeText.text = string.Format("{0}(+{1})", GetRankString(equip.ItemRank), equip.ItemGrade);

            resultEquipmentDesHpValue.text = (equip.InitStatus.Hp + equip.ReinforcementStatus.Hp + equip.AttributeStatus.Hp).ToString();
            resultEquipmentDesAttackValue.text = (equip.InitStatus.Attack + equip.ReinforcementStatus.Attack + equip.AttributeStatus.Attack).ToString();
            resultEquipmentDesDefenceValue.text = (equip.InitStatus.Defence + equip.ReinforcementStatus.Defence + equip.AttributeStatus.Defence).ToString();
            resultEquipmentDesSpeedValue.text = (equip.InitStatus.Speed + equip.ReinforcementStatus.Speed + equip.AttributeStatus.Speed).ToString();
        }

        // X 버튼을 눌렀을 때
        public void ClickButtonX()
        {
            // 이펙트 오브젝트 제거
            Destroy(effect.gameObject);

            // 해당 메세지 창 비활성화
            gameObject.SetActive(false);
        }

        // 랭크에 따른 문자열 구하기
        string GetRankString(EquipmentItem.Rank rank)
        {
            switch (rank)
            {
                case Item.Rank.normal:
                    return "노말";
                case Item.Rank.rare:
                    return "레어";
                case Item.Rank.unique:
                    return "유니크";
                case Item.Rank.legend:
                    return "레전드";
                default:
                    return "등급 없음";
            }
        }
    }
}
