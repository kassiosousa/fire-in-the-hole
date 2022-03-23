using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogoScript : MonoBehaviour {

    GameManager gameManager = new GameManager();
    private SpriteRenderer spriteRender;
    public int Damage = 0;
    public bool IsOriginal = true;
    
    private AudioSource audioFogo;
    public AudioClip Explosao;

	// Use this for initialization
	void Start () {
        // Audios
        audioFogo = GetComponent<AudioSource>();
        audioFogo.PlayOneShot(Explosao, 1);

        gameManager.AddFogoGame();

        if (IsOriginal) { 
            Destroy(gameObject, 20);
            gameManager.RemoveFogoGame();
        }

        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRender.bounds.max).y * -1; // Atualiza a ordem do layer de acordo com eixo Y (Fake Z)
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            // O objeto ta perto e ta pegando fogo aos poucos, avisa a ele para dar dano
            other.gameObject.GetComponent<ItensScript>().InFire = true;
        }
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Pequeno Dano no jogador");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            // O objeto ta perto e ta pegando fogo aos poucos, avisa a ele para dar dano
            other.gameObject.GetComponent<ItensScript>().InFire = true;
        }
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Pequeno Dano no jogador");
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            other.gameObject.GetComponent<ItensScript>().InFire = false;
        }
    }

}
