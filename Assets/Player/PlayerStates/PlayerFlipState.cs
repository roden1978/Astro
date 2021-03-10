public class PlayerFlipState : BaseState
{
    private readonly Player player;
    private System.Type prevState;
    
    public PlayerFlipState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;
        player.MovingRight = !player.MovingRight;
        transform.Rotate(0f, 180f, 0f);
        
        return prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(PlayerFlipState);
    }
}
