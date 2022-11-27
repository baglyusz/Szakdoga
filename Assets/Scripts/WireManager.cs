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
    private TextMeshProUGUI text;

    private string _feedback;


    private void Start()
    {
        _wireList.AddRange(GetComponentsInChildren<Wire>());
        _mustCut = _wireList.FindAll(x => x.ToBeCut).Count;
    }

    private void Update()
    {
        if (_gameEnded)
        {
            canvas.gameObject.SetActive(true);

            text.SetText(_feedback);
            return;
        }

        if (_wireList.FindAll(x => x.Kaboom && x.BecomeCut).Count > 0)
        {
            _feedback = "ohno";
            _gameEnded = true;
        }
        else if (_wireList.FindAll(x => x.ToBeCut && x.BecomeCut).Count >= _mustCut)
        {
            _feedback = "nice";
            _gameEnded = true;
        }
    }
}