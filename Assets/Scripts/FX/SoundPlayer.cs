using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundPlayer : MonoBehaviour {
	public string mixerName;
	public SoundEffect[] soundEffects;
	private AudioSource source;
	private Dictionary<string, AudioClip> sounds;
	// Use this for initialization
	void Start () {
		AudioMixer mixer = Resources.Load<AudioMixer>("WarAudioMixer");
		Debug.Log(mixer);
		source = gameObject.AddComponent<AudioSource>();

		source.outputAudioMixerGroup = mixer.FindMatchingGroups(mixerName)[0];
		sounds = new Dictionary<string, AudioClip>();
		for (int i = 0; i < soundEffects.Length; i++) {
			if(!sounds.ContainsKey(soundEffects[i].name)){
				sounds.Add(soundEffects[i].name, soundEffects[i].clip);
			}
			else {
				Debug.LogError("The re is already an Effect named "+soundEffects[i].name + "!");
			}
		}
	}


	public void Play(string name){
		AudioClip clip = null;
		sounds.TryGetValue(name, out clip);
		Debug.Assert(clip != null, "The clip "+name+" is not added to "+gameObject.name);
		source.clip = clip;
		source.Play();
	}
}

[Serializable]
public class SoundEffect{
	public string name;
	public AudioClip clip;
}
