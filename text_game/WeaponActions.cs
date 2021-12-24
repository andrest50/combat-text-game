using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class WeaponActions
    {
        Random rand = new Random();

        private List<Weapon> AllWeapons;
        private Weapon Weapon;
        private int damage;
        private double weaponBoost;

        public WeaponActions()
        {
            InitWeapons();
        }

        private void InitWeapons()
        {
            AllWeapons = new List<Weapon>();

            var weapon1 = new Weapon()
            {
                Name = "Wooden Sword",
                MinimumDamage = 12,
                MaximumDamage = 20,
                Durability = 100,
                Cost = 0
            };
            AllWeapons.Add(weapon1);

            var weapon2 = new Weapon()
            {
                Name = "Bronze Sword",
                MinimumDamage = 15,
                MaximumDamage = 25,
                Durability = 75,
                Cost = 20
            };
            AllWeapons.Add(weapon2);

            var weapon3 = new Weapon()
            {
                Name = "Sniper",
                MinimumDamage = 17,
                MaximumDamage = 23,
                Durability = 20,
                Cost = 20
            };
            AllWeapons.Add(weapon3);

            var weapon4 = new Weapon()
            {
                Name = "Spear",
                MinimumDamage = 18,
                MaximumDamage = 22,
                Durability = 60,
                Cost = 20
            };
            AllWeapons.Add(weapon4);

            var weapon5 = new Weapon()
            {
                Name = "Silver Sword",
                MinimumDamage = 20,
                MaximumDamage = 30,
                Durability = 70,
                Cost = 30
            };
            AllWeapons.Add(weapon5);

            var weapon6 = new Weapon()
            {
                Name = "Rocket Launcher",
                MinimumDamage = 22,
                MaximumDamage = 28,
                Durability = 40,
                Cost = 30
            };
            AllWeapons.Add(weapon6);

            var weapon7 = new Weapon()
            {
                Name = "Shotgun",
                MinimumDamage = 24,
                MaximumDamage = 26,
                Durability = 35,
                Cost = 30
            };
            AllWeapons.Add(weapon7);

            var weapon8 = new Weapon()
            {
                Name = "Golden Sword",
                MinimumDamage = 25,
                MaximumDamage = 35,
                Durability = 50,
                Cost = 40
            };
            AllWeapons.Add(weapon8);
        }

        public Weapon SetStartingWeapon(string weaponName)
        {
            Weapon = AllWeapons.Where(x => x.Name == weaponName).SingleOrDefault();
            return Weapon;
        }

        public void SetStartingValues()
        {
            SetStartingWeapon("Wooden Sword");
            SetWeaponBoost(1.00);
        }

        public int GetDurability()
        {
            return Weapon.Durability;
        }

        public void SetDurability(int durability)
        {
            Weapon.Durability = durability;
        }

        public void IncrementWeaponBoost(EnemyActions enemyActions)
        {
            weaponBoost += Math.Log10(1 + (0.00002 * (enemyActions.GetEnemyTotalDamage() + enemyActions.GetEnemyStartingHealth())));
        }

        public void SetWeaponBoost(double weaponBoost)
        {
            this.weaponBoost = weaponBoost;
        }

        public double GetWeaponBoost()
        {
            return weaponBoost;
        }

        public void SetWeapon(String weaponName)
        {
            Weapon = AllWeapons.Where(x => x.Name == weaponName).SingleOrDefault();
        }

        public Weapon GetWeapon()
        {
            return Weapon;
        }

        public Weapon GetWeaponByName(string weaponName)
        {
            return AllWeapons.Where(x => x.Name == weaponName).SingleOrDefault();
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
