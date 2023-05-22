using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace DefaultNamespace
{

    /// <summary>
    /// A static class for combat logic and simulation.
    /// </summary>
    internal class Combat
    {

        private int turnCounter;

        private bool isEndOfTurn;

        private List<AbstractActor> characterList;

        private AbstractActor activeActor;

        /// <summary>
        /// Takes a player party and an enemy party, and handles the logic for the combat between them.
        /// </summary>
        /// <param name="thePlayerParty">The party of heroes.</param>
        /// <param name="theEnemyParty">The party of enemies.</param>
        internal async void CombatEncounter(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            turnCounter = 0;

            isEndOfTurn = false;

            characterList = InitiativeRoll(thePlayerParty, theEnemyParty);

            characterList.Sort((x, y) => y.GetCombatInitiative() - x.GetCombatInitiative());



            while (thePlayerParty.isAllAlive && theEnemyParty.isAllAlive)
            {
                turnCounter++;
                foreach (AbstractActor character in characterList)
                {
                    activeActor = character;
                    await TurnOver(isEndOfTurn);
                    isEndOfTurn = false;
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
        internal List<AbstractActor> InitiativeRoll(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
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

        private async Task TurnOver(bool isEndOfTurn)
        {
            while (!isEndOfTurn)
            {
                await Task.Delay(100);
            }
        }

        public AbstractActor GetActiveActor()
        {
            return activeActor;
        }

        public void EndTurn()
        {
            isEndOfTurn = true;
        }

        public int ActorIndex()
        {
            return activeActor.GetPartyPosition();
        }

        public bool isPlayer()
        {
            return (activeActor.GetType() == typeof(PlayerCharacter));
        }

    }
}