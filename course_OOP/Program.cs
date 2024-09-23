using System;
using System.Collections.Generic;
using System.Text;

public class AudioBook
{
    public string Title { get; set; }
    public string Author { get; set; }
    public TimeSpan Duration { get; set; }
    public TimeSpan CurrentPosition { get; set; } = TimeSpan.Zero;

    public void DisplayInfo()
    {
        Console.WriteLine($"\tНазва: {Title}, Автор: {Author}, Тривалість: {Duration}\n");
    }

    public void Play()
    {
        Console.WriteLine($"Відтворення аудіокниги з позиції {CurrentPosition}...");
    }

    public void Pause()
    {
        Console.WriteLine("Пауза аудіокниги...");
    }

    public void Stop()
    {
        Console.WriteLine("Зупинка аудіокниги...");
    }

    public void RememberPosition()
    {
        Console.WriteLine($"Поточна позиція {CurrentPosition} збережена.");
    }

    public void UpdatePosition(TimeSpan position)
    {
        CurrentPosition = position;
        Console.WriteLine($"Позиція оновлена до {CurrentPosition}.");
    }
}

public class Program
{
    static List<AudioBook> scienceFictionBooks = new List<AudioBook>
    {
        new AudioBook { Title = "Дюна", Author = "Френк Герберт", Duration = new TimeSpan(21, 0, 0) },
        new AudioBook { Title = "1984", Author = "Джордж Орвелл", Duration = new TimeSpan(11, 0, 0) },
        new AudioBook { Title = "Фаренгейт 451", Author = "Рей Бредбері", Duration = new TimeSpan(5, 0, 0) },
        new AudioBook { Title = "Кінець Вічності", Author = "Айзек Азімов", Duration = new TimeSpan(8, 0, 0) },
        new AudioBook { Title = "Людина у високому замку", Author = "Філіп Дік", Duration = new TimeSpan(9, 0, 0) },
        new AudioBook { Title = "Зоряний десант", Author = "Роберт Гайнлайн", Duration = new TimeSpan(7, 0, 0) },
        new AudioBook { Title = "Автостопом по галактиці", Author = "Дуглас Адамс", Duration = new TimeSpan(6, 0, 0) },
        new AudioBook { Title = "Сніжний Крушник", Author = "Ніл Стівенсон", Duration = new TimeSpan(17, 0, 0) },
        new AudioBook { Title = "Гіперіон", Author = "Ден Сіммонс", Duration = new TimeSpan(16, 0, 0) },
        new AudioBook { Title = "Темна вежа", Author = "Стівен Кінг", Duration = new TimeSpan(30, 0, 0) }
    };

    static List<AudioBook> userLibrary = new List<AudioBook>();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\tВибір аудіокниги жанру фантастика\n");
            DisplayBooks();

            Console.Write("\nВведіть номер книги для вибору (або 0 для виходу): ");
            string input = Console.ReadLine();

            int choice;
            if (int.TryParse(input, out choice))
            {
                if (choice == 0)
                {
                    break;
                }
                else if (choice > 0 && choice <= scienceFictionBooks.Count)
                {
                    AudioBook selectedBook = scienceFictionBooks[choice - 1];
                    HandleBookSelection(selectedBook);
                }
                else
                {
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                }
            }
            else
            {
                Console.WriteLine("Невірний формат введення, спробуйте ще раз.");
            }


            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    static void DisplayBooks()
    {
        for (int i = 0; i < scienceFictionBooks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scienceFictionBooks[i].Title} - {scienceFictionBooks[i].Author}");
        }
    }

    static void HandleBookSelection(AudioBook book)
    {
        bool validChoice = false;

        while (!validChoice)
        {
            Console.Clear();
            book.DisplayInfo();
            Console.WriteLine("1. Додати до своєї бібліотеки");
            Console.WriteLine("2. Почати прослуховування");
            Console.WriteLine("0. Повернутися до вибору книги");
            Console.Write("\nВаш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddToLibrary(book);
                    validChoice = true;
                    break;
                case "2":
                    PlayBook(book);
                    validChoice = true;
                    break;
                case "0":
                    return; 
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                    break;
            }
        }
    }



    static void AddToLibrary(AudioBook book)
    {
        userLibrary.Add(book);
        Console.WriteLine($"{book.Title} додана до вашої бібліотеки.");
    }

    static void PlayBook(AudioBook book)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine($"\tПрослуховування: {book.Title}\n");
            Console.WriteLine("1. Відтворити");
            Console.WriteLine("2. Пауза");
            Console.WriteLine("3. Зупинити");
            Console.WriteLine("4. Запам'ятати сторінку");
            Console.WriteLine("0. Вийти");
            Console.Write("\nВаш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    book.Play();
                    break;
                case "2":
                    book.Pause();
                    break;
                case "3":
                    book.Stop();
                    break;
                case "4":
                    Console.Write("Введіть позицію (у форматі години:хвилини:секунди): ");
                    TimeSpan position;
                    if (TimeSpan.TryParse(Console.ReadLine(), out position))
                    {
                        book.UpdatePosition(position);
                        book.RememberPosition();
                    }
                    else
                    {
                        Console.WriteLine("Невірний формат.");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }
}
