using UnityEngine;

public class TestAddCurrency : MonoBehaviour
{
    public CurrencySystem currencySystem;

    void Start()
    {
        // Añade monedas de prueba al iniciar la escena
        currencySystem.AddTestCurrency(0, 0, 0);
    }
}