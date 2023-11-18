using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed = 2.0f;  // Velocitat de moviment del personatge

    [SerializeField]
    public float jumpForce = 4.0f;  // For�a del salt

    private bool isGrounded;  // Variable per verificar si el personatge est� en contacte amb el terra
    
    private void Update()
    {
        // Moviment horitzontal
        float Axis = Input.GetAxis("Horizontal"); // Obtenir l'entrada del teclat (A/D o fletxes esquerra/dreta)
        float move = Axis * speed * Time.deltaTime; // Calcular el despla�ament horitzontal

        // Reorientar el personatge segons la direcci� del moviment
        if (Axis > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar cap a la dreta
        }
        else if (Axis < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar cap a l'esquerra
        }

        transform.Translate(move, 0, 0); // Moure el personatge

        // Salt
        if (Input.GetButtonDown("Jump") && isGrounded) // Comprovar si s'ha premut el bot� de salt i est� en terra
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Aplicar una for�a de salt
        }
    }

    // Detectar col�lisi� amb el terra
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D"); // Missatge de registre per a col�lisi�
        if (collision.collider.CompareTag("Ground")) // Comprovar si el personatge est� tocant el terra
        {
            isGrounded = true; // Establir que el personatge est� en terra
        }
    }

    // Detectar quan deixa de tocar el terra
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2D"); // Missatge de registre per a sortida de col�lisi�
        if (collision.collider.CompareTag("Ground")) // Comprovar si el personatge ha deixat de tocar el terra
        {
            isGrounded = false; // Establir que el personatge no est� en terra
        }
    }
}
