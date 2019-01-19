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


	
}
