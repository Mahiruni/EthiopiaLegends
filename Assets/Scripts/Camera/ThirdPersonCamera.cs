using UnityEngine;
namespace EthiopiaLegends.CameraSystem {
 public class ThirdPersonCamera:MonoBehaviour {
  public Transform target;public float distance=6f,height=2f,sensitivity=0.18f;
  float yaw=20f,pitch=18f;
  public void Rotate(Vector2 delta){yaw+=delta.x*sensitivity;pitch=Mathf.Clamp(pitch-delta.y*sensitivity,-10f,65f);}
  void LateUpdate(){if(!target)return;Quaternion r=Quaternion.Euler(pitch,yaw,0);Vector3 focus=target.position+Vector3.up*height;transform.position=focus-r*Vector3.forward*distance;transform.rotation=r;}
 }
}