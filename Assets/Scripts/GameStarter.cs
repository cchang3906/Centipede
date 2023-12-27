using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameStarter : MonoBehaviour
{

    [SerializeField] private GameObject blackScreen;
    [SerializeField] private AudioSource lightsOutSound;
    [SerializeField] private AudioSource lobbyMusic;
    [SerializeField] private AudioSource lightsInSound;
    [SerializeField] private AudioSource lightsOnSound;

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BlackScreen()
    {
        lightsOutSound.Play();
        StartCoroutine(WaitForAudio());
    }

    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(0.2f);
        lobbyMusic.Stop();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(4f);
        lightsInSound.Play();
        yield return new WaitForSeconds(0.5f);
        lightsOnSound.Play();
        NewGame();
    }

    private void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.spaceKey.wasPressedThisFrame)
        {
            BlackScreen();
        }
    }
}
