using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Turret Settings", menuName = "Enemies/Settings/Turret")]
public class TurretSettings : ScriptableObject
{
    [SerializeField] public float rotateSpeed;
    [SerializeField] public float maxRotateAngel;
}
