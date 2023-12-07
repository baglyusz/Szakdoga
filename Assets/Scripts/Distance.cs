using System;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public GameObject key;
    public GameObject unlock;
    public GameObject book;
    private bool _unlocked;

    private void Start()
    {
        canvas.gameObject.SetActive(false);
        book.SetActive(false);
        key.SetActive(true);
        unlock.SetActive(true);
    }

    private void Update()
    {
        var distance = Math.Abs(Vector3.Distance(key.transform.position, unlock.transform.position));

        if (!(distance < 0.05) || _unlocked) return;
        _unlocked = true;

        book.SetActive(true);
        canvas.gameObject.SetActive(true);
        key.SetActive(false);
        unlock.SetActive(false);
    }
}

