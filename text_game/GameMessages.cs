using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public static class GameMessages
    {

        public static string Welcome()
        {
            Console.WriteLine("Welcome to the Game!");
            return Name();           
        }

        public static String Name()
        {

            Console.WriteLine("==Hello, what would you like to be called?==");
            String name = Console.ReadLine();
            Console.WriteLine("You set your name to: " + name + "\n");

            return name;
        }

        public static void MainMenuText()
        {
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Help/Info");
            Console.WriteLine("3. Quit");
        }

        public static void BossAppearText(EnemyActions enemy)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("                             BOSS APPEARING");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("---------------------------------------------------------------------------\n");
            Console.WriteLine("\t*# " + enemy.GetEnemy().Name + " appeared! #*");
        }

        public static void EnemyAppearText(EnemyActions enemy)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("\t*# " + enemy.GetEnemy().Name + " appeared! #*");
        }

        public static void ContinueFightingText()
        {
            Console.WriteLine("\nYou continue to fight!");
        }

        public static void DealingAbilityDamageText(EnemyActions enemy, AbilityActions ability)
        {
            Console.WriteLine("You dealt " + ability.GetDamage() + " damage ");
            Console.WriteLine("The " + enemy.GetEnemy().Name + " dealt " + enemy.GetDamage() + " damage ");
        }

        public static void DealingDamageText(EnemyActions enemy, WeaponActions weapon)
        {
            Console.WriteLine("You dealt " + weapon.GetDamage() + " damage ");
            Console.WriteLine("The " + enemy.GetEnemy().Name + " dealt " + enemy.GetDamage() + " damage ");
        }

        public static void EnemyAliveText(Player player, EnemyActions enemy, WeaponActions weapon)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("\tWeapon: " + weapon.GetWeapon().Name + "\n");
            Console.WriteLine("\tYour HP: " + player.GetHealth());
            Console.WriteLine("\t" + enemy.GetEnemy().Name + "'s HP: " + enemy.GetHealth());
            Console.WriteLine("\n\tWhat would you like to do?");
            Console.WriteLine("\t1. Attack");
            Console.WriteLine("\t2. Drink health potion");
            Console.WriteLine("\t3. Drink shield potion");
            Console.WriteLine("\t4. Abilities");
            Console.WriteLine("\t5. Run!");
        }

        public static void FireballUsed(Abilities ability)
        {
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("                         F I R E B A L L");         
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
        }

        public static void CooldownNotComplete()
        {
            Console.WriteLine("You must wait for the cooldown to hit 0.");
        }

        public static void AbilityOptions(AbilityActions abilities, Abilities ability)
        {
            int i;
            try
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                for (i = 0; i < abilities.OwnedAbilities.Count; i++)
                {
                    Console.WriteLine("" + (i + 1) + ": " + abilities.OwnedAbilities[i].Name);
                    Console.WriteLine("\tCooldown: " + ability.Cooldown);
                    Console.WriteLine("\tMinimum Damage:" + abilities.OwnedAbilities[i].MinimumDamage);
                    Console.WriteLine("\tMaximum Damage:" + abilities.OwnedAbilities[i].MaximumDamage);
                }

                if (abilities.OwnedAbilities.Count == 0)
                    Console.WriteLine("Sorry, but you don't have any abilities yet. Get them in the abilities section of the End of Encounter Menu.");

                Console.WriteLine("0: Exit");
            }
            catch
            {

            }
        }

        public static void PlayerDiedText()
        {
            Console.WriteLine("\nYou have DIED.");
        }

        public static void LeaveDungeonText()
        {
            Console.WriteLine("\nYou exit the dungeon, successful from your adventures!");
        }

        public static void EndEncounterText()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\n1. Continue Fighting");
            Console.WriteLine("2. Exit Dungeon");
            Console.WriteLine("3. Statistics");
            Console.WriteLine("4. Shop");
            Console.WriteLine("5. Abilities");
        }

        public static void Divider(int times)
        {
            for (int i = 0; i < times; i++)
                Console.WriteLine("---------------------------------------------------------------------------");
        }

        public static void HealText(HealthPotions healthPotion)
        {
            Console.WriteLine("You healed yourself for " + healthPotion.GetHealthPotionAmount() + " hp");
            Console.WriteLine("You have " + healthPotion.GetNumPotions() + " health potions left");
        }

        public static void ShieldText(ShieldPotion shieldPotion)
        {
            Console.WriteLine("The shield potion is now active for three turns!");
            Console.WriteLine("You have " + shieldPotion.GetNumPotions() + " shield potions left");
        }

        public static void ShieldTurnUsed(ShieldPotion shieldPotion)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Your shield potion prevented you from taking any damage.");
            Console.WriteLine("You have " + shieldPotion.GetShieldTurns() + " shield turns left until it expires");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        public static void ShieldExpired()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Your shield potion has expired! You'll now take damage again!");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        public static void BossPotionText(int numHealthPotions)
        {
            Console.WriteLine("By defeating the boss, you've earned " + numHealthPotions + " health potions");
        }

        public static void NoPotions()
        {
            Console.WriteLine("You have no potions left.");
        }

        public static void HealFailedText()
        {
            Console.WriteLine("You have the maximum amount of health.");
        }

        public static void RunDamage(Player player, EnemyActions enemy)
        {
            Console.WriteLine("You took " + enemy.GetDamage() + " damage while running!");
        }

        public static void StatisticsText(Player player, WeaponActions weapon, HealthPotions healthPotion, ShieldPotion shieldPotion)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Health: " + player.GetHealth());
            Console.WriteLine("Total Exp: " + player.GetExp());
            Console.WriteLine("Level: " + player.GetLevel());
            Console.WriteLine("Current Weapon Boost: " + weapon.GetWeaponBoost());
            Console.WriteLine(weapon.GetWeaponName() + "'s Durability: " + weapon.GetDurability());
            Console.WriteLine("Number of Enemies Slain: " + player.GetEnemiesSlain());
            Console.WriteLine("Total Damage Dealt: " + player.GetTotalDamage());
            Console.WriteLine("Number of Health Potions: " + healthPotion.GetNumPotions());
            Console.WriteLine("Number of Shield Potions: " + shieldPotion.GetNumPotions());
            Console.WriteLine("Number of Coins: " + player.GetCurrency());
            Console.WriteLine("Skill Points: " + player.GetSkillPoints());
        }

        public static void ShopWeaponsText(Player player, WeaponActions weapon)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Here are the Weapons Available for Purchase:\n");
            Console.WriteLine("Number of Coins: " + player.GetCurrency() + "\n");
            Console.WriteLine("You current weapon is the " + weapon.GetWeaponName() + "\n");
            Console.WriteLine("1. Bronze Sword - " + weapon.GetWeaponByName("Bronze Sword").Cost);
            Console.WriteLine("2. Sniper - " + weapon.GetWeaponByName("Sniper").Cost);
            Console.WriteLine("3. Spear - " + weapon.GetWeaponByName("Spear").Cost);
            Console.WriteLine("4. Silver Sword - " + weapon.GetWeaponByName("Silver Sword").Cost);
            Console.WriteLine("5. Rocket Launcher - " + weapon.GetWeaponByName("Rocket Launcher").Cost);
            Console.WriteLine("6. Shotgun - " + weapon.GetWeaponByName("Shotgun").Cost);
            Console.WriteLine("7. Golden Sword - " + weapon.GetWeaponByName("Golden Sword").Cost);
            Console.WriteLine("8. Shop Menu");
        }

        public static void DisplayCurrentWeaponDamage(WeaponActions weapon)
        {
            Console.WriteLine("\n" + weapon.GetWeaponName() + "'s Damage Range:\n");
            Console.WriteLine("Minimum Damage: " + weapon.GetWeaponMinDamage());
            Console.WriteLine("Maximum Damage: " + weapon.GetWeaponMaxDamage());
        }

        public static void DisplayCurrentEnemyDamage(EnemyActions enemy)
        {
            Console.WriteLine("\n" + enemy.GetEnemyName() + "'s Damage Range:\n");
            Console.WriteLine("Minimum Damage: " + enemy.GetEnemyMinDamage());
            Console.WriteLine("Maximum Damage: " + enemy.GetEnemyMaxDamage());
        }
      
        public static void DisplayEndEncounterStats(Player player, EnemyActions enemy, HealthPotions healthPotion, WeaponActions weapon, ShieldPotion shieldPotion)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("End of Encounter Report:\n");
            DisplayEnemyStats(enemy);
            DisplayPlayerStats(player, enemy, weapon);
            DisplayPotionStats(enemy, healthPotion, shieldPotion);
            //DisplayBoostStats(weapon, enemy);
        }

        public static void DisplayEnemyStats(EnemyActions enemy)
        {
            Console.WriteLine(enemy.GetEnemy().Name + "'s Total Damage: " + enemy.GetEnemyTotalDamage());
            Console.WriteLine("Enemy difficulty boost: " + enemy.GetDifficultyBoost());
        }

        public static void DisplayPlayerStats(Player player, EnemyActions enemy, WeaponActions weapon)
        {
            Console.WriteLine("Player Health: " + player.GetHealth());
            Console.WriteLine("Current Weapon: " + weapon.GetWeaponName());
            Console.WriteLine(weapon.GetWeaponName() + "'s Durability: " + weapon.GetDurability());
            Console.WriteLine("Total Exp: " + player.GetExp() + " (+{0})", player.GetEncounterExp());
            Console.WriteLine("Current Level: " + player.GetLevel());
            Console.WriteLine("Number of Coins: " + player.GetCurrency() + " (+{0})", player.GetEncounterCurrency());
        }

        public static void DisplayPotionStats(EnemyActions enemy, HealthPotions healthPotion, ShieldPotion shieldPotion)
        {
            Console.WriteLine("Health potions dropped: " + enemy.GetDropPotions());
            Console.WriteLine("Health Potions Left: " + healthPotion.GetNumPotions());
            Console.WriteLine("Shield Potions Left: " + shieldPotion.GetNumPotions());
        }

        public static void DisplayBoostStats(WeaponActions weapon, EnemyActions enemy)
        {
            Console.WriteLine("Current Weapon Boost: " + weapon.GetWeaponBoost());
            Console.WriteLine("Current Enemy Difficulty Boost: " + enemy.GetDifficultyBoost());
        }
    }
}
