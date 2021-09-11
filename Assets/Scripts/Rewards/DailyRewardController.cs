using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DailyRewardController
{
    private DailyRewardView _dailyRewardView;

    private List<ContainerRewardSlotView> _slots = new List<ContainerRewardSlotView>();

    public DailyRewardController(DailyRewardView dailyRewardView)
    {
        _dailyRewardView = dailyRewardView;
    }

    public void RefreshView()
    {
        InitSlots();
    }

    private void InitSlots()
    {
        for(var i = 0; i < _dailyRewardView.Rewards.Count; i++)
        {
            var instantSlot = Object.Instantiate(_dailyRewardView.ContainerRewardSlotView, _dailyRewardView.MountRootSlotsReward, false);
            _slots.Add(instantSlot);
        }
    }
}
