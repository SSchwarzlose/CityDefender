using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class mainMenu : MonoBehaviour 
{
    const string Filename = "HighScore.txt"; 
    
    
    string line1, line2;

    private bool windowDiff = false;
    private bool windowMan = false;
    private bool windowCred = false;
    private bool windowOpt = false;
    private bool windowQuit = false;

    private bool windowOpen = false;

    private bool FullScreen = true;
    
    List<Score> MainList = new List<Score>();
    
    public Texture2D mainTexture;
    public Texture2D curserMainMenu;
    
    public GUISkin MMskin;
    public GameObject music;
    private string MusicText = "Musik: An";

   

    private string credits = 
        "Missile Command 3D Remake \n"+
            " \n" +
		"Director: Sascha Schwarzlose\n" +
			"\n" +
        "Programming: Sascha Schwarzlose\n"+
            " \n"+
        "Models and Textures: David Sikorsky\n" +
        "\n" +
        "Mainmenu Background: Michale Yonar " +
		"\n" +
		"Explosions: \n Detonator Explosion Framework by Ben Throop\n" +
		"\n" +
		"Sounds: \n Bottle Rocket Sound by Mike Koenig 'http://soundbible.com/709-Bottle-Rocket.html' \n Attribution 3.0 License\n" + 
		"Background Musik: Army Strong Theme Song Sound by 'TheArmy' \n 'http://soundbible.com/1884-Army-Strong-Theme-Song.html'"
		;
        
    
    private string anleitung = 
        "Anleitung\n\n"+
        " \n" +
        "Du musst deine Staedte vor den feindlichen Raketen schuetzen.\n "+
            " \n" +
        "Mit der Maus steuerst Du die Raketentuerme.\n"+
            " \n" +
        "Mit der linken Maustaste feuert der Raketenturm eine Rakete,\n "+
            " \n" +
        "der dem markierten Punkt am naechsten ist\n"+
            " \n" +
        "Wahlweise kannst Du deine Tuerme mit den \n"+
            " \n" +
        "Tasten 1, 2, 3 manuell abfeuern\n"+
            " \n"+
        "Sind alle Staedte zerstoert, endet das Spiel";


    public enum Page
    { None, Main, Options, Credits, Anleitung, Schwierigkeitsgrad, Quit, diff }

    private Page currentPage;

    private int toolbarInt;
    private readonly string[] toolbarStrings = { "Audio", "Graphik", "Aufloesung" };
    
    void Awake()
    {
        
    }
    
    void Start()
    {
        Cursor.visible = false;
        
        
        if(GameObject.Find("music(Clone)") == null)
        {
            Instantiate(music);
        }
        
        music = GameObject.Find("music(Clone)");
        
        if (!File.Exists(Filename))
        {
            StreamWriter write = new StreamWriter(Filename);
            write.Close();
        }
        
        using (StreamReader reader = new StreamReader(Filename))
        {
            while (((line1 = reader.ReadLine()) != null) && ((line2 = reader.ReadLine ()) != null))
            {
                if (line1 != "" && line2 != "")
                {
                    Score _temp = new Score(int.Parse(line2), line1);
                    MainList.Add(_temp);
                }
            }
        }
        currentPage = Page.Main;
        
    }
    
    void Update()
    {
        if (GameManager.PlayMusic)
        {
            MusicText = "Musik: An";
        }
        else
        {
            MusicText = "Musik: Aus";
        }
    }
    
    void OnGUI()
    {
        GUI.depth = 1;
        GUI.skin = MMskin;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),mainTexture, ScaleMode.StretchToFill);

        Score[] scores = MainList.ToArray();
        
        GUILayout.BeginArea(new Rect(Screen.width/20*6, Screen.height/60*4, 200, 200));
        GUILayout.BeginVertical();
        for(int i = 0; i < Mathf.Min(scores.Length, 5); i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label((i + 1) + "." + " " + scores[i].name + ": " + scores[i].score.ToString());
                    
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        
        GUILayout.BeginArea(new Rect(Screen.width/20*13, Screen.height/60*4, 200, 200));
        GUILayout.BeginVertical();
        for(int i = 5; i < Mathf.Min(scores.Length, 10); i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label((i + 1) + "." + " " + scores[i].name + ": " + scores[i].score.ToString());
                    
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        
            switch (currentPage)
            {
                case Page.Main:
                    {
                        MainMenu();
                        windowDiff = false;
                        windowMan = false;
                        windowOpt = false;
                        windowCred = false;
                        windowQuit = false;
                    } break;
                case Page.diff: windowDiff = true; break;
                case Page.Options: windowOpt = true; break;
                case Page.Credits: windowCred = true; break;
                case Page.Anleitung: windowMan = true; break;
                case Page.Quit: windowQuit = true; break;
            }

        if (windowDiff)
            GUI.Window(0, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), WindowDiff, "Schwierigkeitsgrad");

        if (windowMan)
            GUI.Window(1, new Rect(Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300), WindowManual, anleitung);

        if (windowCred)
        {
            GUI.Window(2, new Rect(Screen.width / 2 - 200, Screen.height / 2 - 250, 400, 500), WindowCredits, credits);
        }
        if (windowOpt)
        {
            GUI.Window(3, new Rect(Screen.width / 2 - 150, Screen.height / 2 -150, 300, 300), WindowOptions, "Optionen");
        }
        if (windowQuit)
        {
			GUI.Window(4, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 200), WindowQuit, "Wirklich Beenden?");
        }
    }

    void ShowBackButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 37, Screen.height / 10 * 9, 75, 20), "Zurueck"))
        {
            currentPage = Page.Main;
        }
    }

    void BeginPage(int width, int height)
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - (width / 2), Screen.height / 2, width, height));
    }

    void EndPage()
    {
        GUI.EndGroup();
        if (currentPage != Page.Main && currentPage != Page.Quit)
        {
            ShowBackButton();
        }
    }
    
    void ResolutionControl()
    {
        FullScreen = GUI.Toggle(new Rect(100, 50, 65, 20), FullScreen, "Vollbild:");
        
        Screen.fullScreen = FullScreen;
            
        if (GUI.Button(new Rect(80, 80, 100, 20), "640 X 480"))
        {
            Screen.SetResolution(640, 480, true);
        }
        if (GUI.Button(new Rect(80, 110, 100, 20), "800 x 600"))
        {
            Screen.SetResolution(800, 600, true);
        }
        if (GUI.Button(new Rect(80, 140, 100, 20), "1280 x 720"))
        {
            Screen.SetResolution(1280, 720, true);
        }
        if (GUI.Button(new Rect(80, 170, 100, 20), "1920 x 1080"))
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }

    void VolumeControl()
    {
        GameManager.PlayMusic = GUI.Toggle(new Rect(8, 50, 60, 20), GameManager.PlayMusic, MusicText);
        GUI.Label(new Rect(8, 80, 90, 20),"Lautstaerke" );
        AudioListener.volume = GUI.HorizontalSlider(new Rect(8, 100, 200, 10), AudioListener.volume, 0, 1);
    }

    void Qualities()
    {
        switch (QualitySettings.currentLevel)
        {
            case QualityLevel.Fastest:
                GUI.Label(new Rect(8, 40, 250, 20), "Schnellste");
                break;
            case QualityLevel.Fast:
                GUI.Label(new Rect(8, 40, 250, 20), "Schnell");
                break;
            case QualityLevel.Simple:
                GUI.Label(new Rect(8, 40, 250, 20), "Einfach");
                break;
            case QualityLevel.Good:
                GUI.Label(new Rect(8, 40, 250, 20), "Gut");
                break;
            case QualityLevel.Beautiful:
                GUI.Label(new Rect(8, 40, 250, 20), "Schoen");
                break;
            case QualityLevel.Fantastic:
                GUI.Label(new Rect(8, 40, 250, 20), "Fantastisch");
                break;
        }
    }

    void QualityControl()
    {
        if (GUI.Button(new Rect(0, 100, 100, 20), "Niedrieger"))
        {
            QualitySettings.DecreaseLevel();
        }
        if (GUI.Button(new Rect(160, 100, 100, 20), "Hoeher"))
        {
            QualitySettings.IncreaseLevel();
        }
    }
    
    void WindowDiff(int windowID)
    {
        GUI.BringWindowToBack(0);

        if (GUI.Button(new Rect(10, 40, 80, 30), "Leicht"))
        {
            GameManager.Difficulty = 1;
            GameManager.DifficultyToString = "Leicht";
            GameManager.OverallScore = 0;
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(110, 40, 80, 30), "Normal"))
        {
            GameManager.Difficulty = 2;
            GameManager.DifficultyToString = "Normal";
            GameManager.OverallScore = 0;
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(210, 40, 80, 30), "Brutal"))
        {
            GameManager.Difficulty = 3;
            GameManager.DifficultyToString = "Brutal";
            GameManager.OverallScore = 0;
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(90, 70, 125, 20), "Abbrechen"))
            currentPage = Page.Main;
    }

    void WindowManual(int windowID)
    {
        GUI.BringWindowToBack(1);
        if (GUI.Button(new Rect(378, 2, 20, 20), "X"))
            currentPage = Page.Main;
    }

    void WindowCredits(int windowID)
    {
        GUI.BringWindowToBack(2);
        if (GUI.Button(new Rect(100, 400, 100, 20), "Schliessen"))
            currentPage = Page.Main;
    }

    void WindowOptions(int windowID)
    {
        GUI.BeginGroup(new Rect(8, 30, 400, 200));
        toolbarInt = GUI.Toolbar(new Rect(8,10, 250, 25), toolbarInt, toolbarStrings);

        switch (toolbarInt)
        {
            case 0: VolumeControl(); break;
            case 1: Qualities(); QualityControl(); break;
            case 2: ResolutionControl(); break;
        }
       
        GUI.EndGroup();

        if (GUI.Button(new Rect(100, 250, 100, 20), "Schliessen"))
            currentPage = Page.Main;
    }

    void WindowQuit(int windowID)
    {
        GUI.BeginGroup(new Rect(10, 50, 300, 100));
        if (GUI.Button(new Rect(10, 65, 100, 30), "Ja"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(150, 65, 100, 30), "Nein"))
            currentPage = Page.Main;
		GUI.EndGroup ();
    }
    void MainMenu()
    {
        BeginPage(210, 250);
        
        if (GUI.Button(new Rect(0, 0, 150, 25), "Start"))
        {
            if (!windowOpen)
                currentPage = Page.diff;
        } 
      
        if (GUI.Button(new Rect(0, 26, 150, 25), "Optionen"))
            currentPage = Page.Options; 
       
        if (GUI.Button(new Rect(0, 52, 150, 25), "Anleitung"))
            currentPage = Page.Anleitung;
           
        if (GUI.Button(new Rect(0, 78, 150, 25), "Credits"))
            currentPage = Page.Credits;
        
        if (GUI.Button(new Rect(0, 104, 150, 25), "Beenden"))
            currentPage = Page.Quit;

        EndPage();
    } 
}

