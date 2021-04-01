using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class AbilityActions : Player
    {
        private WeaponActions WeaponActions { get; set; }
        private Level Level { get; set; }

        public List<Abilities> AllAbilities = new List<Abilities>();
        public List<Abilities> OwnedAbilities = new List<Abilities>();        
        private Abilities Ability;

        public bool runningAbilities = true;
        int choice;
        private int Damage;

        private bool FireballBought;

        Random rand = new Random();

        public AbilityActions()
        {
            InitAbilities();
            //SetStartingWeapon();
        }

        public AbilityActions(WeaponActions weapon, Level level)
        {
            WeaponActions = weapon;
            Level = level;
            InitAbilities();
        }

        public Abilities SetCurrentAbility(String ability)
        {
            Ability = OwnedAbilities.Where(x => x.Name == ability).SingleOrDefault();

            return Ability;
        }


        private void InitAbilities()
        {
            AllAbilities = new List<Abilities>();

            var ability1 = FireballAbility();
            AllAbilities.Add(ability1);
        }

        private Abilities FireballAbility()
        {
            var ability1 = new Abilities()
            {
                Name = "Fireball",
                MinimumDamage = 20,
                MaximumDamage = 25,
                Cooldown = 0,
                Cost = 1
            };
            return ability1;
        }

        public bool GetRunningAbilities()
        {
            return runningAbilities;
        }

        public void SetRunningAbilities(bool running)
        {
            runningAbilities = running;
        }

        public void Choice()
        {
            String choiceString = Console.ReadLine();
            choice = Convert.ToInt32(choiceString);
        }

        public void AbilitiesScreen()
        {
            AbilitiesScreenMessage();
            runningAbilities = true;
            Choice();

            switch (choice)
            {
                case 1:
                    BuyAbilities();
                    break;
                case 2:
                    Console.WriteLine("Coming Soon");
                    //ShopPotions();
                    break;
                case 3:
                    runningAbilities = false;
                    break;
            }
        }

        public void BuyAbilities()
        {
            BuyAbilitiesScreenMessage();
            Choice();

            switch (choice)
            {
                case 1:
                    Console.WriteLine(AllAbilities.Where(x => x.Name == "Fireball").SingleOrDefault().Cost);
                    CheckBuyCost("Fireball", AllAbilities.Where(x => x.Name == "Fireball").SingleOrDefault().Cost, FireballBought);
                    break;
                case 2:
                    Console.WriteLine("Coming Soon");
                    //ShopPotions();
                    break;
                case 3:                  
                    AbilitiesScreen();
                    break;
            }
        }

        public void CheckBuyCost(String name, int cost, bool bought)
        {
            if (Level.GetSkillPoints() >= cost && bought == false)
            {
                AbilityPurchased(name);

                if (name == "Fireball")
                {
                    FireballBought = true;
                    OwnedAbilities.Add(FireballAbility());
                }
                Level.SetSkillPoints(Level.GetSkillPoints() - cost);
            }
            else if (bought == true)
                Console.WriteLine("You already own that ability");

            else
                Console.WriteLine("You don't have the sufficient number of skill points for purchase.");
        }

        public void SetDamage(string abilityName)
        {
            Ability = OwnedAbilities.Where(x => x.Name == abilityName).SingleOrDefault();
            Damage = rand.Next(Convert.ToInt32(Ability.MinimumDamage), Convert.ToInt32(Ability.MaximumDamage) + 1);
        }

        public int GetDamage()
        {
            return Damage;
        }

        public void AbilityPurchased(String name)
        {
            Console.WriteLine("You've purchased the " + name);
        }

        public void BuyAbilitiesScreenMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Abilities Shop");
            Console.WriteLine("Skill Points: " + Level.GetSkillPoints());
            Console.WriteLine("\n1. Fireball");
            Console.WriteLine("2. Orb (Coming Soon)");
            Console.WriteLine("3. Exit");
        }

        public void AbilitiesScreenMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Abilities Menu");
            Console.WriteLine("Skill Points: " + Level.GetSkillPoints());
            Console.WriteLine("\n1. Buy Abilities");
            Console.WriteLine("2. Upgrade Abilities (Coming Soon)");
            Console.WriteLine("3. Exit");
        }
    }
}
