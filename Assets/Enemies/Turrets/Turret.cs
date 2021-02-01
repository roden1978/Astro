using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject player { get; private set; }

    public Transform turretHead;
    public TurretSettings turretSettings;
    
    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;

    private void Awake()
    {
        InitializeStateMashine();
    }

    /*private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "head")
            {
                turretHead = gameObject.transform.GetChild(i);
            }
        }
        
    }*/

    /*private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
    }*/

    private void InitializeStateMashine()
    {
        states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(TurretPatrolState), new TurretPatrolState(this)}
        };
        
        GetComponent<StateMashine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }
}
