using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

[CustomEditor(typeof(Chronometer)), CanEditMultipleObjects]
public class ChronometerInspector : Editor 
{
	private Chronometer chronometer;
	private SerializedProperty maxSecond;

	void OnEnable() 
    {
		maxSecond = serializedObject.FindProperty("maxSecond");
		chronometer = (Chronometer)target;
	}

	public override void OnInspectorGUI() 
    {
		serializedObject.Update();

		EditorGUILayout.Space();

        chronometer.type = (ChronometerType)EditorGUILayout.EnumPopup("Type", chronometer.type);

        GUI.backgroundColor = Color.white;
        {
            if (chronometer.type == ChronometerType.CountDown)
            {
                EditorGUILayout.PropertyField(maxSecond, new GUIContent("Max Time (seconds)"));
                EditorGUILayout.Space();
                ProgressBar(chronometer.maxSecond > 0.0 ? (float)(chronometer.TotalSeconds / chronometer.maxSecond) : 0.0f, "Time " + (int)chronometer.TotalSeconds + " seconds");       
            }
            else
            {
                EditorGUILayout.LabelField("Total Time (seconds)", chronometer.TotalSeconds.ToString());
            }
        }

		EditorGUILayout.BeginHorizontal();
        if (chronometer.IsStopped)
        {
            GUI.backgroundColor = Color.green;
            {
                if (GUILayout.Button("Start"))
                {
                    chronometer.StartTime();
                }
            }
        }
        else
        {
            GUI.backgroundColor = Color.red;
            {
                if (GUILayout.Button("Stop"))
                {
                    chronometer.StopTime();
                }
            }
        }

		if (chronometer.IsPaused){
			GUI.backgroundColor = Color.green;
			{
				if (GUILayout.Button("Resume")){
					chronometer.ResumeTime();
				}
			}
		} 
		else  {
			GUI.backgroundColor = Color.blue;
			{
				if (GUILayout.Button("Pause")){
					chronometer.PauseTime();
				}
			}
		}

		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties ();

        this.Repaint();
	}
	
	void ProgressBar (float value, string label){
		Rect rect = GUILayoutUtility.GetRect(18.0f, 18.0f, "TextField");
		EditorGUI.ProgressBar(rect, value <= 0 ? 0 : value, label);
		EditorGUILayout.Space();
	}	
}
