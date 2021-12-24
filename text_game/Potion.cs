using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Potion
    {
        public int NumPotions { get; set; }
        public int Cost { get; set; }

        public Potion()
        {

        }

        public Potion(int numPotions)
        {
            NumPotions = numPotions;
        }

        public int GetNumPotions()
        {
            return NumPotions;
        }

        public void SetNumPotions(int numPotions)
        {
            this.NumPotions = numPotions;
        }

        public int GetCost()
        {
            return Cost;
        }

        public void SetCost(int cost)
        {
            this.Cost = cost;
        }

        public void IncrementNumPotions(int numPotions)
        {
            this.NumPotions += numPotions;
        }

    }
}
