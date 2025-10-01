using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityExtensions;
using UnityExtensions.UI;

public class Location : ReferenceBehaviour<Location>
{
    #region Fields

    [Header("Core")]
    public Material LocationMaterial;
    public RenderTexture LocationTexture;
    public VideoPlayer Player360;
    public List<GameObject> Points;

    [Header("Buttons")]
    public LayoutComponent Layout;
    public ButtonElement PreviousButton;
    public ButtonElement MenuButton;
    public ButtonElement NextButton;

    private LocationConfig SelectedLocation => Data.Ref.SelectedLocation;
    private int CurrentID => Data.Ref.Locations.IndexOf(SelectedLocation);
    private int PreviousID => CurrentID - 1;
    private int NextID => CurrentID + 1;

    #endregion

    #region Unity Methods

    protected void Awake()
    {
        PreviousButton.OnClick.AddListener(ShowPrevious);
        MenuButton.OnClick.AddListener(ShowMenu);
        NextButton.OnClick.AddListener(ShowNext);
    }

    protected void OnDisable()
    {
        PreviousButton.OnClick.RemoveAllListeners();
        MenuButton.OnClick.RemoveAllListeners();
        NextButton.OnClick.RemoveAllListeners();
    }

    private void Start()
    {
        ToggleButtons();
        TogglePoints();

        LocationMaterial.SetTexture("_MainTex", SelectedLocation.EnvironmentPhoto);
        LocationMaterial.SetFloat("_Rotation", SelectedLocation.Angle);

        if (!string.IsNullOrWhiteSpace(SelectedLocation.EnvironmentVideo))
        {
            Player360.url = SelectedLocation.EnvironmentVideo;
            Player360.Play();
            Player360.prepareCompleted += (arg) =>
                LocationMaterial.SetTexture("_MainTex", LocationTexture);
        }
    }

    #endregion

    #region Public Methods

    public void ShowNext()
    {
        Data.Ref.SelectedLocation = Data.Ref.Locations[NextID];
        SceneManager.LoadSceneAsync("Location Scene");
    }

    public void ShowMenu()
    {
        Data.Ref.SelectedLocation = null;
        SceneManager.LoadSceneAsync("Main Scene");
    }

    public void ShowPrevious()
    {
        Data.Ref.SelectedLocation = Data.Ref.Locations[PreviousID];
        SceneManager.LoadSceneAsync("Location Scene");
    }

    public void ToggleButtons()
    {
        PreviousButton.IsActive = PreviousID != -1;
        NextButton.IsActive = NextID != Data.Ref.Locations.Count;
        Layout.UpdateLayout().Forget();
    }

    public void TogglePoints()
    {
        Points.ForEach(p => p.SetActive(p.name == $"[Points] {SelectedLocation.Code}"));
    }

    #endregion
}
