// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelManager.cs" company="">
//   
// </copyright>
// <summary>
//   The level manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The level manager.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// The instance.
    /// </summary>
    private static LevelManager instance;

    /// <summary>
    /// The city value.
    /// </summary>
    private const int cityValue = 40;

    /// <summary>
    /// The rocket points.
    /// </summary>
    private const int rocketPoints = 4;

    /// <summary>
    /// The cities.
    /// </summary>
    private GameObject[] cities;


    // Level
    /// <summary>
    /// The current level.
    /// </summary>
    private int currentLevel = 1;

    /// <summary>
    /// The next level time.
    /// </summary>
    private float nextLevelTime = 2;

    /// <summary>
    /// The next level.
    /// </summary>
    private bool nextLevel = false;

    // Countdown
    /// <summary>
    /// The cd source.
    /// </summary>
    public AudioClip CDSource;

    /// <summary>
    /// The count down delay.
    /// </summary>
    public int countDownDelay = 5;

    /// <summary>
    /// The count down.
    /// </summary>
    private float countDown = 0;

    /// <summary>
    /// The dummy count.
    /// </summary>
    public int dummyCount;

    // Ammunition
    /// <summary>
    /// The rockets.
    /// </summary>
    public int rockets = 10;

    /// <summary>
    /// The stinger.
    /// </summary>
    public int stinger = 8;

    /// <summary>
    /// The rockets value.
    /// </summary>
    private float rocketsValue = 10;

    /// <summary>
    /// The stinger value.
    /// </summary>
    public float stingerValue = 8;

    /// <summary>
    /// The rocket multiplier.
    /// </summary>
    private float rocketMultiplier = 1.1f;

    /// <summary>
    /// The stinger multiplier.
    /// </summary>
    private float stingerMultiplier = 1.1f;

    /// <summary>
    /// The destroyed stingers.
    /// </summary>
    public int destroyedStingers;

    /// <summary>
    /// The stinger launched.
    /// </summary>
    public int stingerLaunched;
    private List<GameObject> _citiesList;


    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static LevelManager Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether is next level.
    /// </summary>
    public bool IsNextLevel
    {
        get { return nextLevel; }
        set { nextLevel = value; }
    }

    /// <summary>
    /// Gets or sets the destroyed stingers.
    /// </summary>
    public int DestroyedStingers
    {
        get { return destroyedStingers; }
        set { destroyedStingers = value; }
    }

    /// <summary>
    /// Gets or sets the stinger launched.
    /// </summary>
    public int StingerLaunched
    {
        get { return stingerLaunched; }
        set { stingerLaunched = value; }
    }

    /// <summary>
    /// Gets the stinger value.
    /// </summary>
    public int StingerValue
    {
        get { return Mathf.RoundToInt(stingerValue); }
    }

    /// <summary>
    /// Gets or sets the stingers.
    /// </summary>
    public int Stingers
    {
        get { return stinger; }
        set { stinger = value; }
    }

    /// <summary>
    /// Gets the cities.
    /// </summary>
    public GameObject[] Cities
    {
        get { return cities; }
    }

    public List<GameObject> CitiesList
    {
        get { return _citiesList; } 
        
    }

    /// <summary>
    /// Gets the count down.
    /// </summary>
    public float CountDown
    {
        get { return countDown; }
    }

    /// <summary>
    /// Gets the current level.
    /// </summary>
    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    /// <summary>
    /// The start.
    /// </summary>
    private void Start()
    {
        if (GameManager.Difficulty == 2)
        {
            stingerMultiplier = 1.2f;
        }
        else if (GameManager.Difficulty == 3)
        {
            stingerMultiplier = 1.3f;
        }

        if (instance == null)
        {
            instance = this;
        }

        cities = GameObject.FindGameObjectsWithTag("City");


        GetComponent<RocketLauncherControl>().IsControlable = false;
        StartCoroutine("StartCountDown");
        GameManager.UnPauseGame();
        countDown = countDownDelay;
    }

    /// <summary>
    /// The update.
    /// </summary>
    private void Update()
    {
        
        if (stinger <= 0 && nextLevel == false)
        {
            if (nextLevelTime <= 0)
            {
                nextLevelTime = 2;
            }
            else
            {
                nextLevelTime -= Time.deltaTime;
                if (nextLevelTime <= 0)
                {
                    nextLevel = true;
                }
            }
        }
    }


    /// <summary>
    /// The start count down.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator StartCountDown()
    {
        for (countDown = countDownDelay; countDown > 0; countDown--)
        {
            yield return new WaitForSeconds(1);
        }

        GetComponent<RocketLauncherControl>().IsControlable = true;
        GetComponent<HeadUpDisplay>().guiCountDown.enabled = false;
        StopCoroutine("StartCountDown");
    }

    /// <summary>
    /// The decrease rockets.
    /// </summary>
    public void DecreaseRockets()
    {
        if (rockets > 0)
        {
            rockets -= 1;
        }
    }

    /// <summary>
    /// The decrease stinger.
    /// </summary>
    public void DecreaseStinger()
    {
        if (stinger > 0)
        {
            stinger -= 1;
        }
    }

    /// <summary>
    /// The city is destroyed.
    /// </summary>
    public void CityIsDestroyed()
    {
        cities = GameObject.FindGameObjectsWithTag("City");
    }

    /// <summary>
    /// The total city points to string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string TotalCityPointsToString()
    {
        return (Cities.Length*cityValue).ToString();
    }

    /// <summary>
    /// The total rocket points to string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string TotalRocketPointsToString()
    {
        return (rockets*rocketPoints).ToString();
    }

    /// <summary>
    /// The total stinger launched.
    /// </summary>
    public void TotalStingerLaunched()
    {
		if (StingerLaunched <= StingerValue)
        {
            StingerLaunched += 1;
        }
    }

    /// <summary>
    /// The destroyed stingers to string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string DestroyedStingersToString()
    {
        return destroyedStingers.ToString();
    }

    /// <summary>
    /// The add city points.
    /// </summary>
    public void AddCityPoints()
    {
        GameManager.IncreasePoints(Cities.Length*cityValue);
    }

    /// <summary>
    /// The add rocket points.
    /// </summary>
    public void AddRocketPoints()
    {
        GameManager.IncreasePoints(rockets*rocketPoints);
    }

    /// <summary>
    /// The get city count.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public int GetCityCount()
    {
        return Cities.Length;
    }

    /// <summary>
    /// The next level.
    /// </summary>
    public void NextLevel()
    {
        float tmpValue;
        currentLevel++;

        rocketsValue *= rocketMultiplier;
        rockets = Mathf.RoundToInt(rocketsValue);
        
        stingerValue *= stingerMultiplier;
        Stingers = StingerValue;
    }
}