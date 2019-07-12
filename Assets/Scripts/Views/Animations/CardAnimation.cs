using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardAnimation : MonoBehaviour
{
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;

    private void Awake()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0);
    }

    void Start()
    {

        transform.DOScale(1, 4f);

        transform.DOLocalMove(new Vector3(x, y, 0), 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        //第一个参数 ,抖动的方向,例如我这里是从当前位置向上抖动到1的位置
        //第二个参数,抖动的时间
        //第三个参数,抖动的次数
        //第四个参数 0-1之间的数,代表回弹的幅度,
        //假如为零从最高点当前位置 + Vector3.up回来的时候只会回到当前位置,如果为一就会回到当前位置 - Vector3.up的位置
        transform.DOPunchPosition(new Vector3(50, 10, 10), 2, 2, 0f);
        //transform.DOPunchPosition(new Vector3(-10, 0, 0), 2, 2, 0.5f);

    }
}
