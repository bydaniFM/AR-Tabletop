using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControl {

	void BuildPath(Vector3 point);
	void MovementPhase();
	void OnMoveEnd();
	void SelectUnit(UnitController uc);
	int curUnit{
		get;
	}
	bool unitsDeployed{
		get;
	}
	UnitController[] GetUnits();
	bool isOwner(UnitController uc);
	void Damage(UnitController uc, int damage);
	void UpdateControllers();


}
