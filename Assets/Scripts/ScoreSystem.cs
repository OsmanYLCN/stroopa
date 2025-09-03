// 3.09.2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using TMPro; // TextMeshPro i�in gerekli

public class ScoreSystem : MonoBehaviour
{
    private int totalScore = 0; // Toplam puan� tutar
    public TextMeshProUGUI scoreText; // Skoru g�stermek i�in TextMeshPro referans�

    // Puan hesaplama fonksiyonu
    public int CalculateScore(bool isCorrect, float reactionTime)
    {
        int score = 0;

        // Do�ru veya yanl�� cevaba g�re puan ekle/��kar
        if (isCorrect)
        {
            score += 100;
        }
        else
        {
            score -= 50;
        }

        // Tepki s�resine g�re h�z bonusu ekle
        if (reactionTime <= 1f)
        {
            score += 50;
        }
        else if (reactionTime <= 2f)
        {
            score += 30;
        }
        else if (reactionTime <= 3f)
        {
            score += 15;
        }
        else
        {
            score += 5;
        }

        // Toplam puan� g�ncelle
        totalScore += score;

        // Skoru ekrana yazd�r
        UpdateScoreUI();

        // G�ncel toplam puan� d�nd�r
        return totalScore;
    }

    // Skoru UI'da g�ncelleyen fonksiyon
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
        else
        {
            Debug.LogWarning("Score Text atanmad�!");
        }
    }

    // Toplam puan� almak i�in bir getter fonksiyonu
    public int GetTotalScore()
    {
        return totalScore;
    }
}