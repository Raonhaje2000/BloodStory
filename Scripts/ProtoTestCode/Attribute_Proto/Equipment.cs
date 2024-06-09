using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    // 장비 정보
    public enum Part { sword = 0, helmet = 1, armor = 2, pants = 3, boot = 4 };

    Part equipPart;

    Sprite icon;

    string name;
    int grade;

    // 장비 아이템 기본 능력치
    float hp;
    float attack;
    float defence;
    float moveSpeed;

    // 속성으로 오른 능력치
    float attributeHp;
    float attributeAttack;
    float attributeDefence;
    float attributeMoveSpeed;

    // 등록된 속성들
    List<Attribute> attributes;
    List<int> attributeValues;
    int attributeLimit;

    public Equipment(Part equipPart, Sprite icon, string name, int grade, float hp, float attack, float defence, float moveSpeed)
    {
        this.equipPart = equipPart;

        this.icon = icon;

        this.name = name;
        this.grade = grade;

        this.hp = hp;
        this.attack = attack;
        this.defence = defence;
        this.moveSpeed = moveSpeed;

        this.attributeHp = 0.0f;
        this.attributeAttack = 0.0f;
        this.attributeDefence = 0.0f;
        this.attributeMoveSpeed = 0.0f;

        this.attributes = new List<Attribute>();
        this.attributeValues = new List<int>();
        this.attributeLimit = 4;
    }

    public Part EquipPart
    {
        get { return equipPart; }
        set { equipPart = value; }
    }

    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public string Name
    { 
        get { return name; }
        set { name = value; }
    }

    public int Grade
    { 
        get { return grade; }
        set { grade = value; }
    }

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public float Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public float Defence
    {
        get { return defence; }
        set { defence = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float AttributeHp
    {
        get { return attributeHp; }
        set { attributeHp = value; }
    }

    public float AttributeAttack
    {
        get { return attributeAttack; }
        set { attributeAttack = value; }
    }

    public float AttributeDefence
    {
        get { return attributeDefence; }
        set { attributeDefence = value; }
    }

    public float AttributeMoveSpeed
    {
        get { return attributeMoveSpeed; }
        set { attributeMoveSpeed = value; }
    }

    public List<Attribute> Attributes
    {
        get { return attributes; }
        set { attributes = value; }
    }

    public List<int> AttributeValues
    {
        get { return attributeValues; }
        set { attributeValues = value; }
    }

    public int AttributeLimit
    {
        get { return attributeLimit; } 
    }
}
