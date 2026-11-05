using UnityEngine;using EthiopiaLegends.Quests;
namespace EthiopiaLegends.Interaction {
 public class WorldInteractable:MonoBehaviour {
  public enum Kind {Archivist,Clue,Seal} public Kind kind;public QuestTracker quest;public bool used;
  public string Prompt=>kind==Kind.Archivist?"Talk to Archivist":kind==Kind.Clue?"Investigate clue":"Recover royal seal";
  public void Interact(){if(!quest)return;
   if(kind==Kind.Archivist){if(quest.Stage==QuestTracker.QuestStage.NotStarted)quest.Begin();else if(quest.Stage==QuestTracker.QuestStage.ReturnToArchivist)quest.TurnIn();}
   else if(kind==Kind.Clue&&!used&&quest.Stage==QuestTracker.QuestStage.FindClues){quest.FindClue();used=true;gameObject.SetActive(false);}
   else if(kind==Kind.Seal&&quest.Stage==QuestTracker.QuestStage.RecoverSeal){quest.RecoverSeal();gameObject.SetActive(false);}
  }
 }
}