using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberProvider : MonoBehaviour
{
    //it is the total that every level will adhere to
    public int LevelTotal;
    //it is the total on the player
    public int OnPlayerNumber;
    //total no of scenes
    public int noOfScenes;
    //total no for each scene
    public int sceneTotal;
    //text on the playert
    
    //finding the sum to use it in assiging the cover nos
    public int sumOfIndexesOfLargeNumbers;
    public TextMeshProUGUI text;

    public static NumberProvider instance;
    private void Awake()
    {
        MakeInstance();
        LevelTotal=GameManager.instance.LevelTotal;
        noOfScenes=GameManager.instance.noOfScenes;
        sceneTotal = LevelTotal / noOfScenes;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void MakeInstance()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public int[] numberProviderForEachScene(bool friend,int totalNumbers)
    {
        //refer to the rough notebook to know about how the algo works
        int[] tempArrayOfSeparateNos = new int[totalNumbers];
        int[] finalArrayOfSeparateNos = new int[totalNumbers];

        if (friend==true)
        {
            int total = sceneTotal;
            int divider = (totalNumbers * (totalNumbers + 1)) / 2;
            
            int tempsum = 0;
            for (int i=0;i<totalNumbers;i++)
            {
                if (i != (totalNumbers - 1))
                {
                    
                    float x = (float)(i + 1) / divider;
                    
                    tempArrayOfSeparateNos[i] = (int)(x * sceneTotal);
                    
                }
                else
                    tempArrayOfSeparateNos[i] = total - tempsum;
                tempsum += tempArrayOfSeparateNos[i];
            }
            //randomizing the skip of numbers
            int indexToSkip = Random.Range(1, 4);
            

            for(int i=0;i<totalNumbers;i++)
            {

                int indexToExchange = i + indexToSkip;
                if (indexToExchange > (totalNumbers - 1))
                {
                    indexToExchange = indexToExchange % totalNumbers;
                   
                }
                finalArrayOfSeparateNos[indexToExchange] = tempArrayOfSeparateNos[i];
                
            }

        }
        else
        {
            OnPlayerNumber = (int)double.Parse(text.text);
            var multiplier=1.5f;
            var levelNo=GameManager.instance.levelno;
            if(levelNo>=8&&levelNo<=15)
            {
                multiplier=1.45f;
            }
            else if(levelNo>=16&&levelNo<=24)
            {
                multiplier=1.2f;
            }
            else if(levelNo>=25&&levelNo<=32)
            {
                multiplier=1.2f;
            }
            else if(levelNo>32)
            {
                multiplier=1.2f;
            }

            int tempSceneTotalInEnemy;
            //just making sure that the no on the enemy is never 0 or or in positive
            tempSceneTotalInEnemy=(int)((OnPlayerNumber-GameManager.instance.objNos[0])*multiplier);
            int divider = (totalNumbers * (totalNumbers + 1)) / 2;
            int tempSum = 0;
            for(int i=0;i<totalNumbers;i++)
            {
                if (i != (totalNumbers - 1))
                {
                    float x = (float)(i + 1) / divider;
                    tempArrayOfSeparateNos[i] = (int)(x * tempSceneTotalInEnemy);
                    
                }
                else
                    tempArrayOfSeparateNos[i] = tempSceneTotalInEnemy - tempSum;
                tempSum += tempArrayOfSeparateNos[i];

            }
            //randomizing the skip of numbers
            int indexToSkip = Random.Range(1, 4);


            for (int i = 0; i < totalNumbers; i++)
            {

                int indexToExchange = i + indexToSkip;
                if (indexToExchange > (totalNumbers - 1))
                {
                    indexToExchange = indexToExchange % totalNumbers;

                }
                finalArrayOfSeparateNos[indexToExchange] = tempArrayOfSeparateNos[i];

            }
        }
        return finalArrayOfSeparateNos;

    }//number provider for each scene
    public int[] scaleProviderForTheNumbers(int totalNumbers,int[] ArrayOfTherNumbers)
    {
       
        int[] scaleOfTheNumbers = new int[totalNumbers];
        int[] dummyScaleOfNumbers = new int[totalNumbers];

        //assiging the dummyscale
        int tempscale = 5;
        int totalInterval = totalNumbers / 3;
        int tempSumOfTheIndexesOfTheLargestNumbers=0;
        int tempTotalNumbers=totalNumbers; 
        int tempinterval = 0;
        for(int i=0;i<totalNumbers;i++)
        {
            if(tempinterval<totalInterval)
            {
                dummyScaleOfNumbers[i] = tempscale;
                tempinterval += 1;
                if(tempscale>3)
                {
                    tempSumOfTheIndexesOfTheLargestNumbers+=tempTotalNumbers;
                    
                    tempTotalNumbers-=1;
                }
            }
            else
            {
                if (tempscale > 3)
                {

                    tempscale -= 1;
                }
                if(tempscale>3)
                {
                    tempSumOfTheIndexesOfTheLargestNumbers+=tempTotalNumbers;
                    tempTotalNumbers-=1;

                }
                dummyScaleOfNumbers[i] = tempscale;
                tempinterval = 1;
            }
        }
        //assiging the sum of indexes
        sumOfIndexesOfLargeNumbers=tempSumOfTheIndexesOfTheLargestNumbers;
        
        int[] tempArrayOfNumbers = new int[totalNumbers];
        int[] rankWithIndexes = new int[totalNumbers];
        //assigning the values of array to the temparray
        for(int i=0;i<totalNumbers;i++)
        {
            tempArrayOfNumbers[i] = ArrayOfTherNumbers[i];
        }
        //sorting the temparray
        for(int i=0;i<totalNumbers;i++)
        {
            for(int j=i+1;j<totalNumbers;j++)
            {
                if(tempArrayOfNumbers[i]>tempArrayOfNumbers[j])
                {
                    int t = tempArrayOfNumbers[i];
                    tempArrayOfNumbers[i] = tempArrayOfNumbers[j];
                    tempArrayOfNumbers[j] = t;
                }
            }
        }
        //Assigning the rankindexes
        for (int i=0;i<totalNumbers;i++)
        {
            int numberToFind = tempArrayOfNumbers[i];
            
            for(int j=0;j<totalNumbers;j++)
            {
                if(numberToFind==ArrayOfTherNumbers[j])
                {
                    rankWithIndexes[i] = j;
                    break;
                }
            }
        }
        //assiging the scale
        for(int i=0;i<totalNumbers;i++)
        {
            scaleOfTheNumbers[rankWithIndexes[i]] = dummyScaleOfNumbers[totalNumbers-1-i];
        }
        return scaleOfTheNumbers;
        //tatti logic and code but kya karen ab jo hai so hai kaam to kar hi rha hai
    }

}
