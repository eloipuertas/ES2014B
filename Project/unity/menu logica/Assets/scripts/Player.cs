using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour, InterfacePlayer
{
		public List <Feature> features;

		void Awake ()
		{
				DontDestroyOnLoad (gameObject);

		}
		// Use this for initialization
		void Start ()
		{
			features = new List<Feature> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void addFeature (string caracteristica)
		{
				Feature f = new Feature (caracteristica);
				this.features.Add (f);
		}

		public void movePlayer (float x, float y, float z)
		{
			Debug.Log ("Not implemented yet");

		}

		public List<Feature> getFeatures ()
		{
				return features;
		}
}
