using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Enemies/Behaviour/Attack")]
public class BaseAttack : ScriptableObject, IAttackable
{
    [SerializeField]
    [Tooltip("Дистанция при которой начинается атака")]
    private float attackDistance;
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
