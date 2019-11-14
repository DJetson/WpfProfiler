namespace ProfilerCharts.Interfaces
{
    public interface IResultItemViewModel
    {
        int Iteration { get; set; }
        long DeltaTime { get; }
    }
}