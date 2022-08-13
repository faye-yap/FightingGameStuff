using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public int numWins = 0;
    private List<GameObject> playerScore = new List<GameObject>();
    public Sprite replaceImage;
    public GameManager gameManager;
    public GameObject scorePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(init());
    }

    // Update is called once per frame
    public void UpdateScore(int numWins) {
        GameObject scorePip = playerScore[numWins];
        scorePip.GetComponent<Image>().sprite = replaceImage;
        scorePip.transform.localScale = new Vector3(2,2,2);
    }

    private IEnumerator init(){
        yield return null;
        for (int i = 0; i < gameManager.firstTo; i++){            
            GameObject scorePip = Instantiate(scorePrefab,new Vector3(i * 40,0,0),Quaternion.identity);
            scorePip.transform.SetParent(gameObject.transform,false);
            playerScore.Add(scorePip);
        }
        Debug.Log(playerScore.ToString());
    }
}
