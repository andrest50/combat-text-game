using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Shop
    {
        private Player Player { get; set; }
        private HealthPotions HealthPotions { get; set; }
        private ShieldPotion ShieldPotions { get; set; }
        private WeaponActions WeaponActions { get; set; }
        private GameMessages Messages { get; set; }

        private int potionQuantity;

        const int WeaponDamageUpgradeCost = 5;

        public bool runningShop = true;
        private int choice;

        public Shop()
        {

        }

        public Shop(Player player, WeaponActions weapon, HealthPotions healthPotions, GameMessages messages, ShieldPotion shieldPotions)
        {
            Player = player;
            WeaponActions = weapon;
            HealthPotions = healthPotions;
            Messages = messages;
            ShieldPotions = shieldPotions;
        }

        public void ShopItems()
        {
            ShopMenu();
            runningShop = true;
            String choiceString = Console.ReadLine();
            choice = Convert.ToInt32(choiceString);

            switch (choice)
            {
                case 1:
                    ShopWeapons();
                    break;
                case 2:
                    WeaponUpgrades();
                    break;
                case 3:
                    ShopPotions();
                    break;           
                case 4:
                    runningShop = false;
                    break;
            }
        }

        public int GetAction()
        {
            String choiceString = Console.ReadLine();
            choice = Convert.ToInt32(choiceString);

            return choice;
        }

        public void WeaponUpgrades()
        {
            WeaponUpgradesMessage();
            GetAction();

            switch (choice)
            {
                case 1:
                    UpgradeWeaponDamage();
                    break;
                case 2:
                    ShopItems();
                    break;
            }
        }

        public void UpgradeWeaponDamage()
        {
            UpgradeWeaponDamageMessage();
            GetAction();

            switch (choice)
            {
                case 1:
                    CheckCost(WeaponDamageUpgradeCost, "Weapon Damage", "WU");
                    WeaponUpgrades();
                    break;
                case 2:
                    WeaponUpgrades();
                    break;
            }
        }

        public void ShopPotions()
        {
            ShopPotionsMessage();
            runningShop = true;
            choice = GetAction();           

            switch (choice)
            {
                case 1:
                    BuyPotionAmount();
                    CheckCost(HealthPotions.GetCost() * GetPotionAmount(), "Health Potion", "P");
                    break;               
                case 2:
                    BuyPotionAmount();
                    CheckCost(ShieldPotions.GetCost() * GetPotionAmount(), "Shield Potion", "P");
                    break;
                case 3:
                    ShopItems();
                    break;
            }
        }

        public void BuyPotionAmount()
        {
            Console.WriteLine("How many potions do you want to buy?");
            String potionQuantityStr = Console.ReadLine();
            SetPotionAmount(Convert.ToInt32(potionQuantityStr));

        }

        public void SetPotionAmount(int amount)
        {
            potionQuantity = amount;
        }

        public int GetPotionAmount()
        {
            return potionQuantity;
        }


        public void ShopWeapons()
        {
            ShopWeaponsMessage();
            runningShop = true;
            choice = GetAction();

            switch (choice)
            {
                case 1:
                    CheckCost(WeaponActions.GetWeaponByName("Bronze Sword").Cost, "Bronze Sword", "W");
                    break;
                case 2:
                    CheckCost(WeaponActions.GetWeaponByName("Sniper").Cost, "Sniper", "W");
                    break;
                case 3:
                    CheckCost(WeaponActions.GetWeaponByName("Spear").Cost, "Spear", "W");
                    break;
                case 4:
                    CheckCost(WeaponActions.GetWeaponByName("Silver Sword").Cost, "Silver Sword", "W");
                    break;
                case 5:
                    CheckCost(WeaponActions.GetWeaponByName("Rocket Launcher").Cost, "Rocket Launcher", "W");
                    break;
                case 6:
                    CheckCost(WeaponActions.GetWeaponByName("Shotgun").Cost, "Shotgun", "W");
                    break;
                case 7:
                    CheckCost(WeaponActions.GetWeaponByName("Golden Sword").Cost, "Golden Sword", "W");
                    break;
                case 8:
                    ShopItems();
                    break;
            }
        }

        public bool GetRunningShop()
        {
            return runningShop;
        }

        public void SetRunningShop(bool running)
        {
            runningShop = running;
        }

        public void CheckCost(int cost, String item, String type)
        {
            if (Player.GetCurrency() >= cost)
            {
                ItemPurchased(item, type);
                if (type == "W")
                {
                    WeaponActions.SetWeapon(item);
                }
                else if (type == "P")
                {
                    if (item == "Health Potion")
                    {
                        HealthPotions.IncrementNumPotions(potionQuantity);
                        Console.WriteLine("You now have " + HealthPotions.GetNumPotions() + " health potions.");
                    }
                    else if (item == "Shield Potion")
                    {
                        ShieldPotions.IncrementNumPotions(potionQuantity);
                        Console.WriteLine("You now have " + ShieldPotions.GetNumPotions() + " shield potions.");
                    }                 
                }                 
                else if (Player.GetCurrency() >= cost && type == "WU")
                    {
                        WeaponActions.SetWeaponMinDamage(WeaponActions.GetWeaponMinDamage() + 1);
                        WeaponActions.SetWeaponMaxDamage(WeaponActions.GetWeaponMaxDamage() + 1);
                        DisplayWeaponDamage();
                    }
                Player.SetCurrency(Player.GetCurrency() - cost);
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("You don't have the sufficient number of coins for the purchase!");
            }
            
        }

        public void ItemPurchased(String item, String type)
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            if (type == "W")
            {
                Console.WriteLine("You've purchased the " + item);
            }
            else if (type == "P")
            {
                Console.WriteLine("You've purchased " + GetPotionAmount() + " " + item + "s");
            }
            else if (type == "WU")
            {
                Console.WriteLine("You've purchased a boost to your " + WeaponActions.GetWeaponName());
            }
        }

        public void ShopWeaponsMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Here are the Weapons Available for Purchase:\n");
            Console.WriteLine("Number of Coins: " + Player.GetCurrency() + "\n");
            Console.WriteLine("You current weapon is the " + WeaponActions.GetWeaponName() + "\n");
            Console.WriteLine("1. Bronze Sword - " + WeaponActions.GetWeaponByName("Bronze Sword").Cost);
            Console.WriteLine("2. Sniper - " + WeaponActions.GetWeaponByName("Sniper").Cost);
            Console.WriteLine("3. Spear - " + WeaponActions.GetWeaponByName("Spear").Cost);
            Console.WriteLine("4. Silver Sword - " + WeaponActions.GetWeaponByName("Silver Sword").Cost);
            Console.WriteLine("5. Rocket Launcher - " + WeaponActions.GetWeaponByName("Rocket Launcher").Cost);
            Console.WriteLine("6. Shotgun - " + WeaponActions.GetWeaponByName("Shotgun").Cost);
            Console.WriteLine("7. Golden Sword - " + WeaponActions.GetWeaponByName("Golden Sword").Cost);
            Console.WriteLine("8. Shop Menu");
        }

        public void ShopMenu()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Shop!");
            Console.WriteLine("Here are the Items Available for Purchase:\n");
            Console.WriteLine("Number of Coins: " + Player.GetCurrency() + "\n");
            Console.WriteLine("1. Weapons");
            Console.WriteLine("2. Weapon Upgrades");
            Console.WriteLine("3. Potions");
            Console.WriteLine("4. Exit Shop");
        }

        public void DisplayNumPotions()
        {
            Console.WriteLine("You currently have " + HealthPotions.GetNumPotions() + " health potions");
            Console.WriteLine("You also have " + ShieldPotions.GetNumPotions() + " shield potions\n");
        }

        public void ShopPotionsMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Here are the Potions Available for Purchase:\n");
            Console.WriteLine("Number of Coins: " + Player.GetCurrency() + "\n");
            DisplayNumPotions();
            Console.WriteLine("1. Health Potion (each) - " + HealthPotions.GetCost());
            Console.WriteLine("2. Shield Potion (each) - " + ShieldPotions.GetCost());
            Console.WriteLine("3. Shop Menu");
        }

        public void DisplayWeaponDamage()
        {
            Console.WriteLine("\nCurrent Minimum Damage: " + WeaponActions.GetWeaponMinDamage());
            Console.WriteLine("Current Maximum Damage: " + WeaponActions.GetWeaponMaxDamage());
        }

        public void UpgradeWeaponDamageMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Coins: " + Player.GetCurrency());
            DisplayWeaponDamage();
            Console.WriteLine("\n1. Increase Damage Range By 1: 5 coins");
            Console.WriteLine("2. Return to Weapon Upgrades");
        }

        public void WeaponUpgradesMessage()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Here are the upgrades for your weapon!");
            Console.WriteLine("Coins: " + Player.GetCurrency());
            Console.WriteLine("Current Weapon: " + WeaponActions.GetWeaponName());
            Console.WriteLine("\n1. Damage Increase");
            Console.WriteLine("2. Exit");
        }


    }
}
