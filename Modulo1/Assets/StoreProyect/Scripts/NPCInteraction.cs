using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject storeMenu; // Panel de la tienda
    public Transform player; // Transform del jugador
    public float interactionDistance = 2.0f; // Distancia para permitir interacción
    public GameObject interactionText; // Texto flotante para mostrar al acercarse

    private bool isStoreOpen = false; // Estado de la tienda

    void Start()
    {
        // Asegúrate de que la tienda y el texto flotante estén ocultos al inicio
        if (storeMenu != null) storeMenu.SetActive(false);
        if (interactionText != null) interactionText.SetActive(false);
    }

    void Update()
    {
        // Calcula la distancia entre el jugador y el NPC
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= interactionDistance)
            {
                // Mostrar el texto si está dentro del rango
                if (interactionText != null && !interactionText.activeSelf)
                {
                    interactionText.SetActive(true);
                }

                // Permitir abrir la tienda si se presiona "E"
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleStore();
                }
            }
            else
            {
                // Ocultar el texto si está fuera del rango
                if (interactionText != null && interactionText.activeSelf)
                {
                    interactionText.SetActive(false);
                }
            }
        }
    }

    private void ToggleStore()
    {
        if (storeMenu != null)
        {
            isStoreOpen = !isStoreOpen;
            storeMenu.SetActive(isStoreOpen);

            Debug.Log(isStoreOpen ? "Tienda abierta" : "Tienda cerrada");
        }
    }
}
