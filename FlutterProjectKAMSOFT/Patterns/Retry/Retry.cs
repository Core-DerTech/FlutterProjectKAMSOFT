namespace FlutterProjectKAMSOFT.Patterns.Retry
{
    public class Retry
    {
        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, int maxRetries)
        {
            int delay = 1;
            int attemptCounter = 0;
            int additionalTimeToWait = 2;
            while (true)
            {
                try
                {
                    return await operation();
                }
                catch (Exception ex)
                {
                    delay += additionalTimeToWait;
                    attemptCounter++;
                    if (attemptCounter >= maxRetries)
                    {
                        throw new Exception($"Operation failed after {maxRetries} attempts.", ex);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(delay));
                }
            }
        }
    }
}
