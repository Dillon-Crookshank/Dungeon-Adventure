using DefaultNamespace;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// A class to generate usuable cells to move heroes within.
/// </summary>
sealed class buttonFactory : MonoBehaviour
{
    [Header("Events")]
    /// <summary>
    /// The GameEvent called whenever the GUI needs to update its cells.
    /// </summary>
    [SerializeField]
    GameEvent GUIUpdate;

    /// <summary>
    /// The GameEvent called whenever the GUI needs to read in or send out a state of the party.
    /// </summary>
    [SerializeField]
    GameEvent changeFileRequest;


    [Header("Important Sprites")]
    /// <summary>
    /// A reference to the Template object, upon which all hero cells are based upon.
    /// </summary>
    [SerializeField]
    GameObject templateSprite;

    /// <summary>
    /// A reference to the background of the GUI, to accurately place the cells according
    /// to the DISTANCE_FROM_CENTER constant.
    /// </summary>
    [SerializeField]
    GameObject backgroundBasis;

    /// <summary>
    /// A constant to reference a percentage of distance that the center of the button factory is placed from
    /// the center of the screen.
    /// </summary>
    private const float DISTANCE_FROM_CENTER = 0.3f;

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
    /// An array of strings to give the cell game objects names.
    /// </summary>
    private string[] myButtonLabels = { "1", "2", "3", "4", "5", "6" };

    /// <summary>
    /// A party for testing purposes.
    /// </summary>
    private PlayerParty myTestParty;

    /// <summary>
    /// A reference to the party's dictionary of positions, for testing purposes.
    /// </summary>
    private Dictionary<int, AbstractActor> myTestPartyDictionary;

