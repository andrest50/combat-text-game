using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Level : Player
    {
        //private List<Int32> levels = new List<Int32> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private List<Int32> levelUpExp = new List<Int32> {10, 30, 50, 70, 90, 110, 130, 150, 180, 210, 240, 270, 300, 340, 380, 420 };
        //20, 50, 90, 140, 200, 270, 350, 440, 540, 650 

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
                while(exp >= levelUpExp[currentLevel])
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
