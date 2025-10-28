using UnityEngine;

public class Button : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        LoadScene.Load(sceneName);
    }
}
