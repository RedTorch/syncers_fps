using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSpawner_Net : MonoBehaviour, INetworkRunnerCallbacks 
{
	private NetworkRunner _runner;

	private void OnGUI()
	{
		if (_runner == null)
		{
			if (GUI.Button(new Rect(0,0,200,40), "Host"))
			{
				StartGame(GameMode.Host);
			}
			if (GUI.Button(new Rect(0,40,200,40), "Join"))
			{
				StartGame(GameMode.Client);
			}
		}
	}

	async void StartGame(GameMode mode)
	{
		// Create the Fusion runner and let it know that we will be providing user input
		_runner = gameObject.AddComponent<NetworkRunner>();
		_runner.ProvideInput = true;
	
		// Start or join (depends on gamemode) a session with a specific name
		await _runner.StartGame(new StartGameArgs()
		{
			GameMode = mode, 
			SessionName = "TestRoom", 
			Scene = SceneManager.GetActiveScene().buildIndex,
			SceneObjectProvider = gameObject.AddComponent<NetworkSceneManagerDefault>()
		});
	}

	[SerializeField] private NetworkPrefabRef _playerPrefab; // Character to spawn for a joining player
	private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

	public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{
		if (runner.IsServer)
		{
			Vector3 spawnPosition = new Vector3((player.RawEncoded%runner.Config.Simulation.DefaultPlayers)*3,2,0);
			NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
			_spawnedCharacters.Add(player, networkPlayerObject);
			// basically, if player ID of new object and local player does not match, disable cam
			if(player != runner.LocalPlayer)
			{
				_spawnedCharacters[player].GetComponent<InputManager_Net>().cam.enabled = false;
			}
			networkPlayerObject.GetComponent<InputManager_Net>().playerReference=player;
		}
	}

	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
	{
		if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
		{
			runner.Despawn(networkObject);
			_spawnedCharacters.Remove(player);
		}
	}

	public void OnInput(NetworkRunner runner, NetworkInput input)
	{

		var data = new NetworkInputData_Net();

		// if (Input.GetKey(KeyCode.W))
		// 	data.move += Vector2.up;

		// if (Input.GetKey(KeyCode.S))
		// 	data.move += Vector2.down;

		// if (Input.GetKey(KeyCode.A))
		// 	data.move += Vector2.left;

		// if (Input.GetKey(KeyCode.D))
		// 	data.move += Vector2.right;

		data.move = gameObject.GetComponent<PlayerInput_Net>().move;
		data.look = gameObject.GetComponent<PlayerInput_Net>().look;
		data.jump = gameObject.GetComponent<PlayerInput_Net>().jump;
		data.jump2 = gameObject.GetComponent<PlayerInput_Net>().jump2;
		data.fire = gameObject.GetComponent<PlayerInput_Net>().fire;
		data.reload = gameObject.GetComponent<PlayerInput_Net>().reload;
		data.ability1 = gameObject.GetComponent<PlayerInput_Net>().ability1;
		data.callingPlayer = runner.LocalPlayer;

		print("input taken");

		// _spawnedCharacters[runner.LocalPlayer].GetComponent<InputManager_Net>().MoveChar(data);
		
		input.Set(data);
	}
	
	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
	public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
	public void OnConnectedToServer(NetworkRunner runner) { }
	public void OnDisconnectedFromServer(NetworkRunner runner) { }
	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
	public void OnSceneLoadDone(NetworkRunner runner) { }
	public void OnSceneLoadStart(NetworkRunner runner) { }
}