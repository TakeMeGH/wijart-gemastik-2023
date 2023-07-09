using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMobil : MonoBehaviour
{
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;
    private int _myLayerMask = 1 << 6;
    const float RECAPTURETIME = 0.75f;
    float curRecaptureTime;
    [SerializeField] UIFlash cameraUI;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraUI == null || !cameraUI.isActiveAndEnabled){
            GameObject temp = GameObject.FindGameObjectWithTag("CameraUI");
            if(temp != null){
                cameraUI = temp.GetComponent<UIFlash>();
            }
            if(cameraUI == null || !cameraUI.isActiveAndEnabled){
                Cursor.visible = true;
            }
        }
        Vector2 mousePos = Input.mousePosition;
        curRecaptureTime -= Time.deltaTime;
        if (curRecaptureTime < 0 && Input.GetMouseButtonDown(0)){
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _myLayerMask))
            {
                if (_hit.transform.gameObject.tag == "mobil")
                {
                    _hit.transform.gameObject.GetComponent<MobilFlash>().clicked();
                    cameraUI.takePicture();
                    curRecaptureTime = RECAPTURETIME;
                }
            }
        }
    }
}
