using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(fileName = "NewSetting", menuName = "Setting", order = 51)]
public class Setting : ScriptableObject
{
    public PostProcessProfile PostProcessProfile = null;
    public GameObject ItemPrefab;
    public LineInfo[] Lines;

    private static Setting[] _settings;

    public static Setting GetRandomSetting()
    {
        if(_settings == null) _settings = Resources.LoadAll<Setting>("Settings"); ;
        return _settings[Random.Range(0, _settings.Length)];
    }
}
