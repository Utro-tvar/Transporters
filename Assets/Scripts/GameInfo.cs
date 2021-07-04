using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewGamePattern", menuName = "GamePattern", order = 51)]
public class GameInfo : ScriptableObject
{
    public static GameInfo Instanse { get; set; }

    public Camera MainCamera { get; set; }

    public float Speed { get; private set; }
    public float MinSpeed = 0;
    public float MaxSpeed = 3;
    public float Boost = 1;
    public int ItemInterval;
    public int HP;
    [HideInInspector] public Setting Setting;
    [HideInInspector] public LineInfo[] Lines;
    //public Color[] Colors;

    public float TopEdge { get; private set; }
    public float DownEdge { get; private set; }

    private bool _isActive = false;

    public void SetActive(bool state)
    {
        _isActive = state;
        if (state)
        {
            Instanse = this;
        }
    }

    public void Init()
    {
        if (!_isActive) return;
        Setting = Setting.GetRandomSetting();
        Speed = MinSpeed;
        MainCamera = Camera.main;
        MainCamera.GetComponent<CameraScaler>().WakeUp();
        TopEdge = MainCamera.ScreenToWorldPoint(new Vector3(0, MainCamera.pixelHeight - 1, 0)).y;
        DownEdge = MainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        List<LineInfo> lineInfos = new List<LineInfo>();
        foreach (LineInfo lineInfo in Setting.Lines)
        {
            lineInfos.Add(lineInfo);
        }
        int count = lineInfos.Count;
        while(count > 4)
        {
            lineInfos.RemoveAt(Random.Range(0, count));
            --count;
        }
        Lines = lineInfos.ToArray();
    }

    private void OnEnable()
    {
        _isActive = false;
        SceneManager.sceneLoaded += SceneLoaded;
        GameEvents.FixedUpdateCall += AddSpeed;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        GameEvents.FixedUpdateCall -= AddSpeed;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!_isActive) return;
        if (scene == SceneManager.GetSceneByBuildIndex(1))
        {
            Init();
        }
    }

    private void AddSpeed()
    {
        if (!_isActive) return;
        if(Speed < MaxSpeed)
        {
            Speed += Boost * Time.fixedDeltaTime;
        }
    }
}
