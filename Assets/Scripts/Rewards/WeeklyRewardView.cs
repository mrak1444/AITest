using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeeklyRewardView : MonoBehaviour
{
    private const string CurrentSLotInActiveKey = nameof(CurrentSLotInActiveKey);
    private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

    [SerializeField] private float _timeCooldown = 604800;
    [SerializeField] private float _timeDeadline = 1209600;
    [SerializeField] private List<Reward> _rewardsWeekly;
    [SerializeField] private TMP_Text _timerNewReward;
    [SerializeField] private Transform _rootSlotsRewardWeekly;
    [SerializeField] private ContainerRewardSlotView _containerRewardSlotView;
    [SerializeField] private Button _getRewardButton;
    [SerializeField] private Button _resetButton;

    public Button ResetButton => _resetButton;

    public Button GetRewardButton => _getRewardButton;

    public ContainerRewardSlotView ContainerRewardSlotView => _containerRewardSlotView;

    public Transform RootSlotsRewardWeekly => _rootSlotsRewardWeekly;

    public TMP_Text TimerNewReward => _timerNewReward;

    public List<Reward> RewardsWeekly => _rewardsWeekly;

    public float TimeDeadline => _timeDeadline;

    public float TimeCooldown => _timeCooldown;

    public int CurrentSLotInActive
    {
        get => PlayerPrefs.GetInt(CurrentSLotInActiveKey, 0);
        set => PlayerPrefs.SetInt(CurrentSLotInActiveKey, value);
    }

    public DateTime? TimeGetReward
    {
        get
        {
            var data = PlayerPrefs.GetString(TimeGetRewardKey, null);
            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
            else
                PlayerPrefs.DeleteKey(TimeGetRewardKey);
        }
    }

    private void OnDestroy()
    {
        _getRewardButton.onClick.RemoveAllListeners();
        _resetButton.onClick.RemoveAllListeners();
    }
}