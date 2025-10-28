using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoadScene : SingletonMonoBehaviour<LoadScene>
{
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    private static CanvasGroup _canvasGroup;

    protected override void Awake()
    {
        if (CheckInstance() == false)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        _canvasGroup = _fadeCanvasGroup;
        _canvasGroup.alpha = 0;
    }

    /// <summary>
    /// フェード関数
    /// </summary>
    /// <returns></returns>
    public static async UniTask Fade(float start, float end, float duration = 0.5f)
    {
        await DOVirtual.Float(start, end, duration, f =>
        {
            _canvasGroup.alpha = f;
        }).ToUniTask();
        _canvasGroup.alpha = end;
    }

    public static async void Load(string scene)
    {
        await Fade(0, 1, 0.5f);
        SceneManager.LoadScene(scene);
        await Fade(1, 0, 0.5f);
    }
}
