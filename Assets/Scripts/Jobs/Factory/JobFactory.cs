using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JobFactory {
	public enum FACTORYNAMES { WATERING = 0, CLEANING, DELIVERY };
	protected FACTORYNAMES name;
	public readonly float probability;

	protected JobFactory(FACTORYNAMES name, float prop) {
		this.name = name;
		this.probability = prop;
	}

	public FACTORYNAMES GetName() {
		return name;
	}

	public abstract bool CanCreateJob();

	public abstract Job CreateJob();

}