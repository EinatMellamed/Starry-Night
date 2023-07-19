using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndPlace : MonoBehaviour
{
    private bool isDragging = false;
    public bool isInPlace = false;


    private Vector3 offset;
    [SerializeField] GameObject starInPlaceEffect;
    [SerializeField] float pauseStar;

   
    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // Check if the star is close to a "star place" object
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Adjust the threshold (0.5f) as needed
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StarPlace"))
                {
                    // Snap the star into position
                    transform.position = collider.transform.position;

                    // Stop the movement for 10 seconds (you can adjust the duration as needed)
                    StartCoroutine(PauseMovement(pauseStar));
                    isInPlace= true;

                    break;
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private IEnumerator PauseMovement(float duration)
    {


        isDragging = false;
        this.GetComponent<StarMovement>().farwordSpeed = 0f;
        GameObject inPlaceEffect = Instantiate(starInPlaceEffect, transform.position,Quaternion.identity);
        Destroy(inPlaceEffect, 2);
        yield return new WaitForSeconds(duration);
        isInPlace= false;
        this.GetComponent<StarMovement>().farwordSpeed = 2f;
    }
   

}
