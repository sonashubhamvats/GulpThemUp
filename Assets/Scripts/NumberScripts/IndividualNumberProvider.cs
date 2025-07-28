using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndividualNumberProvider : MonoBehaviour
{
    public Text numberOnTheEntity;
    public string realNumberForTheFriend;

  
    private void Awake() {
    }
    // Start is called before the first frame update
    
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AssisgnTheNumber(string no)
    {
        if (gameObject.tag == "Enemy")
        {
            int tempno = (int)double.Parse(no);
            tempno = tempno * -1;

            numberOnTheEntity.text = tempno.ToString();
        }
        else
        {
            numberOnTheEntity.text = realNumberForTheFriend;
        }
    }
    public void AssisgnTheCoverNumber(string no)
    {
        numberOnTheEntity.text=no;
    }
    public int returnCoverNumber(int multiplier)
    {
        int returnNo;

        returnNo=(int)((GameManager.instance.totalNoOnTheCover*multiplier)/NumberProvider.instance.sumOfIndexesOfLargeNumbers);
        if(returnNo==0)
        {
            returnNo++;
        }
        return returnNo;
    }
}
