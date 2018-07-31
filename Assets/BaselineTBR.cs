using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaselineTBR : MonoBehaviour 
{
	private ReadUDP readUDP;
	private double input;
	public double baseline;
	public double[] arrayTBR;
	public double[] arrayTime;
	public int index,calibrateTime;
	private int i;
	private double sum;
	public double min,min2;
	public double max,max2;
	void Start()
	{
		GameObject readUDPObject = GameObject.FindWithTag("ReadUDP");
        if(readUDPObject != null)
        {
            readUDP = readUDPObject.GetComponent<ReadUDP>();
        }
        if(readUDPObject==null)
        {
            Debug.Log("'BaselineTBR' cannot find 'ReadUDP' script");
		}
		arrayTBR=new double[10000];
		arrayTime=new double[10000];
		index=0;
		//min=999999f;
		//min2=999999f;
	}
	void Update()
	{
		//comment
		input=readUDP.dataChanged;
		if(input<min2&&input>0)min2=input;
		if(input>max2&&input>0)max2=input;
		if(baseline==0)
		{
			input=readUDP.dataChanged;
			arrayTBR[index]=input;
			arrayTime[index++]=input;
			if(input<min&&index>5)min=input;
			if(input>max&&input>0)max=input;
			print("Index: "+index);
			print("Value: "+arrayTBR[index-1]);
		}
		if(baseline==0&&index==calibrateTime)
			CalculateBaseline();
	}
	private void CalculateBaseline()
	{
		sum=0f;
		for(i=0;i<index;i++)
			sum+=arrayTBR[i];
		baseline=sum/(double)index;
	}
}
