using UnityEngine;

//[CreateAssetMenu(fileName = "New Slime Attack", menuName = "Enemies/Behaviour/Attack/Slime")]
public class SlimeAttack : IAttackable
{
    private float attackDistance;
    public void Attack()
    {
        Debug.Log("Attack");
    }
}
