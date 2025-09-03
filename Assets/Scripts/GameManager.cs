

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ColorDisplay colorDisplay; // RenkCerceve'yi y�neten script
    public ButtonSpawner buttonSpawner; // Butonlar� y�neten script

    private string currentTargetColor; // �u anki hedef renk
    private string previousTargetColor; // Bir �nceki hedef renk

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        // Yeni butonlar� olu�tur
        buttonSpawner.SpawnUniqueButtons();

        // Yeni bir hedef renk se�
        SetRandomTargetColor();
    }

    private void SetRandomTargetColor()
    {
        // ButtonSpawner'dan olu�turulan butonlar�n renklerini al
        var chosenPrefabs = buttonSpawner.GetChosenPrefabs();
        if (chosenPrefabs == null || chosenPrefabs.Count == 0)
        {
            Debug.LogWarning("Butonlar olu�turulamad�!");
            return;
        }

        // Se�ilen butonlar�n Color Name'lerini al
        var availableColors = new System.Collections.Generic.List<string>();
        foreach (var prefab in chosenPrefabs)
        {
            var info = prefab.GetComponent<ColorButtonInfo>();
            if (info != null && info.colorName != previousTargetColor)
            {
                availableColors.Add(info.colorName);
            }
        }

        // Rastgele bir hedef renk se�
        if (availableColors.Count > 0)
        {
            currentTargetColor = availableColors[Random.Range(0, availableColors.Count)];
            previousTargetColor = currentTargetColor;

            // RenkCerceve'deki metni g�ncelle
            colorDisplay.SetColor(currentTargetColor);
        }
        else
        {
            Debug.LogWarning("Yeni bir hedef renk se�ilemedi!");
        }
    }

    public void OnButtonClicked(string colorName)
    {
        if (colorName == currentTargetColor)
        {
            Debug.Log("Do�ru cevap!");
            StartNewRound(); // Yeni tur ba�lat
        }
        else
        {
            Debug.Log("Yanl�� cevap!");
        }
    }
}