using System;
using System.Collections.Generic;

namespace Model.Core
{
    /// <summary>
    /// Инициализация начальных данных при первом запуске
    /// </summary>
    public static class DataInitializer
    {
        public static List<Coach> Coaches { get; private set; } = new List<Coach>();
        public static List<TrainingGroup> Groups { get; private set; } = new List<TrainingGroup>();

        public static void Initialize()
        {
            if (Coaches.Count > 0) return; // уже инициализировано

            // Создаём тренеров
            var coach1 = new Coach("Смирнов", "Алексей", "Викторович", 42, 'M');
            var coach2 = new Coach("Кузнецова", "Мария", "Андреевна", 35, 'F');
            var coach3 = new Coach("Попов", "Дмитрий", "Сергеевич", 48, 'M');

            Coaches.Add(coach1);
            Coaches.Add(coach2);
            Coaches.Add(coach3);

            // Создаём группы
            var group1 = new TrainingGroup(1, "Группа A1", coach1);
            var group2 = new TrainingGroup(2, "Группа A2", coach1);
            var group3 = new TrainingGroup(3, "Группа B1", coach2);
            var group4 = new TrainingGroup(4, "Группа C1", coach3);
            var group5 = new TrainingGroup(5, "Группа C2", coach3);

            Groups.AddRange(new[] { group1, group2, group3, group4, group5 });

            // Добавляем спортсменов
            AddAthletesToGroup(group1, 8);
            AddAthletesToGroup(group2, 6);
            AddAthletesToGroup(group3, 7);
            AddAthletesToGroup(group4, 5);
            AddAthletesToGroup(group5, 4);
        }

        private static void AddAthletesToGroup(TrainingGroup group, int count)
        {
            for (int i = 1; i <= count; i++)
            {
                var gender = i % 2 == 0 ? 'M' : 'F';
                var athlete = new Athlete(
                    $"Спортсмен{i}",
                    "Фамилия",
                    "Отчество",
                    16 + (i % 12),
                    gender);

                group.ДобавитьСпортсмена(athlete);
            }
        }
    }
}
