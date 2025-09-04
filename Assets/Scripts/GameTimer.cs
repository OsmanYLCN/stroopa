using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 100f; // Oyun süresi sýnýrý (saniye cinsinden)
    private float currentTime; // Kalan süre
    public TextMeshProUGUI timerText; // Zamanlayýcýyý göstermek için TextMeshPro referansý
    public GameManager gameManager; // GameManager referansý

    private bool isGameOver = false; // Oyunun bitip bitmediðini kontrol eder

    void Start()
    {
        currentTime = timeLimit; // Süreyi baþlangýçta 100 olarak ayarla
        UpdateTimerUI(); // UI'ý güncelle
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return; // Eðer oyun bittiyse geri sayýmý durdur

        // Süreyi azalt
        currentTime -= Time.deltaTime;

        // Süre 0'ýn altýna düþerse oyunu bitir
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame();
        }

        // UI'ý güncelle
        UpdateTimerUI();
    }

    // Timer UI'ýný güncelleyen fonksiyon
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString(); // Süreyi tam sayý olarak göster
        }
        else
        {
            Debug.LogWarning("Timer Text atanmadý!");
        }
    }

    // Oyunu bitiren fonksiyon
    private void EndGame()
    {
        isGameOver = true;
        Debug.Log("Süre doldu! Oyun bitti.");
        if (gameManager != null)
        {
            gameManager.GameOver(); // GameManager'daki GameOver fonksiyonunu çaðýr
        }
    }
}
