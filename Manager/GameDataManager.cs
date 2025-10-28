using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private SoundDatasInfo _soundDatasInfo;
    private static List<SoundData> _soundDatas;

    protected void Awake()
    {
        _soundDatas = _soundDatasInfo.SoundDatas;
    }

    public static AudioClip GetSoundData(SoundType soundType)
    {
        var data = _soundDatas.FirstOrDefault(x => x.SoundType == soundType);
        if (data == null)
        {
            Debug.LogError($"データがありません。Key => {soundType}");
        }
        return data.AudioClip;
    }

    public static int GetEnumLength<T>() => Enum.GetValues(typeof(T)).Length;

    public static T GetRandomEnumType<T>() => (T)Enum.ToObject(typeof(T), UnityEngine.Random.Range(0, GetEnumLength<T>()));
}
