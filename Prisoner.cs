using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerDilemma
{
    //Options for Prisoner choice.
    public enum Choice
    {
        ratOut,
        beBro,
        noHistory
    }

        //Base class
    public abstract class Prisoner 
    {
        public abstract Choice ChooseWhetherToTalk(Prisoner otherPrisoner); //Defining the method
        public int YearsInPrison = 0;
        public Choice LastChoice = Choice.noHistory;
        public string Name;
    }
    /// <summary>
    /// The Rat always defects.
    /// </summary>
    public class TheRat : Prisoner
    {
        public TheRat()
        {
            Name = "The Rat";
        }
        public override Choice ChooseWhetherToTalk(Prisoner otherPrisoner) //Implementing the method
        {
            return Choice.ratOut;
        }
    }
    /// <summary>
    /// The Bro always cooperates.
    /// </summary>
    public class TheBro : Prisoner
    {
        public TheBro()
        {
            Name = "The Bro";
        }
        public override Choice ChooseWhetherToTalk(Prisoner otherPrisoner)
        {
            return Choice.beBro;
        }
    }

    /// <summary>
    /// Duh.
    /// </summary>
    public class RandomlyBlames : Prisoner
    {
        public RandomlyBlames()
        {
            Name = "The Indecisive One";
        }
        public override Choice ChooseWhetherToTalk(Prisoner otherPrisoner)
        {
            var random = new Random();
            var choice = random.Next(2) == 0 ? Choice.ratOut : Choice.beBro;
            return choice;
        }
    }
    /// <summary>
    /// Behaves identically to the other prisoner's last choice.
    /// </summary>
    public class SameAsLast : Prisoner
    {
        public SameAsLast()
        {
            Name = "The Copycat";
        }
        public override Choice ChooseWhetherToTalk(Prisoner otherPrisoner)
        {
            return (otherPrisoner.LastChoice == Choice.noHistory) ? Choice.beBro : otherPrisoner.LastChoice;
        }
    }

    /// <summary>
    /// Behaves opposite to the other prisoner's last choice.
    /// </summary>
    public class OppositeOfLast : Prisoner
    {
        public OppositeOfLast()
        {
            Name = "The Hipster";
        }
        public override Choice ChooseWhetherToTalk(Prisoner otherPrisoner)
        {
            return (otherPrisoner.LastChoice == Choice.ratOut) ? Choice.beBro : Choice.ratOut;          
        }
    }

}
