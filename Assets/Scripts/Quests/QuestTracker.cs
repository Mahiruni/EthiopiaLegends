using System;
using UnityEngine;
namespace EthiopiaLegends.Quests {
 public class QuestTracker:MonoBehaviour {
  public enum QuestStage {NotStarted,FindClues,RecoverSeal,ReturnToArchivist,Completed}
  [SerializeField] QuestStage stage; [SerializeField] int cluesFound;
  public QuestStage Stage=>stage; public int CluesFound=>cluesFound;
  public event Action OnChanged;
  public void Begin(){if(stage!=QuestStage.NotStarted)return;stage=QuestStage.FindClues;OnChanged?.Invoke();}
  public void FindClue(){if(stage!=QuestStage.FindClues)return;cluesFound=Mathf.Clamp(cluesFound+1,0,3);if(cluesFound==3)stage=QuestStage.RecoverSeal;OnChanged?.Invoke();}
  public void RecoverSeal(){if(stage!=QuestStage.RecoverSeal)return;stage=QuestStage.ReturnToArchivist;OnChanged?.Invoke();}
  public void TurnIn(){if(stage!=QuestStage.ReturnToArchivist)return;stage=QuestStage.Completed;OnChanged?.Invoke();}
  public void Restore(int stageValue,int clueCount){stage=(QuestStage)Mathf.Clamp(stageValue,0,(int)QuestStage.Completed);cluesFound=Mathf.Clamp(clueCount,0,3);OnChanged?.Invoke();}
 }
}