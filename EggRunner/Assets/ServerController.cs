using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerController : MonoBehaviour
{
    //private string secretKey = "mySecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    public string addLevelURL =     "www2.cs.uregina.ca/~fries20v/CS476/addlevel.php";
    public string getLevelURL =     "www2.cs.uregina.ca/~fries20v/CS476/getlevel.php";
    public string getLevelsURL =    "www2.cs.uregina.ca/~fries20v/CS476/getlevels.php";
    public string addScoreURL =     "www2.cs.uregina.ca/~fries20v/CS476/addscore.php";
    public string getScoreURL =     "www2.cs.uregina.ca/~fries20v/CS476/getscore.php";
 
    public string levelString;

    // remember to use StartCoroutine when calling this function!
    public IEnumerator PostLevel(string author, string levelstring)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("author", author));
        wwwForm.Add(new MultipartFormDataSection("levelstring", levelstring));

        UnityWebRequest www = UnityWebRequest.Post(addLevelURL, wwwForm);
        yield return www.SendWebRequest(); // Wait until the download is done
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else {
            SceneManager.LoadScene("Menu");
        }
    }

    public IEnumerator GetLevel(int id){
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("id", id.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(getLevelURL, wwwForm);
        yield return www.SendWebRequest(); // Wait until the download is done
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            
        }
        else {
            levelString = www.downloadHandler.text;
        }

    }

    public IEnumerator GetLevels(int count, int offset){
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("count", count.ToString()));
        wwwForm.Add(new MultipartFormDataSection("offset", offset.ToString()));
        UnityWebRequest www = UnityWebRequest.Post(getLevelsURL, wwwForm);
        yield return www.SendWebRequest(); // Wait until the download is done
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else {
            levelString = www.downloadHandler.text;
        }
    }

    public IEnumerator PostScore(string player, int level, int score)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("player", player));
        wwwForm.Add(new MultipartFormDataSection("level", level.ToString()));
        wwwForm.Add(new MultipartFormDataSection("score", score.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(addScoreURL, wwwForm);
        yield return www.SendWebRequest(); // Wait until the download is done
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
    }

    public IEnumerator GetScore(int levelID)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

        wwwForm.Add(new MultipartFormDataSection("id", levelID.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(getScoreURL, wwwForm);
        yield return www.SendWebRequest(); // Wait until the download is done
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else {
            levelString = www.downloadHandler.text;
        }
    }


}