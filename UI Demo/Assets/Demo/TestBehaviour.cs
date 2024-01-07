/// 

using UnityEngine;

namespace Demo
{
	public class TestBehaviour : MonoBehaviour
	{
		public float timeScale;

		private void Update()
		{
			transform.eulerAngles = new Vector3(0f, 0f, Time.time * timeScale);
		}
	}
}
