using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityExtensions.UI;
using V2 = UnityEngine.Vector2;

public class MainFrame : ViewComponent<MainFrame>
{
    #region Fields

    [Header("Core Elements")]
    public Element Frame;
    public Element TriangleLeft;
    public Element TriangleRight;
    public Element DownPanel;

    [Header("Elements")]
    public Element Logo;
    public Element HelloText;
    public Element Buttons;

    [Header("Buttons")]
    public ButtonElement LocationsButton;
    public ButtonElement ContactButton;

    [Header("Parameters")]
    public float ToggleDuration;

    #endregion

    #region Unity Methods

    protected void Awake()
    {
        LocationsButton.OnClick.AddListener(ShowLocations);
        ContactButton.OnClick.AddListener(ShowContact);
    }

    protected void OnDisable()
    {
        LocationsButton.OnClick.RemoveAllListeners();
        ContactButton.OnClick.RemoveAllListeners();
    }

    #endregion

    #region Public Methods

    public async void ShowFrame()
    {
        await UniTask.WhenAll(
            Frame.Change(new V2(1024, 720), null, ToggleDuration),
            Logo.Change(new V2(272, 272), new V2(0, -32), ToggleDuration),
            TriangleLeft.Change(new V2(192, 192), V2.zero, 1),
            TriangleRight.Change(new V2(192, 192), V2.zero, 1),
            Buttons.Change(new V2(512, 288), new V2(0, 96), ToggleDuration),
            DownPanel.Change(new V2(1024, 64), V2.zero, ToggleDuration)
        );
    }

    public async void HideFrame()
    {
        await UniTask.WhenAll(
            Logo.Change(new V2(272, 272), new V2(0, 2000), ToggleDuration),
            HelloText.Change(new V2(896, 128), new V2(-2000, 128), ToggleDuration),
            Buttons.Change(new V2(512, 272), new V2(-2000, 96), ToggleDuration)
        );
    }

    public async UniTask ShowSplash()
    {
        await StageOne();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        await StageTwo();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        await StageThree();

        async UniTask StageOne()
        {
            await UniTask.WhenAll(
                Frame.Change(new V2(576, 576), V2.zero, .75f),
                Logo.Change(new V2(512, 512), new V2(0, -32), .75f)
            );
        }

        async UniTask StageTwo()
        {
            await UniTask.WhenAll(
                Frame.Change(new V2(1024, 720), null, 1),
                Logo.Change(new V2(320, 320), new V2(0, -104), 1),
                TriangleLeft.Change(new V2(192, 192), V2.zero, 1),
                TriangleRight.Change(new V2(192, 192), V2.zero, 1),
                HelloText.Change(new V2(896, 128), new V2(0, 128), 1),
                DownPanel.Change(new V2(1024, 64), V2.zero, 1)
            );
        }

        async UniTask StageThree()
        {
            await UniTask.WhenAll(
                Logo.Change(new V2(272, 272), new V2(0, -32), 1),
                HelloText.Change(new V2(896, 128), new V2(-2000, 128), 1),
                Buttons.Change(new V2(512, 272), new V2(0, 96), 1)
            );
        }
    }

    public void ShowLocations()
    {
        HideFrame();
        LocationsFrame.Ref.ShowFrame();
    }

    public void ShowContact()
    {
        HideFrame();
        ContactFrame.Ref.ShowFrame();
    }

    #endregion
}
