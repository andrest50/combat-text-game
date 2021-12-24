using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static text_game.Global;

namespace text_game
{
    public class AbilityActions
    {
        public List<Abilities> AllAbilities;
        public List<Abilities> OwnedAbilities = new List<Abilities>();        
        private Abilities Ability;

        public bool runningAbilities = true;
        int choice;
        private int damage;

        Random rand = new Random();

        public AbilityActions()
        {
            InitAbilities();
        }

        public Abilities SetCurrentAbility(String abilityName)
        {
            Ability = OwnedAbilities.Where(x => x.Name == abilityName).SingleOrDefault();

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

        public bool CheckIfAbilityIsOwned(string abilityName)
        {
            return OwnedAbilities.Exists(ability => ability.Name == abilityName);
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
                    CheckBuyCost("Fireball", AllAbilities.Where(x => x.Name == "Fireball").SingleOrDefault().Cost, CheckIfAbilityIsOwned("Fireball"));
                    break;
                case 2:
                    Console.WriteLine("Coming Soon");
                    break;
                case 3:                  
                    AbilitiesScreen();
                    break;
            }
        }

        public void CheckBuyCost(String abilityName, int cost, bool owned)
        {
            if (player.GetSkillPoints() >= cost && owned == false)
            {
                AbilityPurchasedMessage(abilityName);

                if (abilityName == "Fireball")
                {
                    OwnedAbilities.Add(FireballAbility());
                }
                player.SetSkillPoints(player.GetSkillPoints() - cost);
            }
            else if (owned == true)
                Console.WriteLine("You already own that ability");
            else
                Console.WriteLine("You don't have the sufficient number of skill points for purchase.");
        }

        public void SetDamage(string abilityName)
        {
            Ability = OwnedAbilities.Where(x => x.Name == abilityName).SingleOrDefault();
            damage = rand.Next(Convert.ToInt32(Ability.MinimumDamage), Convert.ToInt32(Ability.MaximumDamage) + 1);
        }

        public int GetDamage()
        {
            return damage;
        }

        public void AbilityPurchasedMessage(String name)
        {
            Console.WriteLine("You've purchased the " + name);
        }

        public void BuyAbilitiesScreenMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Abilities Shop");
            Console.WriteLine("Skill Points: " + player.GetSkillPoints());
            Console.WriteLine("\n1. Fireball");
            Console.WriteLine("2. Orb (Coming Soon)");
            Console.WriteLine("3. Exit");
        }

        public void AbilitiesScreenMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Abilities Menu");
            Console.WriteLine("Skill Points: " + player.GetSkillPoints());
            Console.WriteLine("\n1. Buy Abilities");
            Console.WriteLine("2. Upgrade Abilities (Coming Soon)");
            Console.WriteLine("3. Exit");
        }
    }
}
