using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected UIManager _uIManager;
    protected virtual void Start()
    {
        _uIManager = UIManager.Instance;
    }
}
