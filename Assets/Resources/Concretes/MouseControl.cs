using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�}�E�X����Ɋւ���N���X
public class MouseControl
{
    //�N���b�N�����ӏ��̒n�ʂ̍��W�����߂�
    //�X�N���[����̍��W
    //�Ȃɂ�������Ȃ�������null
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
    //�N���b�N�����ӏ��̒n�ʂ̍��W�����߁A�Ώۂ̍������␳����
    //�X�N���[����̍��W
    //�Ȃɂ�������Ȃ�������null
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
