using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HotbarManager : MonoBehaviour
{
    public Transform hotbarPanel; // Panel donde se mostrarán los slots de la hotbar

    // Diccionario para rastrear los objetos y su cantidad en los slots
    private Dictionary<string, int> itemQuantities = new Dictionary<string, int>();

    public void AddItemToHotbar(GameObject item)
    {
        ItemProperties properties = item.GetComponent<ItemProperties>();

        if (properties == null)
        {
            Debug.LogWarning("El objeto no tiene ItemProperties y no se puede añadir a la hotbar.");
            return;
        }

        // Verifica si el ítem ya existe en la hotbar
        foreach (Transform slot in hotbarPanel)
        {
            HotbarSlot hotbarSlot = slot.GetComponent<HotbarSlot>();
            if (hotbarSlot != null && hotbarSlot.GetItemName() == properties.itemName)
            {
                itemQuantities[properties.itemName]++;
                hotbarSlot.UpdateItemCount(itemQuantities[properties.itemName]);
                return;
            }
        }

        // Busca un slot vacío en la hotbar
        foreach (Transform slot in hotbarPanel)
        {
            HotbarSlot hotbarSlot = slot.GetComponent<HotbarSlot>();
            if (hotbarSlot != null && string.IsNullOrEmpty(hotbarSlot.GetItemName()))
            {
                itemQuantities[properties.itemName] = 1;
                hotbarSlot.SetItem(item.GetComponent<Image>().sprite, itemQuantities[properties.itemName]);
                hotbarSlot.SetItemName(properties.itemName);
                return;
            }
        }

        Debug.LogWarning("No hay slots vacíos disponibles en la hotbar.");
    }
}