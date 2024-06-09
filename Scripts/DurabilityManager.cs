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

        // ������ ����
        Image durabilityImageWeapon;                // ���� ������ �̹���
        Image durabilityImageDefence;               // �� ������ �̹���
        TextMeshProUGUI durabilityText;             // ������ ���� �ؽ�Ʈ
        TextMeshProUGUI durabilityTextValueWeapon;  // ���� ������ ��ġ �ؽ�Ʈ
        TextMeshProUGUI durabilityTextValueDefence; // �� ������ ��ġ �ؽ�Ʈ

        [SerializeField]
        Sprite[] durabilityWeaponSprites;  // ���� ������ �̹��� ���ҽ�
        [SerializeField]
        Sprite[] durabilityDefenceSprites; // �� ������ �̹��� ���ҽ�

        float subtractWeapon;  // ���� �������� ���̴� ��ġ
        float subtractDefence; // �� �������� ���̴� ��ġ

        private void Awake()
        {
            if (instance == null) instance = this;

            // ���� ���ҽ� �� ������Ʈ�� �ҷ�����
            LoadObejcts();
        }

        void Start()
        {
            // �ʱ�ȭ
            Initialize();
        }

        // ���� ���ҽ� �� ������Ʈ�� �ҷ�����
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

        // �ʱ�ȭ
        void Initialize()
        {
            subtractWeapon = 2.0f;
            subtractDefence = 2.0f;

            // ������ ���� (�ۼ�Ʈ ó�� ��)
            SetDurability();
        }

        // ������ UI Ȱ��ȭ ���� ����
        void SetActiveDurability(bool active)
        {
            durabilityImageWeapon.gameObject.SetActive(active);
            durabilityImageDefence.gameObject.SetActive(active);

            durabilityText.gameObject.SetActive(active);
            durabilityTextValueWeapon.gameObject.SetActive(active);
            durabilityTextValueDefence.gameObject.SetActive(active);
        }

        // ������ UI ����
        void SetDurabilityUI(Sprite weapon, Sprite defence, float weaponPercent, float defencePercent)
        {
            durabilityImageWeapon.sprite = weapon;
            durabilityImageDefence.sprite = defence;

            durabilityTextValueWeapon.text = weaponPercent.ToString();
            durabilityTextValueDefence.text = defencePercent.ToString();

            // ���⳪ �� �� �ϳ��� �������� 20% ���Ϸ� ���� ��� ������ UI Ȱ��ȭ, �ƴ� ��� ��Ȱ��ȭ
            if (weaponPercent <= 20.0f || defencePercent <= 20.0f) SetActiveDurability(true);
            else SetActiveDurability(false);
        }

        // ���� ����(�ʵ忡�� ���͸� óġ���� ��) ���� ������ ó��
        public void UseWeapon()
        {
            // ���� ���� ��ġ��ŭ ���� ������ ����
            GameManager.instance.DurabilityWeapon -= subtractWeapon;

            // ���� �������� 0���� ���� ��� 0���� ����
            if (GameManager.instance.DurabilityWeapon < 0) GameManager.instance.DurabilityWeapon = 0;

            // ������ ���� (�ۼ�Ʈ ó�� ��)
            SetDurability();
        }

        // �� ����(�������� ���� ���� ��) �� ������ ó��
        public void UseDefence()
        {
            // �� ���� ��ġ��ŭ �� ������ ����
            GameManager.instance.DurabilityDefence -= subtractDefence;

            // �� �������� 0���� ���� ��� 0���� ����
            if (GameManager.instance.DurabilityDefence < 0) GameManager.instance.DurabilityDefence = 0;

            // ������ ���� (�ۼ�Ʈ ó�� ��)
            SetDurability();
        }

        // ������ ���� (�ۼ�Ʈ ó�� ��)
        public void SetDurability()
        {
            // ����� �� ������ ��ġ�� ���� �̹��� ����
            Sprite weapon = SetWeaponSprite(GameManager.instance.DurabilityWeapon);
            Sprite defence = SetDefenceSprite(GameManager.instance.DurabilityDefence);

            // ����� �� ������ �ۼ�Ʈ ��ġ ��� (���� ��� ������ / ������ �ִ�ġ * 100)
            float weaponPercent = GameManager.instance.DurabilityWeapon / GameManager.instance.DurabilityMax * 100.0f;
            float defencePercent = GameManager.instance.DurabilityDefence / GameManager.instance.DurabilityMax * 100.0f;

            Debug.Log("Weapon: " + weaponPercent + " / Defence: " + defencePercent);

            // ������ UI ����
            SetDurabilityUI(weapon, defence, weaponPercent, defencePercent);
        }

        // ���� ������ ��ġ�� ���� �̹��� ����
        Sprite SetWeaponSprite(float currentDurability)
        {
            // �ۼ�Ʈ ��� (���� ��� ������ / ������ �ִ�ġ * 100)
            float percent = currentDurability / GameManager.instance.DurabilityMax * 100.0f;

            if (percent > 20) return durabilityWeaponSprites[0];      // 100���� 20�ʰ�: ���
            else if (percent > 10) return durabilityWeaponSprites[1]; //  20���� 10�ʰ�: �����
            else if (percent > 0) return durabilityWeaponSprites[2];  //  10����  0�ʰ�: ������
            else return durabilityWeaponSprites[3];                   //  0: ȸ��
        }

        // �� ������ ��ġ�� ���� �̹��� ����
        Sprite SetDefenceSprite(float currentDurability)
        {
            // �ۼ�Ʈ ��� (���� ��� ������ / ������ �ִ�ġ * 100)
            float percent = currentDurability / GameManager.instance.DurabilityMax * 100.0f;

            if (percent > 20) return durabilityDefenceSprites[0];      // 100���� 20�ʰ�: ���
            else if (percent > 10) return durabilityDefenceSprites[1]; //  20���� 10�ʰ�: �����
            else if (percent > 0) return durabilityDefenceSprites[2];  //  10����  0�ʰ�: ������
            else return durabilityDefenceSprites[3];                   //  0: ȸ��
        }

    }
}
