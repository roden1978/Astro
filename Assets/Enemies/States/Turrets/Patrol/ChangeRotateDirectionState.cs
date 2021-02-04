public class ChangeRotateDirectionState : BaseState
{
    private Turret _turret;
    private System.Type _prevState;
    
    public ChangeRotateDirectionState(Turret turret) : base(turret.gameObject)
    {
        _turret = turret;
    }

    public override System.Type Tick()
    {
        if (_prevState == null) _prevState = _turret.GetComponent<StateMashine>().GetPrevState;
        
        _turret.UpDirection = _turret.UpDirection * -1;

        return _prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(ChangeRotateDirectionState);
    }
}
