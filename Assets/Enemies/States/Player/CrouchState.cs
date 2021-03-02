public class CrouchState : BaseState
{
    private Player _player;
    private System.Type _prevState;
    
    public CrouchState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        
        if (_prevState == null) _prevState = _player.GetComponent<StateMashine>().GetPrevState;
        //UI
        if (!_player.UICrouchButton)
        {
            _player.animator.SetBool("crouch", false);
            return _prevState;
        }
       
        //keyboard
        /*if (!_player.UICrouchButton && (int) _player.Direction.y == 1 || (int) _player.Direction.y == -1)
        {
            _player.animator.SetBool("crouch", false);
            return _prevState;
        }*/
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(CrouchState);
    }
}
