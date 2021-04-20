using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace text_game
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameMessages messages = new GameMessages();
            EnemyActions enemyActions = new EnemyActions();
            WeaponActions weaponActions = new WeaponActions();
            PotionActions potionActions = new PotionActions();
            HealthPotions healthPotion = new HealthPotions();
            Player player = new Player();
            ShieldPotion shieldPotion = new ShieldPotion();
            AbilityActions abilityActions = new AbilityActions(weaponActions);
            Abilities ability = new Abilities();
            Shop shop = new Shop(player, weaponActions, healthPotion, messages, shieldPotion);
            GameMechanics mechanics = new GameMechanics(player, enemyActions, weaponActions, messages, potionActions, healthPotion, shop, shieldPotion, abilityActions, ability);

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
                        GameLoop.Loop(enemyActions, player, messages, mechanics, weaponActions);
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
