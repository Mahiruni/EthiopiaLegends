using UnityEngine;using UnityEngine.UI;using EthiopiaLegends.Quests;
namespace EthiopiaLegends.UI {
 public class PrototypeHUD:MonoBehaviour {
  public QuestTracker quest;public Text missionText,interactionText;string prompt="";
  public void SetInteraction(string value){if(prompt==value)return;prompt=value;if(interactionText)interactionText.text=value;}
  void Update(){if(!quest||!missionText)return;
   switch(quest.Stage){
    case QuestTracker.QuestStage.NotStarted:missionText.text="Find the Royal Archivist";break;
    case QuestTracker.QuestStage.FindClues:missionText.text="Investigate clues: "+quest.CluesFound+"/3";break;
    case QuestTracker.QuestStage.RecoverSeal:missionText.text="Recover the royal seal";break;
    case QuestTracker.QuestStage.ReturnToArchivist:missionText.text="Return to the Archivist";break;
    default:missionText.text="MISSION COMPLETE — The Missing Royal Seal";break;
   }
  }
 }
}