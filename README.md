# BroadageSportsCase
Projede genel olarak entity framework dbfirst yapısını kullandım.Console application'da API ' den gelen verileri ilişkisel biçimde veri tabanına ekledim.
ASP.Net CORE MVC projesinde ise Manage Nuget Packages üzerinden Microsoft.EntityFrameworkCore,Microsoft.EntityFrameworkCore.SqlServer,Microsoft.EntityFrameworkCore.Design packagelerini yükledikten sonra Package Manager Console'a 
Scaffold-DbContext "Server=.;Database=BroadageSports;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
komutunu çalıştırarak MSSQL' de oluşturduğum db yapısını Model klasörüne getirmiş oldum.Db ile olan iletişimde Dependency Injection kullandım.View kısmında basit bir tablo oluşuturup listeleme yaptım sadece.
