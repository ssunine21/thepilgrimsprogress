﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	public static QuestManager init;

	private List<Dictionary<string, object>> questData;
	private List<string> sentences = new List<string> { };
	private DialogueManager dialogueManager;

	#region Singleton
	private void Awake() {
		if (init == null) {
			DontDestroyOnLoad(this.gameObject);
			init = this;
		} else {
			Destroy(this.gameObject);
		}
	}
	#endregion Singleton

	private void Start() {
		questData = CSVReader.Read("quest");
		dialogueManager = FindObjectOfType<DialogueManager>();
	}

	public void InsertQuest(string questNum) {
		for (var i = 0; i < questData.Count; ++i) {
			if (questData[i]["no"].ToString().Equals(questNum)) {

				while (i < questData.Count) {
					if (!questData[i]["no"].ToString().Equals(questNum) && !questData[i]["no"].ToString().Equals("")) {
						SendSentences();
						return;
					}
					sentences.Add((string)questData[i]["script"]);
					
					++i;
				}

				SendSentences();
				return;
			}
		}
	}

	private void SendSentences() {
		dialogueManager.ShowDialogue(sentences);
		sentences.Clear();
	}
}