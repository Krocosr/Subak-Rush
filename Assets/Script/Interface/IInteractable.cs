using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void onEnter();
    void onExit();
    void Action(int id);
}
