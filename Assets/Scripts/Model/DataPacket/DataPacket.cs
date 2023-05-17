public class DataPacket
{
    private object myData;
    private string myLabel;
    private string myDestination;

    public DataPacket(object theData, string theLabel)
    {
        GenerateData(theData, theLabel, null);
    }

    public DataPacket(object theData, string theLabel, string theDestination)
    {
        GenerateData(theData, theLabel, theDestination);
    }

    private void GenerateData(object theData, string theLabel, string theDestination)
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
