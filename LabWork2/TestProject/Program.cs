var fileName = "MOCK_DATA.csv";
var filePath = Path.Combine(Environment.CurrentDirectory, fileName);

if (!File.Exists(filePath))
{
    Console.WriteLine("Файла нет!");
    return;
}

int correct = 0;
int incorrect = 0;

using (StreamReader reader = new(filePath))
{
    string line;
    string[] data;
    int count = 0;
    while (!reader.EndOfStream)
    {
        line = await reader.ReadLineAsync();
        data = line.Split(';');
        count++;

        int age;
        if (!int.TryParse(data[2], out age))
        {
            incorrect++;
            Console.WriteLine($"{count}. Username - {data[0]} Password - {data[1]} Age - {data[2]} Email - {data[3]} Result - Incorrect");
            continue;
        }

        if (ValidateUser(data[0], data[1], age, data[3]))
        {
            correct++;
            Console.WriteLine($"{count}. Username - {data[0]} Password - {data[1]} Age - {data[2]} Email - {data[3]} Result - Correct");
        }
        else
        {
            incorrect++;
            Console.WriteLine($"{count}. Username - {data[0]} Password - {data[1]} Age - {data[2]} Email - {data[3]} Result - Incorrect");
        }
    }
}
Console.WriteLine($"Успешны - {correct} Неуспешны - {incorrect} Всего - {incorrect+correct}");
Console.ReadKey();

bool ValidateUser(string username, string password, int age, string email)
{
    if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20)
        return false;

    if (string.IsNullOrEmpty(password) || password.Length < 6 || !password.Any(char.IsDigit))
        return false;

    if (age < 13 || age > 120)
        return false;

    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.EndsWith(".edu"))
        return false;

    return true;
}

