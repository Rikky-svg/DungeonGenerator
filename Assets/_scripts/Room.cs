using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header ("Doors")]
    public GameObject DoorUp;
    public GameObject DoorRight;
    public GameObject DoorDown;
    public GameObject DoorLeft;


    [Header ("Passages")]
    public GameObject PassageUp;
    public GameObject PassageRight;
    public GameObject PassageDown;
    public GameObject PassageLeft;

    public void OpenDoor(int num)
    {
        switch (num)
        {
            case 0:
            DoorUp.SetActive(false);
            PassageUp.SetActive(true);
            break;
            
            case 1:
            DoorRight.SetActive(false);
            PassageRight.SetActive(true);
            break;
            
            case 2:
            DoorDown.SetActive(false);
            PassageDown.SetActive(true);
            break;
            
            case 3:
            DoorLeft.SetActive(false);
            PassageLeft.SetActive(true);
            break;
        }
    }

    public void CloseDoor(int num)
    {
        switch (num)
        {
            case 0:
            DoorUp.SetActive(true);
            PassageUp.SetActive(false);
            break;
            
            case 1:
            DoorRight.SetActive(true);
            PassageRight.SetActive(false);
            break;
            
            case 2:
            DoorDown.SetActive(true);
            PassageDown.SetActive(false);
            break;
            
            case 3:
            DoorLeft.SetActive(true);
            PassageLeft.SetActive(false);
            break;
        }
    }
}
