using System;
using System.Linq;
using System.Text.RegularExpressions;
using Client;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class SyncClockTime : IInitializable
{
    public string ClockTime { get; private set; }
    private const string URL = "https://yandex.com/time/sync.json?geo=213";
    public Clock Clock { get; private set; }
    
    public void Initialize()
    {
        GetTimeFromAPI();
    }

    private async void GetTimeFromAPI()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            await webRequest.SendWebRequest();
            
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"URL Error: {webRequest.error}");
            }
            else
            {
                var jsonResponse = webRequest.downloadHandler.text;
                SetClock(jsonResponse);
            }
        }
    }

    private void SetClock(string jsonResponse)
    {
        var replace = Regex.Replace(jsonResponse, @"[""\""{""}""]", "").Split(',').FirstOrDefault(text => text.StartsWith("time"))?.Split(':');
        var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(replace[1])).UtcDateTime.AddHours(3.0);
        Clock = new Clock(dateTime.Second + dateTime.Minute * 60 + dateTime.Hour * 3600);
    }
}
