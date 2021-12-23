using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class EnemyActions
    {
        Random rand = new Random();

        private List<Enemy> AllEnemies = new List<Enemy>();
        private List<Enemy> AllBosses = new List<Enemy>();
        private Enemy Enemy;
        public bool bossActive = false;
        public int health { get; set; }
        private int damage;
        private int enemyStartingHealth;
        private int enemyTotalDamage;
        private int dropHealthPotions;
        private double difficultyBoost;

        public EnemyActions ()
        {
            InitEnemies();
            InitBosses();
            SetDifficultyBoost(1.00);
        }

        private void InitEnemies()
        {
            AllEnemies = new List<Enemy>();

            var enemy1 = new Enemy()
            {
                Name = "Skeleton",
                MinimumDamage = 12,
                MaximumDamage = 18,
                MinimumHealth = 40,
                MaximumHealth = 50,
                OneHealthPotionDropRate = .50,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy1);

            var enemy2 = new Enemy()
            {
                Name = "Zombie",
                MinimumDamage = 14,
                MaximumDamage = 20,
                MinimumHealth = 30,
                MaximumHealth = 45,
                OneHealthPotionDropRate = .45,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy2);

            var enemy3 = new Enemy()
            {
                Name = "Warrior",
                MinimumDamage = 8,
                MaximumDamage = 15,
                MinimumHealth = 50,
                MaximumHealth = 65,
                OneHealthPotionDropRate = .40,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy3);

            var enemy4 = new Enemy()
            {
                Name = "Assassin",
                MinimumDamage = 17,
                MaximumDamage = 24,
                MinimumHealth = 20,
                MaximumHealth = 30,
                OneHealthPotionDropRate = .55,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy4);

            var enemy5 = new Enemy()
            {
                Name = "Reaper",
                MinimumDamage = 15,
                MaximumDamage = 22,
                MinimumHealth = 35,
                MaximumHealth = 45,
                OneHealthPotionDropRate = .60,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy5);

            var enemy6 = new Enemy()
            {
                Name = "Archer",
                MinimumDamage = 16,
                MaximumDamage = 23,
                MinimumHealth = 25,
                MaximumHealth = 40,
                OneHealthPotionDropRate = .50,
                TwoHealthPotionDropRate = .20
            };
            AllEnemies.Add(enemy6);
        }

        private void InitBosses()
        {
            AllBosses = new List<Enemy>();

            // Level 5 Boss
            var boss1 = new Enemy()
            {
                Name = "Raptor",
                MinimumDamage = 20,
                MaximumDamage = 25,
                MinimumHealth = 100,
                MaximumHealth = 110,
                HealthPotionDropCount = 6
            };
            AllBosses.Add(boss1);

            // Level 10 Boss
            var boss2 = new Enemy()
            {
                Name = "Giant",
                MinimumDamage = 25,
                MaximumDamage = 28,
                MinimumHealth = 150,
                MaximumHealth = 160,
                HealthPotionDropCount = 8
            };
            AllBosses.Add(boss2);

            // Level 15 Boss
            var boss3 = new Enemy()
            {
                Name = "Dementor",
                MinimumDamage = 30,
                MaximumDamage = 34,
                MinimumHealth = 200,
                MaximumHealth = 210,
                HealthPotionDropCount = 10
            };
            AllBosses.Add(boss3);
        }

        public Enemy SetAttributes(Player player)
        {
            if (player.GetLevel() % 5 == 0 && player.GetLevel() != 0)
            {
                SetBoss(player.GetLevel());
                if (Enemy != null) {
                    SetBossHealth(Enemy.Name);
                    Enemy = GetEnemy();
                    SetEnemyStartingHealth(GetHealth());
                    return Enemy;
                }
            }
            bossActive = false; 
            SetEnemy(GetRandomEnemy());
            SetHealth(Enemy.Name);

            Enemy = GetEnemy();
            SetEnemyStartingHealth(GetHealth());

            return Enemy;
        }

        public void SetBoss(int level)
        {
            bossActive = true;
            if (level == 5)
                Enemy = AllBosses.Where(x => x.Name == "Raptor").SingleOrDefault();
            else if (level == 10)
                Enemy = AllBosses.Where(x => x.Name == "Giant").SingleOrDefault();
            else if (level == 15)
                Enemy = AllBosses.Where(x => x.Name == "Dementor").SingleOrDefault();
            Console.WriteLine(Enemy);
        }

        public void NoLongerBoss()
        {
            if(bossActive == true)
            {
                AllBosses.Remove(Enemy);
            }
        }

        public void SetDropPotions()
        {
            if (Enemy.HealthPotionDropCount > 0)
                dropHealthPotions = Enemy.HealthPotionDropCount;
            else if (rand.NextDouble() > (1 - Enemy.ThreeHealthPotionDropRate))
                dropHealthPotions = 3;
            else if (rand.NextDouble() > (1 - Enemy.TwoHealthPotionDropRate))
                dropHealthPotions = 2;
            else if (rand.NextDouble() > (1 - Enemy.OneHealthPotionDropRate))
                dropHealthPotions = 1;
            else
                dropHealthPotions = 0;
        }

        public int GetDropPotions()
        {
            return dropHealthPotions;
        }

        public void IncrementDifficultyBoost()
        {
            difficultyBoost += Math.Log10(1 + (0.0002 * (enemyTotalDamage + enemyStartingHealth)));
        }

        public void SetDifficultyBoost(double difficultyBoost)
        {
            this.difficultyBoost = difficultyBoost;
        }

        public double GetDifficultyBoost()
        {
            return difficultyBoost;
        }

        public Enemy GetEnemy()
        {
            return Enemy;
        }

        public void SetEnemy(String thisEnemy)
        {
            Enemy = AllEnemies.Where(x => x.Name == thisEnemy).SingleOrDefault();
        }

        public String GetRandomEnemy()
        {
            int randNum = rand.Next(AllEnemies.Count());
            return AllEnemies[randNum].Name;
        }

        public void SetDamage()
        {
         if (bossActive)
                damage = rand.Next(Enemy.MinimumDamage, (Enemy.MaximumDamage + 1));
         else
                damage = rand.Next(Convert.ToInt32(Enemy.MinimumDamage * difficultyBoost), Convert.ToInt32((Enemy.MaximumDamage * difficultyBoost + 1)));
        }

        public int GetDamage()
        {         
            return damage;
        }

        public int GetHealth()
        {          
            return health;
        }

        public void SetBossHealth(String enemyName)
        {
            Enemy = AllBosses.Where(x => x.Name == enemyName).SingleOrDefault();
            health = rand.Next(Enemy.MinimumHealth, Enemy.MaximumHealth + 1);
        }

        public void SetHealth(String enemyName)
        {
            Enemy = AllEnemies.Where(x => x.Name == enemyName).SingleOrDefault();
            health = rand.Next(Enemy.MinimumHealth, Enemy.MaximumHealth + 1);
        }

        public void SetHealth(int health)
        {
            this.health = health;
        }

        public int GetEnemyTotalDamage()
        {
            return enemyTotalDamage;
        }

        public void AddEnemyTotalDamage(int damage)
        {
            this.enemyTotalDamage += damage;
        }

        public void SetEnemyTotalDamage(int damage)
        {
            this.enemyTotalDamage = damage;
        }

        public int GetEnemyStartingHealth()
        {
            return enemyStartingHealth;
        }

        public void SetEnemyStartingHealth(int health)
        {
            this.enemyStartingHealth = health;
        }

        public String GetEnemyName()
        {
            return Enemy.Name;
        }

        public int GetEnemyMinDamage()
        {
            return Convert.ToInt32(Enemy.MinimumDamage * difficultyBoost);
        }

        public int GetEnemyMaxDamage()
        {
            return Convert.ToInt32(Enemy.MaximumDamage * difficultyBoost);
        }

    }
}
