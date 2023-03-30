using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core
{
    internal static class RandomCallScriptGenerator
    {

        static private Random _random;
        static private List<CallScript> _scriptList;

        static RandomCallScriptGenerator()
        {
            _random = new Random();
            _scriptList = GenerateScriptList();
        }

        static public CallScript GetRandomCallScript()
        {
            return _scriptList[_random.Next(_scriptList.Count)];
        }

        static private List<CallScript> GenerateScriptList()
        {
            var list = new List<CallScript>();

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Accident",
                    Address = "10 Main Street",
                    Caller_info = "Ted Grant, 123-456-7890",
                    Description = "Red pickup vs white passenger car",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "Vehicles still in roadway", "Tow truck en route for red pickup", "Tow truck on scene", "Tow truck is done, roadway is clear at this time" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Domestic Dispute",
                    Address = "5193 Trevor Drive",
                    Caller_info = "Refused",
                    Description = "Neighbor can hear yelling from house next door",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "No answer at door", "Made contact", "Both parties agreed to separate for evening" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Welfare Check",
                    Address = "225 Wailey Road, Apartment 12",
                    Caller_info = "Sarah, 987-654-3210",
                    Description = "Caller hasn't heard from nephew Greg in three days. Caller is out of town",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "Made contact, he advised he would call Complainant" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Fight",
                    Address = "225 Sprigg Drive",
                    Caller_info = "Refused",
                    Description = "5 people fighting in middle of roadway",

                },
                NumberUnitsNeeded = 3,
                CallCommentsToAdd = new List<string> { "Not seeing anyone in street", "Made contact with resident at 226 Spring Drive", "Resident advised their teenage children were arguing, verbal only. No longer arguing"}
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Traffic Investigation",
                    Address = "I65 NB @ MM 18.6 ",
                    Caller_info = "Julie, 564-879-0258",
                    Description = "Check for ladder in the right lane",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "out of car moving ladder", "Ladder has been moved off roadway" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Theft",
                    Address = "1995 Blueberry Plaza, Suite 1",
                    Caller_info = "Hunter, 489-159-7617",
                    Description = "Staff at shopping center report shoplifter stopped my loss prevention",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "Warrant check - Jon Doe 01/02/1990", "Transporting 1 male to county jail", "at county jail", "Clear from county" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Citizen Assistance",
                    Address = "1 Main Street",
                    Caller_info = "Paul, 486-122-3344",
                    Description = "Caller is are headquarters has questions about civil issue involving tennants",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "Civil issue,  caller referred to clerks office" }
            });
            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Accident",
                    Address = "2000 block West Valley Station Boulevard",
                    Caller_info = "Sarah, 159-753-9876",
                    Description = "three car motor vehicle accident, white pc, grey suv, red suv",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "Passenger complaint of neck pain, start ambulance", "Ambulance en route", "Ambulance on scene", "Patient treated on scene and declined transport" }
            });

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Accident",
                    Address = "1555 Blueberry Plaza",
                    Caller_info = "Ben Sanderson, 417-455-5468",
                    Description = "black van vs silver pc, parking lot section 8",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "Minor accident" }
            });

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Intoxicated Person",
                    Address = "255 Sanderson Circle",
                    Caller_info = "Ben, 810-644-5326",
                    Description = "Subject stumbling appeares under influence, partially in roadway. Wearing jeans, blue hoodie",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "Out on one male", "Warrant check, Cameron Poe, 03/29/1983", "Transporting subject to 289 Sanderson Circle","Subject inside" }
            });

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Drugs",
                    Address = "3573 Denton Drive, #B",
                    Caller_info = "Saul Silver, 561-994-2704",
                    Description = "Marijuana odor coming from neighbor's apartment downstairs",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "No answer at door" }
            });

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Warrant Service",
                    Address = "1 Main Street",
                    Caller_info = "Beau, 546-546-5460",
                    Description = "Caller is on station to turn himself in for warrant - warrant for misdemeanor theft",

                },
                NumberUnitsNeeded = 1,
                CallCommentsToAdd = new List<string> { "Warrant check - Beau Anderson 10-17-1985", "Warrant confirmed", "1 Male transported to county jail", "booking completed" }
            });

            list.Add(new CallScript()
            {
                callModel = new()
                {
                    CallType = "Burglary Report",
                    Address = "867 Foxwood Drive",
                    Caller_info = "Maggie, 112-223-4455",
                    Description = "Caller returned home to find back door broken and property stolen, homeowner already cleared residence",

                },
                NumberUnitsNeeded = 2,
                CallCommentsToAdd = new List<string> { "Clearing residence", "Residence clear, talking to complainant"}
            });


            return list;
        }



    }
}
