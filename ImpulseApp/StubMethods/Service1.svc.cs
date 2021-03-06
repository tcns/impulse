﻿using ImpulseApp.Models.AdModels;
using ImpulseApp.Models.StatModels;
using ImpulseApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StubMethods
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class StubService : IStubService
    {


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }



        public string GenerateStats(int AdID, string beginDate, string endDate)
        {
            DatabaseService.DBServiceClient db = new DatabaseService.DBServiceClient();
            SimpleAdModel ad = db.GetAdById(AdID);
            DateTime bDate = DateTime.Parse(beginDate);
            DateTime eDate = DateTime.Parse(endDate);
            for (DateTime curDate = bDate; curDate.CompareTo(eDate) < 0; curDate = curDate.AddDays(1))
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int sessionCount = r.Next(10);
                for (int i = 0; i < sessionCount; i++)
                {
                    AdSession session = new AdSession
                    {
                        ActiveMilliseconds = r.Next(500),
                        AdId = AdID,
                        DateTimeStart = curDate,
                        DateTimeEnd = curDate.AddSeconds(r.Next(180)),
                        UserBrowser = StubUtils.GenerateBrowser(),
                        UserIp = StubUtils.GenerateIP(),
                        UserLocale = StubUtils.GenerateLocale(),
                        UserLocation = "TestLocation"
                    };
                    int ActivityCount = r.Next(5);
                    for (int j = 0; j < ActivityCount; j++)
                    {
                        NodeLink randomLink = ad.StateGraph.ElementAt(r.Next(ad.StateGraph.Count));
                        int rndNode = randomLink.V1;
                        int rndNodeNext = randomLink.V2;
                        int rand = r.Next(10);
                        if (rand > 5)
                        {
                            rndNode = randomLink.V2;
                            rndNodeNext = randomLink.V1;
                        }
                        Activity act = new Activity
                        {
                            StartTime = curDate,
                            EndTime = curDate,
                            CurrentStateName = ad.AdStates.First(a => a.VideoUnitId == rndNode).Name
                        };
                        for (int k = 0; k < 1; k++)
                        {

                            
                            Click c = new Click
                            {
                                ClickTime = curDate,
                                ClickType = "action-next",
                                ClickZone = "SubZone",
                                ClickCurrentStage = rndNode,
                                ClickNextStage = rndNodeNext,
                                ClickNextTime = randomLink.T
                            };
                            c.ClickText = Generator.GenerateClickName(c);
                            c.ClickStamp = Generator.GenerateClickStamp(c);
                            act.Clicks.Add(c);
                        };
                        session.Activities.Add(act);
                    }
                    db.SaveAdSession(session, true);
                }
            }
            return "success";
        }


        public string GenerateAbStats(int AbID)
        {
            DatabaseService.DBServiceClient db = new DatabaseService.DBServiceClient();
            ABTest ab = db.GetAbTestById(AbID);
            DateTime bDate = ab.DateStart;
            DateTime eDate = ab.DateEnd;
            for (DateTime curDate = bDate; curDate.CompareTo(eDate) < 0; curDate = curDate.AddDays(1))
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int sessionCount = r.Next(10);
                for (int i = 0; i < sessionCount; i++)
                {
                    int rand = r.Next(10);
                    int curId = ab.AdAId.Value;
                    SimpleAdModel ad = ab.AdA;
                    if (rand > 5)
                    {
                        ad = ab.AdB;
                        curId = ab.AdBId.Value;
                    }
                    AdSession session = new AdSession
                    {
                        ActiveMilliseconds = r.Next(500),
                        AdId = curId,
                        DateTimeStart = curDate,
                        DateTimeEnd = curDate.AddSeconds(r.Next(180)),
                        UserBrowser = StubUtils.GenerateBrowser(),
                        UserIp = StubUtils.GenerateIP(),
                        UserLocale = StubUtils.GenerateLocale(),
                        UserLocation = "TestLocation",
                        AbTestId = AbID
                    };
                    int ActivityCount = r.Next(5);
                    for (int j = 0; j < ActivityCount; j++)
                    {
                        NodeLink randomLink = ad.StateGraph.ElementAt(r.Next(ad.StateGraph.Count));
                        int rndNode = randomLink.V1;
                        int rndNodeNext = randomLink.V2;
                        rand = r.Next(10);
                        if (rand > 5)
                        {
                            rndNode = randomLink.V2;
                            rndNodeNext = randomLink.V1;
                        }
                        Activity act = new Activity
                        {
                            StartTime = curDate,
                            EndTime = curDate,
                            CurrentStateName = ad.AdStates.First(a => a.VideoUnitId == rndNode).Name
                        };
                        for (int k = 0; k < 1; k++)
                        {


                            Click c = new Click
                            {
                                ClickTime = curDate,
                                ClickType = "action-next",
                                ClickZone = "SubZone",
                                ClickCurrentStage = rndNode,
                                ClickNextStage = rndNodeNext,
                                ClickNextTime = randomLink.T
                            };
                            c.ClickText = Generator.GenerateClickName(c);
                            c.ClickStamp = Generator.GenerateClickStamp(c);
                            act.Clicks.Add(c);
                        };
                        session.Activities.Add(act);
                    }
                    db.SaveAdSession(session, true);
                }
            }
            return "success";
        }
    }
}
