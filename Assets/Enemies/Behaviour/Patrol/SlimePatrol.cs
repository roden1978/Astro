using UnityEngine;
//[CreateAssetMenu(fileName = "New Ground Patrol", menuName = "Enemies/Behaviour/GroundPatrol")]
public class SlimePatrol : IPatrolable
{
    public SlimePatrol(GameObject gameObject)
    {
        
    }
    public void Patrol()
    {
        Debug.Log("Patrol");
    }

}
