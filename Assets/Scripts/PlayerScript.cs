using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // =================================
    // Elementos do GameObjetct player
    // =================================
    private int coins;
    public float restartLevelDelay = 1f;
    private Animator animPlayer;
    private SpriteRenderer spriteRender;
    private Rigidbody2D rbody;
    private GrabberScript grabbeScript;
    GameManager gameManager = new GameManager();
    public Slider sliderPlayerHealth;
    // =================================
    // Atributos do Player
    // =================================
    public bool facingRight = true;
    private bool isRunning = false;
    AudioSource audioPlayer;
    public AudioClip SomCaminhando;
    private bool FimJogo = false;

    public float maxSpeed = 120f;
    public int HPPlayer = 200;
    public int damageFire = 50;

    // Use this for initialization
    void Start()
    {
        animPlayer = this.GetComponent<Animator>();
        rbody = this.GetComponent<Rigidbody2D>();
        spriteRender = this.GetComponent<SpriteRenderer>();
        audioPlayer = this.GetComponent<AudioSource>();
        grabbeScript = this.GetComponent<GrabberScript>();

        sliderPlayerHealth.value = HPPlayer; // Informa o HP inicial do player
    }

    // Colisão com outros elementos 2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            //Damage(damageFire);
            //other.gameObject.SetActive(false);
        }

        if (other.tag == "Exit")
        {
            //Invoke("goArenaColiseu", restartLevelDelay);
            Application.LoadLevel(Application.loadedLevel); // Reinicia a mesma cena
            enabled = false; // Coloca o jogador desabilitado
        }
        
    }

    private void CheckIfGameOver()
    {
        if (HPPlayer <= 0)
        {
            //animPlayer.SetTrigger("bDeath"); // Animação de morte
            Application.LoadLevel("Menu");
        }
    }
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel); // Reinicia a mesma arena
    }

    public void Damage(int damage)
    {
        animPlayer.SetTrigger("tHit");
        HPPlayer -= damage;
        sliderPlayerHealth.value = HPPlayer;
        CheckIfGameOver();
    }


    void FixedUpdate()
    {
        FimJogo = gameManager.CheckEndGame();
        // Se o jogo não acabou
        if (!FimJogo)
        {
            // ANIMAÇÕES E CALCULO DE VELOCIDADE
            // Atualiza a velocidade de movimentaçao do player e fazer a movimentação do segu rigidbody
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

            if (Mathf.Abs(moveH) > 0)
            { // Se, movimento horizontal apenas, caminha
                animPlayer.SetBool("isWalk", true); // Diz que está caminhando
                //audioPlayer.PlayOneShot(SomCaminhando, 1);
                maxSpeed = 150f;
            }
            else if (Mathf.Abs(moveV) > 0)
            { // Se, movimento vertical, caminha
                animPlayer.SetBool("isWalk", true); // Diz que está caminhando
                //audioPlayer.PlayOneShot(SomCaminhando, 1);
                maxSpeed = 150f;
            }
            else { animPlayer.SetBool("isWalk", false); /* Não está caminhando */ }

            //=======================================================================
            // MOVIMENTAÇÃO DO PERSONAGEM
            Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * maxSpeed * Time.fixedDeltaTime;
            spriteRender.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRender.bounds.max).y * -1; // Atualiza a ordem do layer de acordo com eixo Y (Fake Z)
            // ==========================================================
            if ( (!grabbeScript.grabbed) || (!grabbeScript.MovelBig) ) { if (moveH > 0 && !facingRight) { Flip(); } else if (moveH < 0 && facingRight) { Flip(); } }
            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
            //=======================================================================
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
