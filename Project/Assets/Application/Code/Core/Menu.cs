using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtensions;

public class Menu : ReferenceBehaviour<Menu>
{
    #region Unity Methods

    private async void Start()
    {
        if (PlayerPrefs.HasKey("Opened"))
        {
            MainFrame.Ref.ShowFrame();
            return;
        }

        MainFrame.Ref.ShowSplash().Forget();
        PlayerPrefs.SetString("Opened", "Yes");
    }

    #endregion
}
