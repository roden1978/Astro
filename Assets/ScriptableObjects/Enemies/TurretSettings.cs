using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Turret Settings", menuName = "Enemies/Settings/Turret")]
public class TurretSettings : ScriptableObject
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxRotateAngel;
    [SerializeField] private int maxHealth;

    public int MaxHealth => maxHealth;
    public float RotateSpeed => rotateSpeed;
    public float MAXRotateAngel => maxRotateAngel;
}
