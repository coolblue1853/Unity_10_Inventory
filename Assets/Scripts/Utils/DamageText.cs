using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float floatSpeed = 20f;         // ���� �������� �ӵ�
    public float lifeTime = 1.5f;          // �� ���� ��������
    public Vector3 floatDirection = Vector3.up;  // �������� ����

    private float timer;
    private TextMeshProUGUI tmp;     

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(string text, Color color)
    {
        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = color;
        }

        timer = 0f;
    }

    void Update()
    {

        timer += Time.deltaTime;

        transform.position += floatDirection * floatSpeed * Time.deltaTime;

        if (tmp != null)
        {
            Color c = tmp.color;
            c.a = Mathf.Lerp(1f, 0f, timer / lifeTime);
            tmp.color = c;
        }

        if (timer >= lifeTime)
        {
            Destroy(gameObject); 
        }
    }
}