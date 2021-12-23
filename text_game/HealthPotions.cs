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

        private int healthPotionAmount;

        public int GetHealthPotionAmount()
        {
            return healthPotionAmount;
        }

        public void SetHealthPotionAmount(int healthPotionAmount)
        {
            this.healthPotionAmount = healthPotionAmount;
        }
    }
}
