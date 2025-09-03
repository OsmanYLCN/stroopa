// 3.09.2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

// 3.09.2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public List<GameObject> allPrefabs;  // List of prefabs
    public Transform buttonGrid;         // 2x2 Grid Panel
    public GameManager gameManager;      // Reference to GameManager

    private List<GameObject> chosenPrefabs = new List<GameObject>(); // Selected prefabs

    public List<GameObject> GetChosenPrefabs()
    {
        return chosenPrefabs;
    }

    public void SpawnUniqueButtons()
    {
        // Clear previous buttons
        foreach (Transform child in buttonGrid)
        {
            Destroy(child.gameObject);
        }

        // Track used names and background colors
        HashSet<string> usedNames = new HashSet<string>();
        HashSet<string> usedBackgrounds = new HashSet<string>();
        chosenPrefabs.Clear();

        // Shuffle the prefabs
        List<GameObject> shuffled = new List<GameObject>(allPrefabs);
        Shuffle(shuffled);

        foreach (var prefab in shuffled)
        {
            if (chosenPrefabs.Count >= 4) break;

            var info = prefab.GetComponent<ColorButtonInfo>();
            if (info == null)
            {
                Debug.LogWarning($"Prefab '{prefab.name}' is missing the ColorButtonInfo component!");
                continue;
            }

            // Select prefabs with unique colorName and bgColor
            if (usedNames.Contains(info.colorName) || usedBackgrounds.Contains(info.bgColor))
                continue;

            usedNames.Add(info.colorName);
            usedBackgrounds.Add(info.bgColor);
            chosenPrefabs.Add(prefab);

            // Instantiate the button and bind the click event
            GameObject button = Instantiate(prefab, buttonGrid);
            var btn = button.GetComponent<UnityEngine.UI.Button>();
            if (btn != null)
            {
                string colorName = info.colorName; // Button's Color Name
                btn.onClick.AddListener(() => gameManager.OnButtonClicked(colorName));
            }
        }

        // Warn if not enough unique prefabs were selected
        if (chosenPrefabs.Count < 4)
        {
            Debug.LogWarning("Not enough unique prefabs were found!");
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}