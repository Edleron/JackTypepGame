using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //1881
    private bool _isStarted;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    private ResultCore result;
    public Button TurnButton;
    public GameObject Circle; 			
    public Text CoinsDeltaText; 		
    public Text WalletAmountText; 		
    public int TurnCost = 300;			
    public int CurrencyCoinsAmount = 2000;	
    public int PreviousCoinsAmount;

    private void Awake()
    {
        PreviousCoinsAmount = CurrencyCoinsAmount;
        WalletAmountText.text = CurrencyCoinsAmount.ToString();
    }

    public void TurnWheel()
    {
        if (CurrencyCoinsAmount >= TurnCost)
        {
            _currentLerpRotationTime = 0f;            

            int fullCircles = 5;            

            _finalAngle = -(fullCircles * 360 + DieAngleCalculates() - 60);
            _isStarted = true;

            PreviousCoinsAmount = CurrencyCoinsAmount;

            CurrencyCoinsAmount -= TurnCost;         

            StartCoroutine(HideCoinsDelta(false, TurnCost));
            StartCoroutine(UpdateCoinsAmount());
        }
    }

    private float DieAngleCalculates()
    {
        result = GameObject.FindGameObjectWithTag("PieTag").GetComponent<PieGenerate>().GetResult();
        return result.AtaFinalAngle;
    }

    private void GiveAwardByAngle()
    {
        switch ((int)_startAngle)
        {
            case 0:
                RewardCoins(1000);
                break;
            case -330:
                RewardCoins(200);
                break;
            case -300:
                RewardCoins(100);
                break;
            case -270:
                RewardCoins(500);
                break;
            case -240:
                RewardCoins(300);
                break;
            case -210:
                RewardCoins(100);
                break;
            case -180:
                RewardCoins(900);
                break;
            case -150:
                RewardCoins(200);
                break;
            case -120:
                RewardCoins(100);
                break;
            case -90:
                RewardCoins(700);
                break;
            case -60:
                RewardCoins(300);
                break;
            case -30:
                RewardCoins(100);
                break;
            default:
                RewardCoins(300);
                break;
        }
    }

    private void Update()
    {
        if (_isStarted || CurrencyCoinsAmount < TurnCost)
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 6f;

        _currentLerpRotationTime += Time.deltaTime;
       
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            Debug.LogError("asdad" + _finalAngle);
            _startAngle = _finalAngle % 360;

            //GiveAwardByAngle();
            Debug.LogError(result.AtaGain);
            StartCoroutine(HideCoinsDelta(true, result.AtaGain));
        }

        float t = _currentLerpRotationTime / maxLerpRotationTime;

        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void RewardCoins(int awardCoins)
    {
        CurrencyCoinsAmount += awardCoins;    
        StartCoroutine(UpdateCoinsAmount());
    }

    private IEnumerator HideCoinsDelta(bool value, int awardCoins)
    {       
        if (value)
        {
            CoinsDeltaText.GetComponent<Text>().color = new Color(0, 255, 50, 255);
            CoinsDeltaText.text = awardCoins.ToString();
            yield return new WaitForSeconds(1f);
            CoinsDeltaText.text = "";
        }
        else
        {
            CoinsDeltaText.GetComponent<Text>().color = new Color(255, 255, 255, 255);
            CoinsDeltaText.text = "-" + TurnCost;
            yield return new WaitForSeconds(1f);
            CoinsDeltaText.text = "";
        }
    }

    private IEnumerator UpdateCoinsAmount()
    {
        const float seconds = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            WalletAmountText.text = Mathf.Floor(Mathf.Lerp(PreviousCoinsAmount, CurrencyCoinsAmount, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        PreviousCoinsAmount = CurrencyCoinsAmount;
        WalletAmountText.text = CurrencyCoinsAmount.ToString();
    }
}
