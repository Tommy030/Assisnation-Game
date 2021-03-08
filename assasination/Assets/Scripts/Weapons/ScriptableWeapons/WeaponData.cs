using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon Data", order = 0)]
public class WeaponData : ScriptableObject
{
    public float m_shootRange;
    public float m_damage;
    public float m_fireRate;
}