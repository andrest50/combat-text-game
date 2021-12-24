using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static text_game.Global;

namespace text_game
{
    public class GameLoop
    {

        public static void Loop()
        {
            
            while (mechanics.running)
            {
                mechanics.SetStartingEnemyValues();
                mechanics.EnemyAppear();

                while (enemyActions.GetHealth() > 0)
                {                    
                    //mechanics.PromptSimulateAttacks();
                    mechanics.EncounterOptions();

                    if(!player.GetRan())
                    {
                        if (player.GetHealth() < 1)
                        {
                            mechanics.GameOver();
                            mechanics.running = false;
                            break;
                        }

                        if (enemyActions.health < 1)
                        {
                            mechanics.EnemyDead();
                        }
                    }                     
                }
                Console.ReadLine();
            }
       }
   }
        
}
