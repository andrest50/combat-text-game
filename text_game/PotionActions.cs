using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class PotionActions : Potion
    {
    
        private Potion Potion;
        private HealthPotions HealthPotion;
        //private HealthPotions HealthPotion;
        public String Name { get; set; }

        //public PotionActions()
        //{
        //    InitPotions();
        //}

        //private void InitPotions()
        //{
        //    AllPotions = new List<HealthPotions>();

        //    var healthPotion = new HealthPotions()
        //    {
        //        Name = "HealthPotion",
        //        numHealthPotions = 30,
        //        healthPotionAmount = 30            
        //    };
        //    AllPotions.Add(healthPotion);
        //}

        //public Potion SetStartingPotion()
        //{
        //    Potion = AllPotions.Where(x => x.Name == "HealthPotion").SingleOrDefault();
        //    return Potion;
        //}

        //public void SetPotion(String potionName)
        //{
        //    Potion = AllPotions.Where(x => x.Name == potionName).SingleOrDefault();
        //}



        public Potion GetPotion()
        {
            return Potion;
        }
      

    }
}
