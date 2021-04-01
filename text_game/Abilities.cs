using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class Abilities
    {
        public string Name { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int Cooldown { get; set; }
        public int MinimumHealing { get; set; }
        public int MaximumHealing { get; set; }
        public int Cost { get; set; }
    }
}
