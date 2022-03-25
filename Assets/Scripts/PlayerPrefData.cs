using UnityEngine;

public class PlayerPrefData : MonoBehaviour
{
    public void LoadPPData()
    {
        if (!PlayerPrefs.HasKey("FirstGamePlay") || PlayerPrefs.GetString("FirstGamePlay") == "Yes") //Is this first game play
        {
            PlayerPrefs.DeleteAll();
            SetFirstGamePlay("No");
            SetHeart(0);
            SetCurrentLevel(1);
            SetHighLevel(1);
            SetTotalDiamond(0);
        }

        SetCurrentDiamond(0);
    }

    public void ResetPPData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetCollectableItems()
    {
        SetCurrentDiamond(0);
        //SetHeart(0);
    }
    
    public void CurrentDiamondIncrease()
    {
        SetCurrentDiamond(GetCurrentDiamond() + 1);
    }

    public void HeartIncrease()
    {
        SetHeart(GetHeart() + 1);
    }

    public void HeartDecrease()
    {
        SetHeart(GetHeart() - 1);
    }

    public void AddCurrentDiamondOnTotalDiamond()
    {
        SetTotalDiamond(GetTotalDiamond() + GetCurrentDiamond());
    }

    public void CurrentLevelIncrease()
    {
        SetCurrentLevel(GetCurrentLevel() + 1);
    }
    
    public string GetFirstGamePlay()
    { 
        return PlayerPrefs.GetString("FirstGamePlay");
    }

    public int GetHeart()
    {
        return PlayerPrefs.GetInt("Heart");
    }

    public int GetCurrentDiamond()
    {
        return PlayerPrefs.GetInt("CurrentDiamond");
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    public int GetHighLevel()
    {
        return PlayerPrefs.GetInt("HighLevel");
    }

    public int GetTotalDiamond()
    {
        return PlayerPrefs.GetInt("TotalDiamond");
    }

    public void SetFirstGamePlay(string value)
    {
        PlayerPrefs.SetString("FirstGamePlay", value);
    }

    public void SetHeart(int value)
    {
        PlayerPrefs.SetInt("Heart", value);
    }
    
    public void SetCurrentDiamond(int value)
    {
        PlayerPrefs.SetInt("CurrentDiamond", value);
    }
    
    public void SetCurrentLevel(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }
   
    public void SetHighLevel(int value)
    {
        PlayerPrefs.SetInt("HighLevel", value);
    }
    
    public void SetTotalDiamond(int value)
    {
        PlayerPrefs.SetInt("TotalDiamond", value);
    }


}
