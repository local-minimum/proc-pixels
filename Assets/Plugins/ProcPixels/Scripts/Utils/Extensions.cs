using UnityEngine;
using System.Reflection;

public static class Extensions {

	public static void SendMessageForEditor(this MonoBehaviour monoBehaviour, string message, SendMessageOptions option, params object[] arguments) {
		var behaviours = monoBehaviour.GetComponentsInChildren<MonoBehaviour> ();
		bool success = option == SendMessageOptions.DontRequireReceiver;
		var argumentTypes = System.Type.GetTypeArray (arguments);
		for (int i = 0; i < behaviours.Length; i++) {
			var meth = behaviours [i].GetType ().GetMethod (
				message, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (meth != null) {
				meth.Invoke ((Object)behaviours [i], arguments);
				success = true;
			}					
		}
		if (!success)
			throw new System.ArgumentException ("No object found with function: " + message);			
	}

}
