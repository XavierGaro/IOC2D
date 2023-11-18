using UnityEngine;

public class PlayerControllerAnimated : MonoBehaviour
{
    public float speed = 2.0f;  // Velocitat de moviment del personatge
    public float jumpForce = 4.0f;  // For�a del salt
    private bool isGrounded = true;  // Variable per verificar si el personatge est� en contacte amb el terra

    private Animator animator;  // Refer�ncia a l'Animator del personatge
    private Rigidbody2D rb;  // Refer�ncia al Rigidbody2D per controlar la f�sica

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtenir el component Animator
        rb = GetComponent<Rigidbody2D>(); // Obtenir el component Rigidbody2D
    }

    private void Update()
    {
        // Moviment horitzontal
        float Axis = Input.GetAxis("Horizontal"); // Obtenir l'entrada del teclat (A/D o fletxes esquerra/dreta)
        float move = Axis * speed * Time.deltaTime; // Calcular el despla�ament horitzontal

        // Actualitzar l'Animator amb la velocitat de moviment
        animator.SetFloat("Speed", Mathf.Abs(Axis)); // Estableix el par�metre "Speed" a l'Animator

        // Canviar la direcci� del sprite segons la direcci� de moviment
        if (Axis > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar cap a la dreta
        }
        else if (Axis < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar cap a l'esquerra
        }

        transform.Translate(move, 0, 0); // Moure el personatge

        // Gesti� del salt
        if (Input.GetButtonDown("Jump") && isGrounded) // Comprovar si s'ha premut el bot� de salt i est� en terra
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Aplicar una for�a de salt
            // Com aix� far� que el personatge deixi de tocar el terra s
            // 'activar� la variable isGrounded
        }

        // Actualitzar l'Animator amb l'estat de isGrounded
        animator.SetBool("IsGrounded", isGrounded); // Estableix el par�metre "IsGrounded" a l'Animator

        // Gesti� de l'atac
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    // Detectar col�lisi� amb el terra
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) // Comprovar si el personatge est� tocant el terra
        {
            isGrounded = true; // Establir que el personatge est� en terra
        }
    }

    // Detectar quan deixa de tocar el terra
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) // Comprovar si el personatge ha deixat de tocar el terra
        {
            isGrounded = false; // Establir que el personatge no est� en terra
        }
    }
}
