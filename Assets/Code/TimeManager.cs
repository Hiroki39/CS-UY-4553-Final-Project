using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public float slowdownFactor;
	//public float slowdownLength;

	// void Update() {
	// 	Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
	// 	Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	// }

	public void DoSlowmotion() {
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}

	public void StopSlowmotion() {
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}
}
