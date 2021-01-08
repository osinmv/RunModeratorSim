using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject igtObj;
    public GameObject timer;
    public Sprite cop;
    public Sprite vicecity;
    public Sprite soc;
    public Sprite cs;
    public GameObject gameImage;
    private Image gImage;
    private Text timerIgt;
    private Text info;
    private bool newRun;
    private int currentmistakes;
    private int totalruns;
    private bool goodrun;
    private Random rng;
    public TextAsset names;
    private string[] nicks;
    private Dictionary<int, string> intToGame;
    private Dictionary<int, Sprite> intToImage;
    public GameObject scoreObject;
    private Text score;
    enum Games : int
    {
        Soc = 0,
        Cs = 1,
        Cop = 2,
        VC = 3
    }
    void Start()
    {
        score = scoreObject.GetComponent<Text>();
        info = igtObj.GetComponent<Text>();
        timerIgt = timer.GetComponent<Text>();
        gImage = gameImage.GetComponent<Image>();
        newRun = true;
        totalruns = 0;
        currentmistakes = 0;
        nicks = names.text.Split((char)10);
        intToGame = new Dictionary<int, string>();
        intToImage = new Dictionary<int, Sprite>();
        intToImage.Add(0, soc);
        intToImage.Add(1, cs);
        intToImage.Add(2, cop);
        intToImage.Add(3, vicecity);
        intToGame.Add(0, "Shadow Of ...");
        intToGame.Add(1, "Clear Sky");
        intToGame.Add(2, "Call of Pripyat");
        intToGame.Add(3, "Vice City");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (newRun)
        {
            newRun = false;
            score.text = "Total Runs:"+totalruns+"\nMistakes:"+currentmistakes;
            displayNewRun();
        }
    }
    private void displayNewRun()
    {
        goodrun = true;
        int minutes = Random.Range(9, 15);
        int seconds = Random.Range(0, 60);
        int miliseconds = Random.Range(0, 1000);
        string time = minutes + ":" + seconds + ":" + miliseconds;
        info.text = time;
        if (Random.Range(0, 10) >= 8)
        {
            info.text = minutes + ":" + seconds + ":0" + (miliseconds / 10);
            goodrun = false;
        }
        timerIgt.text = time;

        info.text = info.text + "\n" + nicks[Random.Range(0, nicks.Length)].Replace("  ", "");

        int gameNum = Random.Range(0, 4);
        
        if (gameNum == 3)
        {
            goodrun = false;
            info.text = info.text + "\n" + intToGame[gameNum];
        }
        else if (Random.Range(0, 10) == 0)
        {
            goodrun = false;
            info.text = info.text + "\n" + intToGame[3];
        }
        else
        {
            info.text = info.text + "\n" + intToGame[gameNum];
        }
        gImage.sprite = intToImage[gameNum];
    }
    public void actionRun(bool accepted)
    {
        if (!((!goodrun || accepted) &&(goodrun || !accepted)))
        {
            currentmistakes++;
        }
        totalruns++;
        newRun = true;


    }

}
