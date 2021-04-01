using text_game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class HealthPotions : Potion
    {
        public HealthPotions()
        {
            SetNumPotions(30);
            SetHealthPotionAmount(30);
        }

        //public HealthPotions(int numHealthPotions, int healthPotionAmount)
        //{

        //}

        private int numHealthPotions { get; set; }
        private int healthPotionAmount { get; set; }

        //private int numHealthPotions;
        //private int healthPotionAmount;

        public int GetHealthPotionAmount()
        {
            return healthPotionAmount;
        }

        public void SetHealthPotionAmount(int healthPotionAmount)
        {
            this.healthPotionAmount = healthPotionAmount;
        }

        public int GetNumPotions()
        {
            return numHealthPotions;
        }

        public void SetNumPotions(int numHealthPotions)
        {
            this.numHealthPotions = numHealthPotions;
        }

        public void IncrementNumHealthPotions(int numHealthPotions)
        {
            this.numHealthPotions += numHealthPotions;
        }
    }
}
