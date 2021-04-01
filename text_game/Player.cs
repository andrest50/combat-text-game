using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Player
    {
        Random rand = new Random();
        
        public int health;
        private int enemiesSlain;
        private int totalDamage;
        private int coins;
        private int encounterCoins;
        protected int currentLevel;
        protected int exp;
        protected int encounterExp;
        protected int skillPoints;
        private bool ran = false;

        public string PlayerName { get; set; }

        public Player() { }

        public Player(string name)
        {
            PlayerName = name;
            SetEnemiesSlain(0);
            SetHealth(100);
            SetCurrency(0);
            SetSkillPoints(0);
        }

        //public void SetStartingValues()
        //{
        //    SetEnemiesSlain(0);          
        //    SetHealth(100);
        //}

        public int GetSkillPoints()
        {
            return skillPoints;
        }

        public void SetSkillPoints(int skillPoints)
        {
            this.skillPoints = skillPoints;
        }

        public int GetCurrency()
        {
            return coins;
        }

        public void SetCurrency(int currency)
        {
            coins = currency;
        }

        public void SetCurrency(int enemyTotalDamage, int enemyStartingHealth)
        {
            encounterCoins = (enemyTotalDamage + enemyStartingHealth) / 30;
            coins += encounterCoins;
        }

        public int GetEncounterCurrency()
        {
            return encounterCoins;
        }

        public bool GetRan()
        {
            return ran;
        }

        public void SetRan(bool ran)
        {
            this.ran = ran;
        }

        public void SetTotalDamage(int damage)
        {
            totalDamage += damage;
        }

        public int GetTotalDamage()
        {
            return totalDamage;
        }

        public int GetHealth()
        {
            return health;
        }

        public void TakeDamage(int health, int damage)
        {
            this.health = health - damage;
        }

        public void SetHealth(int health, int healing)
        {
            this.health = health + healing;
        }

        public void SetHealth(int health)
        {
            this.health = health;
        }

        public int GetEnemiesSlain()
        {
            return enemiesSlain;
        }

        public int AddEnemiesSlain()
        {
            enemiesSlain++;
            return enemiesSlain;
        }

        public void SetEnemiesSlain(int numEnemiesSlain)
        {
            this.enemiesSlain = numEnemiesSlain;
        }

    }
}
