using UnityEngine;
using UnityEngine.EventSystems;
using EthiopiaLegends.Player;
namespace EthiopiaLegends.UI {
 public class VirtualJoystick:MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler {
  [SerializeField] RectTransform background,handle;
  [SerializeField] PlayerMotor player;
  Vector2 input;
  public void OnPointerDown(PointerEventData e)=>OnDrag(e);
  public void OnDrag(PointerEventData e){
   if(!background)return;
   if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(background,e.position,e.pressEventCamera,out var point))return;
   Vector2 half=background.rect.size*.5f;
   input=Vector2.ClampMagnitude(new Vector2(half.x>0?point.x/half.x:0,half.y>0?point.y/half.y:0),1f);
   if(handle)handle.anchoredPosition=new Vector2(input.x*half.x*.6f,input.y*half.y*.6f);
   if(player)player.SetMoveInput(input);
  }
  public void OnPointerUp(PointerEventData e){input=Vector2.zero;if(handle)handle.anchoredPosition=Vector2.zero;if(player)player.SetMoveInput(Vector2.zero);}
  void OnDisable(){if(player)player.SetMoveInput(Vector2.zero);}
 }
}