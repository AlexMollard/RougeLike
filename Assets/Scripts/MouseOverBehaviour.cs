using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseOverBehaviour : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 10, 0);
    static TextMeshProUGUI text;
    static GameObject backGround;
    static float timer;

    private static MouseOverBehaviour _Instance = null;
    private static readonly object padlock = new object();

    MouseOverBehaviour()
    {
    }

    public static MouseOverBehaviour Instance
    {
        get
        {
            lock (padlock)
            {
                if (_Instance == null)
                {
                    _Instance = new MouseOverBehaviour();
                }
                return _Instance;
            }
        }
    }

    private void Start()
    {
        backGround = gameObject;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        transform.position = Input.mousePosition + offset;

        if (timer > 0.1f)
        {
            SetActive(false);
        }
    }

    static void SetActive(bool status)
    {
        backGround.SetActive(status);
    }

    public static void SetText(string name)
    {
        text.text = name;
        timer = 0.0f;
        SetActive(true);
    }

}
