using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessingSetter : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PostProcessVolume>().profile = GameInfo.Instanse.Setting.PostProcessProfile;
    }
}
