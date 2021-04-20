using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace text_game
{
    public class Utils
    {

        public Utils()
        {

        }

        public int GetIntInput(int minNum, int maxNum)
        {
            String choice;
            int choiceInt = 0;
            bool valid = false;
            while (!valid)
            {
                choice = Console.ReadLine();

                if (!Regex.IsMatch(choice, @"^\d+$"))
                    continue;

                choiceInt = Convert.ToInt32(choice);

                if (choiceInt > maxNum || choiceInt < minNum)
                    continue;

                valid = true;
            }
            return choiceInt;
        }
    }
}