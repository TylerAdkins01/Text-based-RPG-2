Player P1 = new Player();

// Run each part of the adventure in order, stopping when the player is defeated.
await Functions.Shop(P1);

if (P1.Health > 0)
{
    await Functions.CreatureEncounter(P1);
}

// The travel choice can heal the player, improve strength, or end the adventure.
if (P1.Health > 0)
{
    Functions.Travel(P1);
}

// Only start the boss fight if the player survived the forest.
if (P1.Health > 0)
{
    Functions.FightBoss(P1);
}

if (P1.Health > 0)
{
    P1.getStats();
}
else
{
    Console.WriteLine("You have been defeated...");
}
