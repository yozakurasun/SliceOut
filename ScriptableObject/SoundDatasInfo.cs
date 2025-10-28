using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatasInfo", menuName = "ScriptableObject/SoundDatasInfo", order = 0)]
public class SoundDatasInfo : ScriptableObject
{
    [SerializeField] public List<SoundData> SoundDatas;
}
[Serializable]
public class SoundData
{
    public SoundType SoundType;
    public AudioClip AudioClip;
}
