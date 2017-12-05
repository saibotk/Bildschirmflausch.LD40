using System.Collections;
using System.Collections.Generic;

public class CoffeePot : Item {

	private float fillLevel;
	private float maxFillLevel = 6;

	public CoffeePot (float level) : base ("Coffee Pot", null) {
		this.fillLevel = level;
	}

	// Fills the Coffeecup, 
	// Returns a float, with the left over coffee if there is any. Otherwise it returns 0.
	public float fill(float l) {
		float initlevel = this.fillLevel;
		if (this.fillLevel + l <= this.maxFillLevel) {
			this.fillLevel += l;
			return 0;
		} else {
			this.fillLevel = this.maxFillLevel;
			return l - (this.maxFillLevel - initlevel);
		}
	}

	// Drains the Coffeecup, 
	// Returns a float, with the left over coffee if there is any. Otherwise it returns 0.
	public float drain(float d) {
		float initlevel = this.fillLevel;
		this.fillLevel = (d <= this.fillLevel) ? this.fillLevel - d : 0;
		return (d <= initlevel) ? 0 : d - initlevel;
	}

	public float getFillLevel() {
		return this.fillLevel;
	}

	public float getMaxFillLevel() {
		return this.maxFillLevel;
	}
}
