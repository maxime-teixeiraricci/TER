using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurator : MonoBehaviour {

    public string Unit;
    UnitConfig uc;

    public float getStat(string s)
    {
       foreach(Stat st in uc.stats)
        {
            if (st.cle == s)
                return st.valeur;
        }
        return 0f;
    }

    // Use this for initialization
    void Start()
    {
        Unit = GetComponent<Stats>()._unitType;
        uc = new UnitConfig();
        List<UnitConfig> l = GameObject.Find("GameManager").GetComponent<ConfigLoader>().configuration;
        foreach (UnitConfig u in l)
        {
                if (u.unit == Unit)
                    uc = u;
        }
        if (GetComponent<MovableCharacter>())
            GetComponent<MovableCharacter>().speed = getStat("Speed");

        if (GetComponent<Sight>())
        {
            GetComponent<Sight>()._angle = getStat("ViewAngle");
            GetComponent<Sight>()._distance = getStat("ViewDistance");
        }

        if (GetComponent<Stats>())
        {
            GetComponent<Stats>().SetMaxHealth((int)getStat("MaxHealth"));
            GetComponent<Stats>().SetHealth((int)getStat("MaxHealth"));
            GetComponent<Stats>().SetReloadTime((int)getStat("Reload"));
        }

        if (GetComponent<Inventory>())
            GetComponent<Inventory>()._maxSize = (int)getStat("InventorySize");
    }
}
