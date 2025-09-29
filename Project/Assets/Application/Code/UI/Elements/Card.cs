using System;
using UnityEngine;
using UnityExtensions.UI;

public class Card : MonoBehaviour
{
    #region Fields

    public event Action<LocationConfig> OnClick;

    public ImageElement Photo;
    public TextElement Title;
    public ButtonElement Button;

    #endregion

    #region Public Methods

    public void Set(LocationConfig config)
    {
        Photo.Image.sprite = config.PreviewPhoto;
        name = config.Name;
        Title.SetTextForce(config.Name);
        Button.OnClick.AddListener(() => OnClick(config));
    }

    #endregion
}
