using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HeadUpDisplay : MonoBehaviour
{
	private bool showCountDown = true;
	private bool showGameover = false;
	
	public float startTime = 0.1f;

    public GUISkin MenuSkin;

    private bool windowMan = false;
    private bool windowOpt = false;
    private bool windowGameOver = false;
	
	public GUIText guiScore;
	public GUIText guiCountDown;
	public GUIText guiRokets;
	public GUIText guiCurrentLevel;
	public GUIText guiStingerLeft;
	public GUIText guiDifficulty;
	
	public Texture curser;
	
	public GameObject music;
	
	public enum Page
	{
		None,
		Main,
		Options,
		Anleitung
	}

    private Page currentPage;
	
	
	public string[] credits = {
        "Missile Command",
        "Programming by Sascha",
        "Models and Textures by Michael and David ",
        };

    private string anleitung =
        "Anleitung\n\n" +
        " \n" +
        "Du musst deine Städte vor den feindlichen Raketen schützen.\n " +
            " \n" +
        "Mit der Maus steuerst Du die Raketentürme.\n" +
            " \n" +
        "Mit der linken Maustaste feuert der Raketenturm eine Rakete,\n " +
            " \n" +
        "der dem markierten Punkt am nächsten ist\n" +
            " \n" +
        "Wahlweise kannst Du deine Türme mit den \n" +
            " \n" +
        "Tasten 1, 2, 3 manuell abfeuern\n" +
            " \n" +
        "Sind alle Städte zerstört, endet das Spiel";
	
	private int toolbarInt;
	private string[] toolbarStrings = { "Audio", "Graphics" };
	
	private GUIContent Credits;
	
	void Awake()
	{
		music = GameObject.Find("music(Clone)");
	}
	
	void Start ()
	{
		guiScore.text = "Punkte: " + GameManager.OverallScore;
		guiCountDown.enabled = true;
		guiRokets.text = "Raketen: 10";
		guiCurrentLevel.text = "Level: 1";
		guiStingerLeft.text = "Verbleibende Feinde: 0" ;
		guiDifficulty.text = "Schwierigkeit: " + GameManager.DifficultyToString;
	}
	
	
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && (!GameManager.IsGamePaused || !LevelManager.Instance.IsNextLevel)) 
		{
			GetComponent<RocketLauncherControl> ().IsControlable = false;
			GameManager.PauseGame ();
			switch (currentPage) 
			{
			case Page.None:

				currentPage = Page.Main;
				break;

			case Page.Main:
				if (IsBeginning ())
					GameManager.UnPauseGame ();
				break;

			default:
				currentPage = Page.Main;
				break;
			}
		}
	}
	
	
	
	void OnGUI ()
	{
        GUI.depth = 1;
        GUI.skin = MenuSkin;

        if (windowMan)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 50, Screen.height / 2 - 200, 400, 325), WindowManual, anleitung);
        }

        if (LevelManager.Instance.IsNextLevel)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 200, 200), WindowNextLevel, "Levelende");
        }

        if (windowOpt)
        {
            GUI.Window(2, new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 300, 200), WindowOptions, "Optionen");
        }

        if (LevelManager.Instance.GetCityCount() == 0)
        {
            GUI.Window(3, new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 300, 400), WindowGameOver, "Du hast verloren");
        }
        
		if (GameManager.IsGamePaused == true && !LevelManager.Instance.IsNextLevel) 
        {
			switch (currentPage) 
            {
			    case Page.Main:
				    MainPauseMenu ();
                    windowOpt = false;
                    windowMan = false;
                    
				    break;
			    case Page.Options:
                    //ShowToolbar ();
                    windowOpt = true;
				    break;
                case Page.Anleitung:
                    windowMan = true;
                    break;
			
			}
		}  
		ShowCurrentPoints ();
		ShowRocketsLeft ();
		ShowStingerLeft ();
		ShowCurrentLevel ();
		ShowCountDown ();
        windowGameOver = true;
        
	}

    
	
	public void ShowCurrentPoints ()
	{
		guiScore.text = "Punkte: " + GameManager.OverallScore;
	}
	
	public void ShowRocketsLeft ()
	{
		guiRokets.text = "Raketen: " + LevelManager.Instance.rockets.ToString ();
	}
	
	public void ShowStingerLeft ()
	{
		guiStingerLeft.text = "Atomrakteten: " + LevelManager.Instance.Stingers;
	}
	

	
	public void ShowCountDown ()
	{
		if (showCountDown) {
			float countDown = LevelManager.Instance.CountDown; 
			
			if (countDown <= 0)
				showCountDown = false;
			
			guiCountDown.text = "" + countDown;
		}
	}
	
	void ShowCurrentLevel ()
	{
		guiCurrentLevel.text = "Level: " + LevelManager.Instance.CurrentLevel;
	}
	
	void Qualities ()
	{
        switch (QualitySettings.currentLevel)
        {
            case QualityLevel.Fastest:
                GUI.Label(new Rect(50, 40, 200, 20), "Schnellste");
                break;
            case QualityLevel.Fast:
                GUI.Label(new Rect(50, 40, 200, 20), "Schnell");
                break;
            case QualityLevel.Simple:
                GUI.Label(new Rect(50, 40, 200, 20), "Einfach");
                break;
            case QualityLevel.Good:
                GUI.Label(new Rect(50, 40, 200, 20), "Gut");
                break;
            case QualityLevel.Beautiful:
                GUI.Label(new Rect(50, 40, 200, 20), "Schön");
                break;
            case QualityLevel.Fantastic:
                GUI.Label(new Rect(50, 40, 200, 20), "Fantastisch");
                break;
        }
	}

    void QualityControl()
    {
        if (GUI.Button(new Rect(15, 60, 100, 20), "Niedrieger"))
        {
            QualitySettings.DecreaseLevel();
        }
        if (GUI.Button(new Rect(160, 60, 100, 20), "Höher"))
        {
            QualitySettings.IncreaseLevel();
        }
    }

    void VolumeControl()
    {
        GameManager.PlayMusic = GUI.Toggle(new Rect(50, 30, 60, 20), GameManager.PlayMusic, "Musik: ");
        GUI.Label(new Rect(50, 60, 90, 20), "Lautstä5rke");
        AudioListener.volume = GUI.HorizontalSlider(new Rect(50, 85, 200, 20), AudioListener.volume, 0, 1);
    }
	
	void BeginPage (int width, int height)
	{
        GUI.BeginGroup(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}
	
	void EndPage ()
	{
        
		if (currentPage != Page.Main) {
			ShowBackButton ();
		}
        GUI.EndGroup();
	}

	void MainPauseMenu ()
	{
        BeginPage(210, 250);
        if (GUI.Button(new Rect(0, 0, 150, 25), "Weiter Spielen"))
        {
			GameManager.UnPauseGame ();
			GetComponent<RocketLauncherControl> ().IsControlable = true;
		}

        if (GUI.Button(new Rect(0, 26, 150, 25), "Neustart"))
        {
			GameManager.OverallScore = 0;
			Application.LoadLevel (1);
			GameManager.UnPauseGame ();
		}

        if (GUI.Button(new Rect(0, 52, 150, 25), "Optionen"))
        {
			currentPage = Page.Options;
		}

        if (GUI.Button(new Rect(0, 78, 150, 25), "Anleitung"))
        {
            currentPage = Page.Anleitung;
		}

        if (GUI.Button(new Rect(0, 104, 150, 25), "Beenden"))
        {
			Application.LoadLevel (0);
		}
		
		EndPage ();
	}
	
	bool IsBeginning ()
	{
		return (Time.time < startTime);
	}

	void OnApplicationPause (bool pause)
	{
		if (GameManager.IsGamePaused == true) {
			AudioListener.pause = true;
		}
	}
	
	void ShowBackButton ()
	{
		if (GUI.Button (new Rect (50, 200, 50, 20), "Zurück")) 
        {
			currentPage = Page.Main;
		}
	}

    void WindowManual(int windowID)
    {
        
        if (GUI.Button(new Rect(150, 290, 100, 20), "Schließen"))
        {
            currentPage = Page.Main;
        }
    }

    void WindowNextLevel(int windowID)
    {
        GameManager.PauseGame();

        Destroy(GameObject.FindWithTag("stinger"));
        Destroy(GameObject.FindWithTag("explosion"));
        Destroy(GameObject.FindWithTag("vehicle"));
        Destroy(GameObject.FindWithTag("Sidewinder"));

        GUI.BeginGroup(new Rect(8, 10, 180, 180));
        GUI.Label(new Rect(0, 36, 180, 25), "Punkte: " + GameManager.OverallScore);
        GUI.Label(new Rect(0, 62, 180, 25), "Städte: " + LevelManager.Instance.Cities.Length + " X 40 = " +
            LevelManager.Instance.TotalCityPointsToString());
        GUI.Label(new Rect(0, 89, 180, 25), "Raketen: " + LevelManager.Instance.rockets + "X 4 = " + LevelManager.Instance.TotalRocketPointsToString());

        GUI.Label(new Rect(0, 116, 180, 25), "Zerstörte A-Raketen: " + LevelManager.Instance.DestroyedStingers);
        GUI.EndGroup();

        if (GUI.Button(new Rect(50, 170, 75, 20), "weiter"))
        {
            LevelManager.Instance.StingerLaunched = 0;
            LevelManager.Instance.NextLevel();
            LevelManager.Instance.AddCityPoints();
            LevelManager.Instance.AddRocketPoints();
            LevelManager.Instance.DestroyedStingers = 0;
            GameManager.UnPauseGame();
            LevelManager.Instance.IsNextLevel = false;
        }
    }

    void WindowOptions(int windowID)
    {
        GUI.BeginGroup(new Rect(5, 40, 300, 200));
        toolbarInt = GUI.Toolbar(new Rect(50, 0, 200, 30), toolbarInt, toolbarStrings);
        switch (toolbarInt)
        {
            case 0:
                VolumeControl();
                break;
            case 1:
                Qualities();
                QualityControl();
                break;
        }
       
        GUI.EndGroup();
        if (GUI.Button(new Rect(75, 150, 100, 20), "Schließen"))
        {
            currentPage = Page.Main;
        }
    }

    void WindowGameOver(int windowID)
    {
        GameManager.PauseGame();
        
            GUI.BeginGroup(new Rect(5, 40, 300, 200));
            GUI.Label(new Rect(0, 10, 200, 25), "Deine Punkte: " + GameManager.OverallScore);
            GUI.Label(new Rect(10, 40, 200, 25), "Dein Name: ");
            Highscore.newname = GUI.TextField(new Rect(10, 70, 200, 25), Highscore.newname);
            if (GUI.Button(new Rect(10, 100, 200, 25), "Speichern"))
            {
                if (Highscore.MainList.Count == 0)
                {
                    Score _temp = new Score(GameManager.OverallScore, Highscore.newname);
                    Highscore.MainList.Add(_temp);
                }
                else
                {
                    for (int i = 1; (i <= Highscore.MainList.Count) && (i <= 10); i++)
                    {
                        if (GameManager.OverallScore > Highscore.MainList[i - 1].score)
                        {
                            Score _temp = new Score(GameManager.OverallScore, Highscore.newname);
                            Highscore.MainList.Insert(i - 1, _temp);
                            break;
                        }
                        if ((i == Highscore.MainList.Count) && (i < 10))
                        {
                            Score _temp = new Score(GameManager.OverallScore, Highscore.newname);
                            Highscore.MainList.Add(_temp);
                            break;
                        }
                    }

                }
            }
            if (GUI.Button(new Rect(10, 130, 200, 30), "Hauptmenue"))
            {
                
                using (StreamWriter writer = new StreamWriter(Highscore.Filename, false))
                {
                    foreach (var score in Highscore.MainList)
                    {
                        writer.WriteLine(score.name);
                        writer.WriteLine(score.score);
                    }
                }
                Application.LoadLevel(0);
            }

            if (GUI.Button(new Rect(10, 160, 200, 30), "Nochmal"))
            {
                GameManager.OverallScore = 0;
                Application.LoadLevel(Application.loadedLevel);
                using (StreamWriter writer = new StreamWriter(Highscore.Filename, false))
                {
                    foreach (var score in Highscore.MainList)
                    {
                        writer.WriteLine(score.name);

                        writer.WriteLine(score.score);
                    }
                }
            }

            GUI.EndGroup();
        
    }
}