using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class GameMechanics
    {
        public bool running = true;
        private Player Player { get; set; }
        private EnemyActions EnemyActions { get; set; }
        private Enemy Enemy { get; set; }
        private GameMessages Messages { get; set; }
        private WeaponActions WeaponActions { get; set; }
        private Weapon Weapon { get; set; }
        //private Level Level { get; set; }
        private PotionActions PotionActions { get; set; }
        private ShieldPotion ShieldPotion { get; set; }
        private HealthPotions HealthPotion { get; set; }
        private AbilityActions AbilityActions { get; set; }
        private Abilities Ability { get; set; }
        private Shop Shop { get; set; }
        private Utils Utils { get; set; }

        public GameMechanics() { }            

        public GameMechanics(Player player, EnemyActions enemyActions, WeaponActions weaponActions, 
            GameMessages messages, PotionActions potionActions, HealthPotions healthPotion, Shop shop, 
            ShieldPotion shieldPotion, AbilityActions abilityActions, Abilities ability, Utils utils)
        {
            Player = player;
            //Level = player.level;
            EnemyActions = enemyActions;
            WeaponActions = weaponActions;
            Weapon = WeaponActions.SetStartingWeapon();
            Messages = messages;
            PotionActions = potionActions;
            HealthPotion = healthPotion;
            Shop = shop;
            ShieldPotion = shieldPotion;
            AbilityActions = abilityActions;
            Ability = ability;
            Utils = utils;
            SetStartingValues();
        }

        private void SetStartingValues()
        {
            WeaponActions.SetStartingValues();
            Player.SetStartingExp();
        }

        public void PromptSimulateAttacks()
        {
            Console.WriteLine("Insert Number of Times to Simulate");
            int numTimesInt = Utils.GetIntInput(1, 100);
            SimulateAttacks(numTimesInt);
        }

        public void SimulateAttacks(int numTimes)
        {
            for(int i = 0; i < numTimes; i++)
            {
                Player.SetHealth(1000000);

                while (EnemyActions.GetHealth() > 0)
                {
                    Attack();
                }

                if (EnemyActions.GetHealth() < 0)
                {
                    EnemyDead();
                    SetStartingEnemyValues();
                }                 
            }
            GameMessages.DisplayEndEncounterStats(Player, EnemyActions, HealthPotion, WeaponActions, ShieldPotion);
            PromptSimulateAttacks();

        }

        public int MainMenu()
        {
            //bool startCase = true;
            GameMessages.MainMenuText();
            int choice = Utils.GetIntInput(1, 3);

            return choice;
        }


        public void EncounterOptions()
        {
            EnemyAlive();

            int choiceInt = Utils.GetIntInput(1, 5);
            switch (choiceInt)
            {
                case 1:
                    Attack();
                    Player.SetRan(false);
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
           Enemy = EnemyActions.SetAttributes(Player);
        }

        public void EnemyAppear()
        {
            if (EnemyActions.bossActive == true)
                GameMessages.BossAppearText(EnemyActions);
            else
                GameMessages.EnemyAppearText(EnemyActions);

            GameMessages.DisplayCurrentWeaponDamage(WeaponActions);
            GameMessages.DisplayCurrentEnemyDamage(EnemyActions);
            WeaponActions.SetDurability(WeaponActions.GetDurability() - 1);
        }

        public void EnemyAlive()
        {
            GameMessages.EnemyAliveText(Player, EnemyActions, WeaponActions);
        }

        private void AbilitiesOption()
        {
            GameMessages.AbilityOptions(AbilityActions, Ability);

            int choiceInt = Utils.GetIntInput(0, 1);

            switch (choiceInt)
            {
                case 0:                   
                    EncounterOptions();
                    break;
                case 1:
                    Ability = AbilityActions.SetCurrentAbility("Fireball");
                    if (Ability.Cooldown == 0)
                    {
                        GameMessages.FireballUsed(Ability);
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
            EnemyActions.SetDamage(Player);
            //Weapon = WeaponActions.GetWeapon();
            //WeaponActions.SetDamage(Weapon.Name);
            AbilityActions.SetDamage(Ability.Name);
            Player.SetTotalDamage(AbilityActions.GetDamage());
            EnemyActions.SetEnemyTotalDamage(EnemyActions.GetDamage());
            ShieldActiveCheck();
            Ability.Cooldown = 3;
            EnemyActions.SetHealth(EnemyActions.GetHealth() - AbilityActions.GetDamage());
            //EnemyActions.health -= AbilityActions.GetDamage();
            GameMessages.DealingAbilityDamageText(EnemyActions, AbilityActions);
        }

        private void Shield()
        {
            if (ShieldPotion.GetNumPotions() > 0)
            {
                ShieldPotion.SetNumPotions(ShieldPotion.GetNumPotions() - 1);
                ShieldPotion.SetShieldActive(true);
                ShieldPotion.SetShieldTurns(3);
                GameMessages.ShieldText(ShieldPotion);
            }
            else
            {
                GameMessages.NoPotions();
            }
        }

        private void Heal()
        {
            if (HealthPotion.GetNumPotions() > 0 && (Player.GetHealth() + HealthPotion.GetHealthPotionAmount() <= 150))
            {
                HealthPotion.SetHealthPotionAmount(30);
                Player.SetHealth(Player.GetHealth(), HealthPotion.GetHealthPotionAmount());
                HealthPotion.SetNumPotions(HealthPotion.GetNumPotions() - 1);
                GameMessages.HealText(HealthPotion);
            }
            else if (HealthPotion.GetNumPotions() > 0 && (Player.GetHealth() + HealthPotion.GetHealthPotionAmount()) > 150 && Player.GetHealth() != 150)
            {
                HealthPotion.SetHealthPotionAmount(150 - Player.GetHealth());
                Player.SetHealth(Player.GetHealth(), HealthPotion.GetHealthPotionAmount());
                HealthPotion.SetNumPotions(HealthPotion.GetNumPotions() - 1);
                GameMessages.HealText(HealthPotion);
            }
            else if (Player.GetHealth() == 150)
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
            EnemyActions.SetHealth(0);
            EnemyActions.SetEnemyTotalDamage(0);
            Player.SetRan(true);
            WeaponActions.SetWeapon("Silver Sword");
            Player.TakeDamage(Player.GetHealth(), EnemyActions.GetDamage());
            GameMessages.RunDamage(Player, EnemyActions);
            EndEncounter();
        }

        public void GameOver()
        {
            GameMessages.LeaveDungeonText();
            GameMessages.PlayerDiedText();
            Statistics();
        }

        public void LeaveDungeon()
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
                    if (Player.GetHealth() > 0)
                    {
                        GameMessages.LeaveDungeonText();
                    }
                    else
                    {
                        GameMessages.PlayerDiedText();
                    }
                    running = false;
                    Statistics();
                    break;
                case "no":
                case "n":
                    EndEncounter();
                    break;
            }
        }        

        public void EnemyDead()
        {
            if(Player.GetRan() == false)
            {
                Player.SetEncounterExp(EnemyActions.GetEnemyStartingHealth(), EnemyActions.GetEnemyTotalDamage(), EnemyActions);
                Player.GetExpIncrement();
                Player.SetLevel();
                Player.SetCurrency(EnemyActions.GetEnemyStartingHealth(), EnemyActions.GetEnemyTotalDamage());
                Player.AddEnemiesSlain();
                EnemyActions.SetDropPotions(HealthPotion.GetNumPotions());
                int healthPotionsDropped = EnemyActions.GetDropPotions();
                HealthPotion.IncrementNumHealthPotions(healthPotionsDropped);
                if (EnemyActions.bossActive == true)
                {
                    EnemyActions.NoLongerBoss();
                    GameMessages.BossPotionText(healthPotionsDropped);
                }
                EnemyActions.IncrementDifficultyBoost();
                WeaponActions.IncrementWeaponBoost(EnemyActions);
             }
            GameMessages.DisplayEndEncounterStats(Player, EnemyActions, HealthPotion, WeaponActions, ShieldPotion);
            EnemyActions.SetEnemyTotalDamageToZero(0);
            //Player.SetHealth(Player.GetHealth());
            EndEncounter();
        }

        private void Statistics()
        {
            GameMessages.StatisticsText(Player, WeaponActions, HealthPotion, ShieldPotion);
        }

        private void EndEncounter()
        {
            bool loop = true;

            while (loop)
            {
                GameMessages.EndEncounterText();

                int choiceInt = Utils.GetIntInput(1, 5);

                switch (choiceInt)
                {
                    case 1:
                        GameMessages.ContinueFightingText();
                        loop = false;
                        GameLoop.Loop(EnemyActions, Player, Messages, this, WeaponActions);
                        break;
                    case 2:
                        LeaveDungeon();
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
            AbilityActions.SetRunningAbilities(true);
            while (AbilityActions.GetRunningAbilities() == true)
                AbilityActions.AbilitiesScreen();
        }

        public void ShopScreen()
        {
            Shop.SetRunningShop(true);
            while (Shop.GetRunningShop() == true)
            {
                Shop.ShopItems();
            }               
        }

        private void Attack()
        {
            EnemyActions.SetDamage(Player);
            Weapon = WeaponActions.GetWeapon();
            WeaponActions.SetDamage(Weapon.Name);
            Player.SetTotalDamage(WeaponActions.GetDamage());
            EnemyActions.SetEnemyTotalDamage(EnemyActions.GetDamage());
            ShieldActiveCheck();
            DecrementCooldowns();
            EnemyActions.SetHealth(EnemyActions.GetHealth() - WeaponActions.GetDamage());
            //EnemyActions.health -= WeaponActions.GetDamage();
            GameMessages.DealingDamageText(EnemyActions, WeaponActions);
        }

        private void DecrementCooldowns()
        {
            if (Ability.Cooldown > 0)
                Ability.Cooldown -= 1;
        }

        private void ShieldActiveCheck()
        {
            if (ShieldPotion.GetShieldActive() == false)
                Player.TakeDamage(Player.GetHealth(), EnemyActions.GetDamage());
            else if (ShieldPotion.GetShieldTurns() > 1)
            {
                ShieldPotion.SetShieldTurns(ShieldPotion.GetShieldTurns() - 1);
                GameMessages.ShieldTurnUsed(ShieldPotion);
            }
            else if (ShieldPotion.GetShieldTurns() == 1)
            {
                ShieldPotion.SetShieldTurns(ShieldPotion.GetShieldTurns() - 1);
                GameMessages.ShieldExpired();
            }
            else
            {
                ShieldPotion.SetShieldActive(false);
                Player.TakeDamage(Player.GetHealth(), EnemyActions.GetDamage());
            }
        }
       
    }
}
