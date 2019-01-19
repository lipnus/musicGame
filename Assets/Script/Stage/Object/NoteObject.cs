using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteObject : MonoBehaviour {

	public int score=1;
	public NoteType noteType;
	
	
	public enum NoteType {
		Note, Line
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("충돌");
	}

	
}
