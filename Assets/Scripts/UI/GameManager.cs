using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    // �̱���
    public static GameManager Instance { get; private set; }

    private Character _character;

    // ������Ƽ, get�� ����
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
