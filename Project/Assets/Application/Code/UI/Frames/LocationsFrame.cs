using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityExtensions.UI;
using V2 = UnityEngine.Vector2;

public class LocationsFrame : ViewComponent<LocationsFrame>
{
    #region Fields

    [Header("Core Elements")]
    public Element Frame;
    public Element DownPanel;

    [Header("Elements")]
    public Element Title;
    public Element Cards;
    public Element Button;

    [Header("Buttons")]
    public ButtonElement BackButton;

    [Header("Parameters")]
    public float ToggleDuration;

    [Header("Cards")]
    public GameObject Template;
    public Transform Parent;

    private bool _filled = false;

    #endregion

    #region Unity Methods

    protected void Awake()
    {
        BackButton.OnClick.AddListener(ShowMain);
    }

    protected void OnDisable()
    {
        BackButton.OnClick.RemoveAllListeners();
    }

    #endregion

    #region Public Methods

    public async void ShowFrame()
    {
        FillCard();

        await UniTask.WhenAll(
            Frame.Change(new V2(1696, 1030), null, ToggleDuration),
            Title.Change(new V2(346, 96), new V2(0, -40), ToggleDuration),
            Cards.Change(new V2(1632, 672), new V2(0, 12), ToggleDuration),
            Button.Change(new V2(160, 96), new V2(0, 80), ToggleDuration),
            DownPanel.Change(new V2(1696, 64), new V2(0, 0), ToggleDuration)
        );
    }

    public async void HideFrame()
    {
        await UniTask.WhenAll(
            Title.Change(new V2(346, 96), new V2(0, 2000), ToggleDuration),
            Cards.Change(new V2(1632, 672), new V2(2000, 12), ToggleDuration),
            Button.Change(new V2(160, 96), new V2(0, -2000), ToggleDuration)
        );
    }

    public void ShowMain()
    {
        HideFrame();
        MainFrame.Ref.ShowFrame();
    }

    #endregion

    #region Private Methods

    private void FillCard()
    {
        if (_filled)
        {
            return;
        }

        foreach (LocationConfig config in Data.Ref.Locations)
        {
            Card card = Instantiate(Template, Parent).GetComponent<Card>();
            card.Set(config);
            card.OnClick += HandleCardClick;
        }

        _filled = true;
    }

    private void HandleCardClick(LocationConfig config)
    {
        Data.Ref.SelectedLocation = config;
        SceneManager.LoadSceneAsync("Location Scene");
    }

    #endregion
}
