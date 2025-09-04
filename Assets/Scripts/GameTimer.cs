using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 100f; // Oyun s�resi s�n�r� (saniye cinsinden)
    private float currentTime; // Kalan s�re
    public TextMeshProUGUI timerText; // Zamanlay�c�y� g�stermek i�in TextMeshPro referans�
    public GameManager gameManager; // GameManager referans�

    private bool isGameOver = false; // Oyunun bitip bitmedi�ini kontrol eder

    void Start()
    {
        currentTime = timeLimit; // S�reyi ba�lang��ta 100 olarak ayarla
        UpdateTimerUI(); // UI'� g�ncelle
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return; // E�er oyun bittiyse geri say�m� durdur

        // S�reyi azalt
        currentTime -= Time.deltaTime;

        // S�re 0'�n alt�na d��erse oyunu bitir
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame();
        }

        // UI'� g�ncelle
        UpdateTimerUI();
    }

    // Timer UI'�n� g�ncelleyen fonksiyon
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString(); // S�reyi tam say� olarak g�ster
        }
        else
        {
            Debug.LogWarning("Timer Text atanmad�!");
        }
    }

    // Oyunu bitiren fonksiyon
    private void EndGame()
    {
        isGameOver = true;
        Debug.Log("S�re doldu! Oyun bitti.");
        if (gameManager != null)
        {
            gameManager.GameOver(); // GameManager'daki GameOver fonksiyonunu �a��r
        }
    }
}
