using UnityEngine;
using System.Collections;

public class SoundPack : ScriptableObject 
{
	public float longestClipLength;
	public AudioClip[] clips;
	public string[] keys;
	public bool[] loops;
}
