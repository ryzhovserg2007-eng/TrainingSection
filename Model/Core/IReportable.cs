namespace Model.Core;

/// <summary>
/// Второй интерфейс (требуемый минимум: 2+ интерфейса)
/// Интерфейс для объектов, которые могут генерировать отчеты
/// </summary>
public interface IReportable
{
    string GenerateReport();
}
