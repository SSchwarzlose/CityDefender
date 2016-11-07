using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Highscore : MonoBehaviour 
{
	//HighscoreList variables
	public const string Filename = "HighScore.txt";
	string line1, line2;
	public static string newname = "";
	private const int HighScoreLength = 10;
	public static List<Score> MainList = new List<Score>();
	
	void Start () 
	{
		using (StreamReader reader = new StreamReader(Filename))
		{
			while (((line1 = reader.ReadLine()) !=null) && ((line2 = reader.ReadLine()) != null))
			{
				if (line1 != "" && line2 != "")
				{
					Score _temp = new Score(int.Parse(line2), line1);
					MainList.Add(_temp);
					
				}
			}
		}
	}
}

public class Score
{
	public int score;
	public string name;
	
	public Score(int score, string name)
	{
		this.score = score;
		this.name = name;
	}
	
}
