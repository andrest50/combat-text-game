using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static text_game.Global;

namespace text_game
{
    public class GameMechanics
    {
        public bool running = true;

        public GameMechanics()
        {
            SetStartingValues();
        }

        private void SetStartingValues()
        {
            weaponActions.SetStartingValues();
            player.SetStartingExp();
        }

        public void PromptSimulateAttacks()
        {
            Console.WriteLine("Insert Number of Times to Simulate");
            int numTimesInt = utils.GetIntInput(1, 100);
            SimulateAttacks(numTimesInt);
        }

        public void SimulateAttacks(int numTimes)
        {
            for(int i = 0; i < numTimes; i++)
            {
                player.SetHealth(1000000);

                while (enemyActions.GetHealth() > 0)
                {
                    Attack();
                }

                if (enemyActions.GetHealth() < 0)
                {
                    EnemyDead();
                    SetStartingEnemyValues();
                }                 
            }
            GameMessages.DisplayEndEncounterStats();
            PromptSimulateAttacks();

        }

        public int MainMenu()
        {
            GameMessages.MainMenuText();
            int choice = utils.GetIntInput(1, 3);

            return choice;
        }


        public void EncounterOptions()
        {
            EnemyAlive();

            int choiceInt = utils.GetIntInput(1, 5);
            switch (choiceInt)
            {
                case 1:
                    Attack();
                    player.SetRan(false);
                    break;
                case 2:
                    Heal();
                    break;
                case 3:
                    Shield();
                    break;
                case 4:
                    AbilitiesOption();
                    break;
                case 5:
                    Run();
                    break;
            }
        }
      
        public void SetStartingEnemyValues()
        {
           enemyActions.SetAttributes();
        }

        public void EnemyAppear()
        {
            if (enemyActions.bossActive == true)
                GameMessages.BossAppearText();
            else
                GameMessages.EnemyAppearText();

            GameMessages.DisplayCurrentWeaponDamage();
            GameMessages.DisplayCurrentEnemyDamage();
            weaponActions.SetDurability(weaponActions.GetDurability() - 1);
        }

        public void EnemyAlive()
        {
            GameMessages.EnemyAliveText();
        }

        private void AbilitiesOption()
        {
            GameMessages.AbilityOptions();

            int choiceInt = utils.GetIntInput(0, 1);

            switch (choiceInt)
            {
                case 0:                   
                    EncounterOptions();
                    break;
                case 1:
                    ability = abilityActions.SetCurrentAbility("Fireball");
                    if (ability.Cooldown == 0)
                    {
                        GameMessages.FireballUsed();
                        Fireball();
                    }
                    else
                    {
                        GameMessages.CooldownNotComplete();
                        AbilitiesOption();
                    }
                    break;
            }
        }

        private void Fireball()
        {
            enemyActions.SetDamage();
            //Weapon = WeaponActions.GetWeapon();
            //WeaponActions.SetDamage(Weapon.Name);
            abilityActions.SetDamage(ability.Name);
            player.SetTotalDamage(abilityActions.GetDamage());
            enemyActions.AddEnemyTotalDamage(enemyActions.GetDamage());
            ShieldActiveCheck();
            ability.Cooldown = 3;
            enemyActions.SetHealth(enemyActions.GetHealth() - abilityActions.GetDamage());
            GameMessages.DealingAbilityDamageText();
        }

        private void Shield()
        {
            if (shieldPotion.GetNumPotions() > 0)
            {
                shieldPotion.SetNumPotions(shieldPotion.GetNumPotions() - 1);
                shieldPotion.SetShieldActive(true);
                shieldPotion.SetShieldTurns(3);
                GameMessages.ShieldText();
            }
            else
            {
                GameMessages.NoPotions();
            }
        }

        private void Heal()
        {
            if (healthPotion.GetNumPotions() > 0 && (player.GetHealth() + healthPotion.GetHealthPotionAmount() <= 150))
            {
                healthPotion.SetHealthPotionAmount(30);
                player.SetHealth(player.GetHealth(), healthPotion.GetHealthPotionAmount());
                healthPotion.SetNumPotions(healthPotion.GetNumPotions() - 1);
                GameMessages.HealText();
            }
            else if (healthPotion.GetNumPotions() > 0 && (player.GetHealth() + healthPotion.GetHealthPotionAmount()) > 150 && player.GetHealth() != 150)
            {
                healthPotion.SetHealthPotionAmount(150 - player.GetHealth());
                player.SetHealth(player.GetHealth(), healthPotion.GetHealthPotionAmount());
                healthPotion.SetNumPotions(healthPotion.GetNumPotions() - 1);
                GameMessages.HealText();
            }
            else if (player.GetHealth() == 150)
            {
                GameMessages.HealFailedText();
            }
            else
            {
                GameMessages.NoPotions();
            }
        }        

