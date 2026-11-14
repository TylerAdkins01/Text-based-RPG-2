Player P1 = new Player();

//Begin Encounter at a Shop with the ability to buy one item
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

await Task.Delay(1000); // Simulate delay

while (Character.leave)
{
Console.WriteLine($"\nNarrator: \"it appears you have {P1.Gold} Gold currently, what will you pick?\"");
Console.WriteLine("""
1 - Purchase a sword
2 - Purchase a scroll 
3 - Attack the Shopkeep
4 - Sneakily take an item of the shelf
5 - Leave
""");
string a = Console.ReadLine()!;
Console.Clear();

    switch (a)
    {
        case "1":
            Console.WriteLine("would you like the (1)white sword or the (2)yellow sword?");
            string b = Console.ReadLine()!;
            if (b != "1" && b != "2")
            {
                Console.WriteLine("Narrator: \"That is not a valid option.\"");
            }
            else if (P1.Gold < 1)
            {
                Console.WriteLine("Narrator: \"You do not have enough gold to purchase a sword.\"");
            }
            else
            {
                P1.Gold -= 1;
                if (b == "1")
                {
                    Player.Strength += 2;
                }
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
            else if (P1.Gold < 1)
            {
                Console.WriteLine("Narrator: \"You do not have enough gold to purchase a scroll.\"");
            }
            else
            {
                P1.Gold -= 1;
                if (scroll == "1")
                {
                    P1.hasFireball = true;
                }
                else
                {
                    P1.hasFrost = true;
                }
            }
            Console.Clear();
            break;
        case "3":
            Console.WriteLine("Narrator: \"You attack the Shopkeep!\"");
            await Task.Delay(1000);
            Console.WriteLine("The Shopkeep: \"How dare you attack me!\"");
            await Task.Delay(1000);
            Console.WriteLine("The Shopkeep attacks you back!");
            P1.Health -= 5;
            await Task.Delay(1000);
            Console.WriteLine($"Narrator: \"You have {P1.Health} health remaining.\"");
            if (P1.Health <= 0)
            {
                Console.WriteLine("Narrator: \"You have been defeated by the Shopkeep.\"");
                Character.leave = false;
            }
            await Task.Delay(1000);
            Console.Clear();
            break;
        case "4":
            Console.WriteLine("Narrator: \"You sneakily take an item off the shelf!\"");
            P1.Gold += 1;
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

if (P1.Health > 0)
{
    P1.getStats();
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

    Enemy Guy = new Enemy(10, 6, 2);
    Guy.Gold = 2;
    Guy.getStats();
    Character.leave = true;

    while (Character.leave && P1.Health > 0)
    {
        Console.WriteLine("""
        What will you do?

        1 - Strike the creature
        2 - Negotiate
        3 - Flee
        """);
        if (P1.hasFireball)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("4 - FIREBALL!!!");
            Console.ResetColor();
        }
        if (P1.hasFrost)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("5 - FROST!!!");
            Console.ResetColor();
        }

        string choice = Console.ReadLine()!;
        switch (choice)
        {
            case "1":
                int attackDamage = Math.Max(1, Player.Strength + 3);
                Console.WriteLine(Guy.TakeDamage(attackDamage, P1));
                if (Guy.Health <= 0)
                {
                    Console.WriteLine("The creature falls, and you claim its supplies.");
                    P1.Health = Math.Max(P1.Health, 10);
                    P1.hasBomb = true;
                    Character.leave = false;
                }
                else
                {
                    int enemyDamage = Guy.DealDamage();
                    P1.Health -= enemyDamage;
                    Console.WriteLine($"The creature hits you for {enemyDamage} damage. Health: {P1.Health}");
                    if (P1.Health <= 0)
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
                P1.Health = P1.Health / 2 - 1;
                Character.leave = false;
                break;

            case "4":
                if (P1.hasFireball)
                {
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
                    Console.WriteLine(Guy.TakeDamage(Guy.Health, P1));
                    Console.WriteLine("Congratulations! You have incinerated your first enemy.");
                    P1.hasFireball = false;
                    P1.hasBomb = true;
                    Character.leave = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid option.");
                }
                break;

            case "5":
                if (P1.hasFrost)
                {
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
                    Console.WriteLine(Guy.TakeDamage(frostDamage, P1));
                    P1.hasFrost = false;

                    if (Guy.Health <= 0)
                    {
                        Console.WriteLine("The frozen creature shatters, and you claim its supplies.");
                        P1.Health = Math.Max(P1.Health, 10);
                        P1.hasBomb = true;
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

    if (P1.Health > 0)
    {
        Character.leave = true;
        while (Character.leave && P1.Health > 0)
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
                    P1.Health += 5;
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

                    while (Character.leave && P1.Health > 0)
                    {
                        Console.WriteLine("What will you try?");
                        Console.WriteLine("1 - Move it\n2 - Go around it\n3 - Destroy it");
                        if (P1.hasBomb)
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
                                    P1.Health = 0;
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
                                    P1.Health--;
                                }
                                break;

                            case "4":
                                if (P1.hasBomb)
                                {
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
                                    P1.hasBomb = false;
                                    P1.Health = 0;
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

        if (P1.Health > 0)
        {
            Boss boss = new Boss(10, 6, 2, 2);
            boss.Gold = 5;

            Console.WriteLine("ᘛ⁐̤ᕐᐷ");
            Console.WriteLine("As you leave, a little mouse aproaches you");
            Console.WriteLine("He seems a little more powerful han a typical mouse");
            Console.WriteLine($"Boss Health: {boss.Health}");
            Console.WriteLine($"Boss Power Range: {boss.powerMin} - {boss.powerMax} (+{boss.Bonus} damage)");

            Character.leave = true;
            while (Character.leave && P1.Health > 0)
            {
                Console.WriteLine("""
                What will you do?

                1 - Strike the boss
                2 - Negotiate
                3 - Flee
                """);
                if (P1.hasFireball)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("4 - FIREBALL!!!");
                    Console.ResetColor();
                }
                if (P1.hasFrost)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("5 - FROST!!!");
                    Console.ResetColor();
                }

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        Console.WriteLine(boss.TakeDamage(Math.Max(1, Player.Strength + 3), P1));
                        if (boss.Health <= 0)
                        {
                            Console.WriteLine("The boss is defeated!");
                            Character.leave = false;
                            break;
                        }

                        int bossDamage = boss.DealDamage();
                        P1.Health -= bossDamage;
                        Console.WriteLine($"The boss hits you for {bossDamage} damage. Health: {P1.Health}");
                        break;

                    case "2":
                        Console.WriteLine("The boss refuses to negotiate.");
                        break;

                    case "3":
                        Console.WriteLine("You attempt to escape from the boss.");
                        P1.Health = P1.Health / 2 - 1;
                        Character.leave = false;
                        break;

                    case "4":
                        if (P1.hasFireball)
                        {
                            Console.WriteLine("You unleash a fireball at the boss!");
                            P1.hasFireball = false;
                            Console.WriteLine(boss.TakeDamage(boss.Health, P1));
                            if (boss.Health <= 0)
                            {
                                Console.WriteLine("The boss is defeated!");
                                Character.leave = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("That is not a valid option.");
                        }
                        break;

                    case "5":
                        if (P1.hasFrost)
                        {
                            Console.WriteLine("A wave of frost crashes over the boss, freezing it in place.");
                            int frostDamage = Math.Max(1, Math.Max(1, Player.Strength + 3) / 2);
                            Console.WriteLine(boss.TakeDamage(frostDamage, P1));
                            P1.hasFrost = false;
                            if (boss.Health <= 0)
                            {
                                Console.WriteLine("The boss is defeated!");
                                Character.leave = false;
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

        if (P1.Health > 0)
        {
            P1.getStats();
        }
        else
        {
            Console.WriteLine("You have been defeated...");
        }
    }
    else
    {
        Console.WriteLine("You have been defeated...");
    }
}
