using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    // 싱글턴
    public static GameManager Instance { get; private set; }

    private Character _character;

    // 프로퍼티, get만 지정
    public Character Character => _character;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
       // _character = new Character();
    }

}
