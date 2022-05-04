using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class ScoreSaveLoadManager{
	public static void SaveScore(Leaderboard score){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
	
		ScoreboardData data = new ScoreboardData(score);

		bf.Serialize(stream, data);
		stream.Close();
	}

	public static ScoreboardData LoadScore(){
		if(File.Exists(Application.persistentDataPath + "/player.sav")){
			BinaryFormatter bf = new BinaryFormatter();
		    FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

			ScoreboardData scores = bf.Deserialize(stream) as ScoreboardData;
			stream.Close();
			return scores;
		}
		return null;
		
		
	}
	public static bool CheckFile () {	
		return File.Exists(Application.persistentDataPath + "/player.sav");
	}
	
}


[Serializable]
public class ScoreboardData{

	public float[] scoreF;
	public string[] names;
	public int length;

	public ScoreboardData(Leaderboard board){
		int size = board.scores.Count;
		scoreF = new float[size];
		names = new string[size];
		length = size;
		ArrayList scoreList = new ArrayList();
		ArrayList nameList = new ArrayList();
		foreach(var pairs in board.scores){
			scoreList.Add(pairs.Key);
			nameList.Add(pairs.Value);
		}
		for(int i = 0; i < size; i++){
			scoreF[i] = (float)scoreList[i];
			names[i] = (string)nameList[i];
		}
	}

}

