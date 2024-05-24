// See https://aka.ms/new-console-template for more information
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

Console.WriteLine("Hello, World!");

var cal = new Calendar();

var reccurringRules = new RecurrencePattern("FREQ=DAILY;UNTIL=20240721T07:00:00.000Z");

cal.AddChild(new CalendarEvent()
{
    RecurrenceRules = new List<RecurrencePattern> { reccurringRules },
    Start = new CalDateTime(DateTime.UtcNow)
});

foreach (var item in cal.Events)
{
    Console.WriteLine(item.Start);
}

Console.WriteLine("recurrences");

foreach (var item in cal.GetOccurrences(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddMinutes(10).AddDays(5)))
{
    Console.WriteLine(item.Period.StartTime);
}
//output
// 5/24/2024 19:45:13 UTC
// recurrences
// 5/24/2024 19:45:13 UTC
// 5/25/2024 19:45:13 UTC
// 5/26/2024 19:45:13 UTC
// 5/27/2024 19:45:13 UTC
// 5/28/2024 19:45:13 UTC
// 5/29/2024 19:45:13 UTC
