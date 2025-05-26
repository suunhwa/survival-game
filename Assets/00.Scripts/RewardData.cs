using System;
using System.Collections.Generic;

[Serializable]
public class RewardData
{
    public string Name;             
    public string RewardList;     

    public List<string> RewardListParsed = new();  

    public void SplitRewards()
    {
        if (!string.IsNullOrEmpty(RewardList))
            RewardListParsed = new List<string>(RewardList.Split('|'));
    }
}
