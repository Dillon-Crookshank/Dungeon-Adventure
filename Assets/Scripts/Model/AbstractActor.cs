namespace DefaultNamespace;

internal abstract class AbstractActor
{
    private String name;

    // represent these as an ints but use doubles underneath for more accurate values.
    private double health;

    private double attack;

    private double defence;

    private double mana;

    private int initiative;

    private bool deathStatus;

    // may want to refactor this to map specific changes to values rather than a list
    private List<Buff> buffs;

    // may want to add /which/ attack as a parameter to keep it generic
    private void Attack(AbstractActor theDefender)
    {
        // put attack calculation here
    }

    private void Move()
    {
        // allow movement to an empty adjacent tile in party order
    }

    private bool IsAlive()
    {
        return deathStatus;
    }

    // may need to pass the specific buff intended to use
    private Buff()
    {
        // do buff effect here (merged defend here as well)
    }
}
