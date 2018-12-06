using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;

    private CanvasGroup canvas;
    public float fadeToGameTimer;
    public float fadeToMenuTimerTotal;

    private bool isPaused;
    private Vector3 startPos;
    private Vector3 startRot;
    private bool onMenu;
    private bool onGame;

    public AudioMixerSnapshot beachSnapshot;

    private bool endON;



    void Start () {

        Initialize();

        onGame = false;
        onMenu = true;

    }

    void Update () {


        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && onMenu && !onGame)
        {
            StartCoroutine(FadeToGame());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !onMenu && onGame)
        {
            StartCoroutine(FadeToMenu());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && onMenu)
        {
            Application.Quit();
        }
    }

    IEnumerator FadeToGame()
    {
        Initialize();

        onMenu = false;
        float fadeToGameTimerTotal = fadeToGameTimer;
        while (fadeToGameTimer > 0f)
        {
            fadeToGameTimer -= Time.deltaTime;

            float r = fadeToGameTimer / fadeToGameTimerTotal;
            canvas.alpha = r;

            yield return null;
        }

        fadeToGameTimer = fadeToGameTimerTotal;
        beachSnapshot.TransitionTo(7f);
        onGame = true;
    }

    IEnumerator FadeToMenu()
    {
        onGame = false;
        endON = true;

        float fadeToMenuTimer = 0;
        while (fadeToMenuTimer < 1f)
        {
            fadeToMenuTimer += Time.deltaTime;

            float r = fadeToMenuTimer;
            canvas.alpha = r;
            yield return null;
        }


        onMenu = true;
    }

    public void End()
    {
        StartCoroutine(FadeToMenu());

    }
    private void Initialize()
    {
        
        player = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        canvas = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        canvas.alpha = 1;
        startPos = new Vector3(-35.2f, 3.27f, 12.3f);
        startRot = new Vector3(0, 66.755f, 0);
        player.transform.position = startPos;
        player.transform.rotation = Quaternion.LookRotation(startRot) ;
        isPaused = false;
        FindObjectOfType<AttractPlayer>().attracted = false;
        endON = true;

    }

    void Pause()
    {

        isPaused = !isPaused;

    }

    public void EndRed()
    {
        StartCoroutine(FadeToMenu());
        StartCoroutine(EndRedEnumerator());
    }

    public void EndBlue()
    {

        if (endON)
        {
            StartCoroutine(FadeToMenu());
            endON = false;
        }
        StartCoroutine(EndBlueEnumerator());

    }

    IEnumerator EndRedEnumerator()
    {
        Debug.Log("ENDRED");

        yield return null;
    }

    IEnumerator EndBlueEnumerator()
    {
        Debug.Log("ENDBLUE");

        yield return null;
    }
}
