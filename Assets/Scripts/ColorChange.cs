using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private readonly Color[] _colors = { Color.white, Color.red, Color.green, Color.blue };
    private int _currentColor, length;

    private void Start()
    {
        _currentColor = 0; //White
        length = _colors.Length;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                _currentColor = (_currentColor + 1) % length;
                GetComponent<Renderer>().material.color = _colors[_currentColor];
            }
        }
    }
}
