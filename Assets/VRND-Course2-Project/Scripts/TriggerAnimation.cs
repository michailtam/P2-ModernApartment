using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {
	public string firstTrigger;
	public string secondTrigger;
    public Animator stateMachine;
	public GvrViewer vrViewer;

    private bool created = false;

    void Awake() {
		// IMPORTANT: Changed cause the existing vr camera was overwriten
		if (vrViewer.GetInstanceID() == null) {
			GvrViewer.Create();
            created = true; 
		} 
    }

    void Start() {
        if (created) {
			foreach (Camera c in GvrViewer.Instance.GetComponentsInChildren<Camera>()) {
                c.enabled = false; // to use the Gvr SDK without adding cameras we have to disable them
            }
        }
    }

    void Update() {
		GvrViewer.Instance.UpdateState(); //need to update the data here otherwise we dont get mouse clicks; this is because we are automatically creating the GVRSDK (seems like a bug)
		if (GvrViewer.Instance.Triggered) {

			if (stateMachine.GetCurrentAnimatorStateInfo (0).IsName ("GlobeIdle")) {
				stateMachine.SetTrigger (firstTrigger);
			} else {
				stateMachine.SetTrigger (secondTrigger);
			}
		}
    }
}