using UnityEngine;
using System.Collections.Generic;

namespace DungeonAdventure
{
    /// <summary>
    /// A class to generate usuable cells to display information about the enemy party.
    /// </summary>
    sealed class enemyButtonFactory : MonoBehaviour
    {
        // [Header("Events")]
        // /// <summary>
        // /// The GameEvent called whenever the GUI needs to update its cells.
        // /// </summary>
        // [SerializeField]
        // GameEvent GUIUpdate;

        [Header("Important Sprites")]
        /// <summary>
        /// A reference to the Template object, upon which all hero cells are based upon. Defined in the editor.
        /// </summary>
        [SerializeField]
        GameObject myTemplateSprite;

        /// <summary>
        /// A reference to the background of the GUI, to accurately place the cells according
        /// to the DISTANCE_FROM_CENTER constant. Defined in the editor.
        /// </summary>
        [SerializeField]
        private GameObject myBackgroundBasis;

        /// <summary>
        /// An string to add to the beginning of each button object's name.
        /// </summary>
        private const string BUTTON_PREFIX = "E";

        /// <summary>
        /// A constant to reference a percentage of distance that the center of the button factory is placed from
        /// the center of the screen.
        /// </summary>
        private const float DISTANCE_FROM_CENTER = -0.25f;

        /// <summary>
        /// A constant to reference the maximum number of cells in a party.
        /// </summary>
        private const int MAX_PARTY_SIZE = AbstractParty.MAX_PARTY_SIZE;

        /// <summary>
        /// An array of game objects to store the references to each cell as they are instantiated.
        /// </summary>
        private GameObject[] myArrayOfObjects;

        /// <summary>
        /// An array of vectors to position each of the cells correctly.
        /// </summary>
        private static Vector3[] myPositionVectors;

        /// <summary>
        /// A party for testing purposes.
        /// </summary>
        private EnemyParty myEnemyParty;

        /// <summary>
        /// A reference to the currently active character.
        /// </summary>
        private AbstractCharacter myActiveCharacter;

        /// <summary>
        /// A reference to the party's dictionary of positions, for testing purposes.
        /// </summary>
        private Dictionary<int, AbstractCharacter> myEnemyPartyDictionary;

        /// <summary>
        /// Generates the cells and places two knights in their appropriate locations as a basis
        /// for the GUI.
        /// </summary>
        void Start()
        {
            Vector3 colliderSize = myTemplateSprite.GetComponent<BoxCollider2D>().bounds.size;
            Vector3 scaleSize = new Vector3(
                3 * myTemplateSprite.transform.localScale.x / (MAX_PARTY_SIZE * 1.25f),
                3 * myTemplateSprite.transform.localScale.y / (MAX_PARTY_SIZE * 1.25f),
                0.01f
            );
            myArrayOfObjects = new GameObject[MAX_PARTY_SIZE];
            myPositionVectors = returnPositionVectors(
                colliderSize.x * scaleSize.x,
                colliderSize.y * scaleSize.y
            );

            for (int i = 0; i < MAX_PARTY_SIZE; i++)
            {
                myArrayOfObjects[i] = Instantiate(
                    myTemplateSprite,
                    transform.position,
                    transform.rotation
                );
                myArrayOfObjects[i].transform.localScale = scaleSize;
                myArrayOfObjects[i].name = BUTTON_PREFIX + (i + 1);
                myArrayOfObjects[i].transform.position = (myPositionVectors[i]);
            }
        }

        /// <summary>
        /// Updates the GUI every frame to correlate to the current positioning of each enemy.
        /// </summary>
        void Update()
        {
            if (myEnemyParty != null)
            {
                myEnemyPartyDictionary = myEnemyParty.GetPartyPositions();
                for (int i = 0; i < MAX_PARTY_SIZE; i++)
                {
                    if (myEnemyPartyDictionary.ContainsKey(i + 1))
                    {
                        myArrayOfObjects[i].SendMessage(
                            "SetCharacterRepresentative",
                            myEnemyPartyDictionary[i + 1]
                        );
                        if (myActiveCharacter != null)
                        {
                            myArrayOfObjects[i].SendMessage("CheckActivePlayer", myActiveCharacter);
                        }
                    }
                    else
                    {
                        myArrayOfObjects[i].SendMessage("SetNullCharacterRepresentative");
                    }
                }
            }
        }

