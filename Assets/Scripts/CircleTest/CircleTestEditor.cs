using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Profiling;

 [CustomEditor(typeof(CircleTest))]
 // ^ This is the script we are making a custom editor for.
 public class CircleTestEditor : Editor {
     public override void OnInspectorGUI () {
         DrawDefaultInspector();
         
         if(GUILayout.Button("Start")) {
            ((CircleTest)this.target).start();
         }
    }
 }