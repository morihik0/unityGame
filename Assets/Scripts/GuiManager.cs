using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

    public GameObject timeCount , player , odenCount , enemy;
    public float tmpTimeCount;

    private Text timeCountText , odenCountText;
    private Player playerScript;
    [HideInInspector] public bool active;

    // Use this for initialization
    void Start () {        
        timeCountText = timeCount.GetComponent<Text>();
        odenCountText = odenCount.GetComponent<Text>();
        playerScript = player.GetComponent<Player>();
        timeCountText.text = "time " + string.Format("{0:00.0}", ToRoundDown(tmpTimeCount, 1));
        active = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (active && Time.timeScale != 0)
        {
            if (tmpTimeCount < 0) GameManager.instance.GameOver();
            //タイムカウントの表記
            tmpTimeCount -= Time.deltaTime;
            timeCountText.text = "time " + string.Format("{0:00.0}", ToRoundDown(tmpTimeCount, 1));

            //おでんカウント表記
            odenCountText.text = "<size=17>x</size> " + playerScript.odenCount;
        }
    }
    
    /// ------------------------------------------------------------------------
    /// <summary>
    ///     指定した精度の数値に切り捨てします。</summary>
    /// <param name="dValue">
    ///     丸め対象の倍精度浮動小数点数。</param>
    /// <param name="iDigits">
    ///     戻り値の有効桁数の精度。</param>
    /// <returns>
    ///     iDigits に等しい精度の数値に切り捨てられた数値。</returns>
    /// ------------------------------------------------------------------------
    private double ToRoundDown(double dValue, int iDigits)
    {
        double dCoef = System.Math.Pow(10, iDigits);

        return dValue > 0 ? System.Math.Floor(dValue * dCoef) / dCoef :
                            System.Math.Ceiling(dValue * dCoef) / dCoef;
    }

    public void TimeCountPlus(float f)
    {
        tmpTimeCount += f;
    }
}
