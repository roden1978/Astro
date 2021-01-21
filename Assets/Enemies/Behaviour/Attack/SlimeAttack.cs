using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime Attack", menuName = "Enemies/Behaviour/Attack/Slime")]
public class SlimeAttack : ScriptableObject, IAttackable
{
    [SerializeField]
    [Tooltip("Дистанция при которой начинается атака")]
    private float attackDistance;
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}
