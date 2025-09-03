// 3.09.2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;

public class ColorDisplay : MonoBehaviour
{
    public Image renkImage;

    public Color[] renkListesi; // Renklerin listesi
    public string[] renkAdlari; // Renk adlarýnýn listesi (örneðin: "Sari", "Kirmizi")

    public void SetColor(string colorName)
    {
        for (int i = 0; i < renkAdlari.Length; i++)
        {
            if (renkAdlari[i] == colorName)
            {
                renkImage.color = renkListesi[i];
                return;
            }
        }

        Debug.LogWarning($"Renk '{colorName}' renk listesinde bulunamadý!");
    }
}