using System.Linq;
using System;
using System.Collections.Generic;

namespace DefaultNamespace
{

    /// <summary>
    /// A static class for combat logic and simulation.
    /// </summary>
    internal static class Combat
    {


        /// <summary>
        /// Takes a player party and an enemy party, and handles the logic for the combat between them.
        /// </summary>
        /// <param name="thePlayerParty">The party of heroes.</param>
        /// <param name="theEnemyParty">The party of enemies.</param>
        internal static void CombatEncounter(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            int turnCounter = 0;

            List<AbstractActor> characterList = InitiativeRoll(thePlayerParty, theEnemyParty);

            characterList.Sort((x, y) => y.GetCombatInitiative() - x.GetCombatInitiative());



            while (thePlayerParty.isAllAlive && theEnemyParty.isAllAlive)
            {
                turnCounter++;
                foreach (AbstractActor character in characterList)
                {
                    {
                        // set active character to selected character in index
                        // await their action to go to next character
                    }
                }

                if (!theEnemyParty.isAllAlive)
                {
                    // Congrats! You won!
                }
                else if (!thePlayerParty.isAllAlive)
                {
                    // You've been defeated...
                }
            }
        }


        /// <summary>
        /// Dual purpose: This combines all of the participating Actors into a single list, and
        /// generates their combat initiative by appending a D20 to their base initiatives.
        /// </summary>
        /// <param name="thePlayerParty">The party of heroes.</param>
        /// <param name="theEnemyParty">The party of enemies.</param>
        /// <returns></returns>
        internal static List<AbstractActor> InitiativeRoll(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            Random rng = new Random();

            List<AbstractActor> characters = new List<AbstractActor>();

            foreach (AbstractActor actor in (thePlayerParty.GetPartyPositions().Values
            .Concat(theEnemyParty.GetPartyPositions().Values)))
            {
                characters.Add(actor);
            }


            foreach (AbstractActor actor in characters)
            {
                actor.SetCombatInitiative(actor.GetInitiative() + rng.Next(1, 20));
            }

            return characters;
        }

    }
}