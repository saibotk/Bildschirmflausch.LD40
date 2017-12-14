using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JobFactory {
	public enum FACTORYNAMES { WATERING = 0, CLEANING, DELIVERY };
	protected FACTORYNAMES name;

	protected JobFactory(FACTORYNAMES name) {
		this.name = name;
	}

	public FACTORYNAMES GetName() {
		return name;
	}

	public abstract bool CanCreateJob();

	public abstract Job CreateJob();

}