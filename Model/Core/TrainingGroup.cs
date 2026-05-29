namespace Model.Core;

/// <summary>
/// Класс для группы тренировок (1 тренер + массив спортсменов)
/// Первая часть: основные свойства и методы
/// Имеет 3 части: основная, сортировка/фильтрация, статистика
/// </summary>
public partial class TrainingGroup : IReportable
{
    public int ИД { get; set; }
    public string НазваниеГруппы { get; set; }
    public Coach Тренер { get; set; }
    public List<Athlete> Спортсмены { get; set; } = new();
    public int КоличествоЗанятий { get; set; }
    private List<Feedback> _отзывы = new();

    public TrainingGroup(int ид, string названиеГруппы, Coach тренер)
    {
        ИД = ид;
        НазваниеГруппы = названиеГруппы ?? throw new ArgumentNullException(nameof(названиеГруппы));
        Тренер = тренер ?? throw new ArgumentNullException(nameof(тренер));
        КоличествоЗанятий = 0;
    }

    /// <summary>
    /// Добавить спортсмена в группу
    /// </summary>
    public void ДобавитьСпортсмена(Athlete спортсмен)
    {
        if (спортсмен == null)
            throw new ArgumentNullException(nameof(спортсмен));

        if (!Спортсмены.Contains(спортсмен))
        {
            Спортсмены.Add(спортсмен);
            спортсмен.Groups.Add(this);
        }
    }

    /// <summary>
    /// Удалить спортсмена из группы
    /// </summary>
    public void УдалитьСпортсмена(Athlete спортсмен)
    {
        if (спортсмен != null && Спортсмены.Contains(спортсмен))
        {
            Спортсмены.Remove(спортсмен);
            спортсмен.Groups.Remove(this);
        }
    }

    /// <summary>
    /// Получить информацию о группе
    /// </summary>
    public string ПолучитьИнформацию()
    {
        return $"Группа: {НазваниеГруппы}, Тренер: {Тренер.GetFullName()}, Спортсменов: {Спортсмены.Count}";
    }

    /// <summary>
    /// Добавить отзыв от спортсмена
    /// Спортсмен может оставить оценку только один раз для каждой группы
    /// </summary>
    public void ДобавитьОтзыв(Feedback отзыв)
    {
        if (отзыв == null)
            throw new ArgumentNullException(nameof(отзыв));

        // Проверка: спортсмен уже оставил отзыв?
        if (_отзывы.Any(о => о.Athlete == отзыв.Athlete))
        {
            throw new InvalidOperationException("Этот спортсмен уже оставил отзыв для данной группы");
        }

        _отзывы.Add(отзыв);
        Тренер.AddRating(отзыв.КоличествоЗанятий, отзыв.OценкаТренера);
    }

    /// <summary>
    /// Получить все отзывы по группе
    /// </summary>
    public List<Feedback> ПолучитьОтзывы() => new List<Feedback>(_отзывы);

    /// <summary>
    /// Реализация IReportable - генерация отчета по группе
    /// </summary>
    public string GenerateReport()
    {
        return $"ОТЧЕТ ПО ГРУППЕ\n" +
               $"================\n" +
               $"Название: {НазваниеГруппы}\n" +
               $"Тренер: {Тренер.GetFullName()}\n" +
               $"Количество спортсменов: {Спортсмены.Count}\n" +
               $"Количество занятий: {КоличествоЗанятий}\n" +
               $"Количество отзывов: {_отзывы.Count}";
    }

    public override string ToString()
    {
        return НазваниеГруппы;
    }
}
