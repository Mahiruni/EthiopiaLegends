#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
namespace EthiopiaLegends.EditorTools {
 public static class AndroidBuild {
  public static void Build() {
   const string scenePath="Assets/Scenes/AxumPrototype.unity";
   if(!File.Exists(scenePath))throw new Exception("Missing prototype scene. Run Ethiopia Legends > Create Prototype Scene in the Unity Editor and commit the generated scene.");
   EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
   PlayerSettings.productName="Ethiopia Legends";
   PlayerSettings.companyName="Hisab Technologies";
   PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android,"com.hisabtechnologies.ethiopialegends");
   PlayerSettings.Android.targetArchitectures=AndroidArchitecture.ARM64;
   EditorUserBuildSettings.buildAppBundle=false;
   Directory.CreateDirectory("Builds/Android");
   var options=new BuildPlayerOptions{scenes=new[]{scenePath},locationPathName="Builds/Android/EthiopiaLegends-dev.apk",target=BuildTarget.Android,options=BuildOptions.Development|BuildOptions.AllowDebugging};
   var report=BuildPipeline.BuildPlayer(options);
   if(report.summary.result!=BuildResult.Succeeded)throw new Exception("Android build failed: "+report.summary.result);
   Debug.Log("Android APK build succeeded: "+report.summary.outputPath);
  }
 }
}
#endif
