using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityExtensions.UI;
using V2 = UnityEngine.Vector2;

public class ContactFrame : ViewComponent<ContactFrame>
{
    #region Fields

    [Header("Core Elements")]
    public Element Frame;
    public Element DownPanel;

    [Header("Elements")]
    public Element Title;
    public Element Button;

    [Header("Buttons")]
    public ButtonElement BackButton;

    [Header("Parameters")]
    public float ToggleDuration;

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
        await UniTask.WhenAll(
            Frame.Change(new V2(1280, 848), null, ToggleDuration),
            Title.Change(new V2(576, 128), new V2(0, -32), ToggleDuration),
            Button.Change(new V2(96, 160), new V2(16, 0), ToggleDuration),
            DownPanel.Change(new V2(1280, 64), new V2(0, 0), ToggleDuration)
        );
    }

    public async void HideFrame()
    {
        await UniTask.WhenAll(
            Title.Change(new V2(576, 128), new V2(0, 2000), ToggleDuration),
            Button.Change(new V2(96, 160), new V2(-2000, 0), ToggleDuration)
        );
    }

    public void ShowMain()
    {
        HideFrame();
        MainFrame.Ref.ShowFrame();
    }

    #endregion
}
