using UnityEngine;

namespace UI.Canvas
{
    public sealed class MainMenu : MonoBehaviour
    {
        private const string GITHUB_REPO = "https://github.com/bas-boop/Shuttlecrane";
        
        public void OpenGithub() => Application.OpenURL(GITHUB_REPO);

        public void Quit() => Application.Quit();
    }
}