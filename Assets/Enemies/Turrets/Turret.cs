using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : AEnemy
{
    public GameObject player { get; private set; }

    public Transform turretHead;
    public TurretSettings turretSettings;
    private float upDirection;
    
    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;

   private void Start()
    {
        InitializeStateMashine();
        health = turretSettings.MaxHealth;
    }

    private void InitializeStateMashine()
    {
        states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(TurretPatrolState), new TurretPatrolState(this)},
            {typeof(ChangeRotateDirectionState), new ChangeRotateDirectionState(this)}
        };
        
        GetComponent<StateMashine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    public float UpDirection
    {
        get
        {
            upDirection = Random.Range(-1, 1);
            return upDirection == 0 ? 1 : upDirection;
        }
        set => upDirection = value;
    }
    
}
