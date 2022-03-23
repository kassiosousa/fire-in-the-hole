using UnityEngine;
using System.Collections;

public class ItensScript : MonoBehaviour
{
    public bool InFire = false;
    public bool InGrabbed = false;
    public bool MovelIsBig;
    private int originalHP = 0;
    private SpriteRenderer spriteRenderer;

    // Características
    public GameObject GameObjectFire; // Fogo que vai substituir caso o objeto morra
    public int HPItem = 400;
    public int Peso = 4;
    public int DamageByFire = 4;
    // Variáveis para dar dano apenas 1 vez por segundo
    private int CountSeconds = 1;
    private float QtdTimeInFire = 0f;

    void Awake()
    {
        originalHP = this.HPItem;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.max).y * -1; // Atualiza a ordem do layer de acordo com eixo Y (Fake Z)
    }

    void Update()
    {
        // Se está pegando fogo
        if (InFire)
        {
            QtdTimeInFire += Time.deltaTime; // Recebe o tempo/frames
            // Se o tempo em frames é igual a mais 1 segundo, e
            // Não está sendo seguro, da o dano
            if ((QtdTimeInFire >= CountSeconds) && (!InGrabbed))
            {
                DamageItem(DamageByFire);
                CountSeconds++;
            }
        }
        else if (!InFire && originalHP > HPItem)
        {
            HPItem = originalHP; // Volta o HP Original
            QtdTimeInFire = 0;
            CountSeconds = 1;
        }
    }

    public void DamageItem(int loss)
    {
        HPItem -= loss;
        if (HPItem <= 0)
        {
            // Deve criar outro fogo no lugar do objeto
            GameObjectFire.GetComponent<FogoScript>().IsOriginal = false; // Este novo objeto não é dos originais, então, não deve ser destruído
            GameObject destroyedCarGameObject = (GameObject)Instantiate(GameObjectFire, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }


}
