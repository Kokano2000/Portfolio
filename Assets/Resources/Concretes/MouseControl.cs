using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//マウス操作に関するクラス
public class MouseControl
{
    //クリックした箇所の地面の座標を求める
    //スクリーン上の座標
    //なにも当たらなかったらnull
    public static Vector3? MouseWorldPosition(Vector3 position)
    {
        Vector3? result = null;
        Camera camera = Camera.main;
        var ray = camera.ScreenPointToRay(position);
        List<RaycastHit> hit = Physics.RaycastAll(ray, 100.0f, 15<<15).ToList();
        if(hit.Any())
        {
            result = hit.First().point;
        }
        return result;
    }
    //クリックした箇所の地面の座標を求め、対象の高さ分補正する
    //スクリーン上の座標
    //なにも当たらなかったらnull
    public static Vector3? MouseWorldPosition(Vector3 position, float lossyscale)
    {
        Vector3? result = null;
        Camera camera = Camera.main;
        var ray = camera.ScreenPointToRay(position);
        List<RaycastHit> hit = Physics.RaycastAll(ray, 100.0f, 15 << 15).ToList();
        if (hit.Any())
        {
            Vector3 _result = hit.First().point;
            _result.y += lossyscale / 2;
            result = _result;
        }
        return result;
    }
}
