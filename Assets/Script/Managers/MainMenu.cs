using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private GameObject fadeIn;
    public void StartGame(string sceneName)
    {
        GameObject fade = Instantiate(fadeIn);
        StartCoroutine(WaitForFade(fade.GetComponent<Animator>().runtimeAnimatorController.animationClips[1].length, sceneName));
    }
    public void LoadGame(string sceneName)
    {
        SaveController.menuLoading = true;
        StartGame(sceneName);
    }

    private IEnumerator WaitForFade(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