        /// <summary>
        /// Returns the position vectors to be used to place the enemy cells.
        /// </summary>
        /// <param name="theWidth"> The width of the cell. </param>
        /// <param name="theLength"> The length of the cell. </param>
        /// <returns> The array of position vectors. </returns>
        private Vector3[] returnPositionVectors(float theWidth, float theLength)
        {
            float backgroundCenterPoint = myBackgroundBasis
                .GetComponent<SpriteRenderer>()
                .bounds.size.y;
            Vector3[] returnSet = new Vector3[MAX_PARTY_SIZE];
            Vector2 gapBorder = new Vector2(theWidth / 20, theLength / 20);
            int breakPoint = Mathf.CeilToInt(MAX_PARTY_SIZE / 2);
            Vector2 centeringPoint = new Vector2(
                (gapBorder.x + theWidth),
                (gapBorder.y + theLength)
            );
            if (breakPoint % 2 == 0)
            {
                centeringPoint.x = (gapBorder.y + theWidth) * (MAX_PARTY_SIZE - 1) / MAX_PARTY_SIZE;
            }
            if (breakPoint < MAX_PARTY_SIZE)
            {
                centeringPoint.y = (gapBorder.y + theLength) / 2;
            }
            for (int i = 0; i < breakPoint; i++)
            {
                returnSet[i] = new Vector3(
                    ((gapBorder.x + theWidth) * i) - centeringPoint.x,
                    centeringPoint.y - (backgroundCenterPoint * DISTANCE_FROM_CENTER),
                    0f
                );
            }
            for (int i = breakPoint; i < MAX_PARTY_SIZE; i++)
            {
                returnSet[i] = new Vector3(
                    ((gapBorder.x + theWidth) * (i - breakPoint)) - centeringPoint.x,
                    -centeringPoint.y - (backgroundCenterPoint * DISTANCE_FROM_CENTER),
                    0f
                );
            }
            return returnSet;
        }

        /// <summary>
        /// Changes what party to display on the cells.
        /// </summary>
        /// <param name="theParty"> The enemy party. </param>
        void setDisplayedParty(EnemyParty theParty)
        {
            myEnemyParty = theParty;
        }

        /// <summary>
        /// Changes the reference of the character that is taking their turn.
        /// </summary>
        /// <param name="theAbstractCharacter"> The current turn's character. </param>
        void SetActiveCharacter(AbstractCharacter theAbstractCharacter)
        {
            myActiveCharacter = theAbstractCharacter;
        }

        // /// <summary>
        // /// Intakes a DataPacket and utilizes the data, depending on the label of the packet.
        // /// Can be expanded upon to include more behaviors.
        // /// </summary>
        // /// <param name="sender"> The component that sent the DataPacket. </param>
        // /// <param name="data"> The object (DataPacket) held. </param>
        // public void ReceiveDataPacket(Component sender, object data)
        // {
        //     DataPacket dPacket = (DataPacket)data;
        //     if (dPacket.GetLabel() == "SwapRequest")
        //     {
        //         // Debug.Log((string)dPacket.GetData());
        //         int startPosition = Int32.Parse((string)dPacket.GetData());
        //         int endPosition = Int32.Parse(sender.name);
        //         // Debug.Log(startPosition + " / " + endPosition);
        //         myArrayOfObjects[startPosition - 1].GetComponent<SpriteRenderer>().color = Color.white;
        //         myArrayOfObjects[startPosition - 1].GetComponent<enemyCell>().ToggleClicked();
        //         myEnemyParty.moveCharacter(endPosition, myEnemyPartyDictionary[startPosition]);
        //     }
        // }

        // /// <summary>
        // /// Checks if a given string array representation is a valid set of data.
        // /// </summary>
        // /// <param name="enemy"> The array representation of the enemy. </param>
        // private EnemyCharacter checkValidEnemy(string[] enemy)
        // {
        //     Debug.Log(enemy.Length);
        //     if (enemy.Length != 7)
        //     {
        //         return null;
        //     }
        //     else
        //     {
        //         double[] loadStats = new double[5];
        //         for (int i = 2; i <= 6; i++)
        //         {
        //             if (!Double.TryParse(enemy[i], out loadStats[i - 2]))
        //             {
        //                 return null;
        //             }
        //         }
        //         return new EnemyCharacter(
        //             enemy[1], // Name
        //             loadStats[0], // HP
        //             loadStats[1], // Attack
        //             loadStats[2], // Defense
        //             loadStats[3], // Mana
        //             (int)loadStats[4] // Intiative
        //         );
        //     }
        // }
    }
}
