﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	// Use this for initialization
	public void LoadScene (int level) {
		loadingImage.SetActive (true);
		SceneManager.LoadScene(level);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
