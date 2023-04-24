using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    [SerializeField] float mouseSensetivity = 1f;
    float xAxis,yAxis;
    [SerializeField] Transform cameraFollowPosition;
    [HideInInspector] public Animator anim;

    public AimBaseState currentState;
    public RifleHipState RifleHipState = new RifleHipState();
    public RifleAimingState RifleAimingState = new RifleAimingState();
    //FOV Variables when ads
    [HideInInspector] public CinemachineVirtualCamera VirtualCamera;
    public float adsFOV = 40f;
    [HideInInspector] public float hipFOV = 60f;
    [HideInInspector] public float currentFOV;
    public float FOVSmoothSpeed =10f;

    public Transform aimPosition;
    [SerializeField] float aimSmoothSpeed =20;
    [SerializeField] LayerMask aimMask;
    [HideInInspector] public Vector3 actualAimPosition;

    // Start is called before the first frame update
    void Start()
    {
        VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV = VirtualCamera.m_Lens.FieldOfView;
        anim = GetComponentInChildren<Animator>();
        SwitchState(RifleHipState);
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensetivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensetivity;
        yAxis = Mathf.Clamp(yAxis, -80f, 80f);

        VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(VirtualCamera.m_Lens.FieldOfView, currentFOV, FOVSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask)) 
        {
            aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            actualAimPosition = hit.point;
        }


        currentState.UpdateState(this);



        

    }
    private void LateUpdate()
    {
        cameraFollowPosition.localEulerAngles = new Vector3(yAxis, cameraFollowPosition.localEulerAngles.y, cameraFollowPosition.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState newState) 
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
