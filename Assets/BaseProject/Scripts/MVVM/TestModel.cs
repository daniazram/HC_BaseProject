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

    private void Awake()
    {
        //StartCoroutine(TestRoutine());
    }

    private IEnumerator TestRoutine()
    {
        var wait = new WaitForSeconds(0.25f);

        while(gameObject.activeInHierarchy)
        {
            LevelNumber = Random.Range(0, 1001).ToString();
            yield return wait;
        }
    }
}