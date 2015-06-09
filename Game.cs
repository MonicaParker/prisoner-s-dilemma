using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerDilemma
{
    public class Game
    {

        List<Prisoner> PrisonerList = new List<Prisoner>();
        

        public Game()
        {
            PrisonerList.Add(new TheRat()); 
            PrisonerList.Add(new TheBro());
            PrisonerList.Add(new RandomlyBlames());
            PrisonerList.Add(new SameAsLast());
            PrisonerList.Add(new OppositeOfLast());
        }

        List<int> Rounds = new List<int>();

        //Sets user input as the number of rounds to play and runs the game.
        public void UserInput()
        {
            Console.WriteLine("How many rounds would you like to inflict upon these poor prisoners, \nwho are likely model citizens innocent of any real wrongdoing?");
            int roundsSpecified;
            var userInput = int.TryParse(Console.ReadLine(), out roundsSpecified);
            if (userInput == false || roundsSpecified <= 0) Console.WriteLine("Please specify an actual number.");
            else
            {
                PlayingEachOther(roundsSpecified);
            }
        }


        /// <summary>
        /// Matches each prisoner type against every other.
        /// </summary>
        /// <param name="rounds">Number of rounds specified by the user.</param>
        public void PlayingEachOther(int rounds)
        {

            foreach (var prisoner1 in PrisonerList) 
            {
                foreach (var prisoner2 in PrisonerList)
                {
                    DetermineRound(prisoner1, prisoner2, rounds);
                }
            }

            foreach (var prisoner in PrisonerList)
            {
                Console.WriteLine("\n" + prisoner.Name + " gets a total of " + prisoner.YearsInPrison + " years in prison.");
            }


        }

        /// <summary>
        /// Calculates jail time per answer and tallies round results.
        /// </summary>
        /// <param name="guy">First prisoner compared</param>
        /// <param name="guy2">Second prisoner compared</param>
        /// <param name="rounds">Rounds specified by user</param>
        public void DetermineRound(Prisoner guy, Prisoner guy2, int rounds)
        {
            guy.LastChoice = Choice.noHistory;
            guy2.LastChoice = Choice.noHistory;
            for (var i = 0; i < rounds; i++)
            {
                var a = guy.ChooseWhetherToTalk(guy2);
                var b = guy2.ChooseWhetherToTalk(guy);
                guy.LastChoice = a;
                guy2.LastChoice = b;
                var jailTime = 0;
                var jailTime2 = 0;
                // Two years in prison each if both blame
                if (a == Choice.ratOut && b == Choice.ratOut)
                {
                    jailTime += 2;
                    jailTime2 += 2;
                    //guy.LastChoice = a;
                    //guy2.LastChoice = b;
                }
                // No years for rat and three for bro if rat blames.
                else if (a == Choice.beBro && b == Choice.ratOut) jailTime += 3;
                else if (a == Choice.ratOut && b == Choice.beBro) jailTime2 += 3;
                // One year for each if neither blame.
                else
                {
                    jailTime += 1;
                    jailTime2 += 1;
                    //guy.LastChoice = a;
                    //guy2.LastChoice = b;
                }
                Console.WriteLine(guy.Name + " gets " + jailTime + " years.  |  " + guy2.Name + " gets " + jailTime2 + " years.");
                //guy.YearsInPrison = guy.YearsInPrison + jailTime;
                //guy2.YearsInPrison = guy2.YearsInPrison + jailTime2;
                guy.YearsInPrison += jailTime;
                guy2.YearsInPrison += jailTime2;
            }
        }
    }
}
