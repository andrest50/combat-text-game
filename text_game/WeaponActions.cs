﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class WeaponActions
    {
        Random rand = new Random();
        //public List<Int32> weaponMinDamages = new List<Int32> { 12, 15, 17, 18, 20, 22, 24, 25 };
        //public List<Int32> weaponMaxDamages = new List<Int32> { 20, 25, 23, 22, 30, 28, 26, 35 };
        //public List<String> weapons = new List<String> { "Wooden Sword", "Bronze Sword", "Sniper", "Tarp", "Silver Sword", "Rocket Launcher", "rip", "Gold Sword" };
        private List<Weapon> AllWeapons = new List<Weapon>();
        private Weapon Weapon;
        private int damage;
        private double weaponBoost;
        private double weaponScaling;

        public WeaponActions()
        {
            InitWeapons();
            //SetStartingWeapon();
        }

        private void InitWeapons()
        {
            AllWeapons = new List<Weapon>();

            var weapon1 = new Weapon()
            {
                Name = "Wooden Sword",
                MinimumDamage = 12,
                MaximumDamage = 20,
                Durability = 100
            };
            AllWeapons.Add(weapon1);

            var weapon2 = new Weapon()
            {
                Name = "Bronze Sword",
                MinimumDamage = 15,
                MaximumDamage = 25,
                Durability = 75
            };
            AllWeapons.Add(weapon2);

            var weapon3 = new Weapon()
            {
                Name = "Sniper",
                MinimumDamage = 17,
                MaximumDamage = 23,
                Durability = 50
            };
            AllWeapons.Add(weapon3);

            var weapon4 = new Weapon()
            {
                Name = "Spear",
                MinimumDamage = 18,
                MaximumDamage = 22,
                Durability = 60
            };
            AllWeapons.Add(weapon4);

            var weapon5 = new Weapon()
            {
                Name = "Silver Sword",
                MinimumDamage = 20,
                MaximumDamage = 30,
                Durability = 70
            };
            AllWeapons.Add(weapon5);

            var weapon6 = new Weapon()
            {
                Name = "Rocket Launcher",
                MinimumDamage = 22,
                MaximumDamage = 28,
                Durability = 40
            };
            AllWeapons.Add(weapon6);

            var weapon7 = new Weapon()
            {
                Name = "Shotgun",
                MinimumDamage = 24,
                MaximumDamage = 26,
                Durability = 35
            };
            AllWeapons.Add(weapon7);

            var weapon8 = new Weapon()
            {
                Name = "Golden Sword",
                MinimumDamage = 25,
                MaximumDamage = 35,
                Durability = 50
            };
            AllWeapons.Add(weapon8);
        }

        //public void SetStartingWeapons()
        //{
        //    SetWeapon("Wooden Sword");
        //}

        public void SetStartingValues()
        {
            SetWeaponBoost(1.00);
            SetWeaponScaling(1.00);
        }

        public int GetDurability()
        {
            return Weapon.Durability;
        }

        public void SetDurability(int durability)
        {
            Weapon.Durability = durability;
        }

        public Weapon SetStartingWeapon()
        {
            Weapon = AllWeapons.Where(x => x.Name == "Wooden Sword").SingleOrDefault();
            return Weapon;
        }

        public void SetWeaponScaling(double weaponScaling)
        {
            this.weaponScaling = weaponScaling;
        }

        public void IncrementWeaponBoost(EnemyActions enemyActions)
        {
            weaponBoost += weaponScaling * (0.00002 * (enemyActions.GetEnemyTotalDamage() + enemyActions.GetEnemyStartingHealth()));
            weaponScaling -= 0.005;
        }

        public void SetWeaponBoost(double weaponBoost)
        {
            this.weaponBoost = weaponBoost;
        }

        public double GetWeaponBoost()
        {
            return weaponBoost;
        }

        public void SetWeapon(String thisWeapon)
        {
            Weapon = AllWeapons.Where(x => x.Name == thisWeapon).SingleOrDefault();
        }

        public Weapon GetWeapon()
        {
            return Weapon;
        }

        public String GetWeaponName()
        {
            return Weapon.Name;
        }

        public void SetWeaponMinDamage(int damage)
        {
            Weapon.MinimumDamage = damage;
        }

        public void SetWeaponMaxDamage(int damage)
        {
            Weapon.MaximumDamage = damage;
        }

        public String GetWeaponName(String weaponName)
        {
            return AllWeapons.Where(x => x.Name == "Wooden Sword").SingleOrDefault().Name;
        }

        public int GetWeaponMinDamage()
        {
            return Convert.ToInt32(Weapon.MinimumDamage * weaponBoost);
        }

        public int GetWeaponMaxDamage()
        {
            return Convert.ToInt32(Weapon.MaximumDamage * weaponBoost);
        }

        public void SetDamage(string weaponName)
        {
            Weapon = AllWeapons.Where(x => x.Name == weaponName).SingleOrDefault();
            damage = rand.Next(Convert.ToInt32(Weapon.MinimumDamage * weaponBoost), Convert.ToInt32(Weapon.MaximumDamage * weaponBoost) + 1);
        }

        public int GetDamage()
        {
            return damage;
        }
    }
}
