using System;
using System.Collections;


namespace Assets.Scripts
{
    public class PlayerClass
    {
        public int Id;
        public string pcName;
        public double pcHealth;
        public double pcHealthLost;
        public double pcBulletTotal;
        public double pcBulletsUsed;
        public double pcLives;
        public double pcLivesLost;
        public double pctimeTotal;
        public double pctimePlayed;
        public double pcHealthLeft;
        public double pcBulletsLeft;
        public double pcLivesLeft;
        public double pcTimeLeft;
        public double pcEnemySpeed;
        public double pcStateValue;
        public double pcBossSpawnChance;
        public bool pcSpawnBoss;
        public string pcState;
        public bool pcCheckPoint1;
        public DateTime? pcCheckPoint1Time = null;
        public bool pcCheckPoint2;
        public DateTime? pcCheckPoint2Time = null;
        public bool pcCheckPoint3;
        public DateTime? pcCheckPoint3Time = null;
        public bool pcCheckPoint4;
        public DateTime? pcCheckPoint4Time = null;
        public DateTime? pcTimeStamp;

        public PlayerClass()
        {

        }

        public void Save()
        {
            Assign(SendData.POSTData(this, "/save"));
        }

        public void Update()
        {
            Assign(SendData.POSTData(this, "/update"));
        }

        public static PlayerClass Load(int ID)
        {
            return SendData.GETData(ID.ToString());
        }

        public static PlayerClass Load(string name)
        {
            return SendData.GETData(name);
        }

        private void Assign(PlayerClass pc)
        {
            try
            {
                //Id = pc.Id;
				pcName = pc.pcName;
				pcHealth = pc.pcHealth;
				pcHealthLost = pc.pcHealthLost;
				pcBulletTotal = pc.pcBulletTotal;
				pcBulletsUsed = pc.pcBulletsUsed;
				pcLives = pc.pcLives;
				pcLivesLost = pc.pcLivesLost;
				pctimeTotal = pc.pctimeTotal;
				pctimePlayed = pc.pctimePlayed;
				pcHealthLeft = pc.pcHealthLeft;
				pcBulletsLeft = pc.pcBulletsLeft;
				pcLivesLeft = pc.pcLivesLeft;
				pcTimeLeft = pc.pcTimeLeft;
				pcEnemySpeed = pc.pcEnemySpeed;
                pcStateValue = pc.pcStateValue;
				pcBossSpawnChance = pc.pcBossSpawnChance;
				pcSpawnBoss = pc.pcSpawnBoss;
				pcState = pc.pcState;
				pcCheckPoint1 = pc.pcCheckPoint1;
				pcCheckPoint1Time = pc.pcCheckPoint1Time;
				pcCheckPoint2 = pc.pcCheckPoint2;
				pcCheckPoint2Time = pc.pcCheckPoint2Time;
				pcCheckPoint3 = pc.pcCheckPoint3;
				pcCheckPoint3Time = pc.pcCheckPoint3Time;
				pcCheckPoint4 = pc.pcCheckPoint4;
				pcCheckPoint4Time = pc.pcCheckPoint4Time;
				pcTimeStamp = pc.pcTimeStamp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}