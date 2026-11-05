using UnityEngine;using UnityEngine.EventSystems;
namespace EthiopiaLegends.CameraSystem {
 public class CameraTouchArea:MonoBehaviour,IDragHandler {
  public ThirdPersonCamera cameraRig;
  public void OnDrag(PointerEventData e){if(cameraRig)cameraRig.Rotate(e.delta);}
 }
}