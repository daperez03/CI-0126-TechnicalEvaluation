using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TechnicalEvaluation.Presentation.Unity
{
    public class UIManager : Singleton<UIManager>
    {
        public void ShowUIElement(GameObject uiElement)
        {
            uiElement.SetActive(true);
        }

        public void HideUIElement(GameObject uiElement)
        {
            uiElement.SetActive(false);
        }

        public void ChangeToUIScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ChangeToUIScene(string sceneName, Action callback)
        {
            SceneManager.LoadScene(sceneName);

            StartCoroutine(WaitForSceneLoad(sceneName, callback));
        }

        private IEnumerator WaitForSceneLoad(string sceneName, Action onLoaded)
        {
            yield return null; // Wait one frame

            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name == sceneName)
            {
                Debug.Log("New scene loaded and active: " + activeScene.name);
            }
            else
            {
                Debug.Log("Still in old scene: " + activeScene.name);
            }

            onLoaded?.Invoke();
        }

    }
}
