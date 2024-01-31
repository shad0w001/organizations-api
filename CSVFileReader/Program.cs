using CSVFileReader.DataManager;
using CSVFileReader.FileManager;
using CSVFileReader.LoginService;
using CSVFileReader.Models;
using CSVFileReader.Paths;

Console.ReadLine();

var loginModel = new LoginModel()
{
    Username = "Admin",
    Password = "admin"
};

var file = FileManager.GetFilePath(Paths.GetReadPath());

if(file is not null)
{
    var token = LoginService.Login(loginModel).Result;

    var organizations = DataManager.ReadOrganizationsFromCsv(file);

    await DataManager.SendDataToAPI(organizations, token);

    FileManager.MovieFile(file, Paths.GetWritePath());
}

