using UnityEngine;using EthiopiaLegends.UI;
namespace EthiopiaLegends.Interaction {
 public class PlayerInteractor:MonoBehaviour {
  public float range=3f;public PrototypeHUD hud;WorldInteractable current;
  void Update(){current=null;float best=range;foreach(var candidate in FindObjectsByType<WorldInteractable>(FindObjectsSortMode.None)){
   float d=Vector3.Distance(transform.position,candidate.transform.position);if(d<best){best=d;current=candidate;}}
   if(hud)hud.SetInteraction(current?current.Prompt:"");
#if UNITY_EDITOR
   if(Input.GetKeyDown(KeyCode.E))Interact();
#endif
  }
  public void Interact(){if(current)current.Interact();}
 }
}