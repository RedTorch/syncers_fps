using Fusion;
using UnityEngine;

public struct NetworkInputData_Net: INetworkInput
{
	public Vector2 move;
	public Vector2 look;
	public bool jump;
    public float fire;
    public bool reload;
    public bool ability1;
    public PlayerRef callingPlayer; // the PlayerRef of the player that sent this data
}