using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingManager : MonoBehaviour
{
    public UnitSettingMenu[] _unitSettings;
    public MapSettingMenu _mapSettings;
    public bool _friendlyFire;
	// Use this for initialization
	
    public GameSettings GetSettings()
    {
        GameSettings _settings = new GameSettings();
        _settings._friendlyFire = _friendlyFire;
        _settings._indexSceneMap = _mapSettings._selectedMap._indexScene;
        _settings._initStartUnit = new Dictionary<string, int>();
        _settings._indexListMap = _mapSettings._indexMap;
        foreach (UnitSettingMenu usm in _unitSettings)
        {
            _settings._initStartUnit.Add(usm._unitName, usm._number);
        }

        return _settings;
    }
}
