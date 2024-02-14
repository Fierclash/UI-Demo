/// #DataStructScript

using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
	public class SettingsUI : MonoBehaviour
	{
		[Header("Components")]
		public RectTransform host;
		public Button generalPageButton;
		public Button displayButton;
		public Button audioPageButton;
		public GameObject generalPageGameObject;
		public GameObject displayPageGameObject;
		public GameObject audioPageGameObject;
	}
}
