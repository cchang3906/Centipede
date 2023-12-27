using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;

    private static bool created = false;
    [SerializeField]
    private int maxLives = 3;
    private int level = 2;
    [SerializeField] GameObject centipedeSpawn;
    public List<GameObject> hearts;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] GameObject onLights;

 
    public int lifeCount;

    private void Awake()
    {
        //if (!created)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    created = true;
        //}
    }

    private void Start()
    {
        Init();

        levelText.text = "Level " + level;
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i + 1 > lifeCount)
            {
                hearts[i].SetActive(false);
            }
            else
            {
                hearts[i].SetActive(true);
            }
        }
    }

    public void Init()
    {
        lifeCount = maxLives;
    }
    public void saveGame()
    {
        PlayerPrefs.SetInt("LifeCount", lifeCount);
        PlayerPrefs.Save();
    }

    public void loadGame()
    {
        if (PlayerPrefs.HasKey("LifeCount"))
        {
            lifeCount = PlayerPrefs.GetInt("LifeCount");
        }
    }
    public void loseLife()
    {
        lifeCount--;
        FindObjectOfType<SoundController>().PlayPlayerHit(true);
        StartCoroutine(FindObjectOfType<movement>().FlickerSprite());

        UpdateHealthUI();
        Reload();
        saveGame();
    }

    public void gainLife()
    {
        lifeCount++;

        FindObjectOfType<SoundController>().PlayGainLife();
        UpdateHealthUI();
        saveGame();
    }

    public void Reload()
    {
        FindObjectOfType<CentipedeSpawner>().ResetCentipedes();
        FindObjectOfType<Spider>().Wait();
        FindObjectOfType<MushroomManager>().CountMushrooms();
        StartNewLevel();
    }


    public void LevelCleared()
    {
        FindObjectOfType<SoundController>().PlayShriek(true);
        level++;
        levelText.text = "Level " + level;
        if (IsFactor(level, 3))
        {
            StartCoroutine(LightsOffCouroutine());
        }
        else
        {
            TurnLightsOn();
            FindObjectOfType<SoundController>().PLayForestMusic(true);
            FindObjectOfType<SoundController>().PlayForestAmbience(false);
            StartNewLevel();
        }
        FindObjectOfType<FleaSpawn>().appearanceCounter = (int) (level / 2);
    }

    public bool IsFactor(int number, int factor)
    {
        if (factor == 0)
        {
            return false;
        }

        if (number % factor == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }


    public void StartNewLevel()
    {
        FindObjectOfType<SoundController>().PlayDemogorgonMove(false);
        FindObjectOfType<Centipede>().ResetMovementSound();
        FindObjectOfType<CentipedeSpawner>().SpawnCentipede(Random.Range(6, 12), level);
        FindObjectOfType<Spider>().SetLevel(level);
        StartCoroutine(SpiderRespawnTime());
    }

    public void GameOver()
    {
        FindObjectOfType<movement>().gameObject.SetActive(false);
        FindObjectOfType<Centipede>().ResetCentipede();
        FindObjectOfType<Spider>().gameObject.SetActive(false);
        FindObjectOfType<SoundController>().PLayForestMusic(false);
        FindObjectOfType<SoundController>().PlayDemogorgonMove(false);
        FindObjectOfType<SoundController>().PlayDemobat(false);
        FindObjectOfType<SoundController>().PlayAh();
        gameOverScreen.SetActive(true);
        StartCoroutine(FindObjectOfType<FadeIn>().Fade());
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        FindObjectOfType<SoundController>().PLayForestMusic(false);
        FindObjectOfType<SoundController>().PlayLobbyMusic(true);
        FindObjectOfType<SoundController>().PlayDemogorgonMove(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        FindObjectOfType<SoundController>().PLayForestMusic(true);
        FindObjectOfType<SoundController>().PlayLobbyMusic(false);
        FindObjectOfType<SoundController>().PlayDemogorgonMove(true);
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    private void CutLights()
    {
        onLights.SetActive(false);
        //FindObjectOfType<movement>().LightsOn(true);
        //foreach(CentipedeBodyPart segment in FindObjectsOfType<CentipedeBodyPart>())
        //{
        //segment.Appear(false);
        //}
        FindObjectOfType<Centipede>().Appear(false);
    }

    private void TurnLightsOn()
    {
        onLights.SetActive(true);
        FindObjectOfType<movement>().LightsOn(false);
        FindObjectOfType<Centipede>().Appear(true);
    }

    private IEnumerator LightsOffCouroutine()
    {
        CutLights();
        yield return new WaitForSeconds(0.1f);
        TurnLightsOn();
        yield return new WaitForSeconds(0.2f);
        CutLights();
        yield return new WaitForSeconds(0.1f);
        TurnLightsOn();
        FindObjectOfType<SoundController>().PLayForestMusic(false);
        yield return new WaitForSeconds(0.8f);
        CutLights();
        FindObjectOfType<SoundController>().PlayGeneratorOff();
        FindObjectOfType<SoundController>().PlayForestAmbience(true);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<movement>().LightsOn(true);
        FindObjectOfType<SoundController>().PlayFlashlight();
        StartNewLevel();
    }

    public void Quit() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private IEnumerator SpiderRespawnTime()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<Spider>().Respawn();
    }
}

