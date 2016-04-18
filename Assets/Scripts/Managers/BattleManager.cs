using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	public bool isTimedBattle = true;
	public float timePassed = 0f;
	public float timeLimit = 320f;
	public GameObject playerPrefab;
	public GameObject[] spawnPoints;

	/**
	 *	For the winscreen I propose we do it the same as in Goddown Showoff. A prefab that is an overlay on the screen.  
	 * We can have multiple winners so we turn images on and off based on player ids.
	 */
	public GameObject winScreenPrefab;
	private List<GameObject> players = new List<GameObject>();

	public int PLAYER_LIMIT = 4;

	// Use this for initialization
	void Awake () {
		createPlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTimedBattle && timePassed > timeLimit) {
			showWinner ();
		}
		timePassed += Time.deltaTime;
	}

	private void showWinner() {
		checkWinnerAndSetWinScreenPrefab ();
		winScreenPrefab.SetActive (true);
	}

	private void checkWinnerAndSetWinScreenPrefab() {
		List<Player> mostCoinsLeastDeaths = new List<Player>();
		mostCoinsLeastDeaths.Add(players[0].GetComponent<Player> ());
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
		if (mostCoinsLeastDeaths.Count > 1) {
			string text = "WINNERS: ";
			text += mostCoinsLeastDeaths[0].mId;
			for (int i = 1; i<mostCoinsLeastDeaths.Count; i++) {
				text += " & " + mostCoinsLeastDeaths[i].mId;
			}
			winScreenPrefab.GetComponent<UnityEngine.UI.Text>().text = text;
		} else {
			string text = "WINNER = ";
			text += mostCoinsLeastDeaths[0].mId;
			winScreenPrefab.GetComponent<UnityEngine.UI.Text>().text = text;
		}
	}

	private void createPlayers() {
		for (int i = 0; i<PLAYER_LIMIT; i++) {
			createPlayer(i);
		}
	}

	private void createPlayer(int iterator) {
		players.Add(new GameObject());
		players[iterator].name = "Player" + (iterator + 1);
		players[iterator].AddComponent<Player>();
		players[iterator].GetComponent<Player>().mId = iterator+1;
		players[iterator].transform.position = spawnPoints[iterator].transform.position;
		//players[iterator].AddComponent<SquareShape>();
		//players[iterator].AddComponent<Controller>(); //Add this one last
	}
}
