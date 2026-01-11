using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [Header("Sprites por direção")]
    public Sprite[] upSprites;    // 0 e 1
    public Sprite[] downSprites;  // 0 e 1
    public Sprite[] leftSprites;  // 0 e 1
    public Sprite[] rightSprites; // 0 e 1

    [Header("Configuração de animação")]
    public float animationSpeed = 0.3f; // tempo entre troca de sprites

    private SpriteRenderer spriteRenderer;
    private float timer;
    private int spriteIndex;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se o player está parado, mantém o sprite parado (primeiro do array)
        if (move == Vector2.zero)
        {
            spriteIndex = 0;
            if (spriteRenderer.sprite == null) return;

            if (spriteRenderer.sprite == upSprites[1] || spriteRenderer.sprite == upSprites[0])
                spriteRenderer.sprite = upSprites[0];
            else if (spriteRenderer.sprite == downSprites[1] || spriteRenderer.sprite == downSprites[0])
                spriteRenderer.sprite = downSprites[0];
            else if (spriteRenderer.sprite == leftSprites[1] || spriteRenderer.sprite == leftSprites[0])
                spriteRenderer.sprite = leftSprites[0];
            else if (spriteRenderer.sprite == rightSprites[1] || spriteRenderer.sprite == rightSprites[0])
                spriteRenderer.sprite = rightSprites[0];

            timer = 0;
            return;
        }

        // Escolhe o array correto de sprites dependendo da direção
        Sprite[] currentSprites = downSprites;

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            if (move.x > 0) currentSprites = rightSprites;
            else if (move.x < 0) currentSprites = leftSprites;
        }
        else
        {
            if (move.y > 0) currentSprites = upSprites;
            else if (move.y < 0) currentSprites = downSprites;
        }

        // Animação simples: troca entre os dois sprites baseado no tempo
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            spriteIndex = (spriteIndex + 1) % currentSprites.Length;
            spriteRenderer.sprite = currentSprites[spriteIndex];
            timer = 0;
        }
    }
}
