using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EthiopiaLegends.Player;
using EthiopiaLegends.CameraSystem;
using EthiopiaLegends.Quests;
using EthiopiaLegends.Interaction;
using EthiopiaLegends.Combat;
using EthiopiaLegends.UI;
using EthiopiaLegends.Save;
namespace EthiopiaLegends.Core {
 public class PrototypeBootstrap : MonoBehaviour {
  static Font font;
  static GameObject Box(string name,Vector3 pos,Vector3 scale,Color color,PrimitiveType primitive=PrimitiveType.Cube){
   var go=GameObject.CreatePrimitive(primitive);go.name=name;go.transform.position=pos;go.transform.localScale=scale;
   var renderer=go.GetComponent<Renderer>();renderer.material.color=color;return go;
  }
  public static void Setup(){
   var ground=Box("Axum courtyard",Vector3.zero,new Vector3(12,1,12),new Color(.62f,.48f,.32f),PrimitiveType.Plane);
   var player=Box("Explorer",new Vector3(0,1,0),Vector3.one,new Color(.15f,.32f,.5f),PrimitiveType.Capsule);
   DestroyImmediate(player.GetComponent<CapsuleCollider>());var controller=player.AddComponent<CharacterController>();
   var motor=player.AddComponent<PlayerMotor>();var interactor=player.AddComponent<PlayerInteractor>();var combat=player.AddComponent<PrototypeCombat>();
   var cameraObject=new GameObject("Third Person Camera");cameraObject.tag="MainCamera";var camera=cameraObject.AddComponent<Camera>();cameraObject.AddComponent<AudioListener>();
   var rig=cameraObject.AddComponent<ThirdPersonCamera>();rig.target=player.transform;
   var motorField=typeof(PlayerMotor).GetField("cameraTransform",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic);
   motorField.SetValue(motor,cameraObject.transform);
   var quest=new GameObject("Quest Tracker").AddComponent<QuestTracker>();
   var save=new GameObject("Local Save").AddComponent<LocalSave>();
   typeof(LocalSave).GetField("quest",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).SetValue(save,quest);
   Spawn("Royal Archivist",new Vector3(4,1,4),new Vector3(1,2,1),new Color(.18f,.6f,.45f),WorldInteractable.Kind.Archivist,quest);
   for(int i=0;i<3;i++)Spawn("Clue "+(i+1),new Vector3(-6+i*5,.5f,7),Vector3.one,new Color(.95f,.75f,.2f),WorldInteractable.Kind.Clue,quest);
   Spawn("Royal Seal",new Vector3(0,.5f,18),Vector3.one,new Color(.9f,.5f,.12f),WorldInteractable.Kind.Seal,quest);
   for(int i=0;i<8;i++)Box("Stela "+i,new Vector3(-14+i*4,2,24),new Vector3(1.3f,4,1.3f),new Color(.45f,.43f,.4f));
   var enemy=Box("Raider",new Vector3(4,1,15),Vector3.one,new Color(.6f,.16f,.16f),PrimitiveType.Capsule).AddComponent<PrototypeEnemy>();enemy.player=player.transform;
   var sun=new GameObject("Sun").AddComponent<Light>();sun.type=LightType.Directional;sun.transform.rotation=Quaternion.Euler(45,-35,0);
   if(!Object.FindFirstObjectByType<EventSystem>()){var events=new GameObject("EventSystem");events.AddComponent<EventSystem>();events.AddComponent<StandaloneInputModule>();}
   var canvasObject=new GameObject("Touch HUD");var canvas=canvasObject.AddComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceOverlay;canvasObject.AddComponent<CanvasScaler>().uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;canvasObject.GetComponent<CanvasScaler>().referenceResolution=new Vector2(1920,1080);canvasObject.AddComponent<GraphicRaycaster>();
   var hud=canvasObject.AddComponent<PrototypeHUD>();hud.quest=quest;interactor.hud=hud;
   hud.missionText=Label(canvasObject.transform,"Mission",new Vector2(0,1),new Vector2(20,-20),new Vector2(900,80),TextAnchor.UpperLeft);
   hud.interactionText=Label(canvasObject.transform,"Interaction",new Vector2(.5f,0),new Vector2(-350,120),new Vector2(700,60),TextAnchor.MiddleCenter);
   var joystickBg=Panel(canvasObject.transform,"Joystick",new Vector2(0,0),new Vector2(170,170),new Vector2(220,220),new Color(.15f,.2f,.3f,.45f));
   var handle=Panel(joystickBg,"Handle",new Vector2(.5f,.5f),Vector2.zero,new Vector2(85,85),new Color(.9f,.9f,.9f,.8f));
   var stick=joystickBg.gameObject.AddComponent<VirtualJoystick>();
   SetPrivate(stick,"background",joystickBg);SetPrivate(stick,"handle",handle);SetPrivate(stick,"player",motor);
   var touchArea=Panel(canvasObject.transform,"Camera touch area",new Vector2(.72f,.5f),Vector2.zero,new Vector2(850,780),new Color(0,0,0,0));
   touchArea.gameObject.AddComponent<CameraTouchArea>().cameraRig=rig;
   Button(canvasObject.transform,"Interact",new Vector2(1,0),new Vector2(-175,300),interactor.Interact);
   Button(canvasObject.transform,"Attack",new Vector2(1,0),new Vector2(-175,170),combat.Attack);
   Button(canvasObject.transform,"Save",new Vector2(1,1),new Vector2(-110,-60),save.Save);
   Button(canvasObject.transform,"Load",new Vector2(1,1),new Vector2(-300,-60),save.Load);
  }
  static void SetPrivate(object target,string name,object value)=>target.GetType().GetField(name,System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).SetValue(target,value);
  static void Spawn(string name,Vector3 position,Vector3 scale,Color color,WorldInteractable.Kind kind,QuestTracker quest){
   var go=Box(name,position,scale,color);var item=go.AddComponent<WorldInteractable>();item.kind=kind;item.quest=quest;
  }
  static RectTransform Panel(Transform parent,string name,Vector2 anchor,Vector2 position,Vector2 size,Color color){
   var go=new GameObject(name,typeof(RectTransform),typeof(Image));go.transform.SetParent(parent,false);
   var rect=go.GetComponent<RectTransform>();rect.anchorMin=anchor;rect.anchorMax=anchor;rect.anchoredPosition=position;rect.sizeDelta=size;go.GetComponent<Image>().color=color;return rect;
  }
  static Text Label(Transform parent,string name,Vector2 anchor,Vector2 position,Vector2 size,TextAnchor align){
   var go=new GameObject(name,typeof(RectTransform),typeof(Text));go.transform.SetParent(parent,false);var rect=go.GetComponent<RectTransform>();rect.anchorMin=anchor;rect.anchorMax=anchor;rect.anchoredPosition=position;rect.sizeDelta=size;
   var text=go.GetComponent<Text>();text.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");text.fontSize=35;text.color=Color.white;text.alignment=align;return text;
  }
  static void Button(Transform parent,string label,Vector2 anchor,Vector2 position,UnityEngine.Events.UnityAction callback){
   var rect=Panel(parent,label,anchor,position,new Vector2(180,100),new Color(.13f,.23f,.35f,.9f));var button=rect.gameObject.AddComponent<Button>();button.onClick.AddListener(callback);
   var text=Label(rect,label+" Text",new Vector2(.5f,.5f),Vector2.zero,new Vector2(170,90),TextAnchor.MiddleCenter);text.text=label;
  }
 }
}
