namespace PackageManager.Data
{
    public enum TaskStatus
    {
        New,
        Ready,      // Готова к выполнению
        Performed,  // Выполняется
        Pending,    // В ожидании
        Completed   // Завершена
    }
}
