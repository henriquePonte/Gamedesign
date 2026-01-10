using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [Header("Sprites por direção")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Troca o sprite dependendo da direção do movimento
        if (move.x > 0)        // andando para a direita
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (move.x < 0)   // andando para a esquerda
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (move.y > 0)   // andando para cima
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (move.y < 0)   // andando para baixo
        {
            spriteRenderer.sprite = downSprite;
        }
    }
}
