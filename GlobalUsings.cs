using NUnit.Framework;
using System.Text.RegularExpressions;

[TestFixture]
public class TicketTest
{
    [Test]
    public void TestTicketDataForAllFiles()
    {
        // Путь к папке с файлами
        var folderPath = "C:\\Users\\jdidi\\Downloads\\promit";

        // Получение всех файлов в папке
        var files = Directory.GetFiles(folderPath);

        // Ожидаемые значения для каждого файла
        var expectedValues = new Dictionary<string, Dictionary<string, string>>
        {
            { "Receipt_006", new Dictionary<string, string>
                {
                    { "Date", "2 9 . 0 7 . 2 0 2 3" },
                    { "DepartureStation", "Екатеринбург-Пассажирский         \r" },
                    { "ArrivalStation", "Нижний Тагил                      \r" },
                    { "TicketNumber", "00003" },
                    { "SystemNumber", "0001000000003" },
                    { "Transportation", "Разовый Полный -> П           30\r" },
                    { "TariffCost", "30.00" },
                    { "TotalCost", "30,00" }
                }
            },
            { "Receipt_007", new Dictionary<string, string>
                {
                    { "Date", "2 9 . 0 7 . 2 0 2 3" },
                    { "DepartureStation", "Московский вокзал                 \r" },
                    { "ArrivalStation", "Пл. 75 км                         \r" },
                    { "TicketNumber", "00004" },
                    { "SystemNumber", "0001000000004" },
                    { "Transportation", "Разовый Полный -> П           80\r" },
                    { "TariffCost", "80.00" },
                    { "TotalCost", "80,00" }
                }
            },
        };

        // Регулярные выражения для извлечения данных из файла
        var dateRegex = new Regex(@"на\s+(\d\s\d\s\.\s\d\s\d\s\.\s\d\s\d\s\d\s\d)");
        var departureStationRegex = new Regex(@"от\s+(.+)");
        var arrivalStationRegex = new Regex(@"до\s+(.+)");
        var ticketNumberRegex = new Regex(@"Билет N:\s+(\d+)");
        var systemNumberRegex = new Regex(@"Сист.N:\s+(\d+)");
        var transportationRegex = new Regex(@"Перевозка\s+(.+)");
        var tariffCostRegex = new Regex(@"Стоимость по тарифу:\s+=([\d\.]+)");
        var totalRegex = new Regex(@"ИТОГ:\s+([\d\.,]+)");

        // Проверка каждого файла
        foreach (var filePath in files)
        {
            // Получение имени файла без расширения
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            // Чтение содержимого файла
            var fileContent = File.ReadAllText(filePath);

            // Получение ожидаемых значений для текущего файла
            var expectedValuesForFile = expectedValues[fileName];

            // Проверка каждого значения
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
