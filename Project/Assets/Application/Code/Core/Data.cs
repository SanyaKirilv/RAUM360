using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityExtensions;

public class Data : ReferenceBehaviour<Data>
{
    #region Fiels

    [Header("Locations")]
    public List<LocationConfig> Locations = new List<LocationConfig>();
    public LocationConfig SelectedLocation;

    #endregion

    #region Public Methods

    public LocationConfig GetByName(string name) => Locations.FirstOrDefault(L => L.Name == name);

    #endregion
}
