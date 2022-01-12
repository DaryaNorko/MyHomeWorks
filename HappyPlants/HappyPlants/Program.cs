using HappyPlants.Enums;
using HappyPlants.Exceptions;
using HappyPlants.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace HappyPlants
{
    class Program
    {
        public const string signMainMenu = "*";
        public static string NameDatabaseFile { get; set; } = "Database.json";
        public static string MyDirectDatabase { get; set; }
        public static User user { get; set; }
        public static Repository repository { get; set; }
        public static MyLogger log { get; set; } = new();
        static void Main(string[] args)
        {          
            log.Info(MethodBase.GetCurrentMethod(), "Старт работы программы.");

            MyDirectDatabase = Path.Combine(Directory.GetCurrentDirectory(), "Database");

            if (!Directory.Exists(MyDirectDatabase))
                Directory.CreateDirectory(MyDirectDatabase);

            repository = new();
            if (File.Exists(Path.Combine(MyDirectDatabase, NameDatabaseFile)))
            {
                repository.Users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(Path.Combine(MyDirectDatabase, NameDatabaseFile)));
                log.Info(MethodBase.GetCurrentMethod(), "База данных десериализована");
            }

            if (args.Length > 0)
            {
                log.Info(MethodBase.GetCurrentMethod(), "Открыто окно напоминания о поливе растений.");

                Thread threadForMelody = new(new ThreadStart(PlayMelody));
                threadForMelody.Start();

                user = repository.Users.Single(user => user.UserId == int.Parse(args[0]));

                OpenReminderWindow();

                return;
            }

            Console.WriteLine("Мы рады приветствовать Вас в приложении Happy Plants!");

            bool isExitProgram = false;
            while (!isExitProgram)
            {
                LogInOrSignIn logInOrSignIn = PrintAuthorizationMenu();
                if (logInOrSignIn.Equals(LogInOrSignIn.Exit))
                {
                    Console.WriteLine("До свидания! Будем рады видеть Вас снова!");
                    log.Info(MethodBase.GetCurrentMethod(), "Завершение работы программы.");
                    break;
                }

                user = PassAuthorization(logInOrSignIn);
                Console.Clear();

                TimerCallback timerCallback = new(CallReminder);
                Timer timer = new(timerCallback, null, 0, 60*1000);

                TimerCallback timerSerializeCallback = new(SerializeDatabase);
                Timer timerSerialize = new(timerSerializeCallback, null, 2000, 60 * 1000);

                bool isUserWantExitFromMainMenu = false;

                while (!isUserWantExitFromMainMenu)
                {
                    File.WriteAllText(Path.Combine(MyDirectDatabase, NameDatabaseFile), JsonConvert.SerializeObject(repository.Users));
                    log.Info(MethodBase.GetCurrentMethod(), "База данных сериализована");
                    Console.Clear();

                    MainMenu mainMenu = PrintMainMenu();

                    switch (mainMenu)
                    {
                        case MainMenu.GetListOfPlants:
                            if (user.PrintPlantsListIfFull())
                            {
                                Console.WriteLine("\n Для выхода в Главное меню нажмите любую клавишу.");
                                Console.ReadKey();
                            }
                            break;

                        case MainMenu.AddPlant:
                            repository.AddNewPlant(user);
                            break;

                        case MainMenu.UpdatePlant:
                            if (user.PrintPlantsListIfFull())
                            {
                                Console.WriteLine();

                                Plant plantToUpdate;
                                bool isPlantNumberInputCorrect = false;

                                while (!isPlantNumberInputCorrect)
                                {

                                    Console.WriteLine("Пожалуйста, введите номер растения, которое Вы хотели бы редактировать.");
                                    Console.WriteLine($"Для выхода в Главное меню введите знак {Program.signMainMenu}.");

                                    string userAnswer = Console.ReadLine();

                                    if (int.TryParse(userAnswer, out int plantIndex))
                                    {
                                        if (plantIndex - 1 >= user.plants.Count || plantIndex - 1 < 0)
                                            Console.WriteLine("Растения с таким номером нет в Вашем списке.");
                                        else
                                        {
                                            plantToUpdate = user.plants[plantIndex - 1];
                                            repository.UpdatePlant(user, plantToUpdate);
                                            isPlantNumberInputCorrect = true;
                                        }
                                    }
                                    else
                                    {
                                        if (userAnswer.Trim().Equals(Program.signMainMenu))
                                            isPlantNumberInputCorrect = true;
                                        else
                                            Console.WriteLine("Введено недопустимое значение.");
                                    }
                                }
                            }
                            break;

                        case MainMenu.DeletePlant:
                            repository.DeletePlants(user);
                            break;

                        case MainMenu.UpdateProfileData:

                            bool isUpdateProfileComplete = false;
                            while (!isUpdateProfileComplete)
                            {
                                Console.WriteLine($"Выберите действие: \n" +
                               $"1 - Изменить имя профиля(сейчас - {user.Name}). \n" +
                               "2 - Изменить пароль. \n" +
                               $"{Program.signMainMenu} - Вернуться в Главное меню.");

                                string userAnswer = Console.ReadLine();

                                switch (userAnswer)
                                {
                                    case "1":
                                        repository.RegisterDelegate(InputUserName);
                                        isUpdateProfileComplete = repository.UpdateUserData(user, userAnswer);
                                        break;
                                    case "2":
                                        repository.RegisterDelegate(InputUserPassword);
                                        isUpdateProfileComplete = repository.UpdateUserData(user, userAnswer);
                                        break;
                                    case Program.signMainMenu:
                                        isUpdateProfileComplete = true;
                                        break;
                                    default:
                                        Console.WriteLine("Введено недопустимое значение.");
                                        break;
                                }
                            }
                            break;

                        case MainMenu.DeleteProfile:
                            isUserWantExitFromMainMenu = repository.DeleteProfile(user);
                            timer.Dispose();
                            timerSerialize.Dispose();
                            timer.LogStopTimer();
                            File.WriteAllText(Path.Combine(MyDirectDatabase, NameDatabaseFile), JsonConvert.SerializeObject(repository.Users));
                            log.Info(MethodBase.GetCurrentMethod(), "База данных сериализована.");
                            break;

                        case MainMenu.Exit:
                            timer.Dispose();
                            timerSerialize.Dispose();
                            timer.LogStopTimer();
                            Console.WriteLine("До свидания! Будем рады видеть Вас снова!");
                            log.Info(MethodBase.GetCurrentMethod(), "Завершение работы программы.");

                            isUserWantExitFromMainMenu = true;
                            isExitProgram = true;
                            break;
                    }
                }
            }
        }
        private static void SerializeDatabase(object state)
        {           
            File.WriteAllText(Path.Combine(MyDirectDatabase, NameDatabaseFile), JsonConvert.SerializeObject(repository.Users));
            log.Info(MethodBase.GetCurrentMethod(), "Сработал таймер сериализации базы данных.");
        }
        private static void PlayMelody()
        {
            Console.Beep(247, 500);
            Console.Beep(417, 500);
            Console.Beep(417, 500);
            Console.Beep(370, 500);
            Console.Beep(417, 500);
            Console.Beep(329, 500);
            Console.Beep(247, 500);
            Console.Beep(247, 500);
            Console.Beep(247, 500);
            Console.Beep(417, 500);
            Console.Beep(417, 500);
            Console.Beep(370, 500);
            Console.Beep(417, 500);
            Console.Beep(497, 500);
            Thread.Sleep(500);
            Console.Beep(497, 500);
            Console.Beep(277, 500);
            Console.Beep(277, 500);
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(417, 500);
            Console.Beep(370, 500);
            Console.Beep(329, 500);
            Console.Beep(247, 500);
            Console.Beep(417, 500);
            Console.Beep(417, 500);
            Console.Beep(370, 500);
            Console.Beep(417, 500);
            Console.Beep(329, 500);
        }
        private static void CallReminder(object state)
        {
            if (user.plants.Count > 0)
            {
                var currentTime = DateTime.Now;                
                currentTime = currentTime.AddSeconds(-currentTime.Second).AddMilliseconds(-currentTime.Millisecond);

                // поиск растений, которые необходимо полить сейчас.
                List<Plant> doWateringNowPlants = 
                    user.plants.Where(plant => plant.WateringDate.Date == currentTime.Date && currentTime.TimeOfDay <= plant.WateringTime.TimeOfDay && currentTime.AddMinutes(1).TimeOfDay > plant.WateringTime.TimeOfDay).ToList();

                // поиск растений, время полива которых было пропущено сегодня.

                List<Plant> plantsMissedWateringTimeOrDay =
                    user.plants.Where(plant => plant.WateringDate.Date == currentTime.Date && currentTime.TimeOfDay > plant.WateringTime.TimeOfDay).ToList();

                // поиск растений, день полива которых был пропущен.

                List<Plant> plantsMissedWateringDay = user.plants.Where(plant => plant.WateringDate.Date < currentTime.Date).ToList();

                if (doWateringNowPlants.Count > 0 || plantsMissedWateringTimeOrDay.Count > 0 || plantsMissedWateringDay.Count > 0)
                {
                    var plantsFileDirect = Path.Combine(Directory.GetCurrentDirectory(), "PlantsForWatering");
                    
                    if (!Directory.Exists(plantsFileDirect))
                        Directory.CreateDirectory(plantsFileDirect);

                    File.WriteAllText(Path.Combine(plantsFileDirect, "PlantsForWatering.json"), JsonConvert.SerializeObject(user.plants));                 

                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "HappyPlants.exe");

                    Process.Start(new ProcessStartInfo(dir, user.UserId.ToString()) { UseShellExecute = true });

                    // настройка новой даты полива.

                    if (doWateringNowPlants.Count > 0)
                    {
                        foreach (Plant plant in doWateringNowPlants)
                        {
                            plant.FindNextWateringDate();
                        }
                    }
                    else if(plantsMissedWateringTimeOrDay.Count > 0)
                    {
                        foreach (Plant plant in plantsMissedWateringTimeOrDay)
                        {
                            plant.FindNextWateringDate();
                        }
                    }
                    else if (plantsMissedWateringDay.Count > 0)
                    {
                        foreach (Plant plant in plantsMissedWateringDay)
                        {
                            while (plant.WateringDate.Date <= DateTime.Now.Date)
                            {
                                plant.FindNextWateringDate();
                            }
                        }
                    }
                }               
            }
            return;
        }
        private static void OpenReminderWindow()
        {
            Console.Clear();

            var plantsFileDirect = Path.Combine(Directory.GetCurrentDirectory(), "PlantsForWatering");

            user.plants = JsonConvert.DeserializeObject<List<Plant>>(File.ReadAllText(Path.Combine(plantsFileDirect, "PlantsForWatering.json")));

            var currentTime = DateTime.Now;
            currentTime = currentTime.AddSeconds(-currentTime.Second).AddMilliseconds(-currentTime.Millisecond);           

            List<Plant> doWateringNowPlants =
                user.plants.Where(plant => plant.WateringDate.Date == currentTime.Date && currentTime.TimeOfDay <= plant.WateringTime.TimeOfDay && currentTime.AddMinutes(1).TimeOfDay > plant.WateringTime.TimeOfDay).ToList();
            
            if (doWateringNowPlants.Count > 0)
            {
                Console.WriteLine($"{user.Name}! Пожалуйста, полейте растения:");
                foreach (Plant plant in doWateringNowPlants)
                {
                    Console.WriteLine($"{plant.Name}");
                }

                if (doWateringNowPlants.Any(plant => File.Exists(plant.FullPathImageSave)))
                {
                    Console.WriteLine($"\nВведите 1, чтобы просмотреть фото растения(ний):\n");
                    
                    List<Plant> plantsWithPictures = doWateringNowPlants.Where(plant => File.Exists(plant.FullPathImageSave)).ToList();
                    
                    foreach (Plant plant in plantsWithPictures)
                    {
                        Console.WriteLine(plant.Name);
                    }

                    switch (Console.ReadLine())
                    {
                        case "1":
                            foreach (Plant plant in plantsWithPictures)
                            {
                                Process.Start("explorer.exe", plant.FullPathImageSave);
                            }
                            break;
                        default:
                            break;
                    }
                }              
            }

            List<Plant> plantsMissedWateringTimeOrDay =
                    user.plants.Where(plant => plant.WateringDate.Date == currentTime.Date && currentTime.TimeOfDay > plant.WateringTime.TimeOfDay || plant.WateringDate.Date < currentTime.Date).ToList();

            if (plantsMissedWateringTimeOrDay.Count > 0)
            {
                Console.WriteLine($"{user.Name}! Возможно, Вы пропустили сегодня полив растений:");
                foreach (Plant plant in plantsMissedWateringTimeOrDay)
                {
                   Console.WriteLine(plant.Name);
                }

                List<Plant> plantsMissedWateringDay = user.plants.Where(plant => plant.WateringDate.Date < currentTime.Date).ToList();

                if(plantsMissedWateringDay.Count > 0)
                {
                    Console.WriteLine("Просмотрите следующий день полива растения(ний) согласно графику:");
                    foreach (Plant plant in plantsMissedWateringDay)
                    {
                        Console.Write(plant.Name + "-");
                        while (plant.WateringDate.Date <= DateTime.Now.Date)
                        {
                            plant.FindNextWateringDate();
                        }
                        Console.WriteLine(plant.WateringDate.Date.ToString("M"));
                    }
                    Console.WriteLine("Если хотите установить другой день, отредактируйте дату полива растений в разделе Главного меню \"Редактировать растение\".");
                }

                List<Plant> plantsWithPictures = plantsMissedWateringTimeOrDay.Where(plant => File.Exists(plant.FullPathImageSave)).ToList();
                if (plantsWithPictures.Count > 0)
                {
                    Console.WriteLine($"\nВведите 1, чтобы просмотреть фото растения(ний):\n");


                    foreach (Plant plant in plantsWithPictures)
                    {
                        Console.WriteLine(plant.Name);
                    }

                    switch (Console.ReadLine())
                    {
                        case "1":
                            foreach (Plant plant in plantsWithPictures)
                            {
                                Process.Start("explorer.exe", plant.FullPathImageSave);
                            }
                            break;
                        default:
                            break;
                    }               
                }
            }
        }
        static MainMenu PrintMainMenu()
        {
            MainMenu mainMenu = default;
            bool isCorrectUserAction = false;

            while (!isCorrectUserAction)
            {
                Console.WriteLine(" ГЛАВНОЕ МЕНЮ \n Выберите номер действия: \n" +
                    " 1 - Просмотреть мой список растений. \n" +
                    " 2 - Добавить новое растение в список. \n" +
                    " 3 - Редактировать растение. \n" +
                    " 4 - Удалить растение из списка.\n" +
                    " 5 - Редактировать профиль.\n" +
                    " 6 - Удалить профиль. \n" +
                    " 7 - Выйти из программы.");

                string userAnswer = Console.ReadLine();

                bool isEnumInput = Enum.TryParse(userAnswer, true, out mainMenu);

                if (!isEnumInput || !Enum.IsDefined(typeof(MainMenu), mainMenu))
                {
                    Console.WriteLine("Введено недопустимое значение.");
                }
                else
                {
                    isCorrectUserAction = true;
                }
            }
            Console.Clear();
            return mainMenu;
        }
        static LogInOrSignIn PrintAuthorizationMenu()
        {
            LogInOrSignIn logInOrSignIn = default;
            bool isCorrectUserAction = false;

            while (!isCorrectUserAction)
            {
                Console.WriteLine("Выберите действие: \n" +
                "1 - Пройти регистрацию.\n" +
                "2 - Авторизоваться.\n" +
                "3 - Выйти из программы.");

                string userAnswer = Console.ReadLine();

                if (Enum.TryParse(userAnswer, true, out logInOrSignIn) && Enum.IsDefined(typeof(LogInOrSignIn), logInOrSignIn))
                {
                    if (!repository.Users.Any() && logInOrSignIn == LogInOrSignIn.SignIn)
                        Console.WriteLine("В данной программе ещё не зарегистрировано ни одного профиля. Пожалуйста, зарегистрируйтесь.");
                    else
                    {
                        isCorrectUserAction = true;
                        Console.Clear();
                    }
                }
                else
                    Console.WriteLine("Введено недопустимое значение.");
            }
            return logInOrSignIn;
        }

        static User PassAuthorization(LogInOrSignIn logInOrSignIn)
        {
            switch (logInOrSignIn)
            {
                case LogInOrSignIn.LogIn:
                    bool isUnicUserName = false;
                    string userName;
                    string password;

                    while (!isUnicUserName)
                    {
                        userName = InputUserName();

                        if (repository.Users.Any(s => s.Name == userName))
                        {
                            Console.WriteLine("Это имя уже занято другим пользователем. Введите другое имя.");
                        }
                        else
                        {
                            isUnicUserName = true;
                            password = InputUserPassword();
                            user = new(userName, password);
                            repository.AddUser(user);

                            log.Info(MethodBase.GetCurrentMethod(), $"Профиль {user.Name} (id - {user.UserId}) успешно зарегистрирован в системе.");
                        }
                    }
                    break;

                case LogInOrSignIn.SignIn:
                    bool isCorrectPassword = false;

                    userName = InputUserName();
                    user = repository.Users.SingleOrDefault(s => s.Name == userName);

                    if (user == default)
                        Console.WriteLine($"Пользователь с именем {userName} не найден. Попробуйте ввести имя ещё раз.");
                    else
                    {
                        while (!isCorrectPassword)
                        {
                            Console.WriteLine("Введите пароль.");
                            password = Console.ReadLine();

                            if (!user.ComparePasswords(password))
                                Console.WriteLine("Пароль введён неверно.");
                            else
                                isCorrectPassword = true;
                        }
                        log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) успешно вошел в систему.");
                    }
                    break;
            }
            return user;
        }

        public static string InputUserName()
        {
            bool isCorrectInputName = false;
            string userName = default;

            while (!isCorrectInputName)
            {
                Console.WriteLine("Пожалуйста, введите имя.");
                userName = Console.ReadLine();

                try
                {
                    if (string.IsNullOrWhiteSpace(userName))
                        throw new EmptyOrWhiteSpaceFieldException();
                    else
                        isCorrectInputName = true;
                }
                catch (EmptyOrWhiteSpaceFieldException)
                {
                    Console.WriteLine("Имя профиля не может быть пустым или состоять только из пробелов.");
                    log.Error(MethodBase.GetCurrentMethod(), $"Имя профиля не может быть пустым или состоять только из пробелов.");
                }
            }
            return userName;
        }
        public static string InputUserPassword()
        {
            MyLogger log = new();
            bool isCorrectInputPassword = false;
            string userPassword = default;

            while (!isCorrectInputPassword)
            {
                Console.WriteLine("Пожалуйста, введите пароль.");
                userPassword = Console.ReadLine();

                try
                {
                    if (string.IsNullOrWhiteSpace(userPassword))
                        throw new EmptyOrWhiteSpaceFieldException();
                    else
                    {
                        Console.WriteLine("Повторите пароль.");
                        string userPasswordRepeat = Console.ReadLine();

                        if (!userPassword.Equals(userPasswordRepeat))
                            Console.WriteLine("Пароли должны совпадать. Попробуйте ввести данные ещё раз.");
                        else
                            isCorrectInputPassword = true;
                    }
                }
                catch (EmptyOrWhiteSpaceFieldException)
                {
                    Console.WriteLine("Пароль профиля не может быть пустым или состоять только из пробелов.");
                    log.Error(MethodBase.GetCurrentMethod(), $"Пароль профиля не может быть пустым или состоять только из пробелов.");
                }
            }
            return userPassword;
        }
    }
}
    

