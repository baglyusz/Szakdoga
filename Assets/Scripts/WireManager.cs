using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    private readonly List<Wire> _wireList = new();

    private int _mustCut;

    private bool _gameEnded;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject feedback;

    private void Start()
    {
        _wireList.AddRange(GetComponentsInChildren<Wire>());
        _mustCut = _wireList.FindAll(x => x.ToBeCut).Count;
    }

    private void Update()
    {
        if (_gameEnded) return;

        if (_wireList.FindAll(x => x.Kaboom && x.BecomeCut).Count > 0)
        {
            _gameEnded = true;
            canvas.gameObject.SetActive(true);
            canvas.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 0f, true);
            canvas.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1f, 2000f, false);
        }
        else if (_wireList.FindAll(x => x.ToBeCut && x.BecomeCut).Count >= _mustCut)
        {
            feedback.GetComponent<TMPro.TextMeshProUGUI>().text = "Pair 32!";
            feedback.SetActive(true);

            //SceneManager.LoadScene("");
        }
    }
}