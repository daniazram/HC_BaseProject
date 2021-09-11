using UnityEngine;
using System.Collections;

public class TestModel : ViewModelBase
{
    [SerializeField, FieldToProperty("LevelNumber")]
    private string levelNumber;

    [Bindable]
    public string LevelNumber
    {
        get => levelNumber;
        set => Set(ref levelNumber, value);
    }
}