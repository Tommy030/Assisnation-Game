using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon Data", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("float")]
    public float m_shootRange;
    public float m_fireRate;

    [Header("int")]
    public int m_damage;
}