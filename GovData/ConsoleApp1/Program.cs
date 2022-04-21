using ConsoleApp1;


List<DataToExcel> dataToExcelList = new List<DataToExcel>();
var apiHelper = new ApiHandling("https://dane.biznes.gov.pl");
int count = 0;
var intervalTimer = new System.Timers.Timer(15000);
intervalTimer.Elapsed += IntervalTimer_Elapsed;
intervalTimer.Enabled = true;
intervalTimer.AutoReset = true;
intervalTimer.Start();
Console.ReadKey();

async void IntervalTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
{
    var dataCompanys = await apiHelper.GetCompanys($"api/ceidg/v2/firmy?status=AKTYWNY&datado=2020-10-01&page={count}");
    if(dataCompanys is not null)
    {
        foreach (var firma in dataCompanys.Firmy)
        {

            var dataCompany = await apiHelper.GetPhoneNumber($"api/ceidg/v2/firma?nip={firma.Wlasciciel.Nip}");
            if (dataCompany is not null)
            {
                try
                {
                    DataToExcel toExcel = new DataToExcel()
                    {
                        Imie = firma.Wlasciciel.Imie,
                        Nazwisko = firma.Wlasciciel.Nazwisko,
                        Nazwa = firma.Nazwa,
                        Nip = firma.Wlasciciel.Nip,
                        Miasto = firma.AdresDzialalnosci.Miasto,
                        Wojewodztwo = firma.AdresDzialalnosci.Wojewodztwo,
                        DataRozpoczecia = firma.DataRozpoczecia,
                        Status = firma.Status,
                        Telefon = dataCompany.Firma[0].Telefon,
                    };
                    if (dataToExcelList.Contains(toExcel))
                        continue;
                    dataToExcelList.Add(toExcel);
                    ProjektCSV.CsvTools.AppendToCsvFile(dataToExcelList, "dane.csv", ";");
                }
                catch(Exception)
                {
                    continue;
                }
                
            }
            continue;
        }
        File.AppendAllText("count.csv", count.ToString()+ Environment.NewLine);
        count++;   
    }
}


