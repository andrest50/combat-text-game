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

        //public Level level { get; set; }
        private int health;
        private int enemiesSlain;
        private int totalDamage;
        private int coins;
        private int encounterCoins;
        protected int currentLevel;
        protected int exp;
        protected int encounterExp;
        protected int skillPoints;
        private bool ran;

        private List<Int32> levelUpExp = new List<Int32> { 10, 30, 50, 70, 90, 110, 130, 150, 180, 210, 240, 270, 300, 340, 380, 420 };

        public string playerName { get; set; }

        public Player() 
        {
            SetRan(false);
            SetEnemiesSlain(0);
            SetHealth(100);
            SetCurrency(0);
            SetSkillPoints(0);
        }

        public Player(string name)
        {
            SetPlayerName(name);
            SetRan(false);
            SetEnemiesSlain(0);
            SetHealth(100);
            SetCurrency(0);
            SetSkillPoints(0);
        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public void SetPlayerName(string playerName)
        {
            this.playerName = playerName;
        }

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

        public void SetLevel()
        {
            if (currentLevel == 0 && exp >= levelUpExp[0])
            {
                currentLevel++;
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
                Console.WriteLine("                             LEVEL UP (" + currentLevel + ")");
                Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
                SetSkillPoints(GetSkillPoints() + 1);
            }
            else if (exp >= levelUpExp[currentLevel])
                while (exp >= levelUpExp[currentLevel])
                {
                    currentLevel++;
                    Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
                    Console.WriteLine("                             LEVEL UP (" + currentLevel + ")");
                    Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
                    SetSkillPoints(GetSkillPoints() + 1);
                }
        }

        public void SetLevelDirect(int currentLevel)
        {
            this.currentLevel = currentLevel;
        }

        public int GetLevel()
        {
            return currentLevel;
        }

        public void SetStartingExp()
        {
            SetExp(0);
            SetLevelDirect(0);
        }

        public int GetEncounterExp()
        {
            return encounterExp;
        }

        public void SetEncounterExp(int health, int damage, EnemyActions enemy)
        {
            double multiplier = 1.00;
            if (enemy.bossActive == true)
                multiplier = 0.50;

            encounterExp = Convert.ToInt32((multiplier * ((health + damage) / 10)));
        }

        public int GetExpIncrement()
        {
            exp += encounterExp;

            return exp;
        }

        public int GetExp()
        {
            return exp;
        }

        public void SetExp(int exp)
        {
            this.exp = exp;
        }

    }
}
