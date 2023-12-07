using UnityEngine;

public class Wire : MonoBehaviour, IInteractable
{
    [SerializeField]
    private bool _toBeCut;

    [SerializeField]
    private bool _dummy;

    [SerializeField]
    private bool _kaboom;

    public bool BecomeCut { get; set; }

    public bool ToBeCut { get => _toBeCut; set => _toBeCut = value; }
    public bool Dummy { get => _dummy; set => _dummy = value; }
    public bool Kaboom { get => _kaboom; set => _kaboom = value; }

    public void Start()
    {
        if (Kaboom)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (ToBeCut)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (Dummy)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void Interact()
    {
        BecomeCut = true;
        GetComponent<Renderer>().enabled = false;
    }
}