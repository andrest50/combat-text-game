using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class ShieldPotion : Potion
    {
        public ShieldPotion()
        {
            SetNumPotions(5);
            SetShieldTurns(3);
            SetShieldActive(false);
        }

        private int shieldTurns;
        private bool shieldActive;

        public int GetShieldTurns()
        {
            return shieldTurns;
        }

        public void SetShieldTurns(int shieldTurns)
        {
            this.shieldTurns = shieldTurns;
        }

        public bool GetShieldActive()
        {
            return shieldActive;
        }

        public void SetShieldActive(bool shieldActive)
        {
            this.shieldActive = shieldActive;
        }

    }
}
