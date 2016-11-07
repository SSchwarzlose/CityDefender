using UnityEngine;
using System.Collections;


public static class GameManager
{
	private static bool playMusic = true;
	//Scores
	private static int overallScore = 0;
	
	private static int difficulty = 1;
	private static string difficultyToString = "leicht";
	
	//Pause Game
	private static bool isGamePaused = false;
	
	public static bool IsGamePaused
	{
		get { return isGamePaused; }
	}
	
	public static int Difficulty
	{
		get { return difficulty; }
		set { difficulty = value; }
	}
	
	public static string DifficultyToString
	{
		get {return difficultyToString;}
		set {difficultyToString = value;}
	}
	
	public static bool PlayMusic
	{
		set { playMusic = value; }
		get { return playMusic; }
	}
	
	
	public static int OverallScore
	{
		get { return overallScore; }
		set { overallScore = value; }
	}
	
	public static void IncreasePoints(int points)
	{
		overallScore += points;
	}
	
	public static void PauseGame()
    {	
       Time.timeScale = 0;
       AudioListener.pause = true;
       isGamePaused = true;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
		isGamePaused = false;
	}
	
	
	
}
