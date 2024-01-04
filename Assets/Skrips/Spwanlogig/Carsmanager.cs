using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spwanmanger", menuName = "Manager/Spwanmanger", order = 1)]
public class Carsmanager : MonoBehaviour
{
    

    [SerializeField]
    public CarRealtions HiddenObjekt;
    [SerializeField]
    public Dificulty dificultyObjekt;
    [SerializeField]
    GameObject car;


    List<GameObject> spwarnpoins;
    List<GameObject> carlist;
    List<GameObject> todelaet;
    public int tospwan = 0;
    public int totalspawn = 0;



    // Start is called before the first frame update
    void Start()
    {
        spwarnpoins = new List<GameObject>(GameObject.FindGameObjectsWithTag("Carspwarner"));
        todelaet = new List<GameObject>();
        carlist = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       checkChilden();
        if (transform.childCount < tospwan)
        {
            for (int sp = tospwan; transform.childCount < tospwan && sp > 0; sp--) 
            { 
                Spwancar(); 
            }
            tospwan = transform.childCount;
        }
    }
    [Button("Spwancar")]
    void Spwancar()
    {
        Transform getposion =null;
        int random = Random.Range(0, spwarnpoins.Count-1);
        GameObject VisalCartospwan = HiddenObjekt.getrendom(dificultyObjekt.curve);
        for (int i = 0; getposion == null&&i<10;i++)
        {
            
            try{ getposion = spwarnpoins[(random+i)%spwarnpoins.Count].GetComponent<SpwanObjects>().spwancar(); }
            catch(System.Exception ex) { Debug.Log(ex); };



        }
        if(getposion != null)
        {
            Debug.Log("Spwarncar");
            GameObject Barincar = Instantiate(car, getposion.position, getposion.rotation);
            GameObject Visualcar = Instantiate(VisalCartospwan);
            Barincar.transform.parent= transform;
            Visualcar.transform.parent = Barincar.transform;
            Visualcar.transform.eulerAngles = Barincar.transform.eulerAngles;
            Visualcar.transform.localPosition = Vector3.zero;

            Barincar.SetActive(true);



        }
        
    }

    void checkChilden()
    {
       for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject obj = transform.GetChild(i).gameObject;
        
            if (!obj.activeSelf)
            {
                Debug.Log("Blob");
                for (int b = 0; b < obj.transform.childCount; b++)
                {
                    Destroy(obj.transform.GetChild(i).gameObject);
                }
                Destroy(obj);
            }
        }
    }
}