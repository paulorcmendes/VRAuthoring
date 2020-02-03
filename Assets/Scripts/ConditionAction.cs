using UnityEngine;
using System.Collections;

public enum ConditionActionK{
	//Conditions
	onBegin, onEnd, onAbort, onPause, onResume,
	onSelection, onBeginAttribution, onEndAttribution,
	//Actions
	Start, Stop, Abort, Pause, Resume, Set,
	//None
	none
}

public class ConditionAction : MonoBehaviour
{
	public ConditionActionK myKind;		

	public ConditionActionK MyKind{
		get{
			return this.myKind;
		}
	}
}

