using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour
{
    public GameObject player { get; private set; }

    public Transform turretHead;
    public TurretSettings turretSettings;
    private float upDirection;
    
    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;

    private void Awake()
    {
        InitializeStateMashine();
    }

    private void Start()
    {
        upDirection = Random.Range(-1, 1);
        if (UpDirection == 0) UpDirection = 1;
    }

    /*private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
    }*/

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
        get => upDirection;
        set => upDirection = value;
    }
}
