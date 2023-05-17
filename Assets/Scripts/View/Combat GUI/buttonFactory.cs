using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using System;
using System.Text;

sealed class buttonFactory : MonoBehaviour
{
    [Header("Events")]
    public GameEvent testingHeroInjection;
    public GameEvent changeFileRequest;
    public GameObject templateSprite;
    public GameObject backgroundBasis;

    
    public float GridCenterOnBackgroundPercentage = 0.75f;
    private GameObject[] arrayOfObjects;
    private static Vector3[] positionVectors;
    private string[] buttonLabels = { "1", "2", "3", "4", "5", "6" };
    private Vector2 centeringPoint;
    private Vector2 gapBorder;
    private PlayerParty party;
    private Dictionary<int, AbstractActor> partyDictionary;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        testHero h1 = new testHero("Knight 1", 25, 3, 2, 10, 5);
        testHero h2 = new testHero("Knight 2", 25, 7, 9, 10, 5);
        party = new PlayerParty(h1);
        party.AddActor(h2);
        partyDictionary = party.GetPartyPositions();
        int numObjects = buttonLabels.Length;

        Vector3 colliderSize = templateSprite.GetComponent<BoxCollider2D>().bounds.size;
        Vector3 scaleSize = new Vector3(
            3 * templateSprite.transform.localScale.x / (numObjects * 1.25f),
            3 * templateSprite.transform.localScale.y / (numObjects * 1.25f),
            0.01f
        );
        arrayOfObjects = new GameObject[numObjects];
        positionVectors = returnPositionValues(
            numObjects,
            colliderSize.x * scaleSize.x,
            colliderSize.y * scaleSize.y
        );

        for (int i = 0; i < numObjects; i++)
        {
            arrayOfObjects[i] = Instantiate(templateSprite, transform.position, transform.rotation);
            arrayOfObjects[i].transform.localScale = scaleSize;
            arrayOfObjects[i].name = buttonLabels[i];
            arrayOfObjects[i].transform.position = (positionVectors[i]);
            if (partyDictionary.ContainsKey(i + 1))
            {
                testingHeroInjection.Raise(
                    this,
                    new DataPacket(partyDictionary[i + 1], "CharacterData", arrayOfObjects[i].name)
                );
            }
        }
        int randomIndex = UnityEngine.Random.Range(0, arrayOfObjects.Length);
    }

    void Update(){
        DataPacket dataSent;
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            if (partyDictionary.ContainsKey(i + 1))
            {
                dataSent = new DataPacket(partyDictionary[i + 1], "CharacterData", arrayOfObjects[i].name);
            } else {
                dataSent = new DataPacket(null, "CharacterData", arrayOfObjects[i].name);
            }
            testingHeroInjection.Raise(
                this,
                dataSent
            );
        }
    }

    public void ReceiveDataPacket(Component sender, object data)
    {
        DataPacket dPacket = (DataPacket)data;
        if (dPacket.GetLabel() == "SwapRequest")
        {
            int startPosition = Int32.Parse((string)dPacket.GetData());
            int endPosition = Int32.Parse(sender.name);
            Debug.Log(startPosition + " / " + endPosition);
            arrayOfObjects[startPosition - 1].GetComponent<SpriteRenderer>().color = Color.white;
            // testingHeroInjection.Raise(
            //     this,
            //     new DataPacket(
            //         partyDictionary[startPosition],
            //         "CharacterData",
            //         arrayOfObjects[endPosition - 1].name
            //     )
            // );
            // testingHeroInjection.Raise(
            //     this,
            //     new DataPacket(null, "CharacterData", arrayOfObjects[startPosition - 1].name)
            // );
            arrayOfObjects[startPosition - 1].GetComponent<testButton>().ToggleClicked();
            party.moveCharacter(endPosition, partyDictionary[startPosition]);
        }
    }

    public void HandleFileRequest(Component sender, object data){
        DataPacket dPacket = (DataPacket) data;

        if (dPacket.GetLabel() == "SaveRequest"){
            Debug.Log("A save was requested");
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= AbstractParty.MAX_PARTY_SIZE; i++){
                if (partyDictionary.ContainsKey(i)){
                    AbstractActor actor = partyDictionary[i];
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
                    sb.Append("\n");
                }
            }
            string sbResult = sb.ToString();
            Debug.Log(sbResult);
            changeFileRequest.Raise(this, new DataPacket(sbResult, "PartyData", "Save"));
        } else if (dPacket.GetLabel() == "LoadRequest"){
            partyDictionary.Clear();
            Debug.Log("A load was requested");
            string dPacketString = (string) dPacket.GetData();
            string[] dPacketPartyData = dPacketString.Split("\n");
            foreach (string s in dPacketPartyData){
                string[] heroData = s.Split(",");
                if (checkValidHero(heroData)){
                    partyDictionary.Add(
                        Int32.Parse(heroData[0]), 
                        new testHero(heroData[1],
                        Double.Parse(heroData[2]),
                        Double.Parse(heroData[3]),
                        Double.Parse(heroData[4]), 
                        Double.Parse(heroData[5]), 
                        Int32.Parse(heroData[6])
                        )
                    );
                }
            }
        }
    }

    private bool checkValidHero(string[] hero){
        if (hero.Length != 7){
            return false;
        } else {
            return true;
        }
    }

    private Vector3[] returnPositionValues(int numObjects, float width, float length)
    {
        float backgroundCenterPoint = backgroundBasis.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3[] returnSet = new Vector3[numObjects];
        Vector2 gapBorder = new Vector2(width / 20, length / 20);
        int breakPoint = Mathf.CeilToInt(arrayOfObjects.Length / 2);
        Vector2 centeringPoint = new Vector2((gapBorder.x + width), (gapBorder.y + length));
        if (breakPoint % 2 == 0)
        {
            centeringPoint.x = (gapBorder.y + width) * (numObjects - 1) / numObjects;
        }
        if (breakPoint < numObjects)
        {
            centeringPoint.y = (gapBorder.y + length) / 2;
        }
        for (int i = 0; i < breakPoint; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * i) - centeringPoint.x,
                centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        for (int i = breakPoint; i < numObjects; i++)
        {
            returnSet[i] = new Vector3(
                ((gapBorder.x + width) * (i - breakPoint)) - centeringPoint.x,
                -centeringPoint.y - (backgroundCenterPoint * GridCenterOnBackgroundPercentage),
                0f
            );
        }
        return returnSet;
    }

    public Vector3 getPositionVector(int index)
    {
        if (index > arrayOfObjects.Length)
        {
            throw new UnityException("Illegal position!");
        }
        else
        {
            return positionVectors[index];
        }
    }
}
