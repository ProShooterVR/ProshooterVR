using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogToTextField : MonoBehaviour
{
    public TextMeshPro debugTextField; // Reference to the Text field that will display the Debug.Log messages


    public static DebugLogToTextField Instance;

    
    private void Awake()
    {
       
        Application.logMessageReceived += HandleLogMessageReceived; // Subscribe to the Application.logMessageReceived event
       
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLogMessageReceived; // Unsubscribe from the Application.logMessageReceived event
    }

    private void HandleLogMessageReceived(string logString, string stackTrace, LogType type)
    {
       
        if (type == LogType.Log) // Only display messages with LogType.Log
        {
            debugTextField.text = "<br>" + logString + "<br>"; // Append the logString and a newline character to the Text field's text
        }
    }
}
