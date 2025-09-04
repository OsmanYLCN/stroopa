using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ColorDisplay colorDisplay; // RenkCerceve'yi y�neten script
    public ButtonSpawner buttonSpawner; // Butonlar� y�neten script
    public ScoreSystem scoreSystem; // Skor sistemini y�neten script

    private string currentTargetColor; // �u anki hedef renk
    private string previousTargetColor; // Bir �nceki hedef renk
    private float reactionStartTime; // Tepki s�resi

    public GameObject gameUI; // Oyun UI's�
    public TextMeshProUGUI GameOverText;
    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        reactionStartTime = Time.time;

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
            float reactionTime = Time.time - reactionStartTime;
            bool isCorrect = true;

            scoreSystem.CalculateScore(isCorrect, reactionTime);
            Debug.Log("Do�ru cevap!");
            StartNewRound(); // Yeni tur ba�lat
        }
        else
        {
            bool isCorrect = false;
            scoreSystem.CalculateScore(isCorrect, reactionStartTime);
            Debug.Log("Yanl�� cevap!");
        }
    }

    public void GameOver()
    {
        Debug.Log("Oyun sona erdi!");

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        if (GameOverText != null)
        {
            GameOverText.gameObject.SetActive(true);
            GameOverText.text = "Game Over!\nFinal Score: " + scoreSystem.GetTotalScore(); // Final skoru g�ster
        }
        else
        {
            Debug.LogWarning("Game Over Text atanmad�!");
        }
    }
}