public static class Functions
{
    // Deduct gold only when the character can afford the purchase.
    private static bool TrySpendGold(IGold character, int amount)
    {
        if (character.Gold < amount)
        {
            return false;
        }

        character.Gold -= amount;
        return true;
    }

    // Show the shop menu until the player chooses to leave or is defeated.
    public static async Task Shop(Player player)
    {
        Console.WriteLine("""
           .------\ /------.
           |       -       |
           |       |       |
           |       -       |
           |       |       |
        _______________________
        ===========.===========
          / ~~~~~     ~~~~~ \
         /|     |     |      \
         W   ---  / \  ---   W
         \.      |o o|      ./
          |                 |
          \    #########    /
           \  ## ----- ##  /
            \##         ##/
             \_____v_____/
        """);
        Console.WriteLine("The Shopkeep: \"How may I help you today?\"");

        await Task.Delay(1000);

        // Character.leave is shared with the encounter loops, so reset it for this menu.
        Character.leave = true;
        while (Character.leave)
        {
            Console.WriteLine($"\nNarrator: \"it appears you have {player.Gold} Gold currently, what will you pick?\"");
            Console.WriteLine("""
            1 - Purchase a sword
            2 - Purchase a scroll 
            3 - Attack the Shopkeep
            4 - Sneakily take an item of the shelf
            5 - Leave
            """);
            string choice = Console.ReadLine()!;
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("would you like the (1)white sword or the (2)yellow sword?");
                    string sword = Console.ReadLine()!;
                    if (sword != "1" && sword != "2")
                    {
                        Console.WriteLine("Narrator: \"That is not a valid option.\"");
                    }
                    else if (!TrySpendGold(player, 1))
                    {
                        Console.WriteLine("Narrator: \"You do not have enough gold to purchase a sword.\"");
                    }
                    // Both sword choices cost gold; only the white sword raises strength.
                    else if (sword == "1")
                    {
                        Player.Strength += 2;
                    }
                    Console.Clear();
                    break;

                case "2":
                    Console.WriteLine("would you like the (1)Fireball scroll or the (2)Frost scroll?");
                    string scroll = Console.ReadLine()!;
                    if (scroll != "1" && scroll != "2")
                    {
                        Console.WriteLine("Narrator: \"That is not a valid option.\"");
                    }
                    else if (!TrySpendGold(player, 1))
                    {
                        Console.WriteLine("Narrator: \"You do not have enough gold to purchase a scroll.\"");
                    }
                    else if (scroll == "1")
                    {
                        player.hasFireball = true;
                    }
                    else
                    {
                        player.hasFrost = true;
                    }
                    Console.Clear();
                    break;

                case "3":
                    // The shopkeeper retaliates; defeat ends the shop loop.
                    Console.WriteLine("Narrator: \"You attack the Shopkeep!\"");
                    await Task.Delay(1000);
                    Console.WriteLine("The Shopkeep: \"How dare you attack me!\"");
                    await Task.Delay(1000);
                    Console.WriteLine("The Shopkeep attacks you back!");
                    player.Health -= 5;
                    await Task.Delay(1000);
                    Console.WriteLine($"Narrator: \"You have {player.Health} health remaining.\"");
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("Narrator: \"You have been defeated by the Shopkeep.\"");
                        Character.leave = false;
                    }
                    await Task.Delay(1000);
                    Console.Clear();
                    break;

                case "4":
                    Console.WriteLine("Narrator: \"You sneakily take an item off the shelf!\"");
                    player.Gold += 1;
                    Console.WriteLine("Narrator: \"You gained 1 gold!\"");
                    await Task.Delay(1000);
                    Console.Clear();
                    break;

                case "5":
                    Console.WriteLine("Narrator: \"You leave the shop.\"");
                    Character.leave = false;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Narrator: \"That is not a valid option.\"");
                    Console.Clear();
                    break;
            }
        }
    }

    // Start the first enemy fight and handle attacks, spells, rewards, and escape.
    public static async Task CreatureEncounter(Player player)
    {
        player.getStats();
        Console.WriteLine("You venture into the distance, hoping to vanquish the enemies in your path.");
        Console.WriteLine("""
          |\_/|
        =( o O )=
         /\ " /\
        | |\_/| |
        \_>---<_/
        (___|___)
        Hola mi amigo, mi nombre es Xicohtencatle
        """);
        await Task.Delay(1000);
        Console.WriteLine("It seems an enemy has arrived.");

        Enemy creature = new Enemy(10, 6, 2);
        creature.Gold = 2;
        creature.getStats();
        // Reset the shared loop flag before beginning this encounter.
        Character.leave = true;

        while (Character.leave && player.Health > 0)
        {
            Console.WriteLine("""
            What will you do?

            1 - Strike the creature
            2 - Negotiate
            3 - Flee
            """);
            if (player.hasFireball)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("4 - FIREBALL!!!");
                Console.ResetColor();
            }
            if (player.hasFrost)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("5 - FROST!!!");
                Console.ResetColor();
            }

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    // A normal strike always deals at least one damage; the enemy retaliates if it survives.
                    int attackDamage = Math.Max(1, Player.Strength + 3);
                    Console.WriteLine(creature.TakeDamage(attackDamage, player));
                    if (creature.Health <= 0)
                    {
                        Console.WriteLine("The creature falls, and you claim its supplies.");
                        player.Health = Math.Max(player.Health, 10);
                        player.hasBomb = true;
                        Character.leave = false;
                    }
                    else
                    {
                        int enemyDamage = creature.DealDamage();
                        player.Health -= enemyDamage;
                        Console.WriteLine($"The creature hits you for {enemyDamage} damage. Health: {player.Health}");
                        if (player.Health <= 0)
                        {
                            Console.WriteLine("What a depressing end to your adventure...");
                        }
                    }
                    break;

                case "2":
                    Console.WriteLine("¿Que? Yo no se.");
                    break;

                case "3":
                    Console.WriteLine("You attempt to escape from the creature's explosive weapons.");
                    // Escaping costs part of the player's current health and ends the encounter.
                    player.Health = player.Health / 2 - 1;
                    Character.leave = false;
                    break;

                case "4":
                    if (player.hasFireball)
                    {
                        // Fireball defeats the creature immediately, consumes the scroll, and grants a bomb.
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("""
                           --_--
                        (  -_    _).
                      ( ~       )   )
                    (( )  (    )  ()  )
                     (.   )) (       )
                       ``..     ..``
                            | |
                          (=| |=)
                            | |
                        (../( )\.))
                    """);
                        Console.ResetColor();
                        Console.WriteLine("The creature blows up in a fiery explosion.");
                        Console.WriteLine(creature.TakeDamage(creature.Health, player));
                        Console.WriteLine("Congratulations! You have incinerated your first enemy.");
                        player.hasFireball = false;
                        player.hasBomb = true;
                        Character.leave = false;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option.");
                    }
                    break;

                case "5":
                    if (player.hasFrost)
                    {
                        // Frost is consumed after dealing damage; a surviving creature cannot counterattack.
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("""
                               *  .  *  .  *
                            .  \  |  /  .
                          * --- FROST --- *
                            .  /  |  \  .
                               *  .  *  *
                        """);
                        Console.ResetColor();
                        Console.WriteLine("A wave of frost crashes over the creature, freezing it in place.");
                        int frostDamage = Math.Max(1, Math.Max(1, Player.Strength + 3) / 2);
                        Console.WriteLine(creature.TakeDamage(frostDamage, player));
                        player.hasFrost = false;

                        if (creature.Health <= 0)
                        {
                            Console.WriteLine("The frozen creature shatters, and you claim its supplies.");
                            player.Health = Math.Max(player.Health, 10);
                            player.hasBomb = true;
                            Character.leave = false;
                        }
                        else
                        {
                            Console.WriteLine("The creature cannot move while the frost holds it.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option.");
                    }
                    break;

                default:
                    Console.WriteLine("That is not a valid option.");
                    break;
            }
        }
    }

    // Let the player rest or explore; exploration leads to a branch obstacle.
    public static void Travel(Player player)
    {
        if (player.Health > 0)
        {
            // Reset the shared loop flag before offering the travel choices.
            Character.leave = true;
            while (Character.leave && player.Health > 0)
            {
                Console.WriteLine("Where do you want to go next?");
                Console.WriteLine("""
                1 - Set up camp and rest
                2 - Explore the forest
                """);
                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("You decide to set up camp and rest.");
                        Console.WriteLine("""
            
                        (                 ,&&&.
                        )                .,.&&
                       (  (              \=__/
                           )             ,'-'.
                     (    (  ,,      _.__|/ /|
                      ) /\ -((------((_|___/ |
                    (  // | (`'      ((  `'--|
                  _ -.;_/ \\--._      \\ \-._/.
                 (_;-// | \ \-'.\    <_,\_\`--'|
                 ( `.__ _  ___,')      <_,-'__,'
                  `'(_ )_)(_)_)'
            ------------------------------------------------
            """);
                        player.Health += 5;
                        Player.Strength++;
                        Character.leave = false;
                        break;

                    case "2":
                        Console.WriteLine("You decide to explore the forest.");
                        Console.WriteLine("""
                                                                    ,
                                                .__ ._       \_.
                                         _, _.  '  \/   \.-  /
                                          \/     .-_`   // |/     \,
                         .-"--"-.          \.   '   \`. ||  \.-'  /
                        F        Y        .-.`-(   _/\ V/ \\//,-' >-'   ._,
                       F          Y   .__/   `. \.   ' J   ) ./  / __._/
                      J         \, I    '   _/ \  \  | |  / /  .'-'.-' `._,
               (       L   \_.--.| \_.      ' .___ `\: | / .--'.-'"     \
             \ '\    .  L   /    \\/        ._/`-.`  \ .'.' .'---./__   '
        \__  '\ ) \._/   `-.__. ` \\_. '   .---.  \     /  /  ,   `  `
      --'  \\  ): // \,            `-.`__.'     `- \  /   / _/-.---.__.- .
         _.-`.'/ /'\_, ._     >--.-""'____.--"`_     '   /.'..' \   \   _/`
     _ .---._\ \'/ '__./__.-..  / .-|(    x_.-'___  |   :' /    _..---_' \
     .:' /`\ `. `..'.--'\      /.' /`-`._  `-,'   ` '   I '_.--'__--..___.--._.-
         `  `. `\/'/  _.   _.-'      _.____./ .-.--""-. .-"    ' _..-.---'   \
      -._ .--.\ / /-./     /   .---'-//.___. .-'       \__ .--.  `    `.     '`-
     ,--'/.-. ^.   .-.--.  ` _/    _//     ./   _..   .'  `.    \ \    |_.
        /' | >.   ' | \._.-       '    _..'  `.' . `.       )    | |\  `
      ./ \ \'  ) c| /  \     \_..  .--'    ,\ \_/`  :    )  (`-. `.|`\\
       \'  / ,-.  | ` ./`  ._/ `\\'.--.,-((  `.`.__ |   _/   \    |)  `--._/`
    ______'\   |  < __________  //'  //  _)   )/-._`.  (,-')  )  / \_.    /\. _____
    a:f        |  |        .__./    //  '\  |//    `.\ '\ (  (  <`   ._  '
               >  |      _.  /   ..-\ _    _/ \_.  \ `\    \_ `---.-'__
            . /  `-   _.'        /   `   _/|       J  /`     `-,,-----.`-.
                '  .:'          '`      '          < `   f  I //        `-\_,
                  '                         \.     J        I/\_.        ./
             __/                            `:     I  .:    K  `          `
             \/ )                            `,   J         L
              )_(_                             .  F  .-'    J
             /    `.                           .  I  (.   . I _.-.._
       '    <'    \ )                     _.---.J/      :'   L -'
     .:.     \. _.->/                        _.-'_.)     ` `-.`---.,_.
    :<        (    \                    .--""   .F' J) `.`L.__`-.___
    .:        |-'\_.|                          Y ..Z     ))   `--'  `-
    .         ) | > :                            . '    :'
              / ) L_J                            .x,.
              L_J .,                             .:<.,
            .'`   `                               :J.,`
                                               . ;.+K,:.
                                                   .,L+.,  
    """);
                        Console.WriteLine("It seems that an inconvenient branch lies in your path.");
                        Character.leave = true;

                        while (Character.leave && player.Health > 0)
                        {
                            // Moving the branch fails, while going around it has a random outcome.
                            Console.WriteLine("What will you try?");
                            Console.WriteLine("1 - Move it\n2 - Go around it\n3 - Destroy it");
                            if (player.hasBomb)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("4 - BOOM!!");
                                Console.ResetColor();
                            }

                            string branchChoice = Console.ReadLine()!;
                            switch (branchChoice)
                            {
                                case "1":
                                    Console.WriteLine("You decide to move the branch.");
                                    Console.WriteLine("The branch is far too heavy to move.");
                                    break;

                                case "2":
                                    Console.WriteLine("You choose to go around the branch.");
                                    if (Random.Shared.Next(1, 3) == 1)
                                    {
                                        Console.WriteLine("You successfully go around the branch.");
                                        Console.WriteLine("Why doesn't everyone just walk around it?");
                                        Character.leave = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your foot falls through the leaves, and a pit opens beneath you.");
                                        Console.WriteLine("The fall is fatal, and your adventure comes to a depressing end.");
                                        player.Health = 0;
                                        Character.leave = false;
                                    }
                                    break;

                                case "3":
                                    Console.WriteLine("You try to destroy the branch.");
                                    if (Player.Strength > 5)
                                    {
                                        Console.WriteLine("You successfully destroy the branch.");
                                        Character.leave = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The branch endures your attack, and you suffer a sore wrist.");
                                        player.Health--;
                                    }
                                    break;

                                case "4":
                                    if (player.hasBomb)
                                    {
                                        // The bomb clears the obstacle but also kills the player.
                                        Console.WriteLine("You use the bomb to destroy the branch.");
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("""
                                   ________________
                              ____/ (  (    )   )  \___
                             /( (  (  )   _    ))  )   )\
                           ((     (   )(    )  )   (   )  )
                         ((/  ( _(   )   (   _) ) (  () )  )
                        ( (  ( (_)   ((    (   )  .((_ ) .  )_
                       ( (  )    (      (  )    )   ) . ) (   )
                      (  (   (  (   ) (  _  ( _) ).  ) . ) ) ( )
                      ( (  (   ) (  )   (  ))     ) _)(   )  )  )
                     ( (  ( \ ) (    (_  ( ) ( )  )   ) )  )) ( )
                      (  (   (  (   (_ ( ) ( _    )  ) (  )  )   )
                     ( (  ( (  (  )     (_  )  ) )  _)   ) _( ( )
                      ((  (   )(    (     _    )   _) _(_ (  (_ )
                       (_((__(_(__(( ( ( |  ) ) ) )_))__))_)___)
                       ((__)        \\||lll|l||///          \_))
                                (   /(/ (  )  ) )\   )
                              (    ( ( ( | | ) ) )\   )
                               (   /(| / ( )) ) ) )) )
                             (     ( ((((_(|)_)))))     )
                              (      ||\(|(|)|/||     )
                            (        |(||(||)||||        )
                              (     //|/l|||)|\\ \     )
                            (/ / //  /|//||||\\  \ \  \ _)

                    """);
                                        Console.ResetColor();
                                        Console.WriteLine("The bomb is far more powerful than expected.");
                                        Console.WriteLine("You, as well as the branch, have blown up.");
                                        player.hasBomb = false;
                                        player.Health = 0;
                                        Character.leave = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("That is not a valid option.");
                                    }
                                    break;

                                default:
                                    Console.WriteLine("That is not a valid option.");
                                    break;
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("That is not a valid option.");
                        break;
                }
            }
        }
    }

    // Run the final boss encounter while the player is alive.
    public static void FightBoss(Player player)
    {
        Boss boss = new Boss(10, 6, 2, 2);
        boss.Gold = 5;

        Console.WriteLine("ᘛ⁐̤ᕐᐷ");
        Console.WriteLine("As you leave, the creature you fought returns as a powerful boss!");
        Console.WriteLine($"Boss Health: {boss.Health}");
        Console.WriteLine($"Boss Power Range: {boss.powerMin} - {boss.powerMax} (+{boss.Bonus} damage)");

        // Spells skip the boss's counterattack; normal strikes allow one.
        void AttackBoss(int damage, bool allowCounterattack)
        {
            Console.WriteLine(boss.TakeDamage(damage, player));
            if (boss.Health <= 0)
            {
                Console.WriteLine("The boss is defeated!");
                Character.leave = false;
                return;
            }

            if (allowCounterattack)
            {
                int bossDamage = boss.DealDamage();
                player.Health -= bossDamage;
                Console.WriteLine($"The boss hits you for {bossDamage} damage. Health: {player.Health}");
            }
        }

        // Reset the shared loop flag so the boss fight starts even after earlier encounters.
        Character.leave = true;
        while (Character.leave && player.Health > 0)
        {
            Console.WriteLine("""
            What will you do?

            1 - Strike the boss
            2 - Negotiate
            3 - Flee
            """);
            if (player.hasFireball)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("4 - FIREBALL!!!");
                Console.ResetColor();
            }
            if (player.hasFrost)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("5 - FROST!!!");
                Console.ResetColor();
            }

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    AttackBoss(Math.Max(1, Player.Strength + 3), true);
                    break;

                case "2":
                    Console.WriteLine("The boss refuses to negotiate.");
                    break;

                case "3":
                    Console.WriteLine("You attempt to escape from the boss.");
                    int escapeDamage = player.Health - Math.Max(0, player.Health / 2 - 1);
                    player.Health -= escapeDamage;
                    Console.WriteLine($"You take {escapeDamage} damage while escaping. Health: {player.Health}");
                    Character.leave = false;
                    break;

                case "4":
                    if (player.hasFireball)
                    {
                        Console.WriteLine("You unleash a fireball at the boss!");
                        player.hasFireball = false;
                        AttackBoss(boss.Health, false);
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option.");
                    }
                    break;

                case "5":
                    if (player.hasFrost)
                    {
                        Console.WriteLine("A wave of frost crashes over the boss, freezing it in place.");
                        int frostDamage = Math.Max(1, Math.Max(1, Player.Strength + 3) / 2);
                        AttackBoss(frostDamage, false);
                        player.hasFrost = false;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option.");
                    }
                    break;

                default:
                    Console.WriteLine("That is not a valid option.");
                    break;
            }
        }
    }
}