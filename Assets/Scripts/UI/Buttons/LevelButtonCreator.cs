using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonCreator : MonoBehaviour
{
    private Levels levels;
    private GameObject buttonPrefab;
    void Start()
    {
        LoadResources();
        ButtonCreator();
    }

    private void ButtonCreator()
    {
        for (int i = 0; i < levels.level.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            UILevelButton levelButton = button.GetComponent<UILevelButton>();
            levelButton.SetDate(i + 1);
        }
    }
    private void LoadResources()
    {
        TextAsset json = Resources.Load<TextAsset>("Text/LevelsList");
        levels = JsonUtility.FromJson<Levels>(json.text);
        buttonPrefab = Resources.Load<GameObject>("Prefabs/LevelButton");
    }
}
