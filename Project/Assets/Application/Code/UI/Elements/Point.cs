using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityExtensions.UI;
using V2 = UnityEngine.Vector2;

public class Point : MonoBehaviour
{
    #region Fields

    public static Action<string> OnClick;

    public Element Frame;
    public ImageElement Photo;
    public ButtonElement ButtonOpen;
    public ButtonElement ButtonClose;

    #endregion

    #region Unity Methods

    protected virtual void Start()
    {
        ButtonOpen.OnClick.AddListener(() => OnClick(name));
        ButtonClose.OnClick.AddListener(() => Toggle(""));
        Toggle("");
    }

    private void OnEnable() => OnClick += Toggle;

    private void OnDisable() => OnClick -= Toggle;

    #endregion

    #region Public Methods

    public virtual void Toggle(string name)
    {
        bool state = this.name == name;

        Photo.IsActive = state;
        ButtonClose.IsActive = state;

        if (state)
        {
            Frame.Change(new V2(672, 592), new V2(0, 0), 0.25f).Forget();
        }
        else
        {
            Frame.Change(new V2(128, 128), new V2(0, 0), 0.125f).Forget();
        }
    }

    #endregion
}
