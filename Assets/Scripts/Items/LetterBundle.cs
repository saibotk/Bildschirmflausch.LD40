using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBundle : Item {
	public readonly List<Letter> letters;

	public LetterBundle (List<Letter> l) : base ("Letter Bundle", Resources.Load<Sprite>("letter")) {
		this.letters = l;
	}

	public LetterBundle () : this(new List<Letter> ()){
	}

	public bool Contains(Letter l) {
		return (letters.Find(x => x.job == l.job) != null) ? true : false;
	}

	public bool Remove(Letter l) {
		Letter f = letters.Find (x => x.job == l.job);
		if(f != null) {
			letters.Remove (f);
			return true;
		}
		return false;
	}

	public void Add(Letter l) {
		letters.Add (l);
	}
}
