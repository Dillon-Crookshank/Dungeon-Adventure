using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using System;
using System.Text;

sealed class buttonFactory : MonoBehaviour
{
    [Header("Events")]
    public GameEvent GUIUpdate;
    public GameEvent changeFileRequest;
    public GameObject templateSprite;
    public GameObject backgroundBasis;

    
    public float GridCenterOnBackgroundPercentage = 0.75f;
    private GameObject[] arrayOfObjects;
    private static Vector3[] positionVectors;
    private string[] buttonLabels = { "1", "2", "3", "4", "5", "6" };
    private PlayerParty party;
    private Dictionary<int, AbstractActor> partyDictionary;

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
                GUIUpdate.Raise(
                    this,
                    new DataPacket(partyDictionary[i + 1], "CharacterData", arrayOfObjects[i].name)
                );
            }
        }
        int randomIndex = UnityEngine.Random.Range(0, arrayOfObjects.Length);
    }

    void Update(){
        partyDictionary = party.GetPartyPositions();
        DataPacket dataSent;
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            if (partyDictionary.ContainsKey(i + 1))
            {
                dataSent = new DataPacket(partyDictionary[i + 1], "CharacterData", arrayOfObjects[i].name);
            } else {
                dataSent = new DataPacket(null, "CharacterData", arrayOfObjects[i].name);
            }
            GUIUpdate.Raise(
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
            arrayOfObjects[startPosition - 1].GetComponent<testButton>().ToggleClicked();
            party.moveCharacter(endPosition, partyDictionary[startPosition]);
        }
    }

    public void HandleFileRequest(Component sender, object data){
        DataPacket dPacket = (DataPacket) data;

        if (dPacket.GetLabel() == "SaveRequest"){
            Debug.Log("A save was requested");
            StringBuilder sb = new StringBuilder();
            bool moreThanOne = false;
            for (int i = 1; i <= AbstractParty.MAX_PARTY_SIZE; i++){
                if (partyDictionary.ContainsKey(i)){
                    if (moreThanOne){
                        sb.Append("\n");
                    } else {
                        moreThanOne = true;
                    }
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
                }
            }
            string sbResult = sb.ToString();
            Debug.Log(sbResult);
            changeFileRequest.Raise(this, new DataPacket(sbResult, "PartyData", "Save"));
        } else if (dPacket.GetLabel() == "LoadRequest"){
            
            Debug.Log("A load was requested");
            string dPacketString = (string) dPacket.GetData();
            string[] dPacketPartyData = dPacketString.Split("\n");

            bool stillLoadFlag = true;

            Dictionary<int, AbstractActor> loadPartyDictionary = new Dictionary<int, AbstractActor>();

            foreach (string s in dPacketPartyData){
                Debug.Log(s);
                string[] heroData = s.Split(",");
                testHero loadActor = checkValidHero(heroData);
                if (loadActor != null && !(loadPartyDictionary.ContainsKey(Int32.Parse(heroData[0])))){
                    loadPartyDictionary.Add(Int32.Parse(heroData[0]), loadActor);
                } else {
                    stillLoadFlag = false;
                    break;
                }
            }
            if (stillLoadFlag){

                PlayerParty loadParty = null;
                bool moreThanOne = false;
                for (int i = 1; i <= 6; i++){
                    if (loadPartyDictionary.ContainsKey(i)){
                        if (!moreThanOne){
                            moreThanOne = true;
                            loadParty = new PlayerParty((testHero) loadPartyDictionary[i]);
                        } else {
                            loadParty.AddActor(loadPartyDictionary[i]);
                        }
                    }
                }

                party = loadParty;
            }
        }
    }

    private testHero checkValidHero(string[] hero){
        Debug.Log(hero.Length);
        if (hero.Length != 7){
            return null;
        } else {
            double[] loadStats = new double[5];
            for (int i = 2; i <= 6; i++){
                if (!Double.TryParse(hero[i], out loadStats[i-2])){
                    return null;
                }
            }
            return new testHero(hero[1], loadStats[0], loadStats[1], loadStats[2], loadStats[3], (int) loadStats[4]);
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
}
