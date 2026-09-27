using UnityEngine;

public interface IState
{
	public StateMachines StateMachine {  get; set; }
	void Enter();
	void Execute();
	void Exit();
}
