using StarterAssets;
using UnityEngine;
using UnityEngine;

public class StoreToggleManager : MonoBehaviour
{
    public GameObject playerController; // El objeto del jugador con el script ThirdPersonController
    public GameObject playerCamera; // La cámara del jugador
    public GameObject storeUI; // La interfaz de la tienda
    public Transform npcTransform; // El NPC con el que interactuar

    public float interactionRange = 3f; // Rango de interacción con el NPC

    private bool isStoreOpen = false; // Estado de la tienda

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Tecla para abrir/cerrar la tienda
        {
            if (IsPlayerNearNPC()) // Solo abre la tienda si está cerca del NPC
            {
                ToggleStore();
            }
            else
            {
                Debug.Log("El jugador no está lo suficientemente cerca del NPC.");
            }
        }
    }

    private void ToggleStore()
    {
        isStoreOpen = !isStoreOpen;

        // Activar/desactivar la UI de la tienda
        storeUI.SetActive(isStoreOpen);

        if (isStoreOpen)
        {
            DisablePlayerSystems(); // Desactiva movimiento y cámara
        }
        else
        {
            EnablePlayerSystems(); // Reactiva movimiento y cámara
        }
    }

    private bool IsPlayerNearNPC()
    {
        if (npcTransform == null || playerController == null)
        {
            Debug.LogWarning("NPC o PlayerController no asignados.");
            return false;
        }

        float distanceToNPC = Vector3.Distance(playerController.transform.position, npcTransform.position);
        return distanceToNPC <= interactionRange; // Retorna verdadero si está en rango
    }

    private void DisablePlayerSystems()
    {
        // Desactivar el controlador de jugador
        if (playerController.TryGetComponent<ThirdPersonController>(out var controller))
        {
            controller.EnableController(false); // Método para desactivar movimiento
        }

        // Desactivar la cámara del jugador
        if (playerCamera != null)
        {
            playerCamera.SetActive(false);
        }

        // Mostrar y desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Sistemas desactivados: movimiento y cámara.");
    }

    private void EnablePlayerSystems()
    {
        // Reactivar el controlador de jugador
        if (playerController.TryGetComponent<ThirdPersonController>(out var controller))
        {
            controller.EnableController(true); // Método para activar movimiento
        }

        // Reactivar la cámara del jugador
        if (playerCamera != null)
        {
            playerCamera.SetActive(true);
        }

        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Sistemas reactivados: movimiento y cámara.");
    }
}
