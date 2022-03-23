using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    public GameObject MenuUI; // Panel do menu, para ativar e desativar
    //public GameObject OptionsUI; // Panel das opções, para ativar e desativar
    public GameObject InfoUI; // Panel das opções, para ativar e desativar

	void Start () {
        MenuUI.SetActive(true); // Inicialmente o game tem que ter o menu disponível
	}
	
	// OPÇÕES DO MENU - VIA BOTÕES
    public void OptionsGame()
    {
        MenuUI.SetActive(false);
        InfoUI.SetActive(false);
        //OptionsUI.SetActive(true);
	}
    public void MainMenu()
    {
        //OptionsUI.SetActive(false);
        InfoUI.SetActive(false);
        MenuUI.SetActive(true);
    }
    public void InfoGame()
    {
        InfoUI.SetActive(true);
        //OptionsUI.SetActive(false);
        MenuUI.SetActive(false);
    }

    public void NewGame()
    {
        Application.LoadLevel("Game");
    }
    public void LoadMenu()
    {
        Application.LoadLevel("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
