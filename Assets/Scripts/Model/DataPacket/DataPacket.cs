public class DataPacket
{
    private object myData;
    private string myLabel;
    private string myDestination;

    public DataPacket(in object theData, in string theLabel)
    {
        GenerateData(theData, theLabel, null);
    }

    public DataPacket(in object theData, in string theLabel, in string theDestination)
    {
        GenerateData(theData, theLabel, theDestination);
    }

    private void GenerateData(in object theData, in string theLabel, in string theDestination)
    {
        myData = theData;
        myLabel = theLabel;
        myDestination = theDestination;
    }

    public object GetData()
    {
        return myData;
    }

    public string GetLabel()
    {
        return myLabel;
    }

    public string GetDestination()
    {
        return myDestination;
    }
}
