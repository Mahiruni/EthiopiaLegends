using System;using System.IO;using UnityEngine;using EthiopiaLegends.Quests;
namespace EthiopiaLegends.Save {
 public class LocalSave:MonoBehaviour {
  [Serializable] private class SaveData {public int version=1;public int questStage;public int clues;}
  [SerializeField] QuestTracker quest;
  string SavePath=>Path.Combine(Application.persistentDataPath,"ethiopia_legends_save_v1.json");
  public void Save(){if(!quest)return;var data=new SaveData{questStage=(int)quest.Stage,clues=quest.CluesFound};string temp=SavePath+".tmp";File.WriteAllText(temp,JsonUtility.ToJson(data));if(File.Exists(SavePath))File.Delete(SavePath);File.Move(temp,SavePath);}
  public void Load(){if(!quest||!File.Exists(SavePath))return;try{var data=JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath));if(data!=null&&data.version==1)quest.Restore(data.questStage,data.clues);}catch(Exception e){Debug.LogWarning("Save load failed: "+e.Message);}}
  void OnApplicationPause(bool paused){if(paused)Save();}
  void OnApplicationQuit()=>Save();
 }
}