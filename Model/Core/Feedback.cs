namespace Model.Core;

/// <summary>
/// Класс для хранения отзыва от спортсмена на тренера
/// </summary>
public class Feedback
{
    public Athlete Athlete { get; set; }
    public Coach Coach { get; set; }
    public TrainingGroup Group { get; set; }
    public int OценкаТренера { get; set; } // от 1 до 10
    public DateTime ДатаОтзыва { get; set; }
    public int КоличествоЗанятий { get; set; }

    public Feedback(Athlete athlete, Coach coach, TrainingGroup group, int оценка, int количествоЗанятий)
    {
        if (оценка < 1 || оценка > 10)
            throw new ArgumentException("Оценка должна быть от 1 до 10", nameof(оценка));

        Athlete = athlete ?? throw new ArgumentNullException(nameof(athlete));
        Coach = coach ?? throw new ArgumentNullException(nameof(coach));
        Group = group ?? throw new ArgumentNullException(nameof(group));
        OценкаТренера = оценка;
        КоличествоЗанятий = количествоЗанятий;
        ДатаОтзыва = DateTime.Now;
    }

    public string ПолучитьИнформацию()
    {
        return $"Отзыв от {Athlete.GetFullName()} на тренера {Coach.GetFullName()}: {OценкаТренера}/10";
    }
}
