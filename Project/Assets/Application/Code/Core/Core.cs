using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtensions;

public class Core : ReferenceBehaviour<Core>
{
    #region Fields

    public FrameRate FrameRate = FrameRate.FPS60;

    #endregion

    #region Unity Methods

    protected override void OnEnable()
    {
        base.OnEnable();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = (int)FrameRate;
        SceneManager.LoadScene("Main Scene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Opened");
    }

    #endregion
}
