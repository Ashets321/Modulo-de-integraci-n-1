using UnityEngine;
using TMPro; // Importa el namespace de TextMeshPro

public class CurrencySystem : MonoBehaviour
{
    public int copperCoins = 0;
    public int silverCoins = 0;
    public int goldCoins = 0;

    // Referencias a los textos de UI de TextMeshPro
    public TextMeshProUGUI copperText;
    public TextMeshProUGUI silverText;
    public TextMeshProUGUI goldText;

    public void AddCopper(int amount)
    {
        copperCoins += amount;
        UpdateCurrency();
    }

    public void AddSilver(int amount)
    {
        silverCoins += amount;
        UpdateCurrency();
    }

    public void AddGold(int amount)
    {
        goldCoins += amount;
        UpdateCurrency();
    }

    private void UpdateCurrency()
    {
        if (copperCoins >= 100)
        {
            silverCoins += copperCoins / 100;
            copperCoins %= 100;
        }

        if (silverCoins >= 100)
        {
            goldCoins += silverCoins / 100;
            silverCoins %= 100;
        }

        // Actualizar los textos de UI
        UpdateUI();
    }

    public bool CanAfford(int copperCost, int silverCost, int goldCost)
    {
        int totalPlayerCopper = copperCoins + (silverCoins * 100) + (goldCoins * 10000);
        int totalCostCopper = copperCost + (silverCost * 100) + (goldCost * 10000);

        Debug.Log("Total del jugador en cobre: " + totalPlayerCopper);
        Debug.Log("Costo total en cobre: " + totalCostCopper);

        return totalPlayerCopper >= totalCostCopper;
    }

    public void DeductCost(int copperCost, int silverCost, int goldCost)
    {
        if (CanAfford(copperCost, silverCost, goldCost))
        {
            DeductCopper(copperCost);
            DeductSilver(silverCost);
            DeductGold(goldCost);

            UpdateCurrency();
        }
        else
        {
            Debug.LogWarning("No tienes suficientes monedas para esta compra.");
        }
    }

    private void DeductCopper(int amount)
    {
        while (copperCoins < amount && (silverCoins > 0 || goldCoins > 0))
        {
            if (silverCoins > 0)
            {
                silverCoins--;
                copperCoins += 100; // Convierte 1 plata a 100 cobre
            }
            else if (goldCoins > 0)
            {
                goldCoins--;
                silverCoins += 99; // Convierte 1 oro a 99 plata
                copperCoins += 100; // Convierte 1 oro a 100 cobre
            }
        }

        copperCoins -= amount;
        NormalizeCurrency();
    }

    private void DeductSilver(int amount)
    {
        while (silverCoins < amount && goldCoins > 0)
        {
            goldCoins--;
            silverCoins += 100; // Convierte 1 oro a 100 plata
        }

        silverCoins -= amount;
        NormalizeCurrency();
    }

    private void DeductGold(int amount)
    {
        goldCoins -= amount;
        NormalizeCurrency();
    }

    private void NormalizeCurrency()
    {
        // Asegura que los valores se mantengan en los rangos correctos
        if (copperCoins < 0)
        {
            silverCoins += (copperCoins / 100) - 1;
            copperCoins = (copperCoins % 100) + 100;
        }

        if (silverCoins < 0)
        {
            goldCoins += (silverCoins / 100) - 1;
            silverCoins = (silverCoins % 100) + 100;
        }

        if (copperCoins >= 100)
        {
            silverCoins += copperCoins / 100;
            copperCoins %= 100;
        }

        if (silverCoins >= 100)
        {
            goldCoins += silverCoins / 100;
            silverCoins %= 100;
        }
    }

    // Método para actualizar los textos de UI
    private void UpdateUI()
    {
        copperText.text = copperCoins.ToString();
        silverText.text = silverCoins.ToString();
        goldText.text = goldCoins.ToString();
    }

    // Método para añadir monedas de prueba
    public void AddTestCurrency(int copper, int silver, int gold)
    {
        AddCopper(copper);
        AddSilver(silver);
        AddGold(gold);
    }
}