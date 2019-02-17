using UnityEngine;
using UnityEngine.UI;

public class LogCallback : MonoBehaviour {
	public LoginManager LoginManager;
	
	void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
//		Debug.Log("OnEnable()");
	}
	void OnDisable()
	{
		Application.logMessageReceived -= HandleLog;
//		Debug.Log("OnDisable()");
		
	}
	void HandleLog(string logString, string stackTrace, LogType type)
	{
//		output.text = logString;
//		stack.text = stackTrace;
	}
}
