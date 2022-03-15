
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SimpleUdonElevator : UdonSharpBehaviour
{
    public Transform bottom;
    public Transform top;
    public Transform platform;
    public float speed = 0.25f;
    public float countdown;

    private string state;

    void Start()
    {
        platform.position = bottom.position;
        state = "going up";
    }

    void LateUpdate() {
        if(state == "going up") {
            platform.position = Vector3.Lerp(platform.position, top.position, Time.deltaTime * speed);

            if (Vector3.Distance(platform.position, top.position) < .1) {
                state = "waiting down";
                Debug.Log(state);
                countdown = 100f;
			}
		}

        if(state == "waiting up" || state == "waiting down") {
            countdown--;

            if(countdown <= 0) {
                if(state == "waiting down") {
                    state = "going down";
                    Debug.Log(state);
                }
                if (state == "waiting up") {
                    state = "going up";
                    Debug.Log(state);
                }
			}
		}

        if(state == "going down") {
            platform.position = Vector3.Lerp(platform.position, bottom.position, Time.deltaTime * speed);

            if (Vector3.Distance(platform.position, bottom.position) < .1) {
                state = "waiting up";
                Debug.Log(state);
                countdown = 100f;
            }
        }
	}
}
