using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Controller behavior")] 
    
    //Input variables
    [SerializeField] private Swipe SwipeInput;
    [SerializeField] private Vector3 DesiredPosition;
    
    
    //Current state check variables
    public bool isGrounded = false;
    
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isStopped = false;
    [SerializeField] private bool onLift = false;
    [SerializeField] private bool isFalling = false;
    [SerializeField] private bool isPorted = false;
    
    
    //Other attributes
    public float playerSpeed = 5;
    private Ray PlayerRay, RayDown;
    public int bloodFill;
    public Text bloodFillText;
    public float[] DummyRadius;
    public int MovesCounter;

    
    [Header("Color settings")]
    public Color FilledColor;

    
    
    
    [Header("Particles")]
    public GameObject BloodSplat;

    public GameObject SmokeHit;
    public GameObject BloodHit;

    public GameObject BloodTrail;
    public GameObject SmokeTrail;


    private void Start()
    {
        MovesCounter = 0;
    }

    private void FixedUpdate()
    {
        if (isStopped)
        {
            if (SwipeInput.SwipeLeft)
                DesiredPosition=Vector3.left;
            if (SwipeInput.SwipeRight)
                DesiredPosition=Vector3.right;
            if (SwipeInput.SwipeUp)
                DesiredPosition=Vector3.forward;
            if (SwipeInput.SwipeDown)
                DesiredPosition=Vector3.back;
        }
        
        

        if (DesiredPosition != Vector3.zero && !isMoving)
        {
            isStopped = false;
            isMoving = true;
        }

        
        if (isMoving)
        {
            PlayerRay = new Ray(transform.position, DesiredPosition.normalized);
            RaycastHit hit;
            if (Physics.Raycast(PlayerRay, out hit, 0.5f))
            {
                if (hit.collider.CompareTag("Dummy"))
                {
                    DummyHit(hit.collider.gameObject, hit.collider.GetComponent<DummyController>().DummyType);
//
//                    if (hit.collider.GetComponent<DummyController>().DummyType == DummyType.A)
//                    {
//                        
//                    }
//
//                    Collider[] hitColliders = Physics.OverlapSphere(hit.collider.transform.position, 2.0f);
//                    foreach (var hitCollider in hitColliders)
//                    {
////                        hitCollider.SendMessage("AddDamage");
//                        if(hitCollider.gameObject.layer == 8)
//                            Debug.Log("Object name: " + hitCollider.gameObject.name);
//                    }
//                    
////                    hit.collider.gameObject.SetActive(false);
//                    hit.collider.enabled = false;
////                    hit.collider.gameObject.transform.SetParent(this.transform);
////                    hit.collider.gameObject.transform.position = new Vector3(0,-2.0f,0);
//                    hit.collider.GetComponent<Animator>().SetTrigger("Death");
//                    hit.collider.GetComponent<DummyController>().Death();
//                    GameObject temp = Instantiate(BloodSplat, hit.collider.transform.position + Vector3.up, hit.collider.transform.rotation);
//                    temp.transform.SetParent(hit.collider.transform);
//                    temp.SetActive(true);
//                    updateBloodFillCount(5);
                }
                else if (hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Lift"))
                {
                    if (!isFalling)
                    {
                        isStopped = true;
                        isMoving = false;

                        DesiredPosition = DesiredPosition / 2;
                        
                        if (bloodFill > 0)
                        {
                            Instantiate(BloodHit, this.transform.position + DesiredPosition, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(SmokeHit, this.transform.position + DesiredPosition, Quaternion.identity);
                        }

                        GameManager.instance.SoundController.SwishSound();
                        MovesCounter += 1;
                        DesiredPosition = Vector3.zero;
                    }
                } 
            }
            else
            {
                if (!onLift && isGrounded && !isFalling)
                {
                    PlayerRay = new Ray(transform.position, Vector3.down.normalized);
                    RaycastHit hitDown;
                    if (Physics.Raycast(PlayerRay, out hitDown, 0.5f))
                    {
                        if (hitDown.collider.CompareTag("Filled"))
                        {
                            if (isPorted)
                            {
                                isPorted = false;
                            }
                            SmokeTrail.SetActive(false);
                            BloodTrail.SetActive(true);
                        }
                        else 
                        if (hitDown.collider.CompareTag("Ground"))
                        {
                            
                            if (isPorted)
                            {
                                isPorted = false;
                            }
                            if (bloodFill > 0)
                            {
                                SmokeTrail.SetActive(false);
                                BloodTrail.SetActive(true);
                                hitDown.collider.gameObject.GetComponent<MeshRenderer>().material.color = FilledColor;
                                hitDown.collider.transform.GetChild(0).gameObject.SetActive(true);
//                                GameManager.instance.SoundController.FillSound();
                                hitDown.collider.tag = "Filled";
                                hitDown.collider.GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
                                hitDown.transform.localScale = Vector3.one;
                                updateBloodFillCount(-1);
//                            bloodFill -= 1;
                                GameManager.instance.TotalFilled += 1;
                                if (GameManager.instance.TotalFilled >= GameManager.instance.TotalTiles)
                                {
                                    GameManager.instance.LevelComplete();
                                }
                            }
                            else
                            {
                                BloodTrail.SetActive(false);
                                SmokeTrail.SetActive(true);
                            }
                        }
                        else if (hitDown.collider.CompareTag("Lift"))
                        {
                            
                            onLift = true;
                            isGrounded = false;
                            isStopped = true;
                            isMoving = false;
                            DesiredPosition = Vector3.zero;
                            var tempPosition = hitDown.collider.transform.position;
                            transform.position = new Vector3(tempPosition.x, tempPosition.y+1, tempPosition.z);
                            this.transform.SetParent(hitDown.collider.transform);
                        }
                        else if (hitDown.collider.CompareTag("Portal"))
                        {
                            if (!isPorted)
                            {
                                isPorted = true;
                                isGrounded = false;
                                isStopped = true;
                                isMoving = false;
                                DesiredPosition = Vector3.zero;
                                var tempPosition = hitDown.collider.transform.position;
                                transform.position = new Vector3(tempPosition.x, tempPosition.y+1, tempPosition.z);
                                SwipeInput.enabled = false;
                                transform.DOScale(0, 0.5f).OnComplete(
                                    delegate
                                    {
                                        var newPosition = hitDown.collider.GetComponent<PortalController>().OtherPortal.transform.position;
//                                    transform.position = new Vector3(newPosition.x, newPosition.y+2, newPosition.z);
                                        transform.position = hitDown.collider.GetComponent<PortalController>().OtherPortal
                                            .transform.position;
                                        transform.position += new Vector3(0,0.9f,0);
                                        transform.DOScale(0.95f, 0.5f).OnComplete(
                                            delegate
                                            {
                                                SwipeInput.enabled = true;
                                                isGrounded = true;
//                                            isPorted = false;
                                            });
                                    });
                            }
                            
//                            this.transform.SetParent(hitDown.collider.transform);
                        }
//                        else if (!onGrounded)
//                        {
////                            onLift = false;
//                            isStopped = true;
//                            isMoving = false;
//                            DesiredPosition = Vector3.zero;
//                        }
//                        else
//                        {
//                            onLift = false;
//                            isStopped = true;
//                            isMoving = false;
//                            DesiredPosition = Vector3.zero;
//                        }

                    }
                }
                else if (onLift && !isGrounded)
                {
//                    Debug.Log("Came to the right place");
//                    transform.position += Vector3.down * Time.deltaTime * playerSpeed;
                    var PlayerRay = new Ray(transform.position, Vector3.down.normalized);
                    RaycastHit hitDown;
                    if (Physics.Raycast(PlayerRay, out hitDown, 2.0f))
                    {
                        if (hitDown.collider.CompareTag("Ground") || hitDown.collider.CompareTag("Filled"))
                        {
                            isFalling = true;
                            onLift = false;
                        }
                    }
                }else if (!onLift && isFalling)
                {
                    PlayerRay = new Ray(transform.position, Vector3.down.normalized);
                    RaycastHit hitDown;
                    if (Physics.Raycast(PlayerRay, out hitDown, 0.5f))
                    {
                        if (hitDown.collider.CompareTag("Filled"))
                        {
                            isFalling = false;
                            isGrounded = true;
                        }else if (hitDown.collider.CompareTag("Ground"))
                        {
                            if (bloodFill > 0)
                            {
                                hitDown.collider.gameObject.GetComponent<MeshRenderer>().material.color = FilledColor;
                                hitDown.collider.transform.GetChild(0).gameObject.SetActive(true);
//                                GameManager.instance.SoundController.FillSound();
                                hitDown.collider.tag = "Filled";
                                hitDown.collider.GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
                                hitDown.transform.localScale = Vector3.one;
                                updateBloodFillCount(-1);
//                            bloodFill -= 1;
                                GameManager.instance.TotalFilled += 1;
                                if (GameManager.instance.TotalFilled >= GameManager.instance.TotalTiles)
                                {
                                    GameManager.instance.LevelComplete();
                                }
                            }

                            isFalling = false;
                            isGrounded = true;

                        }else if (hitDown.collider.CompareTag("Walls"))
                        {
                            isFalling = false;
                            isGrounded = true;
                        }
                    }
                }
                else
                {
//                    Debug.Log("Came here");
//                    PlayerRay = new Ray(transform.position, Vector3.down.normalized);
//                    RaycastHit hitDown;
//                    if (Physics.Raycast(PlayerRay, out hitDown, 1.5f))
//                    {
//                        
//                        if(hitDown.collider.CompareTag("Lift"))
//                        {
////                            onLift = false;
//                        }
//                        else if (hitDown.collider.CompareTag("Ground") || hitDown.collider.CompareTag("Walls"))
//                        {
//                            onLift = false;
//                            //hitDown.collider.CompareTag("Ground") || 
//                            Debug.Log("Hit some ground");
////                            onLift = false;
////                            isMoving = false;
////                            DesiredPosition = Vector3.zero;
////                            transform.position += Vector3.down * Time.deltaTime * 5f;
//                        }
//                        else
//                        {
////                            onLift = false;
////                            Debug.Log("In Air");
////                            isStopped = true;
//                        }
//                    }
                }


//                transform.position += DesiredPosition * Time.deltaTime * playerSpeed;
                if (!isStopped)
                {
//                    Debug.Log("Released");
                    this.transform.SetParent(null);
                    if (isFalling)
                    {
                        transform.position += Vector3.down * Time.deltaTime * playerSpeed;
                    }
                    else
                    {
                        transform.position += DesiredPosition * Time.deltaTime * playerSpeed;
                    }

                    
//                    onLift = false;
                }

            }


//            myRigidbody.AddForce(DesiredPosition * 50, ForceMode.Impulse);
//            isMoving = false;
        }
        else
        {
//            if (!onLift && !isGrounded && isStopped)
//            {
//                Debug.Log("Came to the right place");
//                transform.position += Vector3.down * Time.deltaTime * playerSpeed;
//                var PlayerRay = new Ray(transform.position, Vector3.down.normalized);
//                RaycastHit hitDown;
//                if (Physics.Raycast(PlayerRay, out hitDown, 0.5f))
//                {
//                    if (hitDown.collider.CompareTag("Dummy"))
//                    {
//                        hitDown.transform.gameObject.SetActive(false);
//                        GetComponent<PlayerController>().updateBloodFillCount(4);
//                    }else if (hitDown.collider.CompareTag("Ground"))
//                    {
//                        hitDown.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
//                        hitDown.collider.tag = "Filled";
//                        GameManager.instance.TotalFilled += 1;
//                        if (GameManager.instance.TotalFilled >= GameManager.instance.TotalTiles)
//                        {
//                            GameManager.instance.LevelComplete();
//                        }
//                        isGrounded = true;
//                        onLift = false;
//                    }
//                }
//            }
        }






//        if (!isMoving && !isStopped)
//        {
//            if (myRigidbody.velocity == Vector3.zero)
//            {
//                isStopped = true;
//            }
//        }
//        this.transform.position = Vector3.MoveTowards(this.transform.position,
//            DesiredPosition, 3f * Time.deltaTime);


//        if (isMoving && myRigidbody.velocity == Vector3.zero)
//        {
//            DesiredPosition = Vector3.zero;
//            isMoving = false;
//        }




        if (SwipeInput.Tap)
        {
//            Debug.Log("Tap input!");
        }
    }


    public void updateBloodFillCount(int count)
    {
        bloodFill += count;
        if(bloodFillText)
            bloodFillText.text = bloodFill.ToString();
        if(GameManager.instance.BloodCount)
            GameManager.instance.BloodCount.text = bloodFill.ToString();
    }


    public void DummyHit(GameObject DummyObject, DummyType DummyClass)
    {
//        Debug.Log("Int dummy: " + (int)DummyClass);
        HitSplatTiles(DummyObject, DummyClass);
//        Collider[] hitColliders = Physics.OverlapSphere(DummyObject.transform.position, DummyRadius[(int)DummyClass]);
//        foreach (var hitCollider in hitColliders)
//        {
////                        hitCollider.SendMessage("AddDamage");
//            if(hitCollider.gameObject.layer == 8)
//                Debug.Log("Object name: " + hitCollider.gameObject.name);
//        }
                    
//                    hit.collider.gameObject.SetActive(false);
        DummyObject.GetComponent<Collider>().enabled = false;
//                    hit.collider.gameObject.transform.SetParent(this.transform);
//                    hit.collider.gameObject.transform.position = new Vector3(0,-2.0f,0);
//        DummyObject.GetComponent<Animator>().SetTrigger("Death");
        DummyObject.GetComponent<DummyController>().Death();
        GameObject temp = Instantiate(BloodSplat, DummyObject.transform.position + Vector3.up, 
            DummyObject.transform.rotation);
//        temp.transform.SetParent(DummyObject.transform);
        temp.SetActive(true);
        updateBloodFillCount(4);
    }

    public void HitSplatTiles(GameObject Dummy, DummyType DummyKind)
    {
        Collider[] hitColliders = Physics.OverlapSphere(Dummy.transform.position, DummyRadius[(int)DummyKind]);
        foreach (var hitCollider in hitColliders)
        {
//                        hitCollider.SendMessage("AddDamage");
            if (hitCollider.gameObject.layer == 8)
            {
                if (hitCollider.tag.Equals("Ground") || hitCollider.tag.Equals("DummyTile"))
                {
//                    Debug.Log("Cube name :" + hitCollider);
                    hitCollider.GetComponent<BoxCollider>().size = new Vector3(0.95f, 0.95f, 0.95f);
                    hitCollider.transform.localScale = Vector3.one;
                    hitCollider.gameObject.GetComponent<MeshRenderer>().material.color = FilledColor;
                    hitCollider.transform.GetChild(0).gameObject.SetActive(true);
//                    GameManager.instance.SoundController.FillSound();
                    hitCollider.tag = "Filled";
//                    updateBloodFillCount(-1);
//                            bloodFill -= 1;
                    GameManager.instance.TotalFilled += 1;
                    if (GameManager.instance.TotalFilled >= GameManager.instance.TotalTiles)
                    {
                        GameManager.instance.LevelComplete();
                    }
                }
            }
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {
//        if (other.gameObject.CompareTag("Walls"))
//        {
//            DesiredPosition = Vector3.zero;
//            isMoving = false;
//        }
    }
    
}
