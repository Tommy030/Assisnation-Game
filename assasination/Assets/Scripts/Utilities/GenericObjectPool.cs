using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool : MonoBehaviour
{


    [System.Serializable]
    public class Pool     // een class die informatie van een object pool opslaat de class is Serializable dus de waardes kunnen in inspector worden ingevulld
    {
        public string tag;
        public GameObject prefab;
        public int sizeOfPool;
    }

    // een singleton
    public static GenericObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    public Dictionary<string, Queue<GameObject>> pooldic;

    public List<Pool> pools;      // een list van het type pool zodat nieuwe pools in de inspector kunnen worden aangemaakt en aangepast
    void Start()
    {
        pooldic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)   // hier runt hij door de list pools
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();

            for (int i = 0; i < pool.sizeOfPool; i++)   // hier wordt aangegeven hoeveel van elk item moeten worden geinstatiate de hoeveelheid word aangegeven door de sizeofpool die een int is die in de inspector is aangegeven
            {
                GameObject obj = Instantiate(pool.prefab);  // hij maakt het aangegeven gameobject een bewaart een reference er naar
                obj.SetActive(false);          // het object wordt uitgezet
                objectpool.Enqueue(obj);       // en in een queue zodat het makkelijk is om een van de objecten weer terug te vinden
            }

            pooldic.Add(pool.tag, objectpool);    // er wordt dan elke queue samen met de bijhorende tag in een dictionairy gezet
        }
    }


    // dit is de functie waarbij je overal dingen uit een speciefieke pool kan halen en dan goed zetten
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)     // er moet hier een tag worden doorgegeven zodat hij kan zoeken naar de goede queue in de eerder gemaakte dictionairy de is rest gewoon positie en rotatie die het object moet hebben
    {
        // hij checkt of de doorgegeven tag in de dictionairy staat als niet returnt hij
        if (!pooldic.ContainsKey(tag))
        {
            Debug.Log(tag + " bestaat niet check of de naam geving correct is");
            return null;
        }
        GameObject objToSpawn = pooldic[tag].Dequeue();     // hij haalt hier het bovenste object uit de queue en bewaart er een reference naar 

        objToSpawn.SetActive(true);                    // het object wordt aangezet

        // de positie en rotation wordt goed gezet
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        // hij roept ipooled aan dit is een vervanging voor start
        Ipooled pooledObject = objToSpawn.GetComponent<Ipooled>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }
        pooldic[tag].Enqueue(objToSpawn); // hij zet daarna het object weer terug in de goede queue om hem weer later te kunnen herbruiken

        return objToSpawn;
    }

    
}
