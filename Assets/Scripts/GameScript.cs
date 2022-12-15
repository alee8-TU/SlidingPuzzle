using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;

    public List<GameObject> tileList = new List<GameObject>();

    private bool isShuffled = false;
    private bool gameOver = false;

    //private Random r = new Random();  

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShuffled)
        {
            //Shuffle(tileList);

            int n = tileList.Count;  
            while (n > 1)
            {  
                n--;  
                //int k = r.Next(n + 1);
                int k = Random.Range(0, n);
                GameObject value = tileList[k];
                //value.SetTransform(new Vector3(tileList[k].transform.position.x, tileList[k].transform.position.y, 0));

                tileList[k] = tileList[n];
                //tileList[k].SetTransform(new Vector3(tileList[n].transform.position.x, tileList[n].transform.position.y, 0));

                tileList[n] = value; 
                //tileList[n].SetTransform(new Vector3(value.transform.position.x, value.transform.position.y, 0));
            }
            isShuffled = true;

            Debug.Log("x: " + tileList[0].transform.position.x + " y: " + tileList[0].transform.position.y);

            for(int i = 0; i < tileList.Count; i++)
            {
                TilesScript thisTile = tileList[i].transform.GetComponent<TilesScript>();
                thisTile.targetPosition = new Vector3(-8 + 2 * (i % 4), 3 - 2 * (i / 4), 0);
            }
        }


        if(!gameOver)
        {
            gameOver = checkIfGameOver();

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if(hit)
                {
                    //Debug.Log(hit.transform.name);
                    if(Vector2.Distance(emptySpace.position, hit.transform.position) <= 2.1)
                    {
                        Vector2 lastEmptySpacePosition = emptySpace.position;
                        TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
                        emptySpace.position = hit.transform.position;
                        thisTile.targetPosition = lastEmptySpacePosition;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                gameOver = true;
            }
        }
        else
        {
            //Show Win Screen
            SceneManager.LoadScene("GameOverScene");
        }

    }

    bool checkIfGameOver()
    {
        bool ret = true;

        for(int i = 0; i < tileList.Count; i++)
        {
            TilesScript thisTile = tileList[i].transform.GetComponent<TilesScript>();
            //thisTile.targetPosition = new Vector3(-8 + 2 * (i % 4), 3 - 2 * (i / 4), 0);
            if(thisTile.targetPosition != thisTile.correctPosition)
            {
                ret = false;
            }
        }

        return ret;
    }
}
