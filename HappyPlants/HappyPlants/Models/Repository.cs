using HappyPlants.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HappyPlants.Models
{
    delegate string MethodForProfileUpdate();
   
    sealed class Repository : IRepository
    {
        public MyLogger log { get; set; } = new();
        public List<User> Users { get; set; } = new();

        private MethodForProfileUpdate _methodDelegate;

        public void RegisterDelegate(MethodForProfileUpdate methodDelegate)
        {
            _methodDelegate = methodDelegate;
        }
        public void AddUser(User user)
        {
            Users.Add(user);

            int highId = Users.OrderBy(person => person.UserId).Select(person =>person.UserId).Last();
            user.UserId = highId+1;

            log.Info(MethodBase.GetCurrentMethod(), $"Профиль {user.Name}(id - {user.UserId}) добавлен в общую базу данных.");
        }

        public void AddNewPlant(User user)
        {
            bool isAddComplete = false;

            while (!isAddComplete)
            {
                Console.Clear();

                Plant plant = new();

                    Console.WriteLine($"Чтобы выйти в Главное меню, нажмите {Program.signMainMenu}.");

                if (plant.SetName())
                    return;
                if (plant.SetWateringInterval())
                    return;                
                if (plant.SetFirstWateringDay())
                    return;
                if (plant.SetWateringTime())
                    return;
                if (plant.TakePicture(user, plant))
                    return;

                user.plants.Add(plant);
                int highPlantId = user.plants.OrderBy(plant => plant.PlantId).Select(plant => plant.PlantId).Last();
                plant.PlantId = highPlantId + 1;
                log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) добавил в список новое растение {plant.Name} (plantId - {plant.PlantId}).");

                File.WriteAllText(Path.Combine(Program.MyDirectDatabase, Program.NameDatabaseFile), JsonConvert.SerializeObject(Users));
                log.Info(MethodBase.GetCurrentMethod(), "База данных сериализована");
                Console.Clear();

                if(File.Exists(plant.FullPathImageSave))
                    Console.WriteLine("Фото успешно сохранено.");
                Console.WriteLine($"{plant.ToString()}\n РАСТЕНИЕ {plant.Name} ДОБАВЛЕНО В ВАШ СПИСОК!");               

                while (true)
                {
                    Console.WriteLine("\nВыберите действие:" +
                    "\n1 - Добавить растение." +
                    $"\n{Program.signMainMenu} - Вернуться в главное меню.");

                    string userAnswer = Console.ReadLine();
                    if (userAnswer.Equals("1"))
                        break;
                    else if (userAnswer.Equals(Program.signMainMenu))
                    {                        
                        isAddComplete = true;
                        Console.Clear();
                        break;
                    }
                    else
                        Console.WriteLine("Введено невалидное значение.");
                }
            }
        }
        public void UpdatePlant(User user, Plant plantToUpdate)
        {
            Console.Clear();
            Console.WriteLine($" Растение для редактирования: \n{plantToUpdate.ToString()}");
            bool isUpdateComplete = false;

            while (!isUpdateComplete)
            {
                Console.WriteLine("\n Выберите номер параметра, который Вы хотели бы редактировать: \n" +
               "1 - Название растения. \n" +
               "2 - Дата ближайшего полива. \n" +
               "3 - Интервал полива (в днях). \n" +
               "4 - Время полива. \n" +
               "5 - Фото растения. \n" +              
               $"{Program.signMainMenu} - Выйти в главное меню.");

                string userInput = Console.ReadLine();


                if (Enum.TryParse(userInput, true, out ParametersPlantUpdate param))
                {
                    switch (param)
                    {                       
                        case ParametersPlantUpdate.Name:
                            Console.WriteLine($"Чтобы прервать редактирование, введите {Program.signMainMenu}.");
                            plantToUpdate.SetName();
                            isUpdateComplete = CompleteUdpateDecision(plantToUpdate, param);

                            log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) изменил имя растения {plantToUpdate.Name} (plantId - {plantToUpdate.PlantId}).");
                            break;

                        case ParametersPlantUpdate.WateringDate:
                            Console.WriteLine($"Чтобы прервать редактирование, введите {Program.signMainMenu}.");
                            plantToUpdate.SetFirstWateringDay();
                            isUpdateComplete = CompleteUdpateDecision(plantToUpdate, param);

                            log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) изменил дату полива растения {plantToUpdate.Name} (plantId - {plantToUpdate.PlantId}).");
                            break;

                        case ParametersPlantUpdate.WateringDayInterval:
                            Console.WriteLine($"Чтобы в любой момент прервать редактирование, введите {Program.signMainMenu}.");
                            plantToUpdate.SetWateringInterval();

                            isUpdateComplete = CompleteUdpateDecision(plantToUpdate, param);
                            log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) изменил интервал полива растения {plantToUpdate.Name} (plantId - {plantToUpdate.PlantId}).");
                            break;

                        case ParametersPlantUpdate.WateringTime:
                            Console.WriteLine($"Чтобы прервать редактирование, введите {Program.signMainMenu}.");
                            plantToUpdate.SetWateringTime();
                            isUpdateComplete = CompleteUdpateDecision(plantToUpdate, param);

                            log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) изменил время полива растения {plantToUpdate.Name} (plantId - {plantToUpdate.PlantId}).");
                            break;

                        case ParametersPlantUpdate.PlantPicture:
                            Console.Clear();
                            bool isCorrectInput = true;

                            do
                            {
                                Console.WriteLine("Выберите действие: \n" +
                                    "1 - Добавить фото растения. \n" +
                                    "2 - Просмотреть фото растения.\n" +
                                    "3 - Изменить фото растения. \n" +
                                    "4 - Удалить фото. \n" +
                                    $"{Program.signMainMenu} - Выйти в Главное меню.");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        Console.Clear();
                                        if (File.Exists(plantToUpdate.FullPathImageSave))
                                            Console.WriteLine($"Невозможно провести операцию, так как фото этого растения {plantToUpdate.Name} уже существует.");
                                        else
                                            plantToUpdate.TakePicture(user, plantToUpdate);
                                        break;
                                    case "2":
                                        if (!File.Exists(plantToUpdate.FullPathImageSave))
                                            Console.WriteLine($"Фото растения {plantToUpdate.Name} не существует.");
                                        else
                                            Process.Start("explorer.exe", plantToUpdate.FullPathImageSave);
                                        break;
                                    case "3":
                                        if (!File.Exists(plantToUpdate.FullPathImageSave))
                                            Console.WriteLine($"Невозможно провести операцию, так как фото растения {plantToUpdate.Name} не существует.");
                                        else
                                            plantToUpdate.TakePicture(user, plantToUpdate);
                                        break;
                                    case "4":
                                        Console.Clear();
                                        bool isWantDelete;

                                        do
                                        {
                                            isWantDelete = true;

                                            Console.WriteLine("Вы действительно хотите удалить фото растения? \n" +
                                            "1 - Да. \n" +
                                            "2 - Нет. \n");                      

                                            switch (Console.ReadLine())
                                            {
                                                case "1":
                                                    if (!File.Exists(plantToUpdate.FullPathImageSave))
                                                        Console.WriteLine($"Невозможно провести операцию, так как фото растения {plantToUpdate.Name} не существует.");
                                                    else
                                                    {
                                                        File.Delete(plantToUpdate.FullPathImageSave);
                                                        Console.WriteLine($"Фото растения {plantToUpdate.Name} успешно удалено.");
                                                        log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) удалил фото растения {plantToUpdate.Name} (plantId - {plantToUpdate.PlantId}).");
                                                    }
                                                    isWantDelete = true;
                                                    break;
                                                case "2":
                                                    isWantDelete = true;
                                                    break;
                                            default:
                                                    Console.WriteLine("Введено недопустимое значение.");
                                                    isWantDelete = false;
                                                    break;
                                            }
                                        } while (!isWantDelete);
                                        break;
                                    default:
                                        Console.WriteLine("Введено недопустимое значение");
                                        isCorrectInput = false;
                                        break;
                                }
                            } while (!isCorrectInput);

                            isUpdateComplete = CompleteUdpateDecision(plantToUpdate, param);
                            break; 
                            
                        default:
                            Console.WriteLine("Параметра с таким номером не существует.");
                            break;
                    }
                }
                else if (userInput.Equals(Program.signMainMenu))
                    isUpdateComplete = true;
                else
                    Console.WriteLine("Введено недопустимое значение.");
            }
        }
        private bool CompleteUdpateDecision(Plant plantToUpdate, ParametersPlantUpdate parametersPlantUpdate)
        {
            bool returnBoolValue = default;
            bool isUserInputCorrect = false;

            while (!isUserInputCorrect)
            {
                if(!parametersPlantUpdate.Equals(ParametersPlantUpdate.PlantPicture))
                    Console.WriteLine(plantToUpdate.ToString());

                Console.WriteLine("Выберите действие: \n" +
                    "1 - Продолжить редактировать растение. \n" +
                    $"{Program.signMainMenu} - Вернуться в главное меню. ");

                switch (Console.ReadLine())
                {
                    case "1":
                        returnBoolValue = false;
                        isUserInputCorrect = true;
                        break;

                    case Program.signMainMenu:
                        returnBoolValue = true;
                        isUserInputCorrect = true;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine($"Введено недопустимое значение. Пожалуйста, введите 1 или {Program.signMainMenu}.");
                        break;
                }
            }
            return returnBoolValue;
        }

        public void DeletePlants(User user)
        {
            Console.Clear();
            bool isDeleteComplete = false;

            while (!isDeleteComplete)
            {              
                if (user.PrintPlantsListIfFull())
                {
                    Plant plantToDelete;

                    Console.WriteLine("\n Пожалуйста, введите номер растения, которое Вы хотели бы удалить. \n Если Вы хотите удалить все растения из списка, введите 0.");
                    Console.WriteLine($" Для выхода в Главное меню введите знак {Program.signMainMenu}.");

                    string userAnswer = Console.ReadLine();

                    if (int.TryParse(userAnswer, out int plantIndex))
                    {
                        if (plantIndex == 0)
                        {
                            Console.Clear();
                            bool isUserSure = false;

                            while (!isUserSure)
                            {
                                Console.WriteLine("Вы уверены, что хотите удалить весь список растений? \n" +
                                "1 - Да. \n" +
                                "2 - Нет. \n" +
                                $"{Program.signMainMenu} - Выйти в Главное меню.");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        var currentDirectory = Directory.GetCurrentDirectory();
                                        var imageFolderPath = Path.Combine(currentDirectory, "PlantsImages", $"{user.Name}");

                                        if (Directory.Exists(imageFolderPath))
                                            Directory.Delete(imageFolderPath, true);
                                        user.plants.Clear();
                                        log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) удалил весь список растений.");

                                        Console.Clear();
                                        Console.WriteLine(" Ваш список растений пуст. \n Для выхода в Главное меню нажмите любую клавишу.");
                                        Console.ReadKey();

                                        isUserSure = true;
                                        isDeleteComplete = true;
                                        break;

                                    case "2":
                                        isUserSure = true;
                                        break;

                                    case Program.signMainMenu:
                                        isDeleteComplete = true;
                                        isUserSure = true;
                                        break;

                                    default:
                                        Console.WriteLine("Введено недопустимое значение.");
                                        break;
                                }
                            }
                        }
                        else if (plantIndex - 1 >= user.plants.Count && plantIndex > 0)
                            Console.WriteLine("Растения с таким номером нет в Вашем списке.");
                        else if (plantIndex - 1 < user.plants.Count && plantIndex > 0)
                        {
                            plantToDelete = user.plants[plantIndex - 1];

                            Console.Clear();
                            bool isUserSure = false;

                            while (!isUserSure)
                            {
                                Console.WriteLine($"Вы уверены, что хотите удалить растение {plantToDelete.Name}? \n" +
                                "1 - Да. \n" +
                                "2 - Нет. \n" +
                                $"{Program.signMainMenu}  - Выйти в Главное меню.");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        user.plants.Remove(plantToDelete);
                                        if (File.Exists(plantToDelete.FullPathImageSave))
                                            File.Delete(plantToDelete.FullPathImageSave);
                                        log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) успешно удалил растение {plantToDelete.Name} (plantId - {plantToDelete.PlantId}) из списка.");

                                        if (!user.plants.Any())
                                        {
                                            Console.Clear();
                                            Console.WriteLine($" Ваш список растений пуст. \n Для выхода в Главное меню нажмите любую клавишу.");
                                            Console.ReadKey();
                                            isUserSure = true;
                                            isDeleteComplete = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Растение {plantToDelete.Name} было успешно удалено из Вашего списка.");
                                            bool isRepeatAction = false;

                                            while (!isRepeatAction)
                                            {
                                                Console.WriteLine("Выберите действие:  \n" +
                                                "1 - Удалить ещё одно растение. \n" +
                                                $"{Program.signMainMenu} - Вернуться в главное меню. ");

                                                switch (Console.ReadLine())
                                                {
                                                    case "1":
                                                        isRepeatAction = true;
                                                        isUserSure = true;
                                                        break;

                                                    case Program.signMainMenu:
                                                        isDeleteComplete = true;
                                                        isRepeatAction = true;
                                                        isUserSure = true;
                                                        break;

                                                    default:
                                                        Console.WriteLine("Введено недопустимое значение. Пожалуйста, введите 1 или 2.");
                                                        break;
                                                }
                                            }
                                        }
                                        break;

                                    case "2":
                                        isUserSure = true;
                                        break;

                                    case Program.signMainMenu:
                                        isDeleteComplete = true;
                                        isUserSure = true;
                                        break;

                                    default:
                                        Console.WriteLine("Введено недопустимое значение.");
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (userAnswer.Equals(Program.signMainMenu))
                            isDeleteComplete = true;
                        else
                            Console.WriteLine("Введено недопустимое значение.");
                    }                        
                }
                else
                    break;
            }
        }
        public bool UpdateUserData(User user, string inputValue)
        {
            Console.Clear();
            switch (inputValue)
            {
                case "1":
                    Console.WriteLine($"Имя профиля - {user.Name}.");
                    user.Name = _methodDelegate();
                    Console.WriteLine($"Новое имя Вашего профиля - {user.Name}.");
                    log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) успешно изменил имя профиля).");
                    break;
                case "2":
                    while (true)
                    {
                        Console.WriteLine("Введите Ваш действующий пароль");
                        string userPassword = Console.ReadLine();

                        if (user.ComparePasswords(userPassword))
                        {
                            Console.WriteLine("Смена пароля.");
                            user.UpdatePassword(_methodDelegate());
                            Console.WriteLine("Пароль успешно изменён!");
                            log.Info(MethodBase.GetCurrentMethod(), $"Пользователь {user.Name} (id - {user.UserId}) успешно изменил пароль профиля).");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(" Данный пароль недействителен. \n" +
                                " Чтобы ввести пароль ещё раз, введите 1. \n" +
                                " Для выхода в Главное меню введите любое другое значение.");

                            if (Console.ReadLine() != "1")
                                break;
                        }
                    }
                    break;
            }

            bool isUpdateComplete = default;
            bool isUserInputCorrect = false;

            while (!isUserInputCorrect)
            {
                Console.WriteLine(" Выберите действие:" +
                "\n 1 - Продолжить редактирование профиля." +
                $"\n {Program.signMainMenu} - Вернуться в Главное меню.");

                switch (Console.ReadLine())
                {
                    case "1":
                        isUpdateComplete = false;
                        isUserInputCorrect = true;
                        break;
                    case Program.signMainMenu:
                        isUpdateComplete = true;
                        isUserInputCorrect = true;
                        break;
                    default:
                        Console.WriteLine($"Введено недопустимое значение. Пожалуйста, выберите 1 или {Program.signMainMenu}.");
                        break;  
                }
            }
            return isUpdateComplete;
        }

        public bool DeleteProfile(User user)
        {
            Console.Clear();
            Console.WriteLine("Вы уверены, что хотите удалить профиль?");
            Console.WriteLine("1 - Да. Удалить мой профиль. \n" +
                $"{Program.signMainMenu} - Нет. Вернуться в Главное меню.");

            bool isInputCorrect = false;
            bool returnBoolValue = default;

            while (!isInputCorrect)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        isInputCorrect = true;
                        returnBoolValue = true;
                        log.Info(MethodBase.GetCurrentMethod(), $"Профиль {user.Name} (id - {user.UserId}) удалён из системы).");

                        var currentDirectory = Directory.GetCurrentDirectory();
                        var imageFolderPath = Path.Combine(currentDirectory, "PlantsImages", $"{user.Name}");

                        if (Directory.Exists(imageFolderPath))
                            Directory.Delete(imageFolderPath, true);
                        Users.Remove(user);
                        Console.WriteLine("Ваш профиль был успешно удалён. Чтобы выйти в меню авторизации, нажмите любую клавишу.");                   
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case Program.signMainMenu:
                        isInputCorrect = true;
                        returnBoolValue = false;
                        break;
                    default:
                        Console.WriteLine("Введено недопустимое значение.");
                        break;
                }
            }
            return returnBoolValue;
        }
    }
}
