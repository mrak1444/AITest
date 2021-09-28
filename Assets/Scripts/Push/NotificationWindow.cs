using UnityEngine;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using UnityEngine.UI;
using System;

public class NotificationWindow : MonoBehaviour
{
    private const string AndroidNotificationId = "android_notification_id";
    private const string IOSNotificationId = "ios_notification_id";

    [SerializeField] private Button _showNotificationButton;

    private void Start()
    {
        _showNotificationButton.onClick.AddListener(CreateNotifications);
    }

    private void OnDestroy()
    {
        _showNotificationButton.onClick.RemoveAllListeners();
    }

    private void CreateNotifications()
    {
#if UNITY_ANDROID
        CreateNotificationAndroid();
#elif UNITY_IOS
        CreateNotificationIOS();
#endif
    }

    private void CreateNotificationAndroid()
    {
        var androidSettingsChannel = new AndroidNotificationChannel
        {
            Id = AndroidNotificationId,
            Name = "Notifire",
            Description = "Description Notifire",
            Importance = Importance.High,
            CanBypassDnd = true,
            EnableLights = true,
            CanShowBadge = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingsChannel);

        var androidNotification = new AndroidNotification
        {
            //Color = Color.black,
            RepeatInterval = TimeSpan.FromMilliseconds(2),
            Text = "Test Notiffications"
        };

        AndroidNotificationCenter.SendNotification(androidNotification, AndroidNotificationId);
    }

    private void CreateNotificationIOS()
    {
        var iosNotification = new iOSNotification
        {
            Identifier = IOSNotificationId,
            Title = "IOS Notifire",
            Subtitle = "Subtitle IOS Notifire",
            Body = "Discription IOS Notifire",
            Data = "25/09/2021",
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound
        };

        iOSNotificationCenter.ScheduleNotification(iosNotification);
    }
}
