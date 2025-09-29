using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityExtensions.UI;

public class Element : ViewComponent
{
    #region Fields

    public UIConfig Config;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Change(Config).Forget();
    }

    #endregion

    #region Public Methods

    public async UniTask Change(
        Vector2? size = null,
        Vector2? position = null,
        float? duration = null
    )
    {
        await Change(new UIConfig(size, position, duration));
    }

    public async UniTask Change(UIConfig config)
    {
        await UniTask.WhenAll(
            SizeLerp(config.Size, config.Duration),
            MoveLerp(config.Position, true, config.Duration)
        );
    }

    #endregion
}
