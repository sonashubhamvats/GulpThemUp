using System;
[Serializable]
public class BulletCountProvider
{
    public void provideAmmoAndCoverTotal(int level,float damageN,float damageP=0,int damageR=0,int damageM=0)
    {
        int noOfNormal=0,noOfPierce=0,noOfReflective=0,noOfMissile=0;
        //assinging the bullet count to the gamemanager's variables
        if(GameManager.instance.gun_type==1)
        {
            GameManager.instance.normalCount=(int)(200*1.5f);
        }
        else if(GameManager.instance.gun_type==2)
        {
            GameManager.instance.normalCount=(int)(100*1.6f);
            GameManager.instance.PiercingCount=(int)(100*1.6f);
        }
        else if(GameManager.instance.gun_type==3)
        {
            GameManager.instance.normalCount=(int)(60*1.7f);
            GameManager.instance.PiercingCount=(int)(60*1.6f);
            GameManager.instance.reflectiveCount=(int)(26*0.8f);
        }
        else if(GameManager.instance.gun_type==4)
        {
            GameManager.instance.normalCount=(int)(50*1.7f);
            GameManager.instance.PiercingCount=(int)(50*1.7f);
            GameManager.instance.reflectiveCount=(int)(16*0.85f);
            GameManager.instance.MissileCount=5;

        }
        if(damageP==0)
        {
            noOfNormal=200;

        }
        else if(damageR==0)
        {
            noOfNormal=100;
            noOfPierce=100;

        }
        else if(damageM==0)
        {
            noOfNormal=60;
            noOfPierce=60;
            noOfReflective=26;
        }
        else
        {
            noOfNormal=noOfPierce=50;
            noOfReflective=16;
            noOfMissile=5;

        }
        //total cover count on the numbers
        GameManager.instance.totalNoOnTheCover=(int)(noOfNormal*damageN+noOfPierce*damageP+
        noOfReflective*damageR+noOfMissile*damageM);
        
    }
}
