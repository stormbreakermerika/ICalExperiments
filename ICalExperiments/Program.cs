// See https://aka.ms/new-console-template for more information
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

Console.WriteLine("Hello, World!");

var cal = new Calendar();

var period = new Period(new CalDateTime(DateTime.UtcNow), new CalDateTime(DateTime.UtcNow.AddMinutes(10).AddDays(5)));

var freeBusy = new FreeBusy();
freeBusy.DtStart = new CalDateTime(DateTime.UtcNow);
freeBusy.DtEnd = new CalDateTime(DateTime.UtcNow.AddMinutes(10).AddDays(5));
freeBusy.Entries.Add(new FreeBusyEntry(period, FreeBusyStatus.Free));

cal.AddChild<FreeBusy>(freeBusy);

var reccurringRules = new RecurrencePattern("FREQ=DAILY;UNTIL=20240821T07:00:00.000Z");

cal.AddChild(new CalendarEvent()
{
    RecurrenceRules = new List<RecurrencePattern> { reccurringRules },
    Start = new CalDateTime(DateTime.UtcNow),
    End = new CalDateTime(DateTime.UtcNow.AddMinutes(10))
});

foreach (var item in cal.Events)
{
    Console.WriteLine(item.Start);
}

Console.WriteLine("recurrences");

foreach (var item in cal.GetOccurrences(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddMinutes(10).AddDays(5)))
{
    var freeBusy1 = new FreeBusy
    {
        DtStart = new CalDateTime(item.Period.StartTime),
        DtEnd = new CalDateTime(item.Period.EndTime),
        Entries = new List<FreeBusyEntry> { new FreeBusyEntry(item.Period, FreeBusyStatus.Free) }
    };
    cal.AddChild<FreeBusy>(freeBusy1);
    Console.WriteLine(item.Period.StartTime);
}
var serializer = new CalendarSerializer();
Console.WriteLine(serializer.SerializeToString(cal));
//output
// 5/24/2024 19:45:13 UTC
// recurrences
// 5/24/2024 19:45:13 UTC
// 5/25/2024 19:45:13 UTC
// 5/26/2024 19:45:13 UTC
// 5/27/2024 19:45:13 UTC
// 5/28/2024 19:45:13 UTC
// 5/29/2024 19:45:13 UTC
