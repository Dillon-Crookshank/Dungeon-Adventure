using UnityEngine;

/// <summary>
/// A class attached to a combat log object.
/// </summary>
public class combatLog : MonoBehaviour
{
    /// <summary>
    /// The array of text meshes to display the logs stored. Defined in the editor.
    /// </summary>
    [SerializeField]
    private TextMesh[] myLogs;

    /// <summary>
    /// Adds a new log to the combat log "stack".
    /// </summary>
    /// <param name="theNewLog"> The new log to display. </param>
    void UpdateCombatLog(string theNewLog)
    {
        for (int i = 0; i < myLogs.Length - 1; i++)
        {
            myLogs[i].text = myLogs[i + 1].text;
        }
        myLogs[myLogs.Length - 1].text = theNewLog;
    }

    /// <summary>
    /// Clears the combat log.
    /// </summary>
    void ClearCombatLog()
    {
        for (int i = 0; i < myLogs.Length; i++)
        {
            myLogs[i].text = "";
        }
    }
}
