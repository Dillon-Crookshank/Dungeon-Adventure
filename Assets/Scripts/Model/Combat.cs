using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{

    /// <summary>
    /// A static class for combat logic and simulation.
    /// </summary>
    internal class Combat : MonoBehaviour
    {
        
        private int turnCounter;

        private bool isEndOfTurn;

        private List<AbstractCharacter> characterList;

        private AbstractCharacter activeActor;

        /// <summary>
        /// Takes a player party and an enemy party, and handles the logic for the combat between them.
        /// </summary>
        /// <param name="thePlayerParty">The party of heroes.</param>
        /// <param name="theEnemyParty">The party of enemies.</param>
        internal async void CombatEncounter(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            Debug.Log("Encounter started!");
            turnCounter = 0;

            isEndOfTurn = false;

            characterList = InitiativeRoll(thePlayerParty, theEnemyParty);

            characterList.Sort((x, y) => y.CombatInitiative - x.CombatInitiative);

            while (thePlayerParty.isAllAlive && theEnemyParty.isAllAlive)
            {
                turnCounter++;
                foreach (AbstractCharacter character in characterList)
                {
                    activeActor = character;
                    Debug.LogFormat("{0}, initiative: {1}", activeActor.Name, activeActor.CombatInitiative);
                    await TurnOver(activeActor);
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
        internal List<AbstractCharacter> InitiativeRoll(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            System.Random rng = new System.Random();

            List<AbstractCharacter> characters = new List<AbstractCharacter>();

            foreach (AbstractCharacter actor in (thePlayerParty.GetPartyPositions().Values
            .Concat(theEnemyParty.GetPartyPositions().Values)))
            {
                characters.Add(actor);
            }


            foreach (AbstractCharacter actor in characters)
            {
                actor.CombatInitiative = (actor.Initiative + rng.Next(1, 20));
            }

            return characters;
        }

        private async Task TurnOver(AbstractCharacter character)
        {
            while (!isEndOfTurn)
            {
                await Task.Delay(100);
            }
            await Task.Yield();
        }


        public AbstractCharacter GetActiveActor()
        {
            return activeActor;
        }

        public void EndTurn()
        {
            isEndOfTurn = true;
        }

        public int ActorIndex()
        {
            return activeActor.PartyPosition;
        }

        public bool isPlayer()
        {
            return (activeActor.GetType() == typeof(PlayerCharacter));
        }

    }
}