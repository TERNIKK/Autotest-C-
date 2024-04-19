using NUnit.Framework;
using System.Text.RegularExpressions;

[TestFixture]
public class TicketTest
{
    [Test]
    public void TestTicketDataForAllFiles()
    {
        var folderPath = "C:\\Users\\jdidi\\Downloads\\promit";

        var files = Directory.GetFiles(folderPath);

        var expectedValues = new Dictionary<string, Dictionary<string, string>>
        {
            { "Receipt_006", new Dictionary<string, string>
                {
                    { "Date", "2 9 . 0 7 . 2 0 2 3" },
                    { "DepartureStation", "������������-������������         \r" },
                    { "ArrivalStation", "������ �����                      \r" },
                    { "TicketNumber", "00003" },
                    { "SystemNumber", "0001000000003" },
                    { "Transportation", "������� ������ -> �           30\r" },
                    { "TariffCost", "30.00" },
                    { "TotalCost", "30,00" }
                }
            },
            { "Receipt_007", new Dictionary<string, string>
                {
                    { "Date", "2 9 . 0 7 . 2 0 2 3" },
                    { "DepartureStation", "���������� ������                 \r" },
                    { "ArrivalStation", "��. 75 ��                         \r" },
                    { "TicketNumber", "00004" },
                    { "SystemNumber", "0001000000004" },
                    { "Transportation", "������� ������ -> �           80\r" },
                    { "TariffCost", "80.00" },
                    { "TotalCost", "80,00" }
                }
            },
        };

        var dateRegex = new Regex(@"��\s+(\d\s\d\s\.\s\d\s\d\s\.\s\d\s\d\s\d\s\d)");
        var departureStationRegex = new Regex(@"��\s+(.+)");
        var arrivalStationRegex = new Regex(@"��\s+(.+)");
        var ticketNumberRegex = new Regex(@"����� N:\s+(\d+)");
        var systemNumberRegex = new Regex(@"����.N:\s+(\d+)");
        var transportationRegex = new Regex(@"���������\s+(.+)");
        var tariffCostRegex = new Regex(@"��������� �� ������:\s+=([\d\.]+)");
        var totalRegex = new Regex(@"����:\s+([\d\.,]+)");

        foreach (var filePath in files)
        {
            Console.WriteLine($"�������� �����: {filePath}");

            var fileName = Path.GetFileNameWithoutExtension(filePath);

            var fileContent = File.ReadAllText(filePath);

            var expectedValuesForFile = expectedValues[fileName];

            Assert.AreEqual(expectedValuesForFile["Date"], dateRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["DepartureStation"], departureStationRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["ArrivalStation"], arrivalStationRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["TicketNumber"], ticketNumberRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["SystemNumber"], systemNumberRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["Transportation"], transportationRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["TariffCost"], tariffCostRegex.Match(fileContent).Groups[1].Value);
            Assert.AreEqual(expectedValuesForFile["TotalCost"], totalRegex.Match(fileContent).Groups[1].Value);
        }
    }
}
