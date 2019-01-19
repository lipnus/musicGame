using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatObject : MonoBehaviour {

	public int score=100;
	public int difficulty = 1;
	public QuizType quizType;
	
	public enum QuizType{
		Choice, Initial
	}


	
}
