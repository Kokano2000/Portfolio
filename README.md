# C#学習のポートフォリオ
###### 岡野　倭也
  

### 目次

1. #### [はじめに](#はじめに)
1. #### [学習内容及び期間](#学習内容及び期間)
1. #### [制作方針](#制作方針)
1. #### [動作内容](#動作内容)
1. #### [努力点及び直面した課題](#努力点及び直面した課題)
1. #### [おわりに](#おわりに)



### 本文

1. #### はじめに

    岡野倭也と申します。好きなシリーズはメタルギア、ダークソウルです。

    前職でjavaの資格を取得し、プログラミングの基礎的な部分を習得しました為、  
    長らくお世話になったゲーム制作サイトでの制作よりももっと自由に好きなゲームが作ることが出来るのではないかと思い、  
    javaと同じくオブジェクト指向言語であるC#を用いたUnityの学習をしました。
    以下はその際に制作したものです。


1. #### 学習内容及び期間

    |言語|期間|方法|
    |:--:|:--:|:--:|
    |C#|2ヵ月|参考書|

1. #### 制作方針

    * 少人数でも遊べるゲーム
    * 制作物の流用や修正を容易にする為、イメージしやすい範囲で疎結合にする
    * 今回はC#の学習という側面が強い為、コードのコピーはなるべく行わずにまずは考える

1. #### 動作内容

    1. Spaceキーでカメラを自機中心にリセット
    1. カーソルを画面外に出すとその方向へカメラが移動
    1. 右クリックした地点へのキャラクターの移動
    1. Qキーでキャラクターを中心とした投射物の発射
    1. Wキーで一定時間自機を加速
    1. Q、Wキーを押したときの効果を連続で使用できないように制限
    1. 敵（赤い立方体）へ投射物を命中させたときのダメージ計算
    1. 自機及び投射物の障害物への衝突

1. #### 努力点及び直面した課題

    1. [CameraControl.cs](Assets/Resources/Concretes/CameraControl.cs) l.79~  
    Spaceキーでカメラを自機中心にリセットする時にキャラクターの中心が画面の下辺中央に来てしまい、  
    手前側の視野が確保できない  
    →下記のようにz軸をずらすことで対処しました  
    `private void CameraReset()
    {
        if (Input.GetAxis("Jump") > 0){
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - transform.position.y / 1.5f);
        }
    }`
    1. [MouseControl.cs](Assets/Resources/Concretes/MouseControl.cs) l.12~  
    クリック位置と自機が移動する地点にずれが生じる。  
    →カーソル座標に自機を移動させる仕組みにしていましたが、カメラが傾斜している関係上、画面上のカーソルの位置とゲーム内のカーソルのy軸の位置が異なる為と判明しました  
    →下記のようにカメラから画面上のカーソル位置に線を投射し、地面レイヤーのオブジェクトに命中した点に向かって移動するようにしました  
    ```
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
    ```
    1. 自機とMob、あるいは破壊可能オブジェクトを将来的に実装する際、被弾時の挙動などをそれぞれに実装すると煩雑になるため、  
    下記抽象クラスやインタフェースに実装を分割しました
    * [CharacterAbstract.cs](Assets/Resources/Abstracts/CharacterAbstract.cs) 
    * [IControllable.cs](Assets/Resources/Interfaces/IControllable.cs)
    * [IDamageable.cs](Assets/Resources/Interfaces/IDamageable.cs)  


1. #### おわりに  

    ここまで読んでくださり、大変有難うございます。  
    現時点でゲームとは言い難いかもしれませんが、疎結合やオブジェクト指向言語の概念など前職で学習した部分も活かしつつ、  
    これからもUnity及びC#を勉強して参りますので何卒宜しくお願い致します。