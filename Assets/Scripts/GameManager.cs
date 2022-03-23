using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*
 * GameManager
 * Script que gerencia o jogo, perpetuando valores e funções entre as fases/waves
 * Presente no prefab _Game_Manager para que seja chamado sempre que uma Scene precisar
 */

public class GameManager : MonoBehaviour {

    //=================//
    // ** UI CANVAS | VARs ** //
    //===============//
    public GameObject PreGameMessage; // Canvas contendo o texto da arena
    public GameObject GameOverArea; // Area fim de jogo
    public Text GameOverMessage; // Texto fim de jogo
    public Text TimeLeftMessage; // Contador de tempo

    public float levelStartDelay = 2f; // Transição entre cenas
    private Text WaveText; // Texto na HUD da arena
    private bool doingSetup = false; // Indica que o jogo está em uma tela de transição
    List<int> usedValues = new List<int>();
    int Point = 0;
    public Slider sliderFogosGame;
    //====================//
    // ** VAR GLOBAIS ** //
    //===================//
    public int pointsGlobal = 0;
    static int qtdFogosVivos = 1;
    static bool fimGame = false;

    public float timeLeftGame = 120;
    private float minutes;
    private float seconds;

    public GameObject NewFire;
    public GameObject FirePoint_1;
    public GameObject FirePoint_2;
    public GameObject FirePoint_3;
    public GameObject FirePoint_4;
    public GameObject FirePoint_5;
    public GameObject FirePoint_6;
    public GameObject FirePoint_7;
    public GameObject FirePoint_8;

    private AudioSource audio;
    public AudioClip SomGameOver;
    public AudioClip SomGameWin;

	void Start () {
        // Inicia variáveis estáticas
        qtdFogosVivos = 1;
        fimGame = false;

        // Audios
        audio = GetComponent<AudioSource>();

        // Sorteio aleatoriamente 3 focos de incêndio na sala e instancia fogo lá
        for (int i = 0; i < 5; i++)
        {
            Point = UniqueRandomInt(1, 8);
            // Diz que esse é um fogo original para morrer em segundos

            NewFire.GetComponent<FogoScript>().IsOriginal = true;
            if (Point==1)
            {
                Instantiate(NewFire, FirePoint_1.transform.position, FirePoint_1.transform.rotation);
                Destroy(FirePoint_1);
            }
            if (Point == 2)
            {
                Instantiate(NewFire, FirePoint_2.transform.position, FirePoint_2.transform.rotation);
                Destroy(FirePoint_2);
            }
            if (Point == 3)
            {
                Instantiate(NewFire, FirePoint_3.transform.position, FirePoint_3.transform.rotation);
                Destroy(FirePoint_3);
            }
            if (Point == 4)
            {
                Instantiate(NewFire, FirePoint_4.transform.position, FirePoint_4.transform.rotation);
                Destroy(FirePoint_4);
            }
            if (Point == 5)
            {
                Instantiate(NewFire, FirePoint_5.transform.position, FirePoint_5.transform.rotation);
                Destroy(FirePoint_5);
            }
            if (Point == 6)
            {
                Instantiate(NewFire, FirePoint_6.transform.position, FirePoint_6.transform.rotation);
                Destroy(FirePoint_6);
            }
            if (Point == 7)
            {
                Instantiate(NewFire, FirePoint_7.transform.position, FirePoint_7.transform.rotation);
                Destroy(FirePoint_7);
            }
            if (Point == 8)
            {
                Instantiate(NewFire, FirePoint_8.transform.position, FirePoint_8.transform.rotation);
                Destroy(FirePoint_8);
            }
        }

        PreGameMessage.SetActive(true);
        InitGame();
	}

    void Update()
    {
        // SLIDER
        sliderFogosGame.value = qtdFogosVivos;

        // CONTADOR DE TEMPO
        timeLeftGame -= Time.deltaTime;
        minutes = Mathf.Floor(timeLeftGame / 60);
        seconds = timeLeftGame % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            minutes = 0;
            seconds = 0;
        }
        TimeLeftMessage.text = minutes + ":" + Mathf.Round(seconds);

        if (timeLeftGame < 0)
        {
            if (qtdFogosVivos >= 15)
            {
                if (!fimGame) { GameOver(); fimGame = true; }
            }
            else
            {
                if (!fimGame) { GameWin(); fimGame = true; }
            }
        }

    }
    void InitGame()
    {
        doingSetup = true; // Ativar CANVAS quando for utilizar
        Invoke("HideArenaMessage", levelStartDelay); // Esconde a ImageUI em alguns segundos
    }
    private void HideArenaMessage()
    {
        doingSetup = false;
        PreGameMessage.SetActive(false);
    }

    public void GameOver()
    {
        // Toca Som de vitória
        audio.Stop();
        audio.PlayOneShot(SomGameOver, 1);
        // Pausa o jogo e a música principal
        Time.timeScale = 0;

        GameOverArea.SetActive(true);
        GameOverMessage.text = "Game Over!";
        StartCoroutine("WaitSeconds"); 
    }
    public void GameWin()
    {
        // Toca Som de vitória
        audio.Stop();
        audio.PlayOneShot(SomGameWin, 1);
        // Pausa o jogo e a música principal
        Time.timeScale = 0;

        GameOverArea.SetActive(true);
        GameOverMessage.text = "You Win!";
        StartCoroutine("WaitSeconds"); 
    }
    

    public void AddFogoGame() { qtdFogosVivos += 1; }
    public void RemoveFogoGame() { qtdFogosVivos -= 1; }
    public void AddPointsPlayer(int points) { pointsGlobal += points; }
    public int QtdPointsPlayer() { return pointsGlobal; }
    public bool CheckEndGame() { return fimGame; }
    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        return val;
    }

    IEnumerator WaitSeconds() { yield return new WaitForSeconds(5); Application.LoadLevel("Menu"); }
}
