namespace FlutterProjectKAMSOFT.Processing.Filters
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}
