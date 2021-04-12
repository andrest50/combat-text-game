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

        public List<String> Enemies = new List<String> { "Skeleton", "Zombie", "Warrior", "Assassin", "Reaper", "Archer" };
        public List<String> Bosses = new List<String> { "Raptor", "Giant", "Dementor" };     
        //public List<Int32> enemiesMinDamage = new List<Int32> {12, 14, 8, 17, 15, 16 };
        //public List<Int32> enemiesMaxDamage = new List<Int32> {18, 20, 15, 24, 22, 23 };
        //public List<Int32> enemiesMinHealth = new List<Int32> {40, 30, 50, 20, 35, 25 };
        //public List<Int32> enemiesMaxHealth = new List<Int32> {50, 45, 65, 30, 45, 40 };
        private List<Enemy> AllEnemies = new List<Enemy>();
        private List<Enemy> AllBosses = new List<Enemy>();
        private String enemy;
        private Enemy Enemy;
        public bool bossActive = false;
        public int health;
        private int Damage;
        private int enemyStartingHealth;
        private int enemyTotalDamage;
        private int dropHealthPotions;
        private double difficultyBoost = 1.00;
        private double difficultyScaling = 1.00;

        public EnemyActions ()
        {
            InitEnemies();
            InitBosses();
           // int randNum = rand.Next(6);
            //enemy = Enemies[randNum];
           // health = rand.Next(50);
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

            var boss1 = new Enemy()
            {
                Name = "Raptor",
                MinimumDamage = 20,
                MaximumDamage = 25,
                MinimumHealth = 100,
                MaximumHealth = 110,
                OneHealthPotionDropRate = .90,
                TwoHealthPotionDropRate = .50,
                ThreeHealthPotionDropRate = .30
            };
            AllBosses.Add(boss1);

            var boss2 = new Enemy()
            {
                Name = "Giant",
                MinimumDamage = 25,
                MaximumDamage = 28,
                MinimumHealth = 150,
                MaximumHealth = 160,
                OneHealthPotionDropRate = .95,
                TwoHealthPotionDropRate = .60,
                ThreeHealthPotionDropRate = .40
            };
            AllBosses.Add(boss2);

            var boss3 = new Enemy()
            {
                Name = "Dementor",
                MinimumDamage = 30,
                MaximumDamage = 34,
                MinimumHealth = 200,
                MaximumHealth = 210,
                OneHealthPotionDropRate = .99,
                TwoHealthPotionDropRate = .70,
                ThreeHealthPotionDropRate = .50
            };
            AllBosses.Add(boss3);
        }

        public Enemy SetAttributes(Level level)
        {
            if (level.GetLevel() % 5 == 0 && level.GetLevel() != 0)
            {
                SetBoss(level.GetLevel());
                SetBossHealth(Enemy.Name);
            }
            else
            {
                bossActive = false; 
                SetEnemy(GetRandomEnemy());
                SetHealth(Enemy.Name);
            }
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

        public void SetDropPotions(int numPotions)
        {
            if (rand.NextDouble() > (1 - Enemy.ThreeHealthPotionDropRate))
                dropHealthPotions = 3;
            else if (rand.NextDouble() > (1 - Enemy.TwoHealthPotionDropRate))
                dropHealthPotions = 2;
            else if (rand.NextDouble() > (1 - Enemy.OneHealthPotionDropRate))
                dropHealthPotions = 1;
            else
                dropHealthPotions = 0;
        }

        public void IncrementDifficultyBoost()
        {
            difficultyBoost += Math.Log10(1 + (0.0001 * (enemyTotalDamage + enemyStartingHealth)));
            difficultyScaling -= Math.Log10(difficultyBoost);
        }

        public void SetDifficultyBoost(double difficultyBoost)
        {
            this.difficultyBoost = difficultyBoost;
        }

        public double GetDifficultyBoost()
        {
            return difficultyBoost;
        }

        public int GetDropPotions()
        {
            return dropHealthPotions;
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
            int randNum = rand.Next(6);
            enemy = Enemies[randNum];
            Console.WriteLine(enemy);

            return enemy;
        }

        public void SetDamage()
        {           
            Damage = rand.Next(Enemy.MinimumDamage, (Enemy.MaximumDamage + 1));
        }

        public void SetDamage(Level level)
        {
         if (level.GetLevel() % 5 == 0 && level.GetLevel() != 0)
                Damage = rand.Next(Enemy.MinimumDamage, (Enemy.MaximumDamage + 1));
         else
            Damage = rand.Next(Convert.ToInt32(Enemy.MinimumDamage * difficultyBoost), Convert.ToInt32((Enemy.MaximumDamage * difficultyBoost + 1)));
        }

        public void SetDamage(String enemyName)
        {
            Enemy = AllEnemies.Where(x => x.Name == enemyName).SingleOrDefault();
            Damage = rand.Next(Convert.ToInt32(Enemy.MinimumDamage * difficultyBoost), Convert.ToInt32((Enemy.MaximumDamage * difficultyBoost + 1)));
        }

        public int GetDamage()
        {         
            return Damage;
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

        public void SetEnemyTotalDamage(int damage)
        {
            this.enemyTotalDamage += damage;
        }

        public void SetEnemyTotalDamageToZero(int damage)
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
