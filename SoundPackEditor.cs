using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

[CustomEditor(typeof(SoundPack))]
public class SoundPackEditor : Editor {

	public override void OnInspectorGUI ()
	{
		SerializedProperty clips = serializedObject.FindProperty("clips");
		SerializedProperty length = serializedObject.FindProperty("longestClipLength");
		SerializedProperty keys = serializedObject.FindProperty("keys");
		SerializedProperty loops = serializedObject.FindProperty("loops");
		int count = clips.arraySize;
		EditorGUILayout.BeginHorizontal();

		Rect rect = EditorGUILayout.GetControlRect(GUILayout.MinWidth(32));
		if(GUILayout.Button("Clear"))
		{
			count = 0;
			clips.arraySize = 0;
			keys.arraySize = 0;
			loops.arraySize = 0;
		}

		if(GUILayout.Button("Add"))
		{
			++count;
			float delay = 0;
			for(int i = 0; i < clips.arraySize; ++i)
			{
				AudioClip clip = clips.GetArrayElementAtIndex(i).objectReferenceValue as AudioClip;
				if(clip)
				{
				delay = Mathf.Max(delay,clip.length);
				}
			}
			length.floatValue = delay;
		}
		if(GUILayout.Button("Remove"))
		{
			--count;
			float delay = 0;
			for(int i = 0; i < clips.arraySize; ++i)
			{
				AudioClip clip = clips.GetArrayElementAtIndex(i).objectReferenceValue as AudioClip;
				if(clip)
				{
					delay = Mathf.Max(delay,clip.length);
				}
			}
			length.floatValue = delay;
		}
		count = Mathf.Max(0,count);
		EditorGUILayout.EndHorizontal();
		if(count != clips.arraySize)
		{
			clips.arraySize = count;
			keys.arraySize = count;
			loops.arraySize = count;
		}

		for(int i = 0; i < clips.arraySize; ++i)
		{
			EditorGUILayout.BeginHorizontal();

			SerializedProperty clipProp = clips.GetArrayElementAtIndex(i);
			SerializedProperty key = keys.GetArrayElementAtIndex(i);
			SerializedProperty loop = loops.GetArrayElementAtIndex(i);
			
			
			rect = EditorGUILayout.GetControlRect(GUILayout.MinWidth(48));
			EditorGUI.LabelField(rect,"Key");
			key.stringValue = EditorGUILayout.TextField(key.stringValue);

			if(GUILayout.Button("Up"))
			{
				MoveUp(i);
			}
			else if(GUILayout.Button("Down"))
			{
				MoveDown(i);
			}
			else if(GUILayout.Button("Remove"))
			{
				Remove(i);
			}
			EditorGUILayout.EndHorizontal();
			if(i < clips.arraySize)
				{



				EditorGUILayout.BeginHorizontal();
				++EditorGUI.indentLevel;
				rect = EditorGUILayout.GetControlRect(GUILayout.MinWidth(48));
				EditorGUI.LabelField(rect,"Clip");
				AudioClip clip = EditorGUILayout.ObjectField(clipProp.objectReferenceValue,typeof(AudioClip),false) as AudioClip;
				if(clip != clipProp.objectReferenceValue)
				{
					if(clip)
					{
						length.floatValue = Mathf.Max(length.floatValue,clip.length);
					}
					clipProp.objectReferenceValue = clip;
				}
				rect = EditorGUILayout.GetControlRect(GUILayout.MinWidth(48));
				EditorGUI.LabelField(rect,"Loop");
				loop.boolValue = EditorGUILayout.Toggle(loop.boolValue);
				
				--EditorGUI.indentLevel;
				EditorGUILayout.EndHorizontal();
			}
		}
		serializedObject.ApplyModifiedProperties();
    }

	void MoveUp(int index)
	{
		SerializedProperty clips = serializedObject.FindProperty("clips");
		SerializedProperty keys = serializedObject.FindProperty("keys");
		SerializedProperty loops = serializedObject.FindProperty("loops");

		if(index > 0)
		{
			clips.MoveArrayElement(index,index-1);
			keys.MoveArrayElement(index,index-1);
			loops.MoveArrayElement(index,index-1);
		}
	}
	void MoveDown(int index)
	{
		SerializedProperty clips = serializedObject.FindProperty("clips");
		SerializedProperty keys = serializedObject.FindProperty("keys");
		SerializedProperty loops = serializedObject.FindProperty("loops");
		
		if(index < clips.arraySize-1)
		{
			clips.MoveArrayElement(index,index+1);
			keys.MoveArrayElement(index,index+1);
			loops.MoveArrayElement(index,index+1);
		}
	}
	void Remove(int index)
	{
		SerializedProperty clips = serializedObject.FindProperty("clips");
		SerializedProperty keys = serializedObject.FindProperty("keys");
		SerializedProperty loops = serializedObject.FindProperty("loops");
		SerializedProperty length = serializedObject.FindProperty("longestClipLength");
		for(int i = index,c = clips.arraySize-1; i < c; ++i)
		{
			MoveDown(i);
		}
		int count = clips.arraySize;
		--count;
		float delay = 0;
		for(int i = 0; i < clips.arraySize; ++i)
		{
			AudioClip clip = clips.GetArrayElementAtIndex(i).objectReferenceValue as AudioClip;
			if(clip)
			{
				delay = Mathf.Max(delay,clip.length);
			}
		}
		length.floatValue = delay;

		count = Mathf.Max(0,count);
		EditorGUILayout.EndHorizontal();
		if(count != clips.arraySize)
		{
			clips.arraySize = count;
			keys.arraySize = count;
			loops.arraySize = count;
		}
	}

	[MenuItem("Assets/Create/SoundPack")]
	static void CreateSoundPack()
	{
		var asset = ScriptableObject.CreateInstance<SoundPack>();
		var path = AssetDatabase.GetAssetPath(Selection.activeObject);
		
		if (string.IsNullOrEmpty(path))
			path = "Assets";
		else if (Path.GetExtension(path) != "")
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		else
			path += "/";
		
		var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "SoundPack.asset");
		AssetDatabase.CreateAsset(asset, assetPathAndName);
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}
