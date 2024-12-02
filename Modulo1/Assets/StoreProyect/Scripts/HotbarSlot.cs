using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class HotbarSlot : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemCountText; // Usa solo TextMeshProUGUI

    private string itemName;

    // Configura el �tem en el slot
    public void SetItem(Sprite icon, int count)
    {
        itemIcon.sprite = icon;
        itemIcon.enabled = true;
        itemCountText.text = "x" + count;
        itemCountText.enabled = true;
    }

    // Actualiza solo la cantidad de �tems en el slot
    public void UpdateItemCount(int count)
    {
        itemCountText.text = "x" + count;
    }

    // Establece el nombre del �tem (para identificarlo)
    public void SetItemName(string name)
    {
        itemName = name;
    }

    // Obtiene el nombre del �tem (para comparaci�n)
    public string GetItemName()
    {
        return itemName;
    }
}