using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialBreaker : MonoBehaviour
{
    [SerializeField] float camTimerStart;
    [SerializeField] float camTimerEnd;
    [SerializeField] float breakerTimerBuffer;
    [SerializeField] bool VialCam1;
    [SerializeField] bool VialCam2;
    [SerializeField] bool BossCam;
    [SerializeField] bool fallingObjective;
    [SerializeField] bool jumpingObjective;
    [SerializeField] Transform locationBreaker;
    [SerializeField] Transform locationBridge;
    [SerializeField] GameObject breaker;
    [SerializeField] GameObject[] bridges;
    [SerializeField] GameObject vial;
    [SerializeField] GameObject text;
    AnimationScript PlayerAnim;
    CameraScript cameraScript;
    PlayerMovement pMove;

    private void Awake()
    {
        cameraScript = GameObject.Find("CM StateDrivenCamera1").GetComponent<CameraScript>();
        pMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PlayerAnim = GameObject.FindGameObjectWithTag("PlayerAnim").GetComponent<AnimationScript>();
    }

    private void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if(vial == null)
        {
            StartCoroutine(SwitchCameraBack());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            text.SetActive(true);
        }
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) || other.gameObject.CompareTag("Player") && Input.GetKey("joystick button 1"))
        {
            pMove.rb.velocity = Vector3.zero;
            PlayerAnim.SetToIdle();
            if (VialCam1)
            {
                StartCoroutine(SwitchCameraVial1());
            }
            else if(VialCam2)
            {
                StartCoroutine(SwitchCameraVial2());
            }
            else if(BossCam)
            {
                StartCoroutine(SwitchToBossCam());
            }
            pMove.canMove = false;
            Debug.Log(pMove.canMove);
            StartCoroutine(BreakerBuffer());
            text.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<EAudioManager>().Play("buttonPush");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }

    public void InstantiateBreaker()
    {
        Instantiate(breaker, locationBreaker.position, breaker.transform.rotation);
    }

    IEnumerator BreakerBuffer()
    {
        yield return new WaitForSeconds(breakerTimerBuffer);
        InstantiateBreaker();
    }

    IEnumerator SwitchCameraVial1()
    {
        yield return new WaitForSeconds(camTimerStart);
        cameraScript.SwitchToVialCam1();
    }

    IEnumerator SwitchToBossCam()
    {
        yield return new WaitForSeconds(camTimerStart);
        cameraScript.SwitchToBossCam();
    }

    IEnumerator SwitchCameraVial2()
    {
        if (fallingObjective)
        {
            cameraScript.SwitchToVialCam2_1();
        }
        else
        {
            cameraScript.SwitchToVialCam2_2();
        }
        yield return new WaitForSeconds(camTimerStart);
        cameraScript.SwitchToVialCam2();
        yield return new WaitForSeconds(1);
        //Instantiate(bridge, locationBridge.position, breaker.transform.rotation);
        foreach (GameObject bridge in bridges)
        {
            bridge.AddComponent<Rigidbody>();
            bridge.GetComponent<Rigidbody>().mass = 5000;
        }
    }

    IEnumerator SwitchCameraBack()
    {
        yield return new WaitForSeconds(camTimerEnd);
        if (fallingObjective && !pMove.aRPG)
        {
            cameraScript.SwitchTo3rdPerson();
        }
        else if (jumpingObjective && !pMove.aRPG)
        {
            cameraScript.SwitchToJumpObectiveCam();
        }
        else if(!pMove.aRPG)
        {
            cameraScript.SwitchTo3rdPerson();
        }
        else if (pMove.aRPG)
        {
            cameraScript.SwitchToARPG();
        }
        pMove.canMove = true;
        Destroy(gameObject);
    }
}
