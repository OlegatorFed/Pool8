using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Cheats : MonoBehaviour
{
    private (int, Action)[] _cheats;
    private List<KeyCode> _cheatMemory = new List<KeyCode>();
    private CheatTable _table = null;
    
    // All cheats described here
    private void MakeCheats()
    {
        _table = new CheatTable();

        _cheats = _table.Cheats.Select(TranslateTableCheat).ToArray();
    }

    private void Awake()
    {
        MakeCheats();
    }

    private void Update()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (_cheatMemory.Count > 16)
                    _cheatMemory.RemoveAt(16);
                
                _cheatMemory.Insert(0, key);
            }
        }

        int cheatCode = 0;
            
        for(int i=0; i<_cheatMemory.Count; i++)
        {
            cheatCode = (cheatCode, _cheatMemory[i]).GetHashCode();

            (int code, Action method) tableCheat = _cheats.FirstOrDefault(MatchCheat(cheatCode));

            if (tableCheat.code == cheatCode)
            {
                tableCheat.method();
                
                _cheatMemory.Clear();
            }
        }
    }

    private static Func<(int, Action), bool> MatchCheat(int cheatCode)
    {
        return ((int c, Action k) cheat) => cheat.c == cheatCode;
    }

    private (int, Action) TranslateTableCheat((string humanCode, Action method) tableCheat)
    {
        var codeKeys = tableCheat.humanCode
            .Select(CharToKey)
            .Reverse()
            .Aggregate(0, (i, code) => (i, code).GetHashCode());

        return (codeKeys, tableCheat.method);
    }

    private KeyCode CharToKey(char c)
    {
        return (KeyCode) Enum.Parse(typeof(KeyCode), c.ToString());
    }
}
