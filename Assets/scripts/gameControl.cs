using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameControl : MonoBehaviour
{
    GameObject plate;
    public GameObject winLogo;

    List<int> faceindexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 0, 1, 2, 3, 4, 5, 6 };

    public static System.Random rnd = new System.Random();

    public int shuffleNum = 0;
    int[] visibleFaces = { -1, -2 };

    int winGame = 7;

    private void Start()
    {
        int originalLength = faceindexes.Count;

        float yPos = 3.52f;
        float xPos = -4f;

        for (int i = 0; i < 13; i++)
        {
            shuffleNum = rnd.Next(0, (faceindexes.Count));

            var temp = Instantiate(plate, new Vector3(xPos, yPos, 180), Quaternion.identity);

            temp.GetComponent<openPlate>().faceIndex = faceindexes[shuffleNum];
            temp.GetComponent<openPlate>().name = faceindexes[shuffleNum].ToString();

            faceindexes.Remove(faceindexes[shuffleNum]);

            xPos += 3.9f;

            if (i == (originalLength / 3 - 1))
            {
                yPos = 0.25f;
                xPos = -7.8f;
            }
            else if (i == 8)
            {
                yPos = -3f;
                xPos = -7.8f;
            }
        }
        plate.GetComponent<openPlate>().faceIndex = faceindexes[0];
        plate.GetComponent<openPlate>().name = faceindexes[0].ToString();
    }

    public bool TwoCardsUp()
    {
        bool cardsUp = false;

        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            cardsUp = true;
        }

        return cardsUp;
    }

    public void AddVisibleFace(int index)
    {
        if (visibleFaces[0] == -1)
        {
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }
    }

    public void RemoveVisibleFace(int index)
    {
        if (visibleFaces[0] == index)
        {
            visibleFaces[0] = -1;
        }
        else if (visibleFaces[1] == index)
        {
            visibleFaces[1] = -2;
        }
    }

    public bool CheckMatch()
    {
        bool success = false;

        if (visibleFaces[0] == visibleFaces[1])
        {
            GameObject obj1 = GameObject.Find(visibleFaces[0].ToString());
            obj1.name = obj1.name + "_" + obj1.name;

            GameObject obj2 = GameObject.Find(visibleFaces[1].ToString());

            StartCoroutine(DeleteObject(obj1, obj2));

            visibleFaces[0] = -1;
            visibleFaces[1] = -2;

            success = true;

            winGame--;

            if (winGame == 0)
            {
             

                winLogo.SetActive(true);
            }
        }

        return success;
    }

    IEnumerator DeleteObject(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(2f);

        Destroy(obj1);
        Destroy(obj2);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    private void Awake()
    {
        plate = GameObject.Find("Plate");
    }
}
