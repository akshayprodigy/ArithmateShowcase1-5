using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ui_frac_handler : MonoBehaviour
{
    public TEXDraw fraction_txt;// drag texdraw attached gameobject

    void Start()
    {
        send_value(2, 3, 4);
    }


    public void send_value(int w, int n, int d)
    {
        clear();
        fraction_txt.text = w + "\\frac{" + n + "}{" + d + "}";
    }

    public void send_value( int n, int d)
    {
        clear();
        fraction_txt.text = "\\frac{" + n + "}{" + d + "}";
    }

    void clear()
    {
        fraction_txt.text = "";
    }
}
