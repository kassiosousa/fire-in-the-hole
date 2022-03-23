using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour {

    RaycastHit2D hit;
    public Transform HoldPoint; // Ponto de junção entre o player e objetos
    private Animator animPlayer;
    private PlayerScript playerScript;

    public bool grabbed;
    public bool MovelBig; // Recebe do item se é algo grande que está carregando
    public float distance=2f; // Define a distancia para alcançar um objeto

    public float throwForce; // Força ao largar o objeto

    AudioSource audio;
    public AudioClip SomArrastandoLeve;
    public AudioClip SomArrastandoNormal;
    public AudioClip SomArrastandoPesado;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        animPlayer = GetComponent<Animator>();
        playerScript = this.gameObject.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        // Se apertar Espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Se não pegou nada, pega
            if (!grabbed)
            {
                // Monta o raycast que vai veificar o hit
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance); // X -> Define o eixo do raio
                
                // Quer dizer que bateu em algo pegável enquanto apertou Espaço
                if (hit.collider != null && hit.collider.tag == "Grabbable")
                {
                    // Grab
                    //this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbed = true; // Pegou Item
                    // Sempre que pegar um item, informa se é grande ou não
                    MovelBig = hit.collider.gameObject.GetComponent<ItensScript>().MovelIsBig;
                    
                    hit.collider.gameObject.GetComponent<ItensScript>().InGrabbed = true; // Avisa pro item que foi pego pra não pegar fogo
                    animPlayer.SetBool("isGrabbed", true); // Animação de carregar item

                    // Se o item é grande, aumenta o range HoldPosition
                    if (MovelBig) {
                        if (playerScript.facingRight) { HoldPoint.position = new Vector2(HoldPoint.position.x + 0.5f, HoldPoint.position.y); }
                        else { HoldPoint.position = new Vector2(HoldPoint.position.x - 0.5f, HoldPoint.position.y);}
                    }
                }
            }
            // Se já pegou, agora solta ao apertar espaço
            else  {
                // Trhow
                //this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                // Quando solta o item grande, o HoldPosition volta pro lugar
                if (MovelBig) {
                    if (playerScript.facingRight) { HoldPoint.position = new Vector2(HoldPoint.position.x - 0.5f, HoldPoint.position.y); }
                    else { HoldPoint.position = new Vector2(HoldPoint.position.x + 0.5f, HoldPoint.position.y); }
                }

                grabbed = false; //Largou item
                MovelBig = false; // Sempre que largar um item, bota falso
                hit.collider.gameObject.GetComponent<ItensScript>().InGrabbed = false; // Avisa pro item que ele pode pegar fogo
                animPlayer.SetBool("isGrabbed", false); // Parar animação de carregar

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
                }
            }
        }
        // Se o item está seguro, diz que não está em fogo e mantem na posição do Hold
        if (grabbed)
        {

            hit.collider.gameObject.GetComponent<ItensScript>().InFire = false;
            audio.PlayOneShot(SomArrastandoLeve, 0.2F);
            hit.collider.gameObject.transform.position = HoldPoint.position;
        }
	}

    void OnDrawGizmos()
    {
        // Desenha a linha Raycast
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

}
