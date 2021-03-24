using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon Data", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("float")]
    public float m_shootRange;
    public float m_fireRate;
    public float m_reloadTime;

    [Header("int")]
    public int m_damage;
    public int m_ammoAmount;

    
}