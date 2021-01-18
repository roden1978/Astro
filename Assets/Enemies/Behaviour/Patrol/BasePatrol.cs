using UnityEngine;

public class BasePatrol : ScriptableObject, IPatrolable
{
    [SerializeField]
    [Tooltip("Дистанция патрулирования")]
    private float patrolDistance;


    public void Patrol()
    {
        throw new System.NotImplementedException();
    }
}
