using meetingClass;
using System.Text.Json;

string dirName = @"C:\Users\palil\Desktop\Internship\Meeting\First.JSON";
string command = "";

Meeting meetingas = new Meeting();

List<Meeting> meetings = new List<Meeting>();

if (File.Exists(dirName))
{
    using (StreamReader r = new StreamReader(dirName))
    {
        string json = r.ReadToEnd();
        meetings = JsonSerializer.Deserialize<List<Meeting>>(json);
    }
}

while (command != "Exit")
{
    Console.WriteLine("Available commands:");
    Console.WriteLine("'Create meeting', 'Delete meeting', 'Add person', 'Remove person', 'All meetings', 'All meetings filtered', 'Exit'");
    Console.WriteLine("Choose a command");
    command = Console.ReadLine();
    switch (command)
    {
        case "Create meeting":
            meetingas.AddMeetingToFile(meetings, dirName);
            break;
        case "Delete meeting":
            meetingas.DeleteMeetingFromFile(meetings, dirName);
            break;
        case "All meetings":
            meetingas.AllMeetingsPrint(meetings);
            break;
        case "All meetings filtered":
            meetingas.AllMeetingsFilteredPrint(meetings);
            break;
        case "Add person":
            meetingas.AddPersonToMeeting(meetings, dirName);
            break;
        case "Remove person":
            meetingas.RemovePersonFromAMeeting(meetings, dirName);
            break;
        default:
            break;
    }
}