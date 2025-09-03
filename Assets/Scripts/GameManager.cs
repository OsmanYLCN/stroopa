

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ColorDisplay colorDisplay; // RenkCerceve'yi yöneten script
    public ButtonSpawner buttonSpawner; // Butonlarý yöneten script

    private string currentTargetColor; // Þu anki hedef renk
    private string previousTargetColor; // Bir önceki hedef renk

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        // Yeni butonlarý oluþtur
        buttonSpawner.SpawnUniqueButtons();

        // Yeni bir hedef renk seç
        SetRandomTargetColor();
    }

    private void SetRandomTargetColor()
    {
        // ButtonSpawner'dan oluþturulan butonlarýn renklerini al
        var chosenPrefabs = buttonSpawner.GetChosenPrefabs();
        if (chosenPrefabs == null || chosenPrefabs.Count == 0)
        {
            Debug.LogWarning("Butonlar oluþturulamadý!");
            return;
        }

        // Seçilen butonlarýn Color Name'lerini al
        var availableColors = new System.Collections.Generic.List<string>();
        foreach (var prefab in chosenPrefabs)
        {
            var info = prefab.GetComponent<ColorButtonInfo>();
            if (info != null && info.colorName != previousTargetColor)
            {
                availableColors.Add(info.colorName);
            }
        }

        // Rastgele bir hedef renk seç
        if (availableColors.Count > 0)
        {
            currentTargetColor = availableColors[Random.Range(0, availableColors.Count)];
            previousTargetColor = currentTargetColor;

            // RenkCerceve'deki metni güncelle
            colorDisplay.SetColor(currentTargetColor);
        }
        else
        {
            Debug.LogWarning("Yeni bir hedef renk seçilemedi!");
        }
    }

    public void OnButtonClicked(string colorName)
    {
        if (colorName == currentTargetColor)
        {
            Debug.Log("Doðru cevap!");
            StartNewRound(); // Yeni tur baþlat
        }
        else
        {
            Debug.Log("Yanlýþ cevap!");
        }
    }
}