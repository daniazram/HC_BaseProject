using UnityEngine;

public class TestModel : ViewModelBase
{
    [SerializeField]
    private string levelNumber;

    [Bindable]
    public string LevelNumber
    {
        get => levelNumber;
        set => Set(ref levelNumber, value);
    }
}