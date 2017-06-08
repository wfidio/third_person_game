using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager{
	ManagerStatus status{ get;}

    void startUp(NetworkService service);

    //void startUp();
}
