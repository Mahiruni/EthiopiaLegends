#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;
using UnityEngine;
using EthiopiaLegends.Core;
namespace EthiopiaLegends.EditorTools {
 public static class BuildAndGenerate {
  const string ScenePath="Assets/Scenes/AxumPrototype.unity";
  [MenuItem("Ethiopia Legends/Generate Wired Prototype")]
  public static void Generate(){
   var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
   PrototypeBootstrap.Setup();
   Directory.CreateDirectory("Assets/Scenes");
   EditorSceneManager.SaveScene(scene,ScenePath);
   AssetDatabase.Refresh();
   Debug.Log("Generated wired prototype scene: "+ScenePath);
  }
  public static void BuildAndroid(){
   Generate();
   EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
   PlayerSettings.companyName="Hisab Technologies";
   PlayerSettings.productName="Ethiopia Legends";
   PlayerSettings.SetApplicationIdentifier(UnityEditor.Build.NamedBuildTarget.Android,"com.hisabtechnologies.ethiopialegends");
   PlayerSettings.Android.targetArchitectures=AndroidArchitecture.ARM64;
   EditorUserBuildSettings.buildAppBundle=false;
   Directory.CreateDirectory("Builds/Android");
   var options=new BuildPlayerOptions{
    scenes=new[]{ScenePath},
    locationPathName="Builds/Android/EthiopiaLegends-dev.apk",
    target=BuildTarget.Android,
    options=BuildOptions.Development|BuildOptions.AllowDebugging
   };
   var report=BuildPipeline.BuildPlayer(options);
   if(report.summary.result!=BuildResult.Succeeded)throw new Exception("Build failed: "+report.summary.result);
  }
 }
}
#endif