        /// <summary>
        /// This is for testing only
        /// </summary>
        private void Run()
        {
            enemyActions.SetHealth(0);
            enemyActions.SetEnemyTotalDamage(0);
            player.SetRan(true);
            weaponActions.SetWeapon("Silver Sword");
            player.TakeDamage(player.GetHealth(), enemyActions.GetDamage());
            GameMessages.RunDamage();
            EndEncounter();
        }

        public void GameOver()
        {
            GameMessages.LeaveDungeonText();
            GameMessages.PlayerDiedText();
            Statistics();
        }

        public bool LeaveDungeon()
        {
            //Glitch where if you type yes after Yes the first time, it starts next instance.
            String leave;
            Console.WriteLine("Are you sure you want to leave?");
            leave = Console.ReadLine();
            switch (leave)
            {
                case "yes":
                case "y":
                    GameMessages.Divider(1);
                    if (player.GetHealth() > 0)
                    {
                        GameMessages.LeaveDungeonText();
                    }
                    else
                    {
                        GameMessages.PlayerDiedText();
                    }
                    running = false;
                    Statistics();
                    return true;
                default:
                    EndEncounter();
                    return false;
            }
        }        

        public void EnemyDead()
        {
            if(player.GetRan() == false)
            {
                player.SetEncounterExp(enemyActions.GetEnemyStartingHealth(), enemyActions.GetEnemyTotalDamage());
                player.GetExpIncrement();
                player.SetLevel();
                player.SetCurrency(enemyActions.GetEnemyStartingHealth(), enemyActions.GetEnemyTotalDamage());
                player.AddEnemiesSlain();
                enemyActions.SetDropPotions();
                int healthPotionsDropped = enemyActions.GetDropPotions();
                healthPotion.IncrementNumPotions(healthPotionsDropped);
                if (enemyActions.bossActive == true)
                {
                    enemyActions.NoLongerBoss();
                    GameMessages.BossPotionText(healthPotionsDropped);
                }
                enemyActions.IncrementDifficultyBoost();
                weaponActions.IncrementWeaponBoost();
             }
            GameMessages.DisplayEndEncounterStats();
            enemyActions.SetEnemyTotalDamage(0);
            //Player.SetHealth(Player.GetHealth());
            EndEncounter();
        }

        private void Statistics()
        {
            GameMessages.StatisticsText();
        }

        private void EndEncounter()
        {
            bool loop = true;

            while (loop)
            {
                GameMessages.EndEncounterText();

                int choiceInt = utils.GetIntInput(1, 5);

                switch (choiceInt)
                {
                    case 1:
                        GameMessages.ContinueFightingText();
                        loop = false;
                        GameLoop.Loop();
                        break;
                    case 2:
                        if (LeaveDungeon())
                            loop = false;
                        break;
                    case 3:
                        Statistics();
                        break;
                    case 4:
                        ShopScreen();
                        break;
                    case 5:
                        AbilitiesScreen();
                        break;
                }
            }
        }

        public void AbilitiesScreen()
        {
            abilityActions.SetRunningAbilities(true);
            while (abilityActions.GetRunningAbilities() == true)
                abilityActions.AbilitiesScreen();
        }

        public void ShopScreen()
        {
            shop.SetRunningShop(true);
            while (shop.GetRunningShop() == true)
            {
                shop.ShopItems();
            }               
        }

        private void Attack()
        {
            enemyActions.SetDamage();
            weaponActions.SetDamage(weaponActions.GetWeaponName());
            player.SetTotalDamage(weaponActions.GetDamage());
            enemyActions.AddEnemyTotalDamage(enemyActions.GetDamage());
            ShieldActiveCheck();
            DecrementCooldowns();
            enemyActions.SetHealth(enemyActions.GetHealth() - weaponActions.GetDamage());
            GameMessages.DealingDamageText();
        }

        private void DecrementCooldowns()
        {
            if (ability.Cooldown > 0)
                ability.Cooldown -= 1;
        }

        private void ShieldActiveCheck()
        {
            if (shieldPotion.GetShieldActive() == false)
                player.TakeDamage(player.GetHealth(), enemyActions.GetDamage());
            else if (shieldPotion.GetShieldTurns() > 1)
            {
                shieldPotion.SetShieldTurns(shieldPotion.GetShieldTurns() - 1);
                GameMessages.ShieldTurnUsed();
            }
            else if (shieldPotion.GetShieldTurns() == 1)
            {
                shieldPotion.SetShieldTurns(shieldPotion.GetShieldTurns() - 1);
                GameMessages.ShieldExpired();
            }
            else
            {
                shieldPotion.SetShieldActive(false);
                player.TakeDamage(player.GetHealth(), enemyActions.GetDamage());
            }
        }
       
    }
}
