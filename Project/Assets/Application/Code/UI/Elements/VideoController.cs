using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityExtensions.UI;
using V2 = UnityEngine.Vector2;

public class VideoController : Point
{
    #region Fields

    [Header("VideoPlayer")]
    public RawImage RawImage;
    public VideoPlayer VideoPlayer;
    public ButtonElement ButtonPlay;
    public ButtonElement ButtonPause;

    #endregion

    #region Unity Methods

    protected override void Start()
    {
        base.Start();
        ButtonPlay.OnClick.AddListener(Play);
        ButtonPause.OnClick.AddListener(Pause);
        VideoPlayer.url = Data.Ref.SelectedLocation.AddictionalVideo;
    }

    #endregion

    #region Public Methods

    public override void Toggle(string name)
    {
        bool state = this.name == name;

        RawImage.gameObject.SetActive(state);
        ButtonClose.IsActive = state;
        ButtonPlay.IsActive = state;
        ButtonPause.IsActive = state;

        if (state)
        {
            Frame.Change(new V2(1312, 864), new V2(0, 0), 0.25f).Forget();
            Play();
        }
        else
        {
            Frame.Change(new V2(128, 128), new V2(0, 0), 0.125f).Forget();
            Close();
        }
    }

    public void Play()
    {
        Location.Ref.Player360.Pause();
        VideoPlayer.Play();
        ButtonPlay.IsActive = false;
        ButtonPause.IsActive = true;
    }

    public void Pause()
    {
        VideoPlayer.Pause();
        ButtonPlay.IsActive = true;
        ButtonPause.IsActive = false;
    }

    public void Close()
    {
        Location.Ref.Player360.Play();
        VideoPlayer.Stop();
        ButtonPlay.IsActive = false;
        ButtonPause.IsActive = false;
    }

    #endregion
}
