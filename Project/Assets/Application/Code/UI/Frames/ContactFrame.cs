using System.Collections.Generic;
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
    public Element Cards;
    public Element Buttons;

    [Header("Buttons")]
    public LayoutComponent Layout;
    public ButtonElement BackButton;
    public ButtonElement PreviousFrameButton;
    public ButtonElement NextFrameButton;

    [Header("Parameters")]
    public float ToggleDuration;

    [Header("Frames")]
    public List<GameObject> Frames = new List<GameObject>();

    private int _selectedFrame = 0;

    #endregion

    #region Unity Methods

    protected void Awake()
    {
        BackButton.OnClick.AddListener(ShowMain);
        PreviousFrameButton.OnClick.AddListener(ShowPreviousFrame);
        NextFrameButton.OnClick.AddListener(ShowNextFrame);
    }

    protected void OnDisable()
    {
        BackButton.OnClick.RemoveAllListeners();
        PreviousFrameButton.OnClick.RemoveAllListeners();
        NextFrameButton.OnClick.RemoveAllListeners();
    }

    #endregion

    #region Public Methods

    public async void ShowFrame()
    {
        ToggleFrames();

        await UniTask.WhenAll(
            Frame.Change(new V2(1432, 904), null, ToggleDuration),
            Title.Change(new V2(376, 96), new V2(0, -40), ToggleDuration),
            Cards.Change(new V2(1368, 544), new V2(0, 12), ToggleDuration),
            Buttons.Change(new V2(512, 96), new V2(0, 80), ToggleDuration),
            DownPanel.Change(new V2(1432, 64), new V2(0, 0), ToggleDuration)
        );
    }

    public async void HideFrame()
    {
        _selectedFrame = 0;

        await UniTask.WhenAll(
            Title.Change(new V2(376, 96), new V2(0, 2000), ToggleDuration),
            Cards.Change(new V2(1368, 544), new V2(2000, 12), ToggleDuration),
            Buttons.Change(new V2(512, 96), new V2(0, -2000), ToggleDuration)
        );
    }

    public void ShowMain()
    {
        HideFrame();
        MainFrame.Ref.ShowFrame();
    }

    public void ShowPreviousFrame()
    {
        if (_selectedFrame == 0)
        {
            return;
        }

        _selectedFrame--;
        ToggleFrames();
    }

    public void ShowNextFrame()
    {
        if (_selectedFrame == Frames.Count - 1)
        {
            return;
        }

        _selectedFrame++;
        ToggleFrames();
    }

    public void ToggleFrames()
    {
        for (int i = 0; i < Frames.Count; i++)
        {
            Frames[i].SetActive(i == _selectedFrame);
        }

        PreviousFrameButton.IsActive = _selectedFrame != 0;
        NextFrameButton.IsActive = _selectedFrame != Frames.Count - 1;
    }

    #endregion
}
