Player P1 = new Player();

await Functions.Shop(P1);

if (P1.Health > 0)
{
    await Functions.CreatureEncounter(P1);

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
        jrei  `'(_ )_)(_)_)'
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
    }
    else
    {
        Console.WriteLine("You have been defeated...");
    }
}
