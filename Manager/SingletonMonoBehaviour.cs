using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
    }
    protected bool CheckInstance()
    {
        //�C���X�^���X��null�Ȃ玩�g��o�^
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        //�C���X�^���X�������Ȃ�true
        else if (Instance == this)
        {
            return true;
        }
        //��L��if���Ɉ���������Ȃ������玩�g���폜
        Destroy(this);
        return false;
    }
}