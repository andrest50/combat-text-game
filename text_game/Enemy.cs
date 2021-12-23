using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Enemy
    {
        public string Name { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int MinimumHealth { get; set; }
        public int MaximumHealth { get; set; }
        public double OneHealthPotionDropRate { get; set; }
        public double TwoHealthPotionDropRate { get; set; }
        public double ThreeHealthPotionDropRate { get; set; }
        public int HealthPotionDropCount { get; set; }

    }
}
