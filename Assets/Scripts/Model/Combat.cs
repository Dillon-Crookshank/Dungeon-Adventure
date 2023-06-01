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
    internal class Combat
    {
        
        private int turnCounter;

        private bool isEndOfTurn;

        private List<AbstractCharacter> characterList;

        private AbstractCharacter myActiveCharacter;

        private PlayerParty myPlayerParty;

        private EnemyParty myEnemyParty;

        internal Combat(PlayerParty thePlayerParty, EnemyParty theEnemyParty) {
            myPlayerParty = thePlayerParty;
            myEnemyParty = theEnemyParty;

            CombatEncounter();
        }

        /// <summary>
        /// Takes a player party and an enemy party, and handles the logic for the combat between them.
        /// </summary>
        internal async void CombatEncounter()
        {
            Debug.Log("Encounter started!");
            turnCounter = 0;

            isEndOfTurn = false;

            characterList = InitiativeRoll();

            characterList.Sort((x, y) => y.CombatInitiative - x.CombatInitiative);

            while (myPlayerParty.isAllAlive && myEnemyParty.isAllAlive)
            {
                turnCounter++;
                foreach (AbstractCharacter character in characterList)
                {
                    myActiveCharacter = character;
                    Debug.LogFormat("{0}, initiative: {1}", myActiveCharacter.Name, myActiveCharacter.CombatInitiative);
                    
                    if (!isPlayer()) {
                        //Select random move
                        if (myActiveCharacter.IsAlive()){
                            await Task.Delay(500);
                            GameObject.Find("Combat Log").SendMessage("UpdateCombatLog", (myActiveCharacter.Name) + " attacks " + (myPlayerParty.GetPartyPositions())[1].Name + "!");
                            myActiveCharacter.BasicAttack((myPlayerParty.GetPartyPositions())[1]);
                        }
                        isEndOfTurn = true;
                    }

        
                    await TurnOver(myActiveCharacter);
                    isEndOfTurn = false;
                }
            }
        }


        /// <summary>
        /// Dual purpose: This combines all of the participating Actors into a single list, and
        /// generates their combat initiative by appending a D20 to their base initiatives.
        /// </summary>
        /// <returns></returns>
        internal List<AbstractCharacter> InitiativeRoll()
        {
            System.Random rng = new System.Random();

            List<AbstractCharacter> characters = new List<AbstractCharacter>();

            foreach (AbstractCharacter actor in (myPlayerParty.GetPartyPositions().Values
            .Concat(myEnemyParty.GetPartyPositions().Values)))
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
            return myActiveCharacter;
        }

        public void EndTurn()
        {
            isEndOfTurn = true;
        }

        public int ActorIndex()
        {
            return myActiveCharacter.PartyPosition;
        }

        public bool isPlayer()
        {
            return (myActiveCharacter.GetType() == typeof(PlayerCharacter));
        }

    }
}