using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

/*
This script deals with serialization and deserialization of save files,
utilizing the binary formatters, implementing the IDisposable interface.
*/

public static class SaveSystem
{
    const string fileExtension = "/Player.lmao";

    public static void SavePlayer(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path =  Path.Combine(Application.persistentDataPath, fileExtension);
        using(FileStream stream = new FileStream(path, FileMode.Create)){
            PlayerData data = new PlayerData();
        
            formatter.Serialize(stream, data);
            stream.Close();
        }         
    }

    public static PlayerData LoadPlayer(){
        string path = Path.Combine(Application.persistentDataPath, fileExtension);

        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = new PlayerData();

            using(FileStream stream = new FileStream(path, FileMode.Open)){
                data = (PlayerData)formatter.Deserialize(stream);
                stream.Close();
            }            
            return data;
        }
        else{
            Debug.LogError("File not found at" + path);
            return null;
        }
    }
}
