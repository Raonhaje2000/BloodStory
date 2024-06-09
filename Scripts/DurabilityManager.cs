using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RSA
{
    public class DurabilityManager : MonoBehaviour
    {
        public static DurabilityManager instance;

        // 내구도 관련
        Image durabilityImageWeapon;                // 무기 내구도 이미지
        Image durabilityImageDefence;               // 방어구 내구도 이미지
        TextMeshProUGUI durabilityText;             // 내구도 제목 텍스트
        TextMeshProUGUI durabilityTextValueWeapon;  // 무기 내구도 수치 텍스트
        TextMeshProUGUI durabilityTextValueDefence; // 방어구 내구도 수치 텍스트

        [SerializeField]
        Sprite[] durabilityWeaponSprites;  // 무기 내구도 이미지 리소스
        [SerializeField]
        Sprite[] durabilityDefenceSprites; // 방어구 내구도 이미지 리소스

        float subtractWeapon;  // 무기 내구도가 깎이는 수치
        float subtractDefence; // 방어구 내구도가 깎이는 수치

        private void Awake()
        {
            if (instance == null) instance = this;

            // 관련 리소스 및 오브젝트들 불러오기
            LoadObejcts();
        }

        void Start()
        {
            // 초기화
            Initialize();
        }

        // 관련 리소스 및 오브젝트들 불러오기
        void LoadObejcts()
        {
            durabilityWeaponSprites = Resources.LoadAll<Sprite>("Images/DurabilityUI/Weapon");
            durabilityDefenceSprites = Resources.LoadAll<Sprite>("Images/DurabilityUI/Defence");

            durabilityImageWeapon = GameObject.Find("DurabilityImageWeapon").GetComponent<Image>();
            durabilityImageDefence = GameObject.Find("DurabilityImageDefence").GetComponent<Image>();
            durabilityText = GameObject.Find("DurabilityText").GetComponent<TextMeshProUGUI>();

            durabilityTextValueWeapon = GameObject.Find("DurabilityTextValueWeapon").GetComponent<TextMeshProUGUI>();
            durabilityTextValueDefence = GameObject.Find("DurabilityTextValueDefence").GetComponent<TextMeshProUGUI>();
        }

        // 초기화
        void Initialize()
        {
            subtractWeapon = 2.0f;
            subtractDefence = 2.0f;

            // 내구도 세팅 (퍼센트 처리 등)
            SetDurability();
        }

        // 내구도 UI 활성화 여부 세팅
        void SetActiveDurability(bool active)
        {
            durabilityImageWeapon.gameObject.SetActive(active);
            durabilityImageDefence.gameObject.SetActive(active);

            durabilityText.gameObject.SetActive(active);
            durabilityTextValueWeapon.gameObject.SetActive(active);
            durabilityTextValueDefence.gameObject.SetActive(active);
        }

        // 내구도 UI 세팅
        void SetDurabilityUI(Sprite weapon, Sprite defence, float weaponPercent, float defencePercent)
        {
            durabilityImageWeapon.sprite = weapon;
            durabilityImageDefence.sprite = defence;

            durabilityTextValueWeapon.text = weaponPercent.ToString();
            durabilityTextValueDefence.text = defencePercent.ToString();

            // 무기나 방어구 중 하나라도 내구도가 20% 이하로 남은 경우 내구도 UI 활성화, 아닌 경우 비활성화
            if (weaponPercent <= 20.0f || defencePercent <= 20.0f) SetActiveDurability(true);
            else SetActiveDurability(false);
        }

        // 무기 사용시(필드에서 몬스터를 처치했을 때) 무기 내구도 처리
        public void UseWeapon()
        {
            // 무기 감소 수치만큼 무기 내구도 감소
            GameManager.instance.DurabilityWeapon -= subtractWeapon;

            // 무기 내구도가 0보다 적은 경우 0으로 수정
            if (GameManager.instance.DurabilityWeapon < 0) GameManager.instance.DurabilityWeapon = 0;

            // 내구도 세팅 (퍼센트 처리 등)
            SetDurability();
        }

        // 방어구 사용시(보스에게 공격 당할 떄) 방어구 내구도 처리
        public void UseDefence()
        {
            // 방어구 감소 수치만큼 방어구 내구도 감소
            GameManager.instance.DurabilityDefence -= subtractDefence;

            // 방어구 내구도가 0보다 적은 경우 0으로 수정
            if (GameManager.instance.DurabilityDefence < 0) GameManager.instance.DurabilityDefence = 0;

            // 내구도 세팅 (퍼센트 처리 등)
            SetDurability();
        }

        // 내구도 세팅 (퍼센트 처리 등)
        public void SetDurability()
        {
            // 무기와 방어구 내구도 수치에 맞춰 이미지 결정
            Sprite weapon = SetWeaponSprite(GameManager.instance.DurabilityWeapon);
            Sprite defence = SetDefenceSprite(GameManager.instance.DurabilityDefence);

            // 무기와 방어구 내구도 퍼센트 수치 계산 (현재 장비 내구도 / 내구도 최대치 * 100)
            float weaponPercent = GameManager.instance.DurabilityWeapon / GameManager.instance.DurabilityMax * 100.0f;
            float defencePercent = GameManager.instance.DurabilityDefence / GameManager.instance.DurabilityMax * 100.0f;

            Debug.Log("Weapon: " + weaponPercent + " / Defence: " + defencePercent);

            // 내구도 UI 세팅
            SetDurabilityUI(weapon, defence, weaponPercent, defencePercent);
        }

        // 무기 내구도 수치에 맞춰 이미지 결정
        Sprite SetWeaponSprite(float currentDurability)
        {
            // 퍼센트 계산 (현재 장비 내구도 / 내구도 최대치 * 100)
            float percent = currentDurability / GameManager.instance.DurabilityMax * 100.0f;

            if (percent > 20) return durabilityWeaponSprites[0];      // 100이하 20초과: 흰색
            else if (percent > 10) return durabilityWeaponSprites[1]; //  20이하 10초과: 노란색
            else if (percent > 0) return durabilityWeaponSprites[2];  //  10이하  0초과: 빨간색
            else return durabilityWeaponSprites[3];                   //  0: 회색
        }

        // 방어구 내구도 수치에 맞춰 이미지 결정
        Sprite SetDefenceSprite(float currentDurability)
        {
            // 퍼센트 계산 (현재 장비 내구도 / 내구도 최대치 * 100)
            float percent = currentDurability / GameManager.instance.DurabilityMax * 100.0f;

            if (percent > 20) return durabilityDefenceSprites[0];      // 100이하 20초과: 흰색
            else if (percent > 10) return durabilityDefenceSprites[1]; //  20이하 10초과: 노란색
            else if (percent > 0) return durabilityDefenceSprites[2];  //  10이하  0초과: 빨간색
            else return durabilityDefenceSprites[3];                   //  0: 회색
        }

    }
}
