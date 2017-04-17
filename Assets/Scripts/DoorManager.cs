using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class DoorManager : MonoBehaviour {
    //blue red yellow green black
    private GameObject[] door = new GameObject[10];
    private SpriteRenderer[] doorRend = new SpriteRenderer[10];

    private int[] leftArray;
    private int[] rightArray;
    private bool readyColorArray = false;
    private bool startColorArray = true;

    private bool ChangeColor = true;

    // Use this for initialization
    void Start () {
        for(int i=0; i<10; i++)
        {
            door[i] = gameObject.transform.FindChild(i.ToString()).gameObject;
            doorRend[i] = door[i].GetComponent<SpriteRenderer>();
            doorRend[i].color = Color.white;
        }        
    }
	
	// Update is called once per frame
	void Update () {
        if(startColorArray) StartCoroutine(RandomColor());
        if(ChangeColor && readyColorArray)ColorInsert();
        //if (Input.GetButtonDown("Jump")) ChangeColor = true;
    }
    
    private IEnumerator RandomColor()
    {
        startColorArray = false;
        //シャッフルする配列
        int[] ary = new int[] { 0 , 1 , 2 , 3 , 4 };
        rightArray = new int[] { 9, 9, 9, 9, 9 }; //0が入ってると不都合なので適当な数値で初期化
        //シャッフルする
        leftArray = ary.OrderBy(i => Guid.NewGuid()).ToArray();
        //エラーカウント
        int errorCount = 0;

        //色決め
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 5);
            bool colorEr = true;
            while (colorEr)
            {
                colorEr = false;
                //左側と同色、右側に同じ色がある場合はやり直し
                if (leftArray[i] == rand || rightArray.Contains(rand))
                {
                    colorEr = true;
                    if (i == 4) errorCount++;
                }
                //重複パターンもダメ
                for(int j=0; j<5; j++)
                {
                    if (leftArray[j] == rand && rightArray[j] == leftArray[i])
                    {
                        colorEr = true;
                        if (i == 4) errorCount++;
                    }
                }
                if (colorEr)
                {
                    if (errorCount > 5)
                    {
                        startColorArray = true;
                        yield break;
                    }
                    if (rand == 4) rand = 0;
                    else rand++;
                }
                yield return null;
            }
            rightArray[i] = rand;
        }
        readyColorArray = true;
    }

    private void ColorInsert()
    {
        for (int i = 0; i < 5; i++)
        {
            //左側の色処理
            switch (leftArray[i])
            {
                case 0:
                    doorRend[i * 2].color = Color.blue;
                    break;
                case 1:
                    doorRend[i * 2].color = Color.red;
                    break;
                case 2:
                    doorRend[i * 2].color = Color.yellow;
                    break;
                case 3:
                    doorRend[i * 2].color = Color.green;
                    break;
                case 4:
                    doorRend[i * 2].color = Color.black;
                    break;
            }
            //右側の色処理
            switch (rightArray[i])
            {
                case 0:
                    doorRend[(i * 2) + 1].color = Color.blue;
                    break;
                case 1:
                    doorRend[(i * 2) + 1].color = Color.red;
                    break;
                case 2:
                    doorRend[(i * 2) + 1].color = Color.yellow;
                    break;
                case 3:
                    doorRend[(i * 2) + 1].color = Color.green;
                    break;
                case 4:
                    doorRend[(i * 2) + 1].color = Color.black;
                    break;
            }
        }
        ChangeColor = false;
        readyColorArray = false;
        startColorArray = true;
    }

    //ドア接触時のプレイヤーの行き先座標を返す
    public Vector3 PlayerMove(string name)
    {
        Vector3 resultV3 = Vector3.zero;
        var result = doorRend.Where(c => c.color == doorRend[int.Parse(name)].color)
                            .Where(c => c.gameObject.name != name);
        foreach(var ele in result)
        {
            if(ele.transform.position.x < 0) resultV3 = ele.gameObject.transform.position + new Vector3(40,0,0);
            else resultV3 = ele.gameObject.transform.position + new Vector3(-40, 0, 0);
        }
        return resultV3;
    }

    public void DoorColorChange()
    {
        ChangeColor = true;
    }
}
