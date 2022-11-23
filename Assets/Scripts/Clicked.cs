using UnityEngine;

public class Clicked : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out var hit))
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interact();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interact();
            }
        }
    }
}

