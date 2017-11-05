using UnityEngine;
using System.Collections;

namespace BaconFunctions{

	public static class Bacon {

		public static GameObject GetGameObjectInPos(Vector3 targetPos, string tagToCheck) { 
			
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagToCheck); 
			
			foreach(GameObject obj in gameObjects) { 
				if(obj.transform.position == targetPos) 
					return obj; 
			} 
			
			return null; 
		}
		
		public static GameObject GetGameObjectInPosByName(Vector3 targetPos, string tagToCheck, string name) { 
			
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagToCheck); 
			
			foreach(GameObject obj in gameObjects) { 
				if(obj.name == name && obj.transform.position == targetPos) 
					return obj; 
			} 
			
			return null; 
		}

		public static bool IsGameObjectInPos(Vector3 targetPos, string tagToCheck) { 
			
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagToCheck); 
			
			foreach(GameObject obj in gameObjects) { 
				if(obj.transform.position == targetPos) 
					return true; 
			} 
			
			return false; 
		}
		
		public static void SnapGameObjectToGrid (GameObject obj, Vector3 pos, float gridSize){
			
			Vector3 snappedPos = pos;
			snappedPos.x = Mathf.Round(snappedPos.x) * gridSize;
			snappedPos.y = Mathf.Round(snappedPos.y) * gridSize;
			obj.transform.position = snappedPos;
			Debug.Log (snappedPos);
		}

	}
}
