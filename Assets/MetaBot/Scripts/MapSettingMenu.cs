using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSettingMenu : MonoBehaviour
{
    [Header("Map")]
    public MapSetting[] _mapSettings;
    public MapSetting _selectedMap;
    public int _indexMap;

    [Header("Links")]
    public Image _mapThumbnailImage;
    public Button _nextButton;
    public Button _backButton;
    public Text _indexMapText;
    public Text _nameMapText;

    // Use this for initialization
    void Start ()
    {
        print("A " + GameObject.Find("GameManager"));
        print("B " + GameObject.Find("GameManager").GetComponent<GameManager>());
        print("C " + GameObject.Find("GameManager").GetComponent<GameManager>()._gameSettings);
        _indexMap = GameObject.Find("GameManager").GetComponent<GameManager>()._gameSettings._indexListMap;
        UpdateVisual();

    }

    public void Next()
    {
        _indexMap = (_indexMap + 1) % _mapSettings.Length;
        UpdateVisual();
    }

    public void Back()
    {
        _indexMap = (_indexMap + _mapSettings.Length - 1) % _mapSettings.Length;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        _selectedMap = _mapSettings[_indexMap];
        _mapThumbnailImage.sprite = _selectedMap._mapThumbnail;
        _indexMapText.text = ""+(_indexMap + 1) + "/" + _mapSettings.Length;
        _nameMapText.text = "" + _selectedMap._mapName;
}
}

[System.Serializable]
public struct MapSetting
{
    public string _mapName;
    public int _indexScene;
    public Sprite _mapThumbnail;
}