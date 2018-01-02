using System.Collections;
using System.Collections.Generic;
using System;

public static class Hook {
	private static Dictionary<string, List<Delegate>> hooks = new Dictionary<string, List<Delegate>> ();

	public static void CreateHook(string name) {
		if(!hooks.ContainsKey(name))
			hooks.Add (name, new List<Delegate> ());
	}

	public static void AddHook(string name, Delegate func) {
		if (hooks.ContainsKey (name)) {
			hooks [name].Add (func);
		} else {
			List<Delegate> dl = new List<Delegate> ();
			dl.Add (func);
			hooks.Add (name, dl);
		}
	}

	// TODO what about return types e.g. using hooks to check if an action should trigger
	// otherwise just use predefined delegates without return types to improve performance, so no DynamicInvoke is needed, Invoke is much faster.
	public static void CallHook( string name ) {
		if (hooks.ContainsKey (name)) {
			List<Delegate> tmp = hooks [name];
			foreach (Delegate d in tmp) {
				if(d != null) 
					d.DynamicInvoke();
			}
		}
	}
}

