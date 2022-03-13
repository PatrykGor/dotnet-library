# dotnet-library - Patryk Górski

## Instrukcje

Plik sql nie jest potrzebny (projekt korzysta z wtyczki MySql do Entity Framework Core).
Aby baza danych działała wystarczy zmienić wartość "MySql" w sekcji "ConnectionStrings" w pliku appsettings.json w projekcie Biblioteka.Portal:

<pre> 
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    <b>"MySql": "server=localhost;database=librarydb;user=root;"</b>
  }
}
</pre>

## Rozwiązywanie problemów

Jeśli pojawiłyby się jakieś problemy z bazą można wygenerować ponownie migracje z podanymi komendami:

### W konsoli menadżera pakietów będąc w projekcie Biblioteka.Portal:
![image](https://user-images.githubusercontent.com/57003717/158077421-a321ef66-c706-4644-86f8-603884270cf3.png)

### W konsoli systemowej będąc w projekcie Biblioteka.Portal (nietestowane):
```ps
dotnet ef database update
```

## Dodatkowe informacje

Projekt jest również dostępny na [GitHub](https://github.com/PatrykGor/dotnet-library/).
