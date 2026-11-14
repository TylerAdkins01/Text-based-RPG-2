// Player P1 = new Player();

// //Begin Encounter at a Shop with the ability to buy one item
// Console.WriteLine("""  
//    .------\ /------.
//    |       -       |
//    |       |       |
//    |       -       |
//    |       |       |
// _______________________
// ===========.===========
//   / ~~~~~     ~~~~~ \
//  /|     |     |      \
//  W   ---  / \  ---   W
//  \.      |o o|      ./
//   |                 |
//   \    #########    /
//    \  ## ----- ##  /
//     \##         ##/
//      \_____v_____/
// """);
// Console.WriteLine("The Shopkeep: \"How may I help you today?\"");

// await Task.Delay(1000); // Simulate delay

// while(Player.leave){
// Console.WriteLine($"\nNarrator: \"it appears you have {P1.Gold} Gold currently, what will you pick?\"");
// Console.WriteLine("""
// 1 - Purchase a sword
// 2 - Purchase a scroll 
// 3 - Attack the Shopkeep
// 4 - Sneakily take an item of the shelf
// 5 - Leave
// """);
// string a = Console.ReadLine()!;
// Console.Clear();

//     switch (a)
//     {
//         case "1":
//             if (P1.Gold >= 1)
//             {
//                 Console.WriteLine("would you like the (1)white sword or the (2)yellow sword?");
//                 string b = Console.ReadLine()!;
//                 if (b == "1")
//                 {
//                     P1.Gold -= 1;
//                     Player.Strength += 2;
//                 }
//                 else if (b == "2")
//                 {
//                     P1.Gold -= 1;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Narrator: \"That is not a valid option.\"");
//                 }
//             }
//             else
//             {
//                 Console.WriteLine("Narrator: \"You do not have enough gold to purchase a sword.\"");
//             } 
//             Console.Clear();
//             break;

//         case "2":
//             if (P1.Gold >= 1)
//             {
//                 Console.WriteLine("would you like the (1)Fireball scroll or the (2)Frost scroll?");
//                 string b = Console.ReadLine()!;
//                 if (b == "1")
//                 {
//                     P1.Gold -= 1;
//                     P1.hasFireball = true;
//                 }
//                 else if (b == "2")
//                 {
//                     P1.Gold -= 1;
//                     P1.hasFrost = true;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Narrator: \"That is not a valid option.\"");
//                 }
//             }
//             Console.Clear();
//             break;
//         case "3":
//             Console.WriteLine("Narrator: \"You attack the Shopkeep!\"");
//             await Task.Delay(1000);
//             Console.WriteLine("The Shopkeep: \"How dare you attack me!\"");
//             await Task.Delay(1000);
//             Console.WriteLine("The Shopkeep attacks you back!");
//             P1.Health -= 5;
//             await Task.Delay(1000);
//             Console.WriteLine($"Narrator: \"You have {P1.Health} health remaining.\"");
//             if (P1.Health <= 0)
//             {
//                 Console.WriteLine("Narrator: \"You have been defeated by the Shopkeep.\"");
//                 Player.leave = false;
//             }
//             await Task.Delay(1000);
//             Console.Clear();
//             break;
//         case "4":
//             Console.WriteLine("Narrator: \"You sneakily take an item off the shelf!\"");
//             P1.Gold += 1;
//             Console.WriteLine("Narrator: \"You gained 1 gold!\"");
//             await Task.Delay(1000);
//             Console.Clear();
//             break;
//         case "5":
//             Console.WriteLine("Narrator: \"You leave the shop.\"");
//             Player.leave = true;
//             Console.Clear();
//             break;
//         default:
//             Console.WriteLine("Narrator: \"That is not a valid option.\"");
//             Console.Clear();
//             break;
//     }
// }
// Enemy Guy = new Enemy(10, 6, 2);
// Console.WriteLine(Guy.DealDamage());