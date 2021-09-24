using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject Portal;
    public GameObject Portal1;
    public bool Case=false;
    
   


    // Update is called once per frame
    public void ChangePlayerPosition()
    {
        Player.transform.position = Portal1.transform.position;
        /*if (Player.transform.position == Portal.transform.position && Case==false)
        {
            Debug.Log("fff");
            Player.SetActive(false);
            Player.transform.position = Portal1.transform.position;
            Player.SetActive(true);
        }*/

       
        if (Player.transform.position == Portal1.transform.position && Case == false)
        {
            Debug.Log("fff");
            Player.SetActive(false);
            Player.transform.position = Portal.transform.position;
            Player.SetActive(true);
        }
    }


}
