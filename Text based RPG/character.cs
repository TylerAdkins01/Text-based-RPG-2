
public interface IGold
{
    int Gold { set; get; }
}

public abstract class Character
{
    public int Health { get; set; }
    
    public abstract void getStats();
    public static bool leave;
}

public class Player : Character, IGold
{
    public int Gold { get; set; }
    public bool hasBomb { get; set; }
    public bool hasFireball { get; set; }
    public bool hasFrost { get; set; }

    public Player()
    {
        Health = 10;
        Gold = 1;
        hasBomb = false;
        hasFireball = false;
        hasFrost = false;
        leave = true;
    }

    public static int Strength { get; set; } = 0;

    public override void getStats()
    {
        string temp = $"Gold: {Gold}\nHealth: {Health}\n";

        if (hasBomb && hasFireball)
        {
            temp += "You have a bomb and a fireball!\n";
        }
        else if (hasFireball)
        {
            temp += "You have a fireball!\n";
        }
        else if (hasBomb)
        {
            temp += "You have a bomb!\n";
        }

        if (hasFrost)
        {
            temp += "You have a Frost Scroll!\n";
        }

        Console.WriteLine(temp);
        Task.Delay(1000);

        return;
    }
}

public class Enemy : Character, IGold
{
    public int Gold { get; set; }
    public int powerMax { get; init; }
    public int powerMin { get; init; }
    public static int EnemiesDefeated = 0;

    public Enemy(int HP, int Max, int Min)
    {
        Health = HP;
        powerMax = Max;
        powerMin = Min;
        
    }

    public override void getStats()
    {
        Console.WriteLine($"Enemy Health: {Health}");
        Console.WriteLine($"Enemy Power Range: {powerMin} - {powerMax}");
    }

    public virtual string TakeDamage(int DMG)
    {
        bool wasAlive = Health > 0;
        Health -= DMG;
        if (Health > 0)
        {
            return $"{DMG} damage was dealt";
        }
        else
        {
            if (wasAlive)
            {
                EnemiesDefeated++;
            }

            leave = false;
            return "Enemy was Defeated";
        }
    }

    public virtual string TakeDamage(int DMG, Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        string result = TakeDamage(DMG);
        if (Health <= 0)
        {
            player.Gold += Gold;
            Gold = 0;
        }

        return result;
    }

    public virtual int DealDamage()
    {
        return Random.Shared.Next(powerMin, powerMax + 1);
    }
}

public class Boss : Enemy
{
    public int Bonus;

    public Boss(int HP, int Max, int Min, int Resistance) : base(HP, Max, Min)
    {
        Bonus = Resistance;
        Health = HP;
    }

    public override int DealDamage()
    {
        return base.DealDamage() + Bonus;
    }
}
