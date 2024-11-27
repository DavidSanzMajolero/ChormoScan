using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaltonicManager : MonoBehaviour
{
	/*NORMAL - PROTAN - DEUTAN - DALTONICO (Ambos)

	
			TEST LIBRO: (POT SCRIPT)
			1.DERECHA: Daltonico
			2.Arriba: NO ERROR
			3.ABajo: Protan / Daltonico 
			4.ABajo: Protan

			(4-1-2-2)

			DOS POCIONES:
			1. Ambos

			TEST BANNERS:
			1 (74):
				-74: Normal
				-21: Daltonico
			2 (8):
				-8: Normal
				-3: Daltonico (leve)
			3 (45):
				-45: Normal
				-Otro: Daltonico
			4 (96):
				-96: Normal
				-6: Protan
				-9: Deutan

			32: Daltonico*/
	public float normal;
	public float protan;
	public float deutan;
	public float daltonic;

	public void SumNormal()
	{
		normal++;
	}
	public void SumProtan()
	{
		protan++;
	}
	public void SumDeutan()
	{
		deutan++;
	}
	public void SumDaltonic()
	{
		daltonic++;
	}
	public void ShowResults()
	{
		Debug.Log("Normal points: " + normal);
		Debug.Log("Protan points: " + protan);
		Debug.Log("Deutan points: " + deutan);
		Debug.Log("Daltonic points: " + daltonic);
	}
}
