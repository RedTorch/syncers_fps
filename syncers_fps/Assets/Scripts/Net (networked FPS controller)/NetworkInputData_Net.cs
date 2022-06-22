using Fusion;
using UnityEngine;

public struct NetworkInputData_Net: INetworkInput
{
	public Vector2 move;
	public Vector2 look;
	public bool jump;
    public bool fire;
    public bool reload;
    public bool ability1;
}