    /// <summary>
    /// Generates the cells and places two knights in their appropriate locations as a basis
    /// for the GUI.
    /// </summary>
    void Start()
    {
        testHero h1 = new testHero("Knight 1", 25, 3, 2, 10, 5);
        testHero h2 = new testHero("Knight 2", 25, 7, 9, 10, 5);
        myTestParty = new PlayerParty(h1);
        myTestParty.AddActor(h2);
        myTestPartyDictionary = myTestParty.GetPartyPositions();

        Vector3 colliderSize = templateSprite.GetComponent<BoxCollider2D>().bounds.size;
        Vector3 scaleSize = new Vector3(
            3 * templateSprite.transform.localScale.x / (MAX_PARTY_SIZE * 1.25f),
            3 * templateSprite.transform.localScale.y / (MAX_PARTY_SIZE * 1.25f),
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
                templateSprite,
                transform.position,
                transform.rotation
            );
            myArrayOfObjects[i].transform.localScale = scaleSize;
            myArrayOfObjects[i].name = myButtonLabels[i];
            myArrayOfObjects[i].transform.position = (myPositionVectors[i]);
            if (myTestPartyDictionary.ContainsKey(i + 1))
            {
                GUIUpdate.Raise(
                    this,
                    new DataPacket(
                        myTestPartyDictionary[i + 1],
                        "CharacterData",
                        myArrayOfObjects[i].name
                    )
                );
            }
        }
        int randomIndex = UnityEngine.Random.Range(0, MAX_PARTY_SIZE);
    }

    /// <summary>
    /// Updates the GUI every frame to correlate to the current positioning of each hero.
    /// </summary>
    void Update()
    {
        myTestPartyDictionary = myTestParty.GetPartyPositions();
        DataPacket dataSent;
        for (int i = 0; i < MAX_PARTY_SIZE; i++)
        {
            if (myTestPartyDictionary.ContainsKey(i + 1))
            {
                dataSent = new DataPacket(
                    myTestPartyDictionary[i + 1],
                    "CharacterData",
                    myArrayOfObjects[i].name
                );
            }
            else
            {
                dataSent = new DataPacket(null, "CharacterData", myArrayOfObjects[i].name);
            }
            GUIUpdate.Raise(this, dataSent);
        }
    }

    /// <summary>
    /// Intakes a DataPacket and utilizes the data, depending on the label of the packet.
    /// Can be expanded upon to include more behaviors.
    /// </summary>
    /// <param name="sender"> The component that sent the DataPacket. </param>
    /// <param name="data"> The object (DataPacket) held. </param>
    public void ReceiveDataPacket(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket)data;
        if (dPacket.GetLabel() == "SwapRequest")
        {
            Debug.Log((string)dPacket.GetData());
            int startPosition = Int32.Parse((string)dPacket.GetData());
            int endPosition = Int32.Parse(sender.name);
            Debug.Log(startPosition + " / " + endPosition);
            myArrayOfObjects[startPosition - 1].GetComponent<SpriteRenderer>().color = Color.white;
            myArrayOfObjects[startPosition - 1].GetComponent<testButton>().ToggleClicked();
            myTestParty.moveCharacter(endPosition, myTestPartyDictionary[startPosition]);
        }
    }

    /// <summary>
    /// Handles saving and loading parties.
    /// Can be expanded upon to include more behaviors.
    /// </summary>
    /// <param name="sender"> The component that sent the DataPacket. </param>
    /// <param name="data"> The object (DataPacket) held. </param>
    public void HandleFileRequest(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket)data;

        if (dPacket.GetLabel() == "SaveRequest")
        {
            Debug.Log("A save was requested");
            StringBuilder sb = new StringBuilder();
            bool moreThanOne = false;
            for (int i = 1; i <= AbstractParty.MAX_PARTY_SIZE; i++)
            {
                if (myTestPartyDictionary.ContainsKey(i))
                {
                    if (moreThanOne)
                    {
                        sb.Append("\n");
                    }
                    else
                    {
                        moreThanOne = true;
                    }
                    AbstractActor actor = myTestPartyDictionary[i];
                    sb.Append(i);
                    sb.Append(",");
                    sb.Append(actor.GetName());
                    sb.Append(",");
                    sb.Append(actor.GetCurrentHitpoints());
                    sb.Append(",");
                    sb.Append(actor.GetAttack());
                    sb.Append(",");
                    sb.Append(actor.GetDefence());
                    sb.Append(",");
                    sb.Append(actor.GetCurrentMana());
                    sb.Append(",");
                    sb.Append(actor.GetCombatInitiative());
                }
            }
            string sbResult = sb.ToString();
            Debug.Log(sbResult);
            changeFileRequest.Raise(this, new DataPacket(sbResult, "PartyData", "Save"));
        }
        else if (dPacket.GetLabel() == "LoadRequest")
        {
            Debug.Log("A load was requested");
            string dPacketString = (string)dPacket.GetData();
            string[] dPacketPartyData = dPacketString.Split("\n");

            bool stillLoadFlag = true;

            Dictionary<int, AbstractActor> loadPartyDictionary =
                new Dictionary<int, AbstractActor>();

            foreach (string s in dPacketPartyData)
            {
                Debug.Log(s);
                string[] heroData = s.Split(",");
                testHero loadActor = checkValidHero(heroData);
                if (
                    loadActor != null
                    && !(loadPartyDictionary.ContainsKey(Int32.Parse(heroData[0])))
                )
                {
                    loadPartyDictionary.Add(Int32.Parse(heroData[0]), loadActor);
                }
                else
                {
                    stillLoadFlag = false;
                    break;
                }
            }
            if (stillLoadFlag)
            {
                PlayerParty loadParty = null;
                bool moreThanOne = false;
                for (int i = 1; i <= 6; i++)
                {
                    if (loadPartyDictionary.ContainsKey(i))
                    {
                        if (!moreThanOne)
                        {
                            moreThanOne = true;
                            loadParty = new PlayerParty((testHero)loadPartyDictionary[i]);
                        }
                        else
                        {
                            loadParty.AddActor(loadPartyDictionary[i]);
                        }
                    }
                }
                myTestParty = loadParty;
            }
        }
    }


    /// <summary>
    /// Checks if a given string array representation is a valid set of data.
    /// </summary>
    /// <param name="hero"> The array representation of the hero. </param>
    private testHero checkValidHero(string[] hero)
    {
        Debug.Log(hero.Length);
        if (hero.Length != 7)
        {
            return null;
        }
        else
        {
            double[] loadStats = new double[5];
            for (int i = 2; i <= 6; i++)
            {
                if (!Double.TryParse(hero[i], out loadStats[i - 2]))
                {
                    return null;
                }
            }
            return new testHero(
                hero[1],            // Name
                loadStats[0],       // HP
                loadStats[1],       // Attack
                loadStats[2],       // Defense
                loadStats[3],       // Mana
                (int)loadStats[4]   // Intiative
            );
        }
    }

    /// <summary>
    /// Returns the position vectors to be used to place the cells.
    /// </summary>
    /// <param name="width"> The width of the cell. </param>
    /// <param name="length"> The length of the cell. </param>
    /// <returns> The array of position vectors. </returns>
    
    private Vector3[] returnPositionVectors(float width, float length)
    {
        float backgroundCenterPoint = backgroundBasis.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3[] returnSet = new Vector3[MAX_PARTY_SIZE];
        Vector2 gapBorder = new Vector2(width / 20, length / 20);
        int breakPoint = Mathf.CeilToInt(MAX_PARTY_SIZE / 2);
        Vector2 centeringPoint = new Vector2((gapBorder.x + width), (gapBorder.y + length));
        if (breakPoint % 2 == 0)
        {
            centeringPoint.x = (gapBorder.y + width) * (MAX_PARTY_SIZE - 1) / MAX_PARTY_SIZE;
        }
        if (breakPoint < MAX_PARTY_SIZE)
        {
            centeringPoint.y = (gapBorder.y + length) / 2;
        }
        for (int i = 0; i < breakPoint; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * i) - centeringPoint.x,
                centeringPoint.y - (backgroundCenterPoint * DISTANCE_FROM_CENTER),
                0f
            );
        }
        for (int i = breakPoint; i < MAX_PARTY_SIZE; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * (i - breakPoint)) - centeringPoint.x,
                -centeringPoint.y - (backgroundCenterPoint * DISTANCE_FROM_CENTER),
                0f
            );
        }
        return returnSet;
    }

    
}
