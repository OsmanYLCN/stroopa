// 3.09.2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using TMPro; // TextMeshPro için gerekli

public class ScoreSystem : MonoBehaviour
{
    private int totalScore = 0; // Toplam puaný tutar
    public TextMeshProUGUI scoreText; // Skoru göstermek için TextMeshPro referansý

    // Puan hesaplama fonksiyonu
    public int CalculateScore(bool isCorrect, float reactionTime)
    {
        int score = 0;

        // Doðru veya yanlýþ cevaba göre puan ekle/çýkar
        if (isCorrect)
        {
            score += 100;
        }
        else
        {
            score -= 50;
        }

        // Tepki süresine göre hýz bonusu ekle
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

        // Toplam puaný güncelle
        totalScore += score;

        // Skoru ekrana yazdýr
        UpdateScoreUI();

        // Güncel toplam puaný döndür
        return totalScore;
    }

    // Skoru UI'da güncelleyen fonksiyon
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
        else
        {
            Debug.LogWarning("Score Text atanmadý!");
        }
    }

    // Toplam puaný almak için bir getter fonksiyonu
    public int GetTotalScore()
    {
        return totalScore;
    }
}