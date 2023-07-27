using System.Collections;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;



public class DragAndPlace : MonoBehaviour
{
    [SerializeField] GameObject starInPlaceEffect;
    [SerializeField] float pauseStar;
    [SerializeField] ParticleSystem firstTouchEffect;
    [SerializeField] ScoreCounter scoreCounter;
    
    [SerializeField] GameObject trail;
    [SerializeField] float trailDuration;
    [SerializeField] Ease ease;
    public Transform scorePos;

    public bool isInPlace = false;
    private bool isDragging = false;
    [SerializeField] private bool hasTouched = false;



    private Vector3 offset;

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
       
    }

    private void OnMouseDown()
    {
        if(!hasTouched)
        {
            firstTouchEffect.Play();
            hasTouched = true;
        }
           
         

        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }
   
    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StarPlace"))
                {
                    transform.position = collider.transform.position;
                    StartCoroutine(CreateTrail());
                  //  scoreCounter.AddScore();
                    StartCoroutine(PauseMovement(pauseStar));
                    
                    isInPlace = true;

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
        GameManager.Instance.PlaySFX("StarInPlace");
        GameObject inPlaceEffect = Instantiate(starInPlaceEffect, transform.position, Quaternion.identity);
        Destroy(inPlaceEffect, 2);
        yield return new WaitForSeconds(duration);
        isInPlace = false;
        this.GetComponent<StarMovement>().farwordSpeed = 2f;
    }
    private IEnumerator CreateTrail()
    {
        GameObject myTrail = Instantiate(trail, transform.position, Quaternion.identity);

         myTrail.transform.DOMove(scorePos.position, trailDuration).SetEase(ease);
        yield return new WaitForSeconds(trailDuration);
        scoreCounter.AddScore();

        Destroy(myTrail, 6);
        yield return null;

    }

   
}
