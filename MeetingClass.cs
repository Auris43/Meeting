using System.Text.Json;

namespace meetingClass
{
    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Atendees { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Meeting(string name, string responsiblePerson, string description,
            string category, string type,string atendees, string startDate, string endDate)
        {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            Atendees = atendees;
            StartDate = startDate;
            EndDate = endDate;
        }
        public Meeting()
        {
        }
        /// <summary>
        /// Function, that reads users inputs about meeting, and then ads it to the file.
        /// After successful or unsuccessful add, you'll get confirmation message.
        /// The file will be rewriten with new information.
        /// </summary>
        /// <param name="meetings">List of meetings from which we will get meetings data</param>
        /// <param name="dirName">Directory, where the file is located</param>
        /// <returns></returns>
        public List<Meeting> AddMeetingToFile(List<Meeting> meetings, string dirName)
        {
            Console.Clear();

            Meeting meeting = new Meeting();
            Console.WriteLine("Meeting name");
            meeting.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Responsible person");
            meeting.ResponsiblePerson = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Description");
            meeting.Description = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Available categories:");
            Console.WriteLine("Code Monkey/HUB/Short/Team building");
            string category = Console.ReadLine();
            if (category == "Code monkey" || category == "HUB" || category == "Short" ||
                category == "Team building")
            {
                meeting.Category = category;
            }
            else
            {
                Console.WriteLine("New meeting creation failed, unavailable category.");
                return meetings;
            }
            Console.Clear();

            Console.WriteLine("Available types:");
            Console.WriteLine("Live/In person");
            string type = Console.ReadLine();
            if (type == "Live" || type == "In person")
            {
                meeting.Type = type;
            }
            else
            {
                Console.WriteLine("New meeting creation failed, unavailable type.");
                return meetings;
            }
            Console.Clear();

            Console.WriteLine("Start date:");
            Console.WriteLine("DD/MM/YYYY HH:mm");
            meeting.StartDate = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("End date:");
            Console.WriteLine("DD/MM/YYYY HH:mm");
            meeting.EndDate = Console.ReadLine();

            meeting.Atendees = meeting.ResponsiblePerson + "," + meeting.StartDate.Split(" ")[1];

            meetings.Add(meeting);
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            File.Delete(dirName);
            File.WriteAllText(dirName, "[");
            for (int i = 0; i < meetings.Count; i++)
            {
                string json = JsonSerializer.Serialize(meetings[i], options);
                File.AppendAllText(dirName, json);
                if(i < meetings.Count-1)
                    File.AppendAllText(dirName, ",");
            }
            File.AppendAllText(dirName, "]");
            Console.WriteLine("Meeting has been added");
            return meetings;
        }
        /// <summary>
        /// Function, that reads users input and deletes selected meeting from file.
        /// After successful or unsuccessful deletion, you'll get confirmation message.
        /// The file will be rewriten with new information.
        /// </summary>
        /// <param name="meetings">List of meetings from which we will get meetings data</param>
        /// <param name="dirName">Directory, where the file is located</param>
        /// <returns></returns>
        public List<Meeting> DeleteMeetingFromFile(List<Meeting> meetings, string dirName)
        {
            Console.Clear();
            Console.WriteLine("Person, who wants to delete a meeting");
            string manager = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Name of the meeting, You want to delete");
            string meetingName = Console.ReadLine();
            string startDate = "";
            int count=0;
            for(int i = 0; i < meetings.Count; i++)
            {
                if(meetings[i].Name == meetingName)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("There are no meetings with such name.");
            }
            else if (count > 1)
            {
                Console.WriteLine("There are more than one meeting with such Name");
                for (int i = 0; i < meetings.Count; i++)
                {
                    if (meetings[i].Name == meetingName)
                    {
                        meetings[i].ToString();
                    }
                }
                Console.WriteLine("Choose which meeting to delete by writing start date");
                startDate = Console.ReadLine();
            }
            else;
            Console.WriteLine("Are You sure You want to delete this meeting?");
            Console.WriteLine("Yes/No");
            string confirmation = Console.ReadLine();
            if(confirmation == "No")
            {
                Console.WriteLine("Meeting deletion canceled.");
                return meetings;
            }
            for (int i = 0; i < meetings.Count; i++)
            {
                if(startDate == "")
                {
                    if (meetings[i].Name == meetingName)
                        if (meetings[i].ResponsiblePerson == manager)
                            meetings.Remove(meetings[i]);
                        else
                        {
                            Console.WriteLine(manager + " can't delete this meeting");
                            return meetings;
                        }
                }
                else
                {
                    if (meetings[i].Name == meetingName || meetings[i].StartDate == startDate)
                        if (meetings[i].ResponsiblePerson == manager)
                            meetings.Remove(meetings[i]);
                        else
                        {
                            Console.WriteLine(manager + " can't delete this meeting");
                            return meetings;
                        }
                }
            }
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            File.Delete(dirName);
            File.WriteAllText(dirName, "[");
            for (int i = 0; i < meetings.Count; i++)
            {
                string json = JsonSerializer.Serialize(meetings[i], options);
                File.AppendAllText(dirName, json);
                if (i < meetings.Count - 1)
                    File.AppendAllText(dirName, ",");
            }
            File.AppendAllText(dirName, "]");
            Console.WriteLine("Meeting has been Deleted");
            return meetings;
        }
        /// <summary>
        /// Function, that reads users input of meetings name, to which person should be added and persons name, who will be added to the meeting.
        /// The file will be rewriten with new information.
        /// After successful or unsuccessful add, you'll get confirmation message.
        /// </summary>
        /// <param name="meetings">List of meetings, where person will be added</param>
        /// <param name="dirName">Directory, where the file is located</param>
        /// <returns></returns>
        public List<Meeting> AddPersonToMeeting(List<Meeting> meetings, string dirName)
        {
            Console.Clear();
            Console.WriteLine("Name of the meeting");
            string meetingName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Name of the person who is being added");
            string name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("At what time the person is added");
            Console.WriteLine("HH:mm");
            string time = Console.ReadLine();
            Console.Clear();
            bool nameDuplicate = false;
            bool timeDuplicate = false;
            int atendeeCount = 0;
            int whichMeeting = 0;
            int count = 0;
            string meetingStartDate = "";
            string meetingStartHours = "";
            for (int i = 0; i < meetings.Count; i++)
            {
                if (meetings[i].Name == meetingName)
                {
                    whichMeeting = i;
                    atendeeCount = meetings[i].Atendees.Split(",").Length;
                    meetingStartDate = meetings[i].StartDate;
                    meetingStartHours = meetings[i].StartDate.Split(" ")[1];
                    count++;
                }
            }
            for (int i = 0; i < meetings.Count; i++)
            {
                if (meetings[i].Atendees.Split(",").Contains(name))
                {
                    if(meetings[i].StartDate.Split(" ")[0] == meetingStartDate.Split(" ")[0])                                                   //Checks if the meeting is at the same day, as the person's other meetings
                    {
                        if(Int32.Parse(meetings[i].StartDate.Split(" ")[1].Split(":")[0]) < Int32.Parse(meetingStartHours.Split(":")[1]))       //Checks if the meeting is at a latter hour, than the persons' other meetings
                        {
                            if(Int32.Parse(meetings[i].EndDate.Split(" ")[1].Split(":")[0]) > Int32.Parse(meetingStartHours.Split(":")[0]))     //Checks if the meeting overlaps with the other meeting, that the person is attending
                            {
                                timeDuplicate = true;
                            }
                        }
                        if(Int32.Parse(meetings[i].StartDate.Split(" ")[1].Split(":")[0]) == Int32.Parse(meetingStartHours.Split(":")[0]))      //Checks if the meeting is at the same hour, as the persons' other meetings
                        {
                            if(Int32.Parse(meetings[i].StartDate.Split(" ")[1].Split(":")[1]) < Int32.Parse(meetingStartHours.Split(":")[1]))   //Checks if the meetings minutes overlap with other person's meetings
                            {
                                if(Int32.Parse(meetings[i].EndDate.Split(" ")[1].Split(":")[0]) > Int32.Parse(meetingStartHours.Split(":")[0])) //Checks if the meeting is at the latter hour, and overlaps with other meetings
                                {
                                    timeDuplicate = true;
                                }
                            if(Int32.Parse(meetings[i].EndDate.Split(" ")[1].Split(":")[1]) > Int32.Parse(meetingStartHours.Split(":")[1])) //Checks if the meeting is at the latter minutes, and overlaps with other meetings
                                {
                                    timeDuplicate = true;
                                }
                            }   
                        }
                    }
                }
            }
            if (count != 1)
            {
                Console.WriteLine("There are more than one meeting with such name");
                return meetings;
            }
            for (int j = 0; j < atendeeCount; j += 2)
            {
                if (meetings[whichMeeting].Atendees.Split(",")[j] == name)
                {
                    nameDuplicate = true;
                }
            }
            if(nameDuplicate)
            {
                Console.Clear();
                Console.WriteLine("This person is already in this meeting");
                return meetings;
            }
            else if(timeDuplicate)
            {
                Console.WriteLine("This person is already in another meeting at this time");
                Console.WriteLine("Would you still like to add this person to the meeting?");
                Console.WriteLine("Yes/No");
                string confirmation = Console.ReadLine();
                Console.Clear();
                if (confirmation == "Yes")
                {
                    meetings[whichMeeting].Atendees += "," + name + "," + time;
                }
                else
                {
                    Console.WriteLine("Person wasn't added to the meeting");
                    return meetings;
                }
            }
            else
            {
                meetings[whichMeeting].Atendees += "," + name + "," + time;
            }
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            File.Delete(dirName);
            File.WriteAllText(dirName, "[");
            for (int i = 0; i < meetings.Count; i++)
            {
                string json = JsonSerializer.Serialize(meetings[i], options);
                File.AppendAllText(dirName, json);
                if (i < meetings.Count - 1)
                    File.AppendAllText(dirName, ",");
            }
            File.AppendAllText(dirName, "]");
            Console.WriteLine("Person has been added successfully");
            return meetings;
        }
        /// <summary>
        /// Function, that reads users input of meetings name, from which person should be removed and persons name
        /// The file will be rewriten with new information.
        /// After successful or unsuccessful remove, you'll get confirmation message.
        /// </summary>
        /// <param name="meetings">List of meetings from which the person will be removed</param>
        /// <param name="dirName">Directory, where the file is located</param>
        /// <returns></returns>
        public List<Meeting> RemovePersonFromAMeeting(List<Meeting> meetings, string dirName)
        {
            Console.Clear();

            Console.WriteLine("Name of the meeting");
            string meetingName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Name of the person who is removed");
            string name = Console.ReadLine();
            int count = 0;
            for (int i = 0; i < meetings.Count; i++)
            {
                if(meetings[i].Name == meetingName)
                {
                    count++;
                    if (meetings[i].Atendees.Split(",").Contains(name))
                    {
                        if(meetings[i].ResponsiblePerson != name)
                        {
                            for (int x = 0; x < meetings[i].Atendees.Split(",").Count(); x++)
                            {
                                if (meetings[i].Atendees.Split(",")[x] == name)
                                {
                                    int calc = meetings[i].Atendees.IndexOf(name);
                                    int length = name.Length + 7;
                                    meetings[i].Atendees = meetings[i].Atendees.Remove(calc-1, length);
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Can't remove responsible person from the meeting");
                            return meetings;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Meeting doesn't have atendee wth such name");
                        return meetings;
                    }
                }
            }
            if (count == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no meeting with such name");
                return meetings;
            }
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            File.Delete(dirName);
            File.WriteAllText(dirName, "[");
            for (int i = 0; i < meetings.Count; i++)
            {
                string json = JsonSerializer.Serialize(meetings[i], options);
                File.AppendAllText(dirName, json);
                if (i < meetings.Count - 1)
                    File.AppendAllText(dirName, ",");
            }
            File.AppendAllText(dirName, "]");
            Console.Clear();
            Console.WriteLine(name + " has been Deleted from the meeting");
            return meetings;
        }
        /// <summary>
        /// Function, that prints all meetings to console terminal.
        /// </summary>
        /// <param name="meetings">List of meetings from which we will get meetings data</param>
        public void AllMeetingsPrint(List<Meeting> meetings)
        {
            Console.Clear();
            Console.WriteLine("All recorded meetings unfiltered:");
            Console.WriteLine("");
            for (int i = 0; i < meetings.Count; i++)
            {
                Console.WriteLine("Name: " + meetings[i].Name);
                Console.WriteLine("Responsible person: " + meetings[i].ResponsiblePerson);
                Console.WriteLine("Description: " + meetings[i].Description);
                Console.WriteLine("Category: " + meetings[i].Category);
                Console.WriteLine("Type: " + meetings[i].Type);
                Console.WriteLine("Atendees: " + meetings[i].Atendees);
                Console.WriteLine("Start date: " + meetings[i].StartDate);
                Console.WriteLine("End date: " + meetings[i].EndDate);
                Console.WriteLine("");
            }
        }
        /// <summary>
        /// Function, that reads users input and prints all meetings with selected filter to console terminal.
        /// </summary>
        /// <param name="meetings">List of meetings from which we will get meetings data</param>
        public void AllMeetingsFilteredPrint(List<Meeting> meetings)
        {
            Console.Clear();
            Console.WriteLine("Filter by:");
            Console.WriteLine("'Description', 'Responsible person', 'Category', 'Type', 'Number of attendees', 'By date'");
            string filteredBy = "";
            string filter = Console.ReadLine();
            switch (filter)
            {
                case "Description":
                    Console.WriteLine("Description to filter by:");
                    filteredBy = Console.ReadLine();
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if (meetings[i].Description.Contains(filteredBy))
                        {
                            OneMeetingPrint(meetings[i]);
                        }
                    }
                    break;
                case "Responsible person":
                    Console.WriteLine("Responsible person to filter by:");
                    filteredBy = Console.ReadLine();
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if (meetings[i].ResponsiblePerson.Contains(filteredBy))
                        {
                            OneMeetingPrint(meetings[i]);
                        }
                    }
                    break;
                case "Category":
                    Console.WriteLine("Category to filter by:");
                    filteredBy = Console.ReadLine();
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if (meetings[i].Category.Contains(filteredBy))
                        {
                            OneMeetingPrint(meetings[i]);
                        }
                    }
                    break;
                case "Type":
                    Console.WriteLine("Type to filter by:");
                    filteredBy = Console.ReadLine();
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if (meetings[i].Type.Contains(filteredBy))
                        {
                            OneMeetingPrint(meetings[i]);
                        }
                    }
                    break;
                case "Number of attendees":
                    Console.WriteLine("Number of attendees to filter by:");
                    int countOfAtendees = Int32.Parse(Console.ReadLine());
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if ((meetings[i].Atendees.Split(",").Count()/2) > countOfAtendees || (meetings[i].Atendees.Split(",").Count() / 2) == countOfAtendees)
                        {
                            OneMeetingPrint(meetings[i]);
                        }
                    }
                    break;
                case "By date":
                    Console.WriteLine("Date from (DD/MM/YYYY):");
                    string dateFrom = Console.ReadLine();
                    Console.WriteLine("Date to (DD/MM/YYYY):");
                    string dateTo = Console.ReadLine();
                    int count = 0;
                    for (int i = 0; i < meetings.Count; i++)
                    {
                        if(Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[2]) >= Int32.Parse(dateFrom.Split("/")[2]) && 
                            Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[2]) <= Int32.Parse(dateTo.Split("/")[2]))
                        {
                            if (Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[1]) >= Int32.Parse(dateFrom.Split("/")[1]) &&
                            Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[1]) <= Int32.Parse(dateTo.Split("/")[1]))
                            {
                                if (Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[0]) >= Int32.Parse(dateFrom.Split("/")[0]) &&
                            Int32.Parse(meetings[i].StartDate.Split(" ")[0].Split("/")[0]) <= Int32.Parse(dateTo.Split("/")[0]))
                                {
                                    count++;
                                    OneMeetingPrint(meetings[i]);
                                }
                            }
                        }
                    }
                    if (count == 0)
                    {
                        Console.WriteLine("There are none meetings from " + dateFrom + " to " + dateTo);
                    }
                    break;
            }
        }
        /// <summary>
        /// Function that prints one selected meeting to console terminal
        /// </summary>
        /// <param name="meeting">Meeting, which will be printed to console terminal</param>
        public void OneMeetingPrint(Meeting meeting)
        {
            Console.WriteLine("Name: " + meeting.Name);
            Console.WriteLine("Responsible person: " + meeting.ResponsiblePerson);
            Console.WriteLine("Description: " + meeting.Description);
            Console.WriteLine("Category: " + meeting.Category);
            Console.WriteLine("Type: " + meeting.Type);
            Console.WriteLine("Atendees: " + meeting.Atendees);
            Console.WriteLine("Start date: " + meeting.StartDate);
            Console.WriteLine("End date: " + meeting.EndDate);
            Console.WriteLine("");
        }
    }
}
