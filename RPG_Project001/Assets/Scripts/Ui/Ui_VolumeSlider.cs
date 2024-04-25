using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Ui_VolumeSlider : MonoBehaviour
{
    //ui �����̾��� value�� audiomixer parameter�� ��������ֱ� ���� Ŭ���� 
    public Slider slider;
    public string parameter; //audioMixer���� �Ķ������ �̸��� �������� �ֱ� ������ �̸� ��Ī���� ���ڿ� ������ ����

    [SerializeField] private AudioMixer audioMixer; //audiomixer�� ����ϱ� ���� ����
    [SerializeField] private float multiplier;    //�����̴� ���� ũ�⸦ �����ϴ� ���� 

    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(parameter,Mathf.Log10(_value) * multiplier); //multiplier�� ���� Ŀ������ ũ�� �ٲ� �׷��� log10�� �־���
        //Mathf.Log10 : �������ٴ� �۰�, �����ϰ� ���� ��ȭ�� ǥ�� 
    }
    public void LoadSlider(float _value)
    {
        slider.value = _value;
        if(_value >= 0.001f)//min���� 0.001f ���� ���� �ҷ��� �� ������ �Ѵ�.
        {
            slider.value = _value;
        }
    }
}
