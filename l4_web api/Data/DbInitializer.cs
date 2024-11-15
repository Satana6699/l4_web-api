using l4_web_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace l4_web_api.Data
{
    public static class DbInitializer
    {
        static DateOnly GetRandomDate(DateOnly start, DateOnly end)
        {
            Random random = new Random();
            int range = (end.DayNumber - start.DayNumber);
            int randomDayNumber = random.Next(0, range + 1);
            return start.AddDays(randomDayNumber);
        }
        public static void Initialize(MedicinalProductsContext db)
        {
            db.Database.EnsureCreated();

            // Проверка занесены ли
            if (db.Medicines.Any())
            {
                return;   // База данных инициализирована
            }

            Random rand = new(0);

            InitializeMedicines(db, medicine_number: 30, rand);
            InitializeDiseses(db, disese_number: 30, rand);
            InitializeFamilyMembers(db, familyMember_number: 30, rand);
            InitializeCostMedicines(db, costMedicine_number: 400, rand);
            InitializePrescriptions(db, prescription_number: 400, rand);
            InitializeDiseasesAndSymptoms(db, diseasesAndSymptoms_number: 400, rand);
        }

        private static void InitializeMedicines(MedicinalProductsContext db, int medicine_number, Random rand)
        {
            string[] medicine_name = ["Аспирин", "Ибупрофен", "Нурофен Форте", "Парацетамол", "Цитрамон", "Ринза", "Антигриппин",
                                      "Валидол", "Анальгин Экспресс", "Пенталгин", "Амоксиклав", "Супрастин", "Кардиомагнил"];
            string[] medicine_indications = ["Лихорадка и жар", "Боль различной локализации", "Симптомы простуды и гриппа",
                                             "Повышенная температура тела", "Воспаление суставов", "ОРВИ и ОРЗ", "Спазмы сосудов головного мозга",
                                             "Воспаление горла", "Проблемы с пищеварением", "Инфекции дыхательных путей", "Обострение ринита"];
            string[] medicine_contraindications = ["Аллергия на компоненты препарата", "Язвенная болезнь желудка", "Беременность и лактация",
                                                   "Печеночная недостаточность", "Сахарный диабет", "Нарушения свертываемости крови",
                                                   "Анемия", "Бронхиальная астма", "Детский возраст до 6 лет", "Гиперчувствительность"];
            string[] medicine_manufacturer = ["Борщаговский ХФЗ", "Фармакор Продакшн", "Биофарм", "ОАО \"Гедеон Рихтер\"", "Акрихин",
                                              "Валента Фарм", "ООО \"Медок\"", "Санофи-Авентис", "Берлин Хеми", "Розекфарм",
                                              "Хинди Фармасьютикалс", "ЗАО \"Биосинтез\"", "Бионорика"];
            string[] medicine_packaging = ["Таблетка 500 мг", "Капсулы 250 мг", "Сироп 125 мг/5 мл", "Таблетка 1 г", "Раствор 100 мг/мл",
                                           "Порошок 200 мг", "Суспензия 50 мг/мл", "Мазь 10 мг/г", "Таблетка 5 мг", "Ампула 50 мг"];
            int count_medicine_name = medicine_name.GetLength(0);
            int count_medicine_indications = medicine_indications.GetLength(0);
            int count_medicine_contraindications = medicine_contraindications.GetLength(0);
            int count_medicine_manufacturer = medicine_manufacturer.GetLength(0);
            int count_medicine_packaging = medicine_packaging.GetLength(0);

            for (int i = 0; i < medicine_number; i++)
            {
                db.Medicines.Add(new Medicine
                {
                    Name = medicine_name[rand.Next(count_medicine_name)],
                    Indications = medicine_indications[rand.Next(count_medicine_indications)],
                    Contraindications = medicine_contraindications[rand.Next(count_medicine_contraindications)],
                    Manufacturer = medicine_manufacturer[rand.Next(count_medicine_manufacturer)],
                    Packaging = medicine_packaging[rand.Next(count_medicine_packaging)]
                });
            }
            db.SaveChanges();
        }
        private static void InitializeFamilyMembers(MedicinalProductsContext db, int familyMember_number, Random rand)
        {
            string[] names = {"Иван", "Мария", "Петр", "Анна", "Сергей",
                             "Александр", "Екатерина", "Дмитрий", "Ольга", "Светлана",
                             "Николай", "Татьяна", "Анастасия", "Максим", "Виктория",
                             "Андрей", "Елена", "Роман", "Ксения", "Владимир",
                             "Дарья", "Игорь", "Алёна", "Арсений", "Полина",
                             "Михаил", "Маргарита", "Константин", "София", "Григорий",
                             "Юлия", "Степан", "Елизавета", "Денис", "Антонина"};
            string[] genders = { "Мужской", "Женский"};
            for (int i = 0; i < familyMember_number; i++)
            {
                db.FamilyMembers.Add(new FamilyMember
                {
                    Name = names[rand.Next(names.GetLength(0))],
                    Age = rand.Next(5, 80),
                    Gender = genders[rand.Next(genders.GetLength(0))],
                });
            }
            db.SaveChanges();
        }
        private static void InitializeCostMedicines(MedicinalProductsContext db, int costMedicine_number, Random rand)
        {
            string[] costMedicines_manufacturer = ["Борщаговский ХФЗ", "Фармакор Продакшн", "Биофарм", "ОАО \"Гедеон Рихтер\"", "Акрихин",
                                              "Валента Фарм", "ООО \"Медок\"", "Санофи-Авентис", "Берлин Хеми", "Розекфарм",
                                              "Хинди Фармасьютикалс", "ЗАО \"Биосинтез\"", "Бионорика"];
            int count_costMedicines_manufacturer = costMedicines_manufacturer.GetLength(0);
            for (int i = 0; i < costMedicine_number; i++)
            {
                var allMedicines = db.Medicines.ToList(); // Извлекаем все записи
                var randomIndex = rand.Next(allMedicines.Count); // Генерируем случайный индекс
                var medicine = allMedicines[randomIndex]; // Выбираем случайную запись
                if (medicine == null) { continue; }
                db.CostMedicines.Add(new CostMedicine
                {
                    MedicinesId = medicine.Id,
                    Price = rand.Next(20, 200),
                    Date = GetRandomDate(new DateOnly(2000, 1, 1), DateOnly.FromDateTime(DateTime.Now)),
                    Manufacturer = costMedicines_manufacturer[rand.Next(count_costMedicines_manufacturer)],
                    Medicines = medicine,
                });
            }
            db.SaveChanges();
        }

        private static void InitializeDiseses(MedicinalProductsContext db, int disese_number, Random rand)
        {
            string[] disease_names = {"Грипп", "ОРВИ", "Пневмония", "Бронхит", "Астма",
                                      "Диабет", "Гипертония", "Аллергия", "Ожирение", "Инфаркт",
                                      "Инсульт", "Туберкулез", "Рак", "Гепатит", "Гастрит"};
            string[] durations = {"1-2 дня", "1 неделя", "2-3 недели", "1-2 месяца", "Хроническое течение"};
            string[] symptoms = {"Кашель", "Температура", "Слабость", "Головная боль", "Одышка",
                                 "Тошнота", "Рвота", "Боли в животе", "Сыпь", "Потеря аппетита"};
            string[] consequences = { "Нет", "Осложнения в виде пневмонии", "Хронический бронхит", "Сахарный диабет", "Сердечно-сосудистые заболевания" };
            for (int i = 0; i < disese_number; i++)
            {
                db.Diseases.Add(new Disease
                {
                    Name = disease_names[rand.Next(disease_names.GetLength(0))],
                    Duration = durations[rand.Next(durations.GetLength(0))],
                    Symptoms = symptoms[rand.Next(symptoms.GetLength(0))],
                    Consequences = consequences[rand.Next(consequences.GetLength(0))]
                });
            }
            db.SaveChanges();
        }
        private static void InitializePrescriptions(MedicinalProductsContext db, int prescription_number, Random rand)
        {
            for (int i = 0; i < prescription_number; i++)
            {
                var disease = db.Diseases.OrderBy(d => Guid.NewGuid()).FirstOrDefault(); // Get the first record
                var familyMember = db.FamilyMembers.OrderBy(f => Guid.NewGuid()).FirstOrDefault();
                if (disease == null && familyMember == null) { continue; }
                db.Prescriptions.Add(new Prescription
                {
                    FamilyMemberId = familyMember.Id,
                    DiseasesId = disease.Id,
                    Date = GetRandomDate(new DateOnly(2000, 1, 1), DateOnly.FromDateTime(DateTime.Now)),
                });
            }
            db.SaveChanges();
        }
        private static void InitializeDiseasesAndSymptoms(MedicinalProductsContext db, int diseasesAndSymptoms_number, Random rand)
        {
            string[] dosages = {
                    "1 таблетка 3 раза в день",
                    "2 таблетки 1 раз в день",
                    "5 мл сиропа 2 раза в день",
                    "1 капсула перед едой",
                    "10 мг 1 раз в день",
                    "500 мг 1 раз в день",
                    "1 пакетик 2 раза в день",
                    "1 ампула 1 раз в 2 дня",
                    "2 таблетки на ночь",
                    "125 мг 3 раза в день",
                    "10 мл раствора 1 раз в день",
                    "3 капсулы 2 раза в день",
                    "1 суппозиторий 1 раз в день",
                    "2 мл инъекции 1 раз в неделю",
                    "1 таблетка по мере необходимости"
                };
            for (int i = 0; i < diseasesAndSymptoms_number; i++)
            {
                var disease =  db.Diseases.OrderBy(d => Guid.NewGuid()).FirstOrDefault();
                var medicine = db.Medicines.OrderBy(m => Guid.NewGuid()).FirstOrDefaultAsync();
                if (disease == null && medicine == null) { continue; }
                db.DiseasesAndSymptoms.Add(new DiseasesAndSymptom
                {
                    MedicinesId = medicine.Id,
                    DiseasesId = disease.Id,
                    Dosage = dosages[rand.Next(dosages.Length)],
                });
            }
            db.SaveChanges();
        }
    }
}
