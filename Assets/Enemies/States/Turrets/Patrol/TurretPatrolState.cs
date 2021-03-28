using UnityEngine;

public class TurretPatrolState : BaseState
{
    private Turret _turret;
    public TurretPatrolState(Turret turret) : base(turret.gameObject)
    {
        _turret = turret;
        
        if (_turret.player == null)
        {
            _turret.SetTarget(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    public override System.Type Tick()
    {
       
            _turret.turretHead.RotateAround(_turret.turretHead.position,
                Vector3.forward,
                _turret.turretSettings.RotateSpeed * Time.deltaTime * _turret.UpDirection);
        
        if (_turret.turretHead.rotation.z >= _turret.turretSettings.MAXRotateAngel)
            return typeof(ChangeRotateDirectionState);
        
        if (_turret.turretHead.rotation.z <= -_turret.turretSettings.MAXRotateAngel)
            return typeof(ChangeRotateDirectionState);
        
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(TurretPatrolState);
    }
  
}
