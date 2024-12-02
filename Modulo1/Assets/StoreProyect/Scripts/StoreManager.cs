using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public CurrencySystem currencySystem; // Sistema de monedas
    public Transform storePanel; // Panel que contiene los slots de la tienda
    public Button buyButton; // Botón de compra
    public HotbarManager hotbarManager; // Referencia al HotbarManager
    public TextMeshProUGUI storeLevelText; // Texto que muestra el nivel de la tienda

    [SerializeField] private int storeLevel = 1; // Nivel de la tienda
    [SerializeField] private int itemsPurchased = 0; // Cantidad de objetos comprados

    private GameObject selectedItem = null; // Objeto actualmente seleccionado
    private GameObject selectedSlot = null; // Slot actualmente seleccionado

    void Start()
    {
        // Configura los botones de los slots
        foreach (Transform slot in storePanel)
        {
            Button slotButton = slot.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() => SelectItem(slot.gameObject));
            }
        }

        // Configura el botón de compra
        if (buyButton != null)
        {
            buyButton.onClick.AddListener(BuySelectedItem);
        }

        UpdateStoreLevelUI();
        UnlockItemsForLevel(storeLevel); // Inicializa los ítems visibles para el nivel actual
    }

    public void SelectItem(GameObject slot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.GetComponent<Image>().color = Color.white; // Restablece el color del slot anterior
        }

        selectedSlot = slot;
        selectedSlot.GetComponent<Image>().color = Color.cyan; // Resalta el slot seleccionado

        // Busca el ItemProperties en los hijos del slot seleccionado
        ItemProperties itemProperties = slot.GetComponentInChildren<ItemProperties>(true);
        if (itemProperties != null)
        {
            selectedItem = itemProperties.gameObject;
            Debug.Log("Objeto seleccionado: " + itemProperties.itemName);
        }
        else
        {
            selectedItem = null;
            Debug.LogWarning("El slot seleccionado no contiene un objeto válido.");
        }
    }

    public void BuySelectedItem()
    {
        if (selectedItem != null)
        {
            ItemProperties properties = selectedItem.GetComponent<ItemProperties>();

            if (properties != null)
            {
                if (currencySystem.CanAfford(properties.copperCost, properties.silverCost, properties.goldCost))
                {
                    currencySystem.DeductCost(properties.copperCost, properties.silverCost, properties.goldCost);
                    hotbarManager.AddItemToHotbar(selectedItem);

                    itemsPurchased++;
                    Debug.Log($"Compra realizada: {properties.itemName}. Ítems comprados: {itemsPurchased}");
                    CheckStoreLevel();
                }
                else
                {
                    Debug.Log("No tienes suficiente dinero para comprar este objeto.");
                }
            }
            else
            {
                Debug.LogWarning("El objeto seleccionado no tiene propiedades de ítem.");
            }
        }
        else
        {
            Debug.LogWarning("No hay ningún objeto seleccionado para comprar.");
        }
    }

    private void CheckStoreLevel()
    {
        // Actualiza el nivel de la tienda según los ítems comprados
        if (storeLevel == 1 && itemsPurchased >= 5)
        {
            storeLevel = 2;
            Debug.Log("Tienda subió a nivel 2");
        }
        else if (storeLevel == 2 && itemsPurchased >= 8)
        {
            storeLevel = 3;
            Debug.Log("Tienda subió a nivel 3");
        }
        else if (storeLevel == 3 && itemsPurchased >= 10)
        {
            storeLevel = 4;
            Debug.Log("Tienda subió a nivel 4");
        }

        UnlockItemsForLevel(storeLevel);
        UpdateStoreLevelUI();
    }

    private void UnlockItemsForLevel(int level)
    {
        Debug.Log($"Desbloqueando ítems para nivel: {level}");
        foreach (Transform slot in storePanel)
        {
            ItemProperties itemProperties = slot.GetComponentInChildren<ItemProperties>(true);
            if (itemProperties != null)
            {
                int requiredLevel = GetRequiredLevel(itemProperties.itemName);
                Debug.Log($"Ítem: {itemProperties.itemName}, Nivel requerido: {requiredLevel}, Nivel actual: {level}");

                // Activa el ítem solo si el nivel actual de la tienda es suficiente
                slot.gameObject.SetActive(requiredLevel <= level);

                if (requiredLevel == level)
                {
                    Debug.Log($"Ítem {itemProperties.itemName} desbloqueado para el nivel {level}");
                }
            }
            else
            {
                Debug.LogWarning($"El slot {slot.name} no contiene un componente ItemProperties.");
            }
        }
    }
    private int GetRequiredLevel(string itemName)
    {
        // Define los niveles necesarios para desbloquear cada ítem
        switch (itemName)
        {
            case "Pocion de vida menor":
                return 2; // Nivel 2 requerido
            case "Belladona":
                return 3; // Nivel 3 requerido
            case "Gafas":
                return 4; // Nivel 4 requerido
            default:
                return 1; // Nivel 1 para ítems básicos
        }
    }

    private void UpdateStoreLevelUI()
    {
        if (storeLevelText != null)
        {
            storeLevelText.text = storeLevel.ToString(); // Solo muestra el nivel actual
        }
        else
        {
            Debug.LogWarning("El texto del nivel de la tienda no está asignado.");
        }
    }
}
