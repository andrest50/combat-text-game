using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_game
{
    public class GameLoop
    {

        public static void Loop(EnemyActions enemyActions, Player player, GameMessages txt, GameMechanics mechanics, WeaponActions weapon)
        {
            
            while (mechanics.running)
            {
                mechanics.SetStartingEnemyValues();
                mechanics.EnemyAppear();             

                while (enemyActions.health > 0)
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
