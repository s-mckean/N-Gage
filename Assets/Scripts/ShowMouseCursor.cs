using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouseCursor : MonoBehaviour {

	private void Awake() {
		// make sure mouse cursor can be seen
		 Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
	}
}
