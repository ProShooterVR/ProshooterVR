using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour
{
    

    public TextMeshPro logText;


    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logText, string stackTrace, LogType type)
    {
        // Display the log message in the Unity Text component
        if (type == LogType.Log || type == LogType.Error)
        {
            // Display the log message in the Unity Text component
            LogMessage(logText);
        }
    }

    void LogMessage(string message)
    {
        if (logText != null)
        {
            logText.text += message + "\n";
        }
        else
        {
            Debug.LogWarning("Log Text component is not assigned!");
        }
    }
}
