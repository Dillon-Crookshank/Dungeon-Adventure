using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonAdventure
{

    /// <summary>
    /// A class for combat logic and simulation.
    /// </summary>
    internal class Combat
    {

        /// <summary>
        /// The current turn number.
        /// </summary>
        private int turnCounter;

        /// <summary>
        /// A boolean for whether an individual Character's turn has ended.
        /// </summary>
        internal bool isEndOfTurn;

        /// <summary>
        /// A List of all the Characters involved in the Combat.
        /// </summary>
        private List<AbstractCharacter> characterList;

        /// <summary>
        /// The currently active Character in the Combat.
        /// </summary>
        private AbstractCharacter myActiveCharacter;

        /// <summary>
        /// The Player's party in the Combat.
        /// </summary>
        private PlayerParty myPlayerParty;

        /// <summary>
        /// The Enemy's party in the Combat.
        /// </summary>
        private EnemyParty myEnemyParty;

        /// <summary>
        /// Constructor for Combat that sets the different parties and starts a Combat encounter.
        /// </summary>
        /// <param name="thePlayerParty">The Player's party.</param>
        /// <param name="theEnemyParty">The Enemy's party.</param>
        internal Combat(in PlayerParty thePlayerParty, in EnemyParty theEnemyParty)
        {
            myPlayerParty = thePlayerParty;
            myEnemyParty = theEnemyParty;
            GameObject.Find("Combat Log").SendMessage("ClearCombatLog");
            CombatEncounter();
        }

        /// <summary>
        /// Combat logic simulator.
        /// Builds the combined list of Characters and sorts them by their Initiative rolls.
        /// Continues a combat loop until one party is defeated.
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

                        GameObject.Find("Combat Log").SendMessage("UpdateCombatLog", "It is " + (myActiveCharacter.Name) + "'s turn!");
                        GameObject.Find("ActionButtons").SendMessage("UnlockButtons", true);
                        if (myActiveCharacter.CurrentMana < myActiveCharacter.MySpecialAttack.SpecialAttackManaCost)
                        {
                            GameObject.Find("SpecialAttackButton").SendMessage("SetClickable", false);
                        }
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
        /// <returns>The combined list of Characters in Combat sorted by CombatInitiative.</returns>
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

        /// <summary>
        /// Checks every 100ms for the end of the Player's turn after an action is performed.
        /// </summary>
        /// <param name="character">The character who's turn we are awaiting to finish.</param>
        private async Task TurnOver(AbstractCharacter character)
        {
            while (!isEndOfTurn)
            {
                await Task.Delay(100);
            }
            await Task.Yield();
        }


        /// <summary>
        /// Accessor for the ActiveCharacter.
        /// </summary>
        /// <returns></returns>
        public AbstractCharacter GetActiveCharacter()
        {
            return myActiveCharacter;
        }

        /// <summary>
        /// Ends the current character's turn.
        /// </summary>
        internal void EndTurn()
        {
            isEndOfTurn = true;
        }

        /// <summary>
        /// Checks whether the Active Character is a player or an enemy.
        /// </summary>
        /// <returns>True if Active Character is a player.</returns>
        internal bool isPlayer()
        {
            return (myActiveCharacter.GetType() == typeof(PlayerCharacter));
        }

    }
}