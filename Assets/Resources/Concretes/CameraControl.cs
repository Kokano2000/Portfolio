using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //カーソル位置
    Vector2 mousePos;
    //カメラ移動速度
    float cameraMoveSpeed = 10.0f;
    //ズーム感度
    [SerializeField] private float zoomSens;
    //プレイヤー
    [SerializeField] GameObject player;
    //カメラ高さ制限
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    private void Awake()
    {
        mousePos = Input.mousePosition;
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - transform.position.y / 1.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
        CameraReset();
        CameraMove();
    }

    //カーソルが端っこにある時画面を移動させるメソッド
    private void CameraMove()
    {
        mousePos = Input.mousePosition;
        Vector3 destination = transform.position;

        if (mousePos.x <= 0)
        {
            destination += Vector3.left;
        }
        else if (mousePos.x >= Screen.width)
        {
            destination += Vector3.right;
        }
        if (mousePos.y <= 0)
        {
            destination += Vector3.back;
        }
        else if (mousePos.y >= Screen.height)
        {
            destination += Vector3.forward;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, cameraMoveSpeed * Time.deltaTime);
    }

    //カメラのズームインアウトメソッド
    private void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector3 cameraPos = transform.position;
            cameraPos.y = Mathf.Clamp(cameraPos.y + Input.GetAxis("Mouse ScrollWheel") * zoomSens, minHeight, maxHeight);
            transform.position = cameraPos;
        }
    }

    //カメラをキャラクター中心にする
    private void CameraReset()
    {
        if (Input.GetAxis("Jump") > 0){
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - transform.position.y / 1.5f);
        }
    }
}
