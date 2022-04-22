using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    private int _levelIndex;

    public void SetDate(int index)
    {
        _levelIndex = index;
        _text.text = _levelIndex.ToString();
    }
    public void LevelLoading()
    {
        LevelLoader.loadedLevel = _levelIndex;
        SceneManager.LoadScene(1);
    }
}
