#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using EthiopiaLegends.Player;
using EthiopiaLegends.CameraSystem;
using EthiopiaLegends.Quests;
using EthiopiaLegends.Interaction;
using EthiopiaLegends.Combat;
namespace EthiopiaLegends.EditorTools {
 public static class PrototypeSceneBuilder {
  [MenuItem("Ethiopia Legends/Create Prototype Scene")]
  public static void Create(){
   var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
   var ground=GameObject.CreatePrimitive(PrimitiveType.Plane);ground.name="Axum Prototype Ground";ground.transform.localScale=new Vector3(12,1,12);
   var player=GameObject.CreatePrimitive(PrimitiveType.Capsule);player.name="Explorer";player.transform.position=new Vector3(0,1,0);
   Object.DestroyImmediate(player.GetComponent<CapsuleCollider>());
   player.AddComponent<CharacterController>();player.AddComponent<PlayerMotor>();player.AddComponent<PlayerInteractor>();player.AddComponent<PrototypeCombat>();
   var camera=new GameObject("Third Person Camera");camera.tag="MainCamera";camera.AddComponent<Camera>();camera.AddComponent<AudioListener>();
   var rig=camera.AddComponent<ThirdPersonCamera>();rig.target=player.transform;
   var quest=new GameObject("Quest Tracker").AddComponent<QuestTracker>();
   Spawn("Royal Archivist",new Vector3(3,1,4),WorldInteractable.Kind.Archivist,quest);
   for(int i=0;i<3;i++)Spawn("Clue "+(i+1),new Vector3(-4+i*3,0.5f,8),WorldInteractable.Kind.Clue,quest);
   Spawn("Royal Seal",new Vector3(0,0.5f,16),WorldInteractable.Kind.Seal,quest);
   for(int i=0;i<8;i++){var p=GameObject.CreatePrimitive(PrimitiveType.Cube);p.name="Stone Stela "+i;p.transform.position=new Vector3(-15+i*4,2,23);p.transform.localScale=new Vector3(1.3f,4,1.3f);}
   var light=new GameObject("Sun").AddComponent<Light>();light.type=LightType.Directional;light.transform.rotation=Quaternion.Euler(45,-30,0);
   System.IO.Directory.CreateDirectory("Assets/Scenes");
   EditorSceneManager.SaveScene(scene,"Assets/Scenes/AxumPrototype.unity");
   Debug.Log("Prototype scene created. Wire camera, joystick, UI, and build settings before Android build.");
  }
  static void Spawn(string name,Vector3 position,WorldInteractable.Kind kind,QuestTracker quest){
   var go=GameObject.CreatePrimitive(PrimitiveType.Cube);go.name=name;go.transform.position=position;
   var item=go.AddComponent<WorldInteractable>();item.kind=kind;item.quest=quest;
  }
 }
}
#endif