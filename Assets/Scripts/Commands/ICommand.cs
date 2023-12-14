using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    // Ejecución de orden
    void Do();

    // Patron Memento
    void Undo();
}
