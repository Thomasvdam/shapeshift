﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public bool isTimedBattle = false;
	public float timePassed = 0;
	public float timeLimit;
	public GameObject playerPrefab;

	/**
	 *	For the winscreen I propose we do it the same as in Goddown Showoff. A prefab that is an overlay on the screen.  
	 * We can have multiple winners so we turn images on and off based on player ids.
	 */
	public GameObject winScreenPrefab;
	private List<GameObject> players = new List<GameObject>();

	private const float TIME_LIMIT_DEFAULT = 100f;
	private const int PLAYER_LIMIT_DEFAULT = 4;

	// Use this for initialization
	void Start () {
		timeLimit = TIME_LIMIT_DEFAULT;
		createPlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimedBattle && timePassed > timeLimit) {
			timePassed += Time.deltaTime;
			showWinner ();
		}
	}

	private void showWinner() {
		checkWinnerAndSetWinScreenPrefab ();
		//winScreenPrefab.show()
	}

	private void checkWinnerAndSetWinScreenPrefab() {
		List<Player> mostCoinsLeastDeaths = new List<Player>();
		mostCoinsLeastDeaths [0] = players [0].GetComponent<Player> ();
		for (int i = 1; i<players.Count; i++) {
			Player player = players[i].GetComponent<Player>();
	
			for(int j = 0; j<mostCoinsLeastDeaths.Count; j++) {
				if(player.coins > mostCoinsLeastDeaths[j].coins) {
					mostCoinsLeastDeaths.RemoveAt(j);
					mostCoinsLeastDeaths.Insert(j, player);
					break;
				} else if(player.coins == mostCoinsLeastDeaths[j].coins) {
					if(player.deaths < mostCoinsLeastDeaths[j].deaths) {
						mostCoinsLeastDeaths.RemoveAt(j);
						mostCoinsLeastDeaths.Insert(j, player);
						break;
					} else if(player.deaths == mostCoinsLeastDeaths[j].deaths) {
						mostCoinsLeastDeaths.Add(player);
						break;
					}
				}
			}
		}

		setWinScreenPrefab (mostCoinsLeastDeaths);
	}

	private void setWinScreenPrefab(List<Player> mostCoinsLeastDeaths){
		for (int i = 0; i<mostCoinsLeastDeaths.Count; i++) {
			//winScreenPrefab.setWinningPlayer(player.id);
		}
	}

	private void createPlayers() {
		for (int i = 0; i<PLAYER_LIMIT_DEFAULT; i++) {
			players.Add(new GameObject());
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().mId = i+1;
			players[i].AddComponent<SquareShape>();
			players[i].AddComponent<Controller>(); //Add this one last
			players[i].GetComponent<Controller>().mId = i+1;
		}
	}
}