using System;
using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

public class FloatingController : MonoBehaviour
{

    [Header("Controller behavior")] 
    public bool isActive = true;
    public bool isFalling = false;
    [SerializeField] private Drag DragInput;
    [SerializeField] private Swipe SwipeInput;
//    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private float m_Speed;
    public Vector2 xLimit, zLimit;
    private float speedmodifier;
    public float degreesPerSecond = 15.0f;
    // Position Storage Variables
    public Vector3 posOffset = new Vector3 ();
    public Vector3 tempPos = new Vector3 ();
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float moveSpeed = 3f;
    public GameObject[] DummyPlayers;
    public float requiredDistance;
    public float minimumDistanced, tempDistanced;
    public int selectedDummy = 0;
    public int highlightedDummy = 0;




    public float yPosition;
    public Vector3 cacheTouch;
    private Vector3 offset;


    private void Awake()
    {
//        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speedmodifier = 0.001f;
        DummyPlayers = new GameObject[GameObject.FindGameObjectsWithTag("Dummy").Length];
        DummyPlayers = GameObject.FindGameObjectsWithTag("Dummy");
        HighlightDummy();
    }

    private void Update()
    {

        HighlightDummy();
        if (Input.GetMouseButtonUp(0))
        {
            CaptureDummy();
        }

        if (isActive)
        {
//            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
            // Float up/down with a Sin()

            if (Input.GetMouseButtonDown(0))
            {
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (DragInput.Dragging)
            {
//                Vector3 currentTouch = Vector3.zero;

                Vector3 curScreenPoint = Input.mousePosition;
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                //new Vector3(-1f,0,3.5f)
                Vector3 tempPosition = new Vector3(curPosition.x, transform.position.y, curPosition.z);
                Vector3 clampedPosition = new Vector3(Mathf.Clamp(tempPosition.x, xLimit.x, xLimit.y), tempPosition.y, 
                    Mathf.Clamp(tempPosition.z, zLimit.x, zLimit.y));
                transform.position = clampedPosition;

//                Vector3 mouse = Input.mousePosition;
//                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
//                RaycastHit hit;
//                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, 8))
//                {
////                    Vector3 currentTouch = hit.point;
////                    Vector3 movementDelta = cacheTouch - currentTouch;
//                    this.transform.position = hit.point;
//                }
                //Drop raycast from current touch position
                //Store the place where the raycast touches
                //Vector3 movement = raycast touch - cache touch


//            Vector3 m_Input = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));


//                Vector3 tempPosition = new Vector3(
//                    transform.position.x + Input.GetAxis("Mouse X") * speedmodifier * m_Speed,
//                    transform.position.y,
//                    transform.position.z + Input.GetAxis("Mouse Y") * speedmodifier * m_Speed);
//      
////            Vector3 tempPosition = transform.position + m_Input * Time.deltaTime * m_Speed;
//                Vector3 clampedPosition = new Vector3(Mathf.Clamp(tempPosition.x, xLimit.x, xLimit.y), tempPosition.y, 
//                    Mathf.Clamp(tempPosition.z, zLimit.x, zLimit.y));
////            myRigidbody.MovePosition(clampedPosition);
//                transform.position = clampedPosition;

                //Store the current touch position in a cache variable
//                cacheTouch = currentTouch;
            }
            else
            {
                posOffset = new Vector3(transform.position.x, yPosition, transform.position.z);
                tempPos = posOffset;
                tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
                // Input.GetAxis("Horizontal")*
                this. transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);   
 
                transform.position = tempPos;
            }

            if (DragInput.Tap)
                Debug.Log("Tap input!");
        }
        
        if(isFalling)
        {
            isActive = false;
            DragInput.enabled = false;
            this.transform.position = new Vector3(DummyPlayers[selectedDummy].transform.position.x, 
                transform.position.y, DummyPlayers[selectedDummy].transform.position.z);
            this.transform.eulerAngles = Vector3.zero;
            transform.position += Vector3.down * Time.deltaTime * moveSpeed;
            var PlayerRay = new Ray(transform.position, Vector3.down.normalized);
            RaycastHit hit;
            if (Physics.Raycast(PlayerRay, out hit, 0.5f))
            {
                if (hit.collider.CompareTag("Dummy"))
                {
                    GetComponent<PlayerController>().DummyHit(hit.collider.gameObject, hit.collider.GetComponent<DummyController>().DummyType);
//                   hit.transform.gameObject.SetActive(false);
//                   hit.collider.enabled = false;
////                    hit.collider.gameObject.transform.SetParent(this.transform);
////                    hit.collider.gameObject.transform.position = new Vector3(0,-2.0f,0);
//                   hit.collider.GetComponent<Animator>().SetTrigger("Death");
//                   hit.collider.GetComponent<DummyController>().Death();
//                   GameObject temp = Instantiate(GetComponent<PlayerController>().BloodSplat, hit.collider.transform.position + Vector3.up, hit.collider.transform.rotation);
//                   temp.transform.SetParent(hit.collider.transform);
//                   temp.SetActive(true);
////                   Instantiate(GetComponent<PlayerController>().BloodSplat, hit.collider.transform.position, hit.collider.transform.rotation);
//                   GetComponent<PlayerController>().updateBloodFillCount(4);
                }else if (hit.collider.CompareTag("DummyTile") || hit.collider.CompareTag("Filled"))
                {
//                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
//                    hit.collider.tag = "Filled";
//                    GameManager.instance.TotalFilled += 1;
//                    if (GameManager.instance.TotalFilled >= GameManager.instance.TotalTiles)
//                    {
//                        GameManager.instance.LevelComplete();
//                    }
                    GetComponent<PlayerController>().enabled = true;
                    GetComponent<PlayerController>().isGrounded = true;
                    SwipeInput.enabled = true;
                    this.enabled = false;
                    isFalling = false;
                }
            }
        }
    }

    public void CaptureDummy()
    {
        for (int i = 0; i < DummyPlayers.Length; i++)
        {
            var tempDistance = Vector3.Distance(DummyPlayers[i].transform.position, this.transform.position);
//            Debug.Log("Distance: " + tempDistance);
            if (tempDistance <= requiredDistance)
            {
                if (tempDistance < minimumDistanced)
                {
                    minimumDistanced = tempDistance;
                    selectedDummy = i;
                }
                isFalling = true;
            }
        }
    }
    
    public void HighlightDummy()
    {
        for (int i = 0; i < DummyPlayers.Length; i++)
        {
            DummyPlayers[i].transform.GetChild(0).GetComponent<Outline>().eraseRenderer = true;
            var tempDistance = Vector3.Distance(DummyPlayers[i].transform.position, this.transform.position);
//            Debug.Log("Distance: " + tempDistance);
            if (tempDistance < tempDistanced)
            {
                tempDistanced = tempDistance;
                highlightedDummy = i;
            }
        }
        DummyPlayers[highlightedDummy].transform.GetChild(0).GetComponent<Outline>().eraseRenderer = false;
        tempDistanced = 1000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.CompareTag("Dummy"))
            {
                isActive = false;
                DragInput.enabled = false;
//                myRigidbody.constraints = RigidbodyConstraints.None;
//                myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                this.transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
                this.transform.eulerAngles = Vector3.zero;
//                myRigidbody.useGravity = true;
                isFalling = true;
//                GetComponent<PlayerController>().enabled = true;
//                SwipeInput.enabled = true;
//                this.enabled = false;
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dummy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
