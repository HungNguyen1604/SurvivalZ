using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibraryControl : BySingleton<SpriteLibraryControl>
{
    [SerializeField]
    private List<Sprite> sprites;
    private Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sprite sp in sprites)
        {
            dic.Add(sp.name, sp);
        }
    }

    public Sprite GetSpriteByName(string name_sp)
    {
        return dic[name_sp];
    }
}
