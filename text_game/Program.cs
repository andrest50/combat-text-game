using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using static text_game.Global;

namespace text_game
{
    public static class Global
    {
        public static GameMessages messages = new GameMessages();
        public static EnemyActions enemyActions = new EnemyActions();
        public static WeaponActions weaponActions = new WeaponActions();
        public static HealthPotions healthPotion = new HealthPotions();
        public static Player player = new Player();
        public static ShieldPotion shieldPotion = new ShieldPotion();
        public static AbilityActions abilityActions = new AbilityActions();
        public static Abilities ability = new Abilities();
        public static Utils utils = new Utils();
        public static Shop shop = new Shop();
        public static GameMechanics mechanics = new GameMechanics();
    }

    public static class Program
    {
        public struct DebugOption
        {
            public DebugOption(int index, string option)
            {
                Index = index;
                Option = option;
            }
            public int Index { get; }
            public string Option { get; }
        }

        public const bool DEBUG = true;
        static void Main(string[] args)
        {
            string playerName = GameMessages.Welcome();
            player.SetPlayerName(playerName);

            int option = mechanics.MainMenu();          
            bool startCase = true;

            while (startCase)
            {
                switch (option)
                {
                    case 1:
                        startCase = false;
                        Console.WriteLine("==Welcome to the Dungeon ==");
                        Console.WriteLine("==On your journey, you bring " + healthPotion.GetNumPotions() + " health potions to regenerate when in peril==");
                        Console.WriteLine("==You were given the bronze sword to fend off the enemies. Goodluck!==");

                        List<DebugOption> debugOptions = new List<DebugOption>()
                        {
                            new DebugOption(1, "Skip to level 5"),
                            new DebugOption(2, "Skip to level 10"),
                            new DebugOption(3, "Skip to level 15"),
                            new DebugOption(4, "Get 1000 coins"),
                            new DebugOption(5, "Get 1000 health potions"),
                            new DebugOption(6, "Get a golden sword")
                        };

                        List<DebugOption> selectedDebugOptions = new List<DebugOption>();

                        int choice = 1;
                        while (DEBUG == true && choice != 0)
                        {
                            Console.WriteLine("==DEBUG Menu==");

                            Console.WriteLine("Selected debug options: ");
                            if (selectedDebugOptions.Count() == 0)
                            {
                                Console.WriteLine("None");
                            }
                            else
                            {
                                foreach (DebugOption opt in selectedDebugOptions)
                                {
                                    Console.WriteLine("{0}", opt.Option);
                                }
                            }

                            Console.WriteLine("Available debug options: ");
                            int i = 1;
                            foreach (DebugOption opt in debugOptions)
                            {
                                Console.WriteLine("({0}) {1}", i, opt.Option);
                                i++;
                            }
                            Console.WriteLine("(0) Done");
                            choice = utils.GetIntInput(0, debugOptions.Count);
                            if(choice == 0)
                            {
                                break;
                            }
                            if(debugOptions[choice-1].Index == 1 && player.GetLevel() < 5)
                            {
                                player.SetLevelDirect(5);
                            }
                            else if (debugOptions[choice - 1].Index == 2 && player.GetLevel() < 10)
                            {
                                player.SetLevelDirect(10);
                            }
                            else if (debugOptions[choice - 1].Index == 3 && player.GetLevel() < 15)
                            {
                                player.SetLevelDirect(15);
                            }
                            else if (debugOptions[choice - 1].Index == 4)
                            {
                                player.SetCurrency(1000);
                            }
                            else if (debugOptions[choice - 1].Index == 5)
                            {
                                healthPotion.SetNumPotions(1000);
                            }
                            else if (debugOptions[choice - 1].Index == 6)
                            {
                                weaponActions.SetWeapon("Golden Sword");
                            }
                            selectedDebugOptions.Add(debugOptions[choice - 1]);
                            debugOptions.RemoveAt(choice - 1);
                        }
                        GameLoop.Loop();
                        break;
                    case 2:
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine(" The objective of the game is to stay alive as long as possible.\n Waves of enemies will " +
                            "appear as you fight your way through the dungeon.\n Gain potions to stay alive and gather stronger weapons " +
                            "to fight your foes.\n How many enemies can you take out?\n");
                        option = mechanics.MainMenu();
                        startCase = true;
                        break;
                    case 3:
                        break;
                }
            }
        }       
    }
}
