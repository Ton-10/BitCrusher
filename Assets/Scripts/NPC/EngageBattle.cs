
public class EngageBattle : Action
{
    public override void StartAction()
    {
        base.StartAction();
        Player.GetComponent<CombatManager>().StartCombat(gameObject);
    }
}
