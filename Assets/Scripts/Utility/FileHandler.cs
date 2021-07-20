using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FileHandler : MonoBehaviour
{
    private string path = "Assets/Resources/Misc/HighScores_StampfGraben.txt";

    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Highscores: \n");
        }
    }

    public static string ReadFile(string path)
    {
        var streamReader = new StreamReader(path);
        string fileContent = streamReader.ReadToEnd();
        streamReader.Close();
        return fileContent;
    }

    public void CalculateHighScore(string finalTime)
    {
        double[] _finalTime = ConvertTimeStringToIntArr(finalTime);
        string fileContent = ReadFile(path);
        List<string> Lines = fileContent.Split('\n').ToList();
        double[] previousTimes = ConvertTimeListIntoIntArr(Lines);
        List<double> resultList = new List<double>();
        for (int i = 0; i < previousTimes.Length; i += 2)
        {
            if (_finalTime[0] <= previousTimes[i]) //then we have the upper cutoff for the minutes i.e. placement cannot be any higher. now we do the same for the seconds
            {
                for (int j = i + 1; j < previousTimes.Length; j += 2)
                {
                    if (_finalTime[1] <= previousTimes[j]) //then we have the placement of our player
                    {
                        var playerPlacement = j-1;
                        resultList = previousTimes.ToList();
                        resultList.Insert(playerPlacement, _finalTime[0]);
                        resultList.Insert(playerPlacement+1, _finalTime[1]);
                    }
                }

                File.WriteAllText(path, "Highscores: \n");
                int placement = 0;
                for (int k = 0; k < resultList.Count - 1; k += 2)
                {
                    placement++;
                    string content = (placement + ". " + resultList[k].ToString() + ":" + resultList[k + 1].ToString() + "\n");
                    File.AppendAllText(path, content);
                }

                return;
            }
        }
    }

    private double[] ConvertTimeListIntoIntArr(List<string> list)
    {
        List<double> doubleList = new List<double> { };
        for (int i = 1; i < list.Count-1; i++) //we start at one bc the first line of the list is "Highscores:" and thus not a time we want as int
        {
            var initArr = list[i].Split(' '); //split into "rank" "time" "at" "date"
            var arr = initArr[1].Split(':'); //split into minutes and seconds
            for (int j = 0; j < arr.Length; j++)
            {
                doubleList.Add(Double.Parse(arr[j]));
            }
        }

        var doubleArr = doubleList.ToArray();
        return doubleArr;
    }
    
    private double[] ConvertTimeStringToIntArr(string time)
    {
        var arr = time.Split(':');
        var floatArr = new double[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            floatArr[i] = Double.Parse(arr[i]);
        }
        
        return floatArr;
    }
}
