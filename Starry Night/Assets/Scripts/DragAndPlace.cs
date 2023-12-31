using System.Collections;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;



public class DragAndPlace : MonoBehaviour
{
    public bool firstStarInPlace;
    [SerializeField] GameObject starInPlaceEffect;
    [SerializeField] float pauseStar;
    [SerializeField] ParticleSystem firstTouchEffect;
   public Animator starAnim;

    [SerializeField] GameObject trail;
    [SerializeField] float trailDuration2;
    [SerializeField] Ease ease;
    [SerializeField] GameObject scorePos;




    

    public bool isInPlace = false;
    private bool isDragging = false;
  [SerializeField] bool hasTouched = false;



    private Vector3 offset;

    private void Start()
    {
        scorePos = GameObject.FindGameObjectWithTag("ScorePos");
    }

    private void OnMouseDown()
    {
        if(!hasTouched)
        {
            firstTouchEffect.Play();
            starAnim.SetTrigger("firstTouch");
            hasTouched = true;
          
        }

        starAnim.SetBool("inTouchMode", true);

        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }
   
    private void OnMouseUp()
    {
       
        starAnim.SetBool("inTouchMode", false);
        if (isDragging)
        {
            isDragging = false;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StarPlace"))
                {
                    firstStarInPlace = true;
                    transform.position = collider.transform.position;

                      StartCoroutine(CreateTrail());

                      StartCoroutine(GameManager.Instance.AddScoreWithEffects());
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

    public IEnumerator CreateTrail()
    {
        Time.timeScale = 1f;
        GameObject myTrail = Instantiate(trail, transform.position, Quaternion.identity);
      
        myTrail.transform.DOMove(scorePos.transform.position, trailDuration2).SetEase(ease);
        
        Destroy(myTrail, 6);
        yield return null;

    }


   
}
