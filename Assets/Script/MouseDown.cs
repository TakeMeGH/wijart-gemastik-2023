using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
  private Camera _mainCamera;


  private Ray _ray;
  private RaycastHit _hit;
  private int _myLayerMask = 1 << 6;

  public RaycastHit[] hits;
  Coroutine boxDebug;

  private void Start()
  {
    _mainCamera = Camera.main;
  }


  private void Update()
  {
    Vector3 boxSize = new Vector3(5, 5, 0);
    Vector3 offset = _ray.direction * 10;
    Vector2 mousePos = Input.mousePosition;
    Vector3 originPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    // Debug.Log(originPos);
    // Debug.Log((originPos - boxSize + offset) + " + " + (originPos - boxSize + new Vector3(boxSize.x * 2, 0, 0) + offset));
    // Debug.DrawLine(originPos - boxSize + offset, originPos - boxSize + new Vector3(boxSize.x * 2, 0, 0) + offset, Color.red);
    // Debug.DrawLine(originPos - boxSize + offset, originPos - boxSize + new Vector3(0, boxSize.y * 2, 0) + offset, Color.red);
    // Debug.DrawLine(originPos + boxSize + offset, originPos + boxSize - new Vector3(boxSize.x * 2, 0, 0) + offset, Color.red);
    // Debug.DrawLine(originPos + boxSize + offset, originPos + boxSize - new Vector3(0, boxSize.y * 2, 0) + offset, Color.red);

    if (Input.GetMouseButtonDown(0))
    {
      // _ray = new Ray(
      //     _mainCamera.ScreenToWorldPoint(Input.mousePosition),
      //     _mainCamera.transform.forward);
      //     Debug.Log(_mainCamera.transform.forward);
      // or:
      _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
      // boxDebug = StartCoroutine(debugBoxCast());
      Debug.Log(_mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _mainCamera.nearClipPlane)));
      if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _myLayerMask))
      {
        if (_hit.transform.gameObject.tag == "mobil")
        {
          Debug.Log("Click");
          _hit.transform.GetComponent<Renderer>().material.color =
              _hit.transform.GetComponent<Renderer>().material.color == Color.red ? Color.blue : Color.red;
        }
      }
      // foreach (RaycastHit obj in hits)
      // {
      //   if (obj.transform.gameObject.tag == "mobil")
      //   {
      //     Debug.Log("Click");
      //     obj.transform.GetComponent<Renderer>().material.color =
      //         obj.transform.GetComponent<Renderer>().material.color == Color.red ? Color.blue : Color.red;
      //   }
      //   // Destroy(obj.transform.gameObject);
      // }

      //   for ()
      //     if (_hit.transform == transform)
      //     {
      //       Debug.Log("Click");
      //       _renderer.material.color =
      //           _renderer.material.color == Color.red ? Color.blue : Color.red;
      //     }
      // if (Physics.RaycastAll(_ray, out _hit, Mathf.Infinity, _myLayerMask))
      // {
      //     Debug.Log(_hit.transform);
      //     if (_hit.transform == transform)
      //     {
      //         Debug.Log("Click");
      //         _renderer.material.color =
      //             _renderer.material.color == Color.red ? Color.blue : Color.red;
      //     }
      // }
    }
  }

  IEnumerator debugBoxCast()
  {
    Vector3 originPos = _ray.origin;
    Vector3 dir = _ray.direction;

    for (int i = 0; i < 10; i++)
    {
      Vector3 boxSize = new Vector3(5, 5, 0);
      Vector3 offset = new Vector3(0, 0, 10);
      originPos += dir;
      Debug.Log(originPos);
      float curtime = 5f;
      while (curtime > 0)
      {
        Debug.DrawLine(originPos - boxSize / 2 + offset, originPos + boxSize / 2 + offset, Color.red);
        curtime -= 0.1f;
        yield return new WaitForSeconds(0.1f);

      }
    }

    boxDebug = null;
  }
}
