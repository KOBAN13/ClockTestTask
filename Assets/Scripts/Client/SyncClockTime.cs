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
    public ClockDigital ClockDigital { get; private set; }
    public ClockAnalog ClockAnalog { get; private set; }
    
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
                SetClockDigital(jsonResponse);
                SetClockAnalog(jsonResponse);
            }
        }
    }

    private string[] GetFormattedStringTime(string jsonResponse)
    {
        return Regex.Replace(jsonResponse, @"[""\""{""}""]", "")
            .Split(',')
            .FirstOrDefault(text => text.StartsWith("time"))?
            .Split(':');
    }

    private void SetClockDigital(string jsonResponse)
    {
        var replace = GetFormattedStringTime(jsonResponse);
        var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(replace[1])).UtcDateTime.AddHours(3.0);
        ClockDigital = new ClockDigital(dateTime.Second + dateTime.Minute * 60 + dateTime.Hour * 3600);
    }

    private void SetClockAnalog(string jsonResponse)
    {
        ClockAnalog = new ClockAnalog();
    }
}
