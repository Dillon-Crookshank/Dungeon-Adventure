using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonAdventure
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

        internal Combat(PlayerParty thePlayerParty, EnemyParty theEnemyParty)
        {
            myPlayerParty = thePlayerParty;
            myEnemyParty = theEnemyParty;
            GameObject.Find("Combat Log").SendMessage("ClearCombatLog");
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
                    character.CurrentMana = 5;
                    Debug.LogFormat("{0}, initiative: {1}", myActiveCharacter.Name, myActiveCharacter.CombatInitiative);

                    if (!isPlayer())
                    {
                        GameObject.Find("ActionButtons").SendMessage("UnlockButtons", false);
                        //Select random move
                        if (myActiveCharacter.IsAlive())
                        {
                            await Task.Delay(500);

                            PlayerCharacter target = (PlayerCharacter)myPlayerParty.GetPartyPositions().ElementAt(Random.Range(0, myPlayerParty.GetPartyPositions().Count)).Value;

                            GameObject.Find("Combat Log").SendMessage("UpdateCombatLog", (myActiveCharacter.Name) + " attacks " + target.Name + "...");
                            await Task.Delay(300);
                            double damage = myActiveCharacter.BasicAttack(target);
                            GameObject.Find("Combat Log").SendMessage("UpdateCombatLog", ("The attack deals " + damage + " damage!"));
                            if (!target.IsAlive()) { GameObject.Find("Combat Log").SendMessage("UpdateCombatLog", (target.Name + " dies!")); }
                        }
                        isEndOfTurn = true;
                    }
                    else
                    {
                        GameObject.Find("ActionButtons").SendMessage("UnlockButtons", true);
                    }
                    if (!(myPlayerParty.isAllAlive && myEnemyParty.isAllAlive))
                    {
                        break;
                    }
                    await TurnOver(myActiveCharacter);
                    isEndOfTurn = false;
                    if (!(myPlayerParty.isAllAlive && myEnemyParty.isAllAlive))
                    {
                        break;
                    }
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