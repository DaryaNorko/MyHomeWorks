using HappyPlants.Exceptions;
using System;
using System.IO;
using System.Reflection;
using TakePicture;

namespace HappyPlants.Models
{
    sealed class Plant
    {
        public string Name { get; set; }
        private MyLogger log { get; set; } = new();
        public int PlantId { get; set; } = 0;
        public int WateringDayInterval { get; set; }
        public DateTime WateringDate { get; set; }
        public DateTime WateringTime { get; set; }
        public string FullPathImageSave { get; set; }

        public Plant()
        {
        }
        public override string ToString()
        {
            if (File.Exists(FullPathImageSave))
                return $"Название растения - {Name}. Ближайший день полива - {WateringDate.Date.ToString("M")}, время полива - {WateringTime.ToString(@"HH\:mm")}. Интервал полива - {WateringDayInterval} дн. Фото растения добавлено.";
            else
                return $"Название растения - {Name}. Ближайший день полива - {WateringDate.Date.ToString("M")}, время полива - {WateringTime.ToString(@"HH\:mm")}. Интервал полива - {WateringDayInterval} дн. Фото растения отсутствует.";
        }
        public bool SetName()
        {
            bool isCorrectInputName = false;           

            while (!isCorrectInputName) 
            {
                Console.WriteLine("Введите название растения.");
                string plantName = Console.ReadLine();

                if (plantName.Equals(Program.signMainMenu))
                {
                    isCorrectInputName = true;
                    return true;
                }
                else
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(plantName))
                            throw new EmptyOrWhiteSpaceFieldException();
                        else
                        {
                            Name = plantName;
                            isCorrectInputName = true;
                        }
                    }
                    catch (EmptyOrWhiteSpaceFieldException)
                    {
                        Console.WriteLine("Название растения не может быть пустым или состоять только из побелов.");
                        log.Error(MethodBase.GetCurrentMethod(), $"Имя растения является пустым или состоит только из пробелов.");
                    }                    
                }
            }
            return false;
        }
        public void FindNextWateringDate()
        {
            WateringDate = WateringDate.AddDays(WateringDayInterval);
        }
        public bool SetWateringInterval()
        {
            bool isCorrectIntervalInput = false;

            while (!isCorrectIntervalInput)
            {
                Console.WriteLine("Введите интервал дней, который будет сохраняться между поливами растения.");
                string userInput = Console.ReadLine();

                if (userInput.Equals(Program.signMainMenu))
                {
                    isCorrectIntervalInput = true;
                    return true;
                }
                else
                {
                    if (!int.TryParse(userInput, out int wateringInterval))
                        Console.WriteLine("Введенное Вами значение не является числом. \n");
                    else
                    {
                        if (wateringInterval <= 0)
                            Console.WriteLine("Введено недопустимое значение. \n");
                        else if (wateringInterval > 50)
                            Console.WriteLine("Введено слишком большое значение. \n");
                        else
                        {
                            WateringDayInterval = wateringInterval;
                            isCorrectIntervalInput = true;
                        }
                    }
                }
            }
            return false;
        }

        public bool SetWateringTime()
        {
            bool isCorrectInputTime = false;

            while (!isCorrectInputTime)
            {
                Console.WriteLine("Задайте время напоминания о поливе в формате чч:мм (например 10:00).");
                string userInput = Console.ReadLine();

                if (userInput.Equals(Program.signMainMenu))
                {
                    isCorrectInputTime = true;
                    return true;
                }
                else
                {
                    if (!DateTime.TryParse(userInput, out DateTime wateringTime))
                        Console.WriteLine("Введено некорректное значение. \n");
                    else
                    {
                        WateringTime = wateringTime;
                        isCorrectInputTime = true;
                    }
                }
            }
            return false;
        }

        public bool SetFirstWateringDay()
        {
            bool isWateringDateInput = false;

            while (!isWateringDateInput)
            {
                Console.WriteLine("Установите дату ближайшего полива: \n" +
                "1 - Cегодня. \n" +
                "2 - Завтра. \n" +
                "3 - Ввести другую дату.");

                string userInput = Console.ReadLine();

                if (userInput.Equals(Program.signMainMenu))
                {
                    isWateringDateInput = true;
                    return true;
                }
                else
                {
                    switch (userInput)
                    {
                        case "1":
                            WateringDate = DateTime.Now.Date;
                            isWateringDateInput = true;
                            break;
                        case "2":
                            WateringDate = DateTime.Now.AddDays(1).Date;
                            isWateringDateInput = true;
                            break;
                        case "3":
                            Console.WriteLine($"Введите дату ближайшего полива в формате гггг,мм,дд. Например, {DateTime.Now.Year},01,01.");
                            string userInputDate = Console.ReadLine();

                            if (DateTime.TryParse(userInputDate, out DateTime firstWateringDate))
                            {
                                if (firstWateringDate < DateTime.Now)
                                    Console.WriteLine("Введенная дата не должна быть раньше сегодняшнего дня.");
                                else
                                {
                                    WateringDate = firstWateringDate.Date;
                                    isWateringDateInput = true;
                                }
                            }
                            else
                                Console.WriteLine("Дата введена в несоответствующем образцу формате или введено недопустимое значение.");
                            break;
                        default:
                            Console.WriteLine("Введено недопустимое значение.");
                            break;
                    }
                }    
            }
            return false;
        }
        public bool TakePicture(User user, Plant plant)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var imageFolderPath = Path.Combine(currentDirectory, "PlantsImages", $"{user.Name}");

            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            string imageName = $"{plant.Name}{plant.PlantId}.jpg";
            FullPathImageSave = Path.Combine(imageFolderPath, imageName);

                while (true)
                {
                    Console.WriteLine("Хотите добавить (или изменить) фотографию растения? \n" +
                    "1 - Да. \n" +
                    "2 - Нет.");

                    string userAnswer = Console.ReadLine();

                    if (userAnswer.Equals("2"))
                        return false;
                    else if (userAnswer.Equals(Program.signMainMenu))
                        return true;
                    else if (userAnswer.Equals("1"))
                    {
                        Form1 picture = new Form1(FullPathImageSave);
                        var result = picture.ShowDialog();
                        break;
                    }
                    else
                        Console.WriteLine("Введено невалидное значение.");
                }
                Console.Clear();
                if (File.Exists(FullPathImageSave))
                {
                    Console.WriteLine($"Фото растения {plant.Name} добавлено!");
                }
                else
                    Console.WriteLine("Фото растения не добавлено.");

            return false;
        }
    }
}
