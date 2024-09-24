using System;
using System.Linq;
using System.Text.RegularExpressions;
using Client;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class SyncClockTime : IInitializable, IDisposable
{
    private const string URL = "https://yandex.com/time/sync.json?geo=213";
    private IDisposable _disposable;
    private ClockController _clockController;

    public SyncClockTime(ClockController clockController)
    {
        _clockController = clockController;
    }

    public void Initialize()
    {
        GetTimeFromAPI();
        SubscribeTimer();
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
                SetClockAnalog();
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
        _clockController.SetDigitalClock(new ClockDigital(dateTime.Second + dateTime.Minute * 60 + dateTime.Hour * 3600));
    }
    
    private void SetClockAnalog()
    {
        _clockController.SetAnalogClock(new ClockAnalog());
    }

    private void SubscribeTimer()
    {
        _disposable = Observable.Timer(TimeSpan.FromHours(1f), TimeSpan.FromHours(1f))
            .Subscribe(_ => GetTimeFromAPI());
    }

    private void UnsubscribeTimer()
    {
        _disposable.Dispose();
    }

    public void Dispose()
    {
        UnsubscribeTimer();
    }
}
