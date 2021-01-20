using UnityEngine;
[CreateAssetMenu(fileName = "New Ground Patrol", menuName = "Enemies/Behaviour/GroundPatrol")]
public class SlimePatrol : ScriptableObject, IPatrolable
{
    [SerializeField]
    [Tooltip("Дистанция патрулирования")]
    private float patrolDistance;


    public void Patrol()
    {
    }
}